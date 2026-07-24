namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Representa qualquer das 4 roles que aparecem no CT-e
    /// (rem, dest, exped, receb)
    /// </summary>
    public class Partner
    {

        public Guid Id { get; init; }

        public string CnpjOrCpf { get; init; } // CNPJ ou CPF
        public string StateTaxId { get; init; } // IE - Inscrição Estadual
        public string LegalNameOrFullName { get; init; } // Razão social ou nome
        public Address Address { get; init; }
        public string Phone { get; init; }
        public string Email { get; init; }

        protected Partner() {

        }

        public Partner(string cnpjOrCpf, string legalNameOrFullName, Address address, string stateTaxId)
        {
            CnpjOrCpf = cnpjOrCpf;
            LegalNameOrFullName = legalNameOrFullName.Trim();
            Address = address;
            StateTaxId = stateTaxId.Trim();
        }
    }
}
