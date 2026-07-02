using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Enum;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EmbarcaPro.API.Services
{
    public class UserService(ApplicationDbContext context, IPasswordService passwordService, IConfiguration configuration) : IUserService
    {

        public async Task<(bool Success, string Message)> RegisterUserAsync(RegisterRequest request)
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

        public async Task<(bool Success, string Message, LoginResponse? Data)> LoginAsync(LoginRequest request)
        {

            // Verificar se o usuário existe no banco
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return (false, "E-mail ou senha incorretos.", null);
            }

            var isPasswordValid = passwordService.VerifyPassword(request.Password, user.PasswordHash);
            if (isPasswordValid)
            {
                return (false, "E-mail ou senha incorretos.", null);
            }

            var token = GenerateJwtToken(user);

            var responseData = new LoginResponse(token, user.Name, user.Email);
            return (true, "Login efetuado com sucesso!", responseData);

        }
        
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("Secret")
                ?? throw new InvalidOperationException("JWT Secret não configurada.");

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.GetValue<int>("ExpiryInMinutes")),
                Issuer = jwtSettings.GetValue<string>("Issuer"),
                Audience = jwtSettings.GetValue<string>("Audience"),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
