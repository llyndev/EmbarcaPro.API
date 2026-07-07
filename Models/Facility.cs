namespace EmbarcaPro.API.Models
{
    public class Facility
    {

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string? Cnpj { get; private set; }

        public Address Address { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        protected Facility() { }

        public Facility(string name, Address address, string? cnpj = null)
        {
            Name = name.Trim();
            Address = address;
            Cnpj = string.IsNullOrWhiteSpace(cnpj) ? null : cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
