using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enum;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class UserService(ApplicationDbContext context, IPasswordService passwordService) : IUserService
    {

        public async Task<(bool Sucess, string Message)> RegisterUserAsync(RegisterRequest request)
        {
            var emailExists = await context.Users.AnyAsync(u => u.Email == request.Email);

            if (emailExists)
            {
                return (false, "Este e-mail já existe.");
            }

            var hashedPassword = passwordService.HashPassword(request.Password);

            var newUser = new User(
                name: request.Name,
                email: request.Email,
                passwordHash: hashedPassword,
                role: UserRole.Consulta
            );

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            return (true, "Usuário registrado com sucesso!");

        }
    }
}
