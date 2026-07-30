using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Transportadora emitente do CTe
    /// </summary>
    public class Company
    {

        public int Id { get; init; }
        public Guid PublicId { get; init; } = Guid.NewGuid();

        public string Cnpj { get; init; }
        public string StateTaxId { get; init; } // IE - Inscrição Estadual
        public string LegalName { get; init; } // Razão social
        public string? TradeName { get; init; } // Nome fantasia

        /// <summary>
        /// Código de Regime Tributário - CRT (1-Simples, 2-Simples excesso, 3-Normal)
        /// </summary>
        public CrtType CrtCode { get; init; }

        public Address Address { get; init; }

        /// <summary>
        /// Registro Nacional de Transportadores Rodoviários de Cargas
        /// Obrigatório para o modal rodoviário
        /// </summary>
        public string Rntrc { get; init; }

        
        // Configuração fiscal / integração SEFAZ
        public string IssuingAuthorityState { get; init; } // UF autorizadora
        public bool IsProductionEnvironment { get; private set; } // false = homologação
        public string? CertificateThumbprint { get; private set; } // referêrencia ao certificado A1/A3 

        public int CurrentSeries { get; private set; }
        public int LastCteNumber { get; private set; }

        protected Company()
        {

        }

        public Company(string cnpj, string stateTaxId, string legalName, string? tradeName, CrtType crtCode,
            Address address, string issuingAuthorityState, string? rntrc = null, int currentSeries = 1)
        {

            var cleanCnpj = OnlyDigits(cnpj);

            if (cleanCnpj.Length != 14)
                throw new ArgumentException("CNPJ deve conter 14 dígitos.", nameof(cnpj));

            if (currentSeries < 1)
                throw new ArgumentException("A série deve ser maior ou igual a 1.", nameof(currentSeries));

            Cnpj = cleanCnpj;
            StateTaxId = stateTaxId;
            LegalName = legalName.Trim();
            TradeName = string.IsNullOrWhiteSpace(tradeName) ? null : tradeName.Trim();
            CrtCode = crtCode;
            Address = address;
            IssuingAuthorityState = issuingAuthorityState.Trim().ToUpperInvariant();
            Rntrc = rntrc;
            CurrentSeries = currentSeries;
            LastCteNumber = 0;
            IsProductionEnvironment = false;
        }


        /// <summary>
        /// Reserva e retorna o próximo número do CTe para a série atual desta empresa.
        /// </summary>
        public int GetNextCteNumber()
        {
            LastCteNumber++;
            return LastCteNumber;
        }

        /// <summary>
        /// Troca a série de emissão e reinicia a numeração.
        /// Cada série tem sua própria contagem começando em 1
        /// </summary>
        public void ChangeSeries(int newSeries)
        {
            if (newSeries < 1)
                throw new ArgumentException("A série deve ser maior ou igual a 1.", nameof(newSeries));

            if (newSeries == CurrentSeries)
                return;

            CurrentSeries = newSeries;
            LastCteNumber = 0;
        }

        public void SetCertificate(string thumbprint)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(thumbprint);
            CertificateThumbprint = thumbprint;
        }

        public void EnableProduction()
        {
            if (string.IsNullOrWhiteSpace(CertificateThumbprint))
                throw new InvalidOperationException("Configure o certificado digital antes de habilitar produção.");

            IsProductionEnvironment = true;
        }

        private static string OnlyDigits(string value) =>
            new(value.Where(char.IsDigit).ToArray());
    }
}
