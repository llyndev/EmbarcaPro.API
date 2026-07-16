using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    public class User
    {

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public UserRole Role { get; set; }

        public UserStatus Active { get; private set; }

        public DateTime RegisterDate { get; private set; }

        protected User() { }

        public User(string name, string email, string passwordHash, UserRole role)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Active = UserStatus.Active;
            RegisterDate = DateTime.UtcNow;
        }

    }
}
