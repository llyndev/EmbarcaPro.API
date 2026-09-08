namespace EmbarcaPro.API.Models
{
    public class Driver
    {

        public int Id { get; init; }
        public Guid PublicId { get; init; } = Guid.NewGuid();

        public int CompanyId { get; private set; }
        public virtual Company Company { get; private set; } = null!;

        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public string Cnh { get; private set; }
        public Address Address { get; private set; }

        public bool IsActive { get; private set; }
        public DateTime CreateAt { get; private set; }

        protected Driver() { }

        public Driver(Company company, string name, string phone, string email, string cpf, string cnh, Address address)
        {
            ArgumentNullException.ThrowIfNull(company);

            Company = company;
            Name = name.Trim();
            Phone = phone.Trim();
            Email = email.Trim().ToLowerInvariant();
            Cpf = cpf.Replace(".", "").Replace("-", "").Trim();
            Cnh = cnh.Trim();
            Address = address;

            IsActive = true;
            CreateAt = DateTime.UtcNow;
        }

        public void Deactive() => IsActive = false;
        public void Active() => IsActive = true;
    }
}
