namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// infDoc group - cada NFe transportado dentro do CT-e
    /// Um CT-e pode referenciar N NFs
    /// </summary>
    public class ReferencedInvoice
    {
        public int Id { get; init; }
        public int CteId { get; private set; }

        public string NfeAccessKey { get; init; } = null!;
        public decimal? InvoiceValue { get; init; }
        public string? OrderNumber { get; init; }

        protected ReferencedInvoice() { }

        public ReferencedInvoice(string nfeAccessKey, decimal? invoiceValue = null, string? orderNumber = null)
        {
            NfeAccessKey = nfeAccessKey.Trim();
            InvoiceValue = invoiceValue;
            OrderNumber = string.IsNullOrWhiteSpace(orderNumber) ? null : orderNumber.Trim();
        }

    }
}
