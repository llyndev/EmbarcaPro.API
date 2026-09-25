using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// CT-e (Conhecimento de Transporte Eletrônico) — documento fiscal da viagem.
    /// Aggregate root: controla a máquina de estados (Rascunho → Autorizado → Cancelado / Denegado)
    /// e a composição do valor do frete.
    /// </summary>
    public class Cte
    {
        // Identificação do CT-e
        public int Id { get; init; }
        public Guid PublicId { get; init; } = Guid.NewGuid();

        public string Uf { get; private set; } // UF de emissão
        public int Series { get; private set; }
        public int Number { get; private set; }
        public string NumericCode { get; private set; } = null!;
        public int IssuenceType { get; private set; } = 1;
        public string AccessKey { get; private set; } // preenchida após autorização


        public DateTime IssueDateTime { get; private set; }
        public CteType Type { get; private set; }
        public CteServiceType ServiceType { get; private set; }
        public CteTransportMode TransportMode { get; private set; }
        public string PredominantCfop { get; private set; } = null!;
        public string OriginIbgeCityCode { get; private set; } = null!; // munícipio de início de prestação
        public string DestinationIbgeCityCode { get; private set; } = null!; // munícipio de fim de prestação
        public PartnerType Taker { get; private set; }
        
        // Informações emitente CT-e
        public int CompanyId { get; private set; }
        public virtual Company Company { get; private set; } = null!;

        // Vínculo operacional
        public int? FreightId { get; private set; }
        public virtual Freight? Freight { get; private set; }

        // Informações rem, dest, exped, receb
        private readonly List<CtePartner> _partners = new();
        public virtual IReadOnlyCollection<CtePartner> Partners => _partners.AsReadOnly();

        // Valores de prestação
        public decimal TotalServiceValue { get; private set; } // vTPrest
        public decimal AmountReceivable { get; private set; } // vRec

        private readonly List<CteFreightComponent> _freightComponents = new();
        public virtual IReadOnlyCollection<CteFreightComponent> FreightComponents => _freightComponents.AsReadOnly();

        // Impostos
        public virtual IcmsTax? Icms { get; private set; }

        // Informações do CT-e Normal
        public virtual Cargo? Cargo { get; private set; }

        private readonly List<ReferencedInvoice> _referencedInvoices = new();
        public virtual IReadOnlyCollection<ReferencedInvoice> ReferencedInvoices => _referencedInvoices.AsReadOnly();

        // Informações do Modal do CT-e / Padrão Rodoviário
        public string? CarrierRntrc { get; private set; }

        // Controle interno do CT-e
        public CteStatus Status { get; private set; } = CteStatus.Draft;
        public string? AuthorizationProtocol { get; private set; }
        public DateTime? AuthorizationDateTime { get; private set; }
        public string? RejectionReason { get; private set; }
        public string? SignedXml { get; private set; }
        public string? AuthorizedXml { get; private set; }

        public DateTime AuthorizedAt { get; private set; }
        public DateTime CanceledAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<CteEvent> _events = new();
        public virtual IReadOnlyCollection<CteEvent> Events => _events.AsReadOnly();

        protected Cte() { }

        public Cte(
            Company company,
            CteType type,
            CteServiceType serviceType,
            CteTransportMode transportMode,
            PartnerType taker,
            string predominantCfop,
            string originIbgeCode,
            string destinationIbgeCode,
            decimal totalServiceValue,
            decimal amountReceivable,
            Freight? freight = null)
        {
            ArgumentNullException.ThrowIfNull(company);
            ArgumentException.ThrowIfNullOrWhiteSpace(predominantCfop);
            ArgumentException.ThrowIfNullOrWhiteSpace(originIbgeCode);
            ArgumentException.ThrowIfNullOrWhiteSpace(destinationIbgeCode);

            if (!Enum.IsDefined(taker))
                throw new ArgumentException("Tomador do serviço inválido.", nameof(taker));
            
            if (totalServiceValue <= 0)
                throw new ArgumentException("O valor total do serviço deve ser maior que zero.");

            if (amountReceivable < 0 || amountReceivable > totalServiceValue)
                throw new ArgumentException("O valor a receber deve estar entre zero e o valor total do serviço.");

            Company = company;
            Freight = freight;

            Uf = company.Address.Uf;
            Series = company.CurrentSeries;
            Number = company.GetNextCteNumber();
            NumericCode = CteAccessKeyGerator.GenerateNumericCode(Number);
            CarrierRntrc = company.Rntrc;

            Type = type;
            ServiceType = serviceType;
            TransportMode = transportMode;
            Taker = taker;
            PredominantCfop = predominantCfop.Trim();
            OriginIbgeCityCode = originIbgeCode.Trim();
            DestinationIbgeCityCode = destinationIbgeCode.Trim();

            TotalServiceValue = totalServiceValue;
            AmountReceivable = amountReceivable;

            IssueDateTime = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
            Status = CteStatus.Draft;
        }

        public void AddFreightComponent(string name, decimal value)
        {
            EnsureDraft("adicionar componentes de frete");

            if (value <= 0)
            {
                throw new ArgumentException("O valor do componente deve ser maior que zero.", nameof(value));
            }

            _freightComponents.Add(new CteFreightComponent(name, value));
        }

        public void SetCargo(Cargo cargo)
        {
            ArgumentNullException.ThrowIfNull(cargo);
            EnsureDraft("definir carga");
            Cargo = cargo;
        }

        public void SetIcms(IcmsTax icms)
        {
            ArgumentNullException.ThrowIfNull(icms);
            EnsureDraft("definir icms");
            Icms = icms;
        }

        public void AddPartner(Partner partner, PartnerType type)
        {
            ArgumentNullException.ThrowIfNull(partner);
            EnsureDraft("adicionar parceiros");

            if (_partners.Any(p => p.Type == type))
                throw new InvalidOperationException($"Este CT-e já possuí um parceiro do tipo {type}.");

            _partners.Add(new CtePartner(partner, type));
        }

        public void RemovePartner(PartnerType type)
        {
            EnsureDraft("remover parceiros");

            var existente = _partners.FirstOrDefault(p => p.Type == type);
            if (existente is not null)
                _partners.Remove(existente);
        }

        public void AddReferencedInvoice(string nfeAccessKey, decimal? invoiceValue = null, string? orderNumber = null)
        {
            EnsureDraft("adicionar notas fiscais");

            var chave = nfeAccessKey?.Trim() ?? string.Empty;

            if (chave.Length != 44 || !chave.All(char.IsDigit))
                throw new ArgumentException("A chave de acesso da NF-e deve ter 44 dígitos numéricos.", nameof(nfeAccessKey));

            if (_referencedInvoices.Any(i => i.NfeAccessKey == chave))
                throw new InvalidOperationException("Este NF-e já está referenciado neste CT-e");


            _referencedInvoices.Add(new ReferencedInvoice(chave, invoiceValue, orderNumber));
        }

        // Transições de estado
        public void MarkAsSigned(string signedXml)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(signedXml);
            EnsureDraft("assinar");

            if (string.IsNullOrWhiteSpace(AccessKey))
                throw new InvalidOperationException("Gere a chave de acesso antes de assinar o CT-e.");
            
            EnsureReadyForTransmission();

            SignedXml = signedXml;
            Status = CteStatus.AwaitingAuthorization;
        }

        // ---------- Máquina de estados ----------

        public void Authorize()
        {
            if (Status != CteStatus.Draft)
                throw new InvalidOperationException("Apenas um CT-e em rascunho pode ser autorizado.");

            ValidateFreightComposition();

            Status = CteStatus.Authorized;
            AuthorizedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status != CteStatus.Authorized)
                throw new InvalidOperationException("Apenas um CT-e autorizado pode ser cancelado.");

            Status = CteStatus.Canceled;
            CanceledAt = DateTime.UtcNow;
        }

        public void Deny()
        {
            if (Status != CteStatus.Draft)
                throw new InvalidOperationException("Apenas um CT-e em rascunho pode ser denegado.");

            Status = CteStatus.Denied;
        }

        /// <summary>
        /// Registra um evento pós-autorização (cancelamento, CC-e, comprovante de entrega).
        /// </summary>
        public void RegisterEvent(CteEventType type, int sequenceNumber, String? justification = null)
        {
            if (Status is not (CteStatus.Authorized or CteStatus.Canceled))
                throw new InvalidOperationException("Eventos só podem ser registrados em CT-e autorizados.");

            if (_events.Any(e => e.Type == type && e.SequenceNumber == sequenceNumber))
                throw new InvalidOperationException($"Já existe um evento {type} com a sequência {sequenceNumber}.");

            _events.Add(new CteEvent(type, sequenceNumber, DateTime.UtcNow, justification));

            if (type == CteEventType.Cancellation)
            {
                CanceledAt = DateTime.UtcNow;
                Status = CteStatus.Canceled;
            }
        }

        private void EnsureDraft(string action)
        {
            if (Status != CteStatus.Draft)
                throw new InvalidOperationException($"Não é possível {action}: o CT-e não está mais em rascunho.");
        }

        /// <summary>
        /// Verifica se o CT-e tem tudo que a SEFAZ exige antes de transmitir.
        /// </summary>
        public void EnsureReadyForTransmission()
        {
            if (Cargo is null)
                throw new InvalidOperationException("Informe os dados da carga antes de enviar o CT-e ao sefaz.");

            if (Icms is null)
                throw new InvalidOperationException("Informe a tributação de ICMS antes de enviar o CT-e ao sefaz.");

            if (_referencedInvoices.Count == 0)
                throw new InvalidOperationException("O CT-e precisa referenciar ao menos uma NF-e.");

            foreach (var papel in new[] { PartnerType.Shipper, PartnerType.Consignee})
            {

                if (_partners.All(p => p.Type != papel))
                    throw new InvalidOperationException($"O CT-e exige um parceiro do tipo {papel}.");

            }

            if (!_partners.Any(p => p.Type == Taker))
                throw new InvalidOperationException(
                    $"O tomadro de serviço ({Taker}) precisa estar cadastrado como parceiro deste CT-e.");

            if (TransportMode == CteTransportMode.Road && string.IsNullOrWhiteSpace(CarrierRntrc))
                throw new InvalidOperationException("O RNTRC é obrigatório no modal rodoviário.");

            ValidateFreightComposition();

        }

        /// <summary>
        /// A soma dos componentes de frete deve ser igual ao valor total do serviço.
        /// </summary>
        public void ValidateFreightComposition()
        {
            if (_freightComponents.Count == 0)
                throw new InvalidOperationException("O CT-e precisa de ao menos um componente de frete.");

            var sum = _freightComponents.Sum(c => c.Value);

            if (sum != TotalServiceValue)
                throw new InvalidOperationException(
                    $"A soma dos componentes ({sum:N2}) não confere com o valor total do serviço ({TotalServiceValue:N2}).");
        }

        public Partner? GetPartner(PartnerType type) =>
            _partners.FirstOrDefault(p => p.Type == type)?.Partner;
        
        /// <summary>
        /// Gera e fixa a chave de acesso de 44 dígitos.
        /// </summary>
        /// <param name="issuenceType"></param>
        public void AssignAccessKey(int issuenceType = 1)
        {
            EnsureDraft("gerar a chave de acesso");

            if (!string.IsNullOrWhiteSpace(AccessKey))
                return;

            if (Company is null)
                throw new InvalidOperationException("A empresa emitente precisa estar carregada para gerar a chave.");

            // Os 2 primeiros dígitos do código IBGE do município é o código da UF.
            var ufibgeCode = Company.Address.IbgeCode[..2];

            AccessKey = CteAccessKeyGerator.Generate(
                ufibgeCode,
                IssueDateTime,
                Company.Cnpj,
                Series,
                Number,
                NumericCode,
                IssuenceType);

            IssuenceType = issuenceType;
        }


    }
}
