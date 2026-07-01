using BCrypt.Net;
using EmbarcaPro.API.Services.Interfaces;

namespace EmbarcaPro.API.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            // O WorkFactor 12 atrasa o processamento o suficiente para inviabilizar ataques de bruteforce.
            // mas é rápido o bastante para não prejudicar a experiência do seu usuário.
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12); ;
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

    }
}
