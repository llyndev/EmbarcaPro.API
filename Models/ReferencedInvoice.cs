namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// infDoc group - cada NFe transportado dentro do CT-e
    /// Um CT-e pode referenciar N NFs
    /// </summary>
    public class ReferencedInvoice
    {

        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid CteId { get; init; }

        public string NfeAccessKey { get; init; }
        public decimal? InvoiceValue { get; init; }
        public string? OrderNumber { get; init; }

        protected ReferencedInvoice() { }

        public ReferencedInvoice(Guid cteId, string nfeAccessKey, decimal? invoiceValue = null, string? orderNumber = null)
        {
            var key = nfeAccessKey.Trim();

            if (key.Length != 44)
                throw new ArgumentException("A chave de acesso da NF-e deve ter 44 dígitos.", nameof(nfeAccessKey));

            CteId = cteId;
            NfeAccessKey = nfeAccessKey.Trim();
            InvoiceValue = invoiceValue;
            OrderNumber = orderNumber?.Trim();
        }

    }
}
