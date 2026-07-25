namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Representa qualquer das 4 roles que aparecem no CT-e
    /// (rem, dest, exped, receb)
    /// </summary>
    public class Partner
    {

        public Guid Id { get; init; } = Guid.NewGuid();

        public string CnpjOrCpf { get; init; } // CNPJ ou CPF
        public string? StateTaxId { get; init; } // IE - Inscrição Estadual
        public string LegalNameOrFullName { get; init; } // Razão social ou nome
        public Address Address { get; init; }
        public string? Phone { get; init; }
        public string? Email { get; init; }

        protected Partner() {

        }

        public Partner(string cnpjOrCpf, string legalNameOrFullName, Address address,
            string? stateTaxId = null, string? phone = null, string? email = null)
        {
            CnpjOrCpf = cnpjOrCpf.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
            LegalNameOrFullName = legalNameOrFullName.Trim();
            Address = address;
            StateTaxId = string.IsNullOrWhiteSpace(stateTaxId) ? null : stateTaxId.Trim();
            Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();
            Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        }
    }
}
