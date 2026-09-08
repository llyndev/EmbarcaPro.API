using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    public class User
    {

        public int Id { get; init; }
        public Guid PublicId { get; init; } = Guid.NewGuid();

        public int CompanyId { get; private set; }
        public virtual Company Company { get; private set; } = null!;

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public UserRole Role { get; private set; }

        public UserStatus Active { get; private set; }

        public DateTime RegisterDate { get; private set; }

        protected User() { }

        public User(Company company, string name, string email, string passwordHash, UserRole role)
        {

            ArgumentNullException.ThrowIfNull(company);

            Company = company;
            Name = name.Trim();
            Email = email.Trim().ToLowerInvariant();
            PasswordHash = passwordHash;
            Role = role;
            Active = UserStatus.Active;
            RegisterDate = DateTime.UtcNow;
        }

        public void ChangeRole(UserRole role) => Role = role;

        public void Block() => Active = UserStatus.Blocked;

        public void Activate() => Active = UserStatus.Active;

        public void ChangePassword(string passwordHash)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(PasswordHash);

            PasswordHash = passwordHash;
        }

    }
}
