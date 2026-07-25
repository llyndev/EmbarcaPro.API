using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Conhecimento de Transporte Eletrônico (CT-e).
    /// 
    /// TODO:
    /// - Implementar validações de domínio nos métodos de mutação.
    /// - Revisar regras de transição de status.
    /// - Validar consistência antes da assinatura e autorização.
    /// - Completar regras de negócio conforme o Manual de Orientação do CT-e.
    /// </summary>
    public class Cte
    {
        // Identificação do CT-e
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Uf { get; init; } // UF de emissão
        public int Series { get; init; }
        public int Number {  get; init; }
        public string AccessKey { get; private set; } // preenchida após autorização


        public DateTime IssueDateTime { get; init; }
        public CteType Type { get; init; }
        public CteServiceType ServiceType { get; init; }
        public CteTransportMode TransportMode { get; init; }
        public string PredominantCfop { get; init; }
        public string OriginIbgeCityCode { get; init; } // munícipio de início de prestação
        public string DestinationIbgeCityCode { get; init; } // munícipio de fim de prestação

        // Informações emitente CT-e
        public Guid CompanyId { get; init; }
        public virtual Company Company { get; private set; } = null!;

        // Informações rem, dest, exped, receb
        private readonly List<CtePartner> _partners = new();
        public virtual IReadOnlyCollection<CtePartner> Partners => _partners.AsReadOnly();


        // Valores de prestação
        public decimal TotalServiceValue { get; init; } // vTPrest
        public decimal AmountReceivable { get; init; } // vRec
        private readonly List<CteFreightComponent> _freightComponents = new();
        public virtual IReadOnlyCollection<CteFreightComponent> FreightComponents => _freightComponents.AsReadOnly();

        // Impostos
        public virtual IcmsTax Icms { get; private set; } = null!;

        // Informações do CT-e Normal
        public virtual Cargo Cargo { get; private set; } = null!;
        private readonly List<ReferencedInvoice> _referencedInvoices = new();
        public virtual IReadOnlyCollection<ReferencedInvoice> ReferencedInvoices => _referencedInvoices.AsReadOnly();

        // Informações do Modal do CT-e / Padrão Rodoviário
        public string? CarrierRntrc { get; init; }

        // Controle interno do CT-e
        public CteStatus Status { get; private set; } = CteStatus.Draft;
        public string? AuthorizationProtocol { get; private set; }
        public DateTime? AuthorizationDateTime {  get; private set; }
        public string? RejectionReason { get; private set; }
        public string? SignedXml { get; private set; }
        public string? AuthorizedXml { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private readonly List<CteEvent> _events = new();
        public virtual IReadOnlyCollection<CteEvent> Events => _events.AsReadOnly();

        protected Cte() { }

        public Cte(Company company, string uf, int series, int number, DateTime issueDateTime, CteType type, CteServiceType serviceType,
            CteTransportMode transportMode, string predominantCfop, string originIbgeCode, string destinationIbgeCode, 
            decimal totalServiceValue, decimal amountReceivable)
        {
            CompanyId = company.Id;
            Company = company;
            Uf = uf.Trim();
            Series = series;
            Number = number;
            IssueDateTime = issueDateTime;
            Type = type;
            ServiceType = serviceType;
            TransportMode = transportMode;
            PredominantCfop = predominantCfop;
            OriginIbgeCityCode = originIbgeCode.Trim();
            DestinationIbgeCityCode = destinationIbgeCode.Trim();
            TotalServiceValue = totalServiceValue;
            AmountReceivable = amountReceivable;
            CarrierRntrc = company.Rntrc;
            CreatedAt = DateTime.UtcNow;
        }
        

    }
}
