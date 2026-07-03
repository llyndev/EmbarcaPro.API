namespace EmbarcaPro.API.Models
{
    public class Driver
    {

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }

        public string Cpf { get; private set; }

        public string Cnh { get; private set; }

        public Address Address { get; private set; }

        public bool IsActive { get; private set; }
        public DateTime CreateAt { get; private set; }

        protected Driver() { }

        public Driver(string name, string phone, string email, string cpf, string cnh, Address address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Cpf = cpf.Replace(".", "").Replace("-", "");
            Cnh = cnh;
            Address = address;

            IsActive = true;
            CreateAt = DateTime.UtcNow;
        }

        public void Deactive() => IsActive = false;
        public void Active() => IsActive = true;
    }
}
