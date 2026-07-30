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
        public int CompanyId { get; init; }
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
        public DateTime AuthorizedAt { get; private set; }
        public DateTime CancelledAt { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private readonly List<CteEvent> _events = new();
        public virtual IReadOnlyCollection<CteEvent> Events => _events.AsReadOnly();

        protected Cte() { }

        public Cte(Company company, string uf, int series, int number, DateTime issueDateTime, CteType type, CteServiceType serviceType,
            CteTransportMode transportMode, string predominantCfop, string originIbgeCode, string destinationIbgeCode, 
            decimal totalServiceValue, decimal amountReceivable)
        {
            
            if (totalServiceValue <= 0)
                throw new ArgumentException("O valor total do serviço deve ser maior que zero.");

            if (amountReceivable < 0 || amountReceivable > totalServiceValue)
                throw new ArgumentException("O valor a receber deve estar entre zero e o valor total do serviço.");
          
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
      
              public void AddFreightComponent(int cteId,string name, decimal value)
        {
            EnsureDraft("adicionar componentes de frete");
            _freightComponents.Add(new CteFreightComponent(cteId, name, value));
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

            Status = CteStatus.Cancelled;
            CancelledAt = DateTime.UtcNow;
        }

        public void Deny()
        {
            if (Status != CteStatus.Draft)
                throw new InvalidOperationException("Apenas um CT-e em rascunho pode ser denegado.");

            Status = CteStatus.Denied;
        }

        private void EnsureDraft(string action)
        {
            if (Status != CteStatus.Draft)
                throw new InvalidOperationException($"Não é possível {action}: o CT-e não está mais em rascunho.");
        }

    }
}
