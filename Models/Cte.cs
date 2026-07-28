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
        public int Id { get; private set; }

        public int Number { get; private set; }

        public int FreightId { get; private set; }
        public virtual Freight Freight { get; private set; } = null!;

        public CteStatus Status { get; private set; }

        public decimal TotalServiceValue { get; private set; }
        public decimal AmountReceivable { get; private set; }

        // Preenchido futuramente pela integração com a SEFAZ (fora de escopo por enquanto).
        public string? AccessKey { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? AuthorizedAt { get; private set; }
        public DateTime? CanceledAt { get; private set; }

        private readonly List<CteFreightComponent> _freightComponents = new();
        public virtual IReadOnlyCollection<CteFreightComponent> FreightComponents => _freightComponents.AsReadOnly();

        protected Cte() { }

        public Cte(int freightId, int number, decimal totalServiceValue, decimal amountReceivable)
        {
            if (totalServiceValue <= 0)
                throw new ArgumentException("O valor total do serviço deve ser maior que zero.");

            if (amountReceivable < 0 || amountReceivable > totalServiceValue)
                throw new ArgumentException("O valor a receber deve estar entre zero e o valor total do serviço.");

            FreightId = freightId;
            Number = number;
            TotalServiceValue = totalServiceValue;
            AmountReceivable = amountReceivable;
            Status = CteStatus.Draft;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddFreightComponent(string name, decimal value)
        {
            EnsureDraft("adicionar componentes de frete");
            _freightComponents.Add(new CteFreightComponent(name, value));
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

            Status = CteStatus.Canceled;
            CanceledAt = DateTime.UtcNow;
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
