# EmbarcaPro.API

API REST para gestão de transporte rodoviário de cargas (TMS) com emissão de Conhecimento de Transporte Eletrônico (CT-e modelo 57, layout 4.00).

O foco do projeto é a emissão fiscal: numeração com garantia de unicidade, montagem da chave de acesso, geração do XML no layout oficial, validação contra os schemas XSD da SEFAZ e assinatura digital XMLDSig.

## Stack

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10 com PostgreSQL (Npgsql)
- Autenticação JWT com autorização por perfil
- Scalar para documentação interativa da API
- BCrypt para hash de senha

## Arquitetura

O projeto é um monolito em camadas, com as regras de negócio concentradas nos modelos de domínio.

```
Controllers/      HTTP: rotas, autorização por perfil, tradução de ServiceResult em status code
Services/         Orquestração: transações, consultas, chamadas ao domínio
Models/           Domínio: entidades com invariantes e máquina de estados
Data/             DbContext, mapeamentos e conversores do EF
Dtos/             Contratos de entrada e saída da API
Xml/              DTOs de serialização do CT-e e mappers para o layout 4.00
Common/           Helpers, opções de configuração, paginação e resultado padronizado
```

Três princípios orientam a separação:

**O domínio não conhece formatos externos.** A entidade `Cte` não tem atributos de XML nem de JSON. Os códigos numéricos da SEFAZ (`tpCTe`, `toma`, `modal`, `cUnid`, `CRT`) ficam isolados em `Xml/Mappers/CteXmlCodes.cs`, traduzidos na borda. Assim a ordem dos enums em C# é livre, e um formato de saída novo não contamina os outros.

**Validação em três níveis.** O DTO valida formato (tamanho, padrão, obrigatoriedade). O service valida existência (o município existe na tabela IBGE? o parceiro pertence à empresa?). O domínio valida regras do documento montado (o tomador está entre os parceiros? os componentes somam o valor total?). A duplicação entre DTO e domínio é intencional: o DTO dá erro cedo e com boa mensagem, o domínio garante a regra por qualquer caminho de entrada.

**Mappers não fazem I/O.** As classes de `Xml/Mappers` recebem as entidades já carregadas e devolvem os objetos de XML. Quem consulta o banco é o service. Isso torna a montagem do documento testável sem banco.

## Multiempresa

Todas as entidades operacionais carregam `CompanyId`, e o `ApplicationDbContext` aplica query filters globais a partir da claim `company_id` do token. Um CT-e ou parceiro de outra empresa simplesmente não existe nas consultas, sem necessidade de repetir `WHERE company_id` em cada query.

A empresa emitente nunca vem do corpo da requisição, apenas do token. As exceções ao filtro são o login (que precisa achar o usuário antes de saber a empresa) e a tabela de municípios, que é dado público compartilhado.

## Emissão do CT-e

```
POST /api/ctes              Draft                  rascunho com numeração reservada
PUT  /api/ctes/{id}/prepare Draft                  valida o documento e fixa a chave de acesso
PUT  /api/ctes/{id}/sign    AwaitingAuthorization  assina o XML (XMLDSig enveloped)
PUT  /api/ctes/{id}/authorize  Authorized          registra a autorização
```

### Numeração fiscal

A numeração do CT-e é sequencial por empresa e série, e não admite duplicidade nem buracos silenciosos. Duas requisições simultâneas leriam o mesmo `last_cte_number` e gerariam o mesmo número.

A solução é um lock pessimista: `SELECT ... FOR UPDATE` na linha da empresa, dentro de uma transação que cobre a reserva do número e a gravação do documento. A segunda requisição espera o commit da primeira e lê o valor já atualizado.

Como o `EnableRetryOnFailure` está ativo, transações iniciadas manualmente exigem execution strategy — o bloco inteiro roda dentro de `CreateExecutionStrategy().ExecuteAsync(...)`, e o change tracker é limpo no início para que uma repetição não reaproveite estado da tentativa anterior.

### Chave de acesso

Os 44 dígitos são montados pelo emitente, não pela SEFAZ: UF, ano e mês de emissão, CNPJ, modelo, série, número, forma de emissão, código numérico aleatório e dígito verificador por módulo 11 (`Common/Helpers/CteAccessKeyGerator.cs`).

O código numérico (`cCT`) usa `RandomNumberGenerator` em vez de `Random`, porque a função dele é dificultar que chaves válidas sejam adivinhadas a partir do CNPJ e do número. Ele é gerado uma vez e persistido: chave e XML precisam carregar sempre o mesmo valor.

A geração da chave é idempotente — chamar `prepare` duas vezes devolve a mesma chave, já que trocá-la criaria outro documento na prática.

### XML

O XML é gerado com `XmlSerializer` sobre classes anotadas que espelham o layout, uma por grupo (`ide`, `emit`, `rem`, `dest`, `vPrest`, `imp`, `infCTeNorm`, `infRespTec`). A ordem das propriedades nas classes é a ordem das tags exigida pelo XSD.

Dois padrões recorrentes:

- **Campos opcionais** usam `ShouldSerializeXxx()`, porque tag vazia é rejeitada pela SEFAZ.
- **Decimais** são expostos como `string` e formatados com casas fixas via `Common/Helpers/XmlDecimal.cs` (`vTPrest` com 2 casas, `qCarga` com 4, sempre com ponto e `InvariantCulture`).

Os campos de texto passam por `Common/Helpers/XmlText.cs`, que remove acentos, colapsa espaços e aplica o limite de cada campo. O cadastro preserva a acentuação; a normalização acontece só na saída.

### Municípios

O XML exige código IBGE, nome e UF dos municípios de início e fim da prestação. A tabela `cities` (5.570 registros) usa o código IBGE como chave natural e guarda o nome já normalizado. O código da UF é propriedade calculada a partir dos dois primeiros dígitos, e não coluna, para não existir possibilidade de divergência.

O nome que vai para o XML vem sempre da tabela, nunca do texto digitado no cadastro do parceiro.

### Validação contra o XSD

`GET /api/ctes/{id}/xml/validate` e `.../xml/signed/validate` validam o documento contra os schemas oficiais usando `XmlReaderSettings` com `ValidationType.Schema`. O `XmlSchemaSet` é carregado uma vez e reaproveitado.

A validação acusa ordem de tags, tamanho de campo, padrões e elementos obrigatórios faltando — os mesmos erros que a SEFAZ devolveria, em milissegundos e com o caminho do elemento.

### Assinatura digital

Assinatura XMLDSig enveloped sobre o elemento `infCte`, referenciado pelo atributo `Id` (`"CTe" + chave de acesso`). Transformações `enveloped-signature` e `C14N`, digest SHA-1 e assinatura RSA-SHA1 — algoritmos exigidos pelo layout, apesar de o SHA-1 estar obsoleto para uso criptográfico geral.

Dois detalhes que invalidam a assinatura se ignorados: o XML precisa ser serializado sem identação (espaços entre tags alteram o digest) e o `XmlDocument` precisa ser carregado com `PreserveWhitespace = true`.

## Endpoints

Todos exigem `Authorization: Bearer <token>`, exceto o onboarding e o login.

### Autenticação

| Método | Rota | Perfil |
|---|---|---|
| POST | `/api/auth/onbord` | público — cria empresa e usuário administrador |
| POST | `/api/auth/register` | Admin — cria usuário na empresa |
| POST | `/api/auth/login` | público |

### CT-e

| Método | Rota | Perfil |
|---|---|---|
| POST | `/api/ctes` | Admin, Operacional |
| GET | `/api/ctes` | autenticado |
| GET | `/api/ctes/{id}` | autenticado |
| PUT | `/api/ctes/{id}/prepare` | Admin, Operacional |
| PUT | `/api/ctes/{id}/sign` | Admin, Operacional |
| PUT | `/api/ctes/{id}/authorize` | Admin |
| PUT | `/api/ctes/{id}/cancel` | Admin |
| PUT | `/api/ctes/{id}/deny` | Admin |
| GET | `/api/ctes/{id}/xml` | Admin, Operacional |
| GET | `/api/ctes/{id}/xml/validate` | Admin, Operacional |
| GET | `/api/ctes/{id}/xml/signed/validate` | Admin, Operacional |

### Cadastros e operação

| Método | Rota |
|---|---|
| POST / GET | `/api/partners`, `/api/partners/{id}` |
| PUT | `/api/partners/{id}/activate` |
| POST / GET | `/api/trucks`, `/api/trucks/{plate}` |
| POST / GET | `/api/trailer`, `/api/trailer/{plate}` |
| POST / GET | `/api/drivers`, `/api/drivers/cpf`, `/api/drivers/name` |
| POST / GET | `/api/freights`, `/api/freights/{id}` |
| PUT | `/api/freights/{id}/start`, `/finish`, `/cancel` |
| GET / PUT | `/api/users`, `/api/users/{id}` |

Documentação interativa em `/scalar` (apenas em desenvolvimento).

## Como executar

### Pré-requisitos

- SDK do .NET 10
- PostgreSQL (local ou Neon)
- `dotnet-ef` instalado: `dotnet tool install --global dotnet-ef`

### Configuração

Crie um arquivo `.env` na raiz com a string de conexão:

```
NEON_CONNECTION_STRING=Host=...;Database=embarcapro;Username=...;Password=...;SSL Mode=Require
```

Em `appsettings.Development.json`, preencha o segredo do JWT, os dados do responsável técnico e o caminho do certificado:

```json
{
  "JwtSettings": { "Secret": "<chave de 32+ caracteres>" },
  "TechnicalResponsible": {
    "Cnpj": "", "Contact": "", "Email": "", "Phone": ""
  },
  "DigitalCertificate": {
    "Path": "/caminho/para/certificado.pfx",
    "Password": "<senha>"
  }
}
```

### Schemas XSD

Baixe o pacote de schemas do CT-e 4.00 no Portal Nacional (cte.fazenda.gov.br, em Documentos → Esquemas XSD) e extraia todos os `.xsd` na pasta `Schemas/`. Eles são copiados para a saída do build e usados pelos endpoints de validação.

### Certificado

Para desenvolvimento, um certificado autoassinado é suficiente — a estrutura da assinatura é idêntica à de um A1 real, mudando apenas a validade jurídica:

```bash
openssl req -x509 -newkey rsa:2048 -keyout chave.pem -out cert.pem -days 365 -nodes \
  -subj "/CN=RAZAO SOCIAL:CNPJ/C=BR"

openssl pkcs12 -export -out certificado-teste.pfx -inkey chave.pem -in cert.pem \
  -passout pass:<senha>
```

### Banco e execução

```bash
dotnet ef database update
dotnet run
```

Para popular a tabela de municípios, importe os dados da API de localidades do IBGE em um CSV com as colunas `ibge_code,name,uf` (nome sem acento e em maiúsculas) e carregue com `\copy cities (ibge_code, name, uf) FROM 'cities.csv' WITH (FORMAT csv)`.

## Limitações conhecidas

- **Reforma tributária.** Os grupos de IBS e CBS da NT 2026.002 ainda não foram implementados. Eles são opcionais no XSD, mas obrigatórios nas regras de validação da SEFAZ para emitentes do Regime Normal desde 03/08/2026.
- **Transmissão.** A comunicação com os webservices da SEFAZ não está implementada; `authorize` registra a transição de estado sem consultar o ambiente autorizador.
- **Coerência CRT × CST.** Não há validação cruzando o regime tributário do emitente com a situação tributária do ICMS. Emitente do Simples Nacional deve usar os CST específicos do regime.
- **Fuso horário.** A data de emissão é convertida para -03:00 fixo, o que não cobre emitentes fora do horário de Brasília.
- **Escopo do layout.** Apenas CT-e normal, modal rodoviário, sem contingência, sem grupos opcionais como `compl` e `infCTeSupl` (QR Code do DACTE).
- **Inutilização de numeração.** Números reservados que não viram documento autorizado precisam ser inutilizados junto à SEFAZ; o evento não está implementado.
- **DACTE.** A representação gráfica em PDF não faz parte do escopo atual.

## Próximos passos

1. Grupos de IBS e CBS (NT 2026.002)
2. Transmissão e consulta de recibo nos webservices de homologação
3. Eventos: cancelamento, carta de correção e inutilização
4. Geração do DACTE
5. MDF-e, para vincular vários CT-e a uma viagem
6. Testes automatizados do gerador de chave e das regras do agregado `Cte`
