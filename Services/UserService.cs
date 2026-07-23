using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using EmbarcaPro.API.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmbarcaPro.API.Services
{
    public class UserService(ApplicationDbContext context, IPasswordService passwordService, IOptions<JwtSettings> jwtOptions) : IUserService
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;


        public async Task<ServiceResult<UserResponse>> RegisterUserAsync(RegisterRequest request)
        {
            var emailExists = await context.Users.AnyAsync(u => u.Email == request.Email);

            if (emailExists)
            {
                return ServiceResult<UserResponse>.Fail("Este e-mail já existe.", ErrorType.Conflict);
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

            var response = new UserResponse(
                newUser.Id,
                newUser.Name,
                newUser.Email,
                newUser.Role,
                newUser.Active,
                newUser.RegisterDate);

            return ServiceResult<UserResponse>.Ok(response ,"Usuário registrado com sucesso!");

        }

        public async Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest request)
        {

            // Verificar se o usuário existe no banco
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return ServiceResult<LoginResponse>.Fail("E-mail ou senha incorretos.", ErrorType.Validation);
            }

            var isPasswordValid = passwordService.VerifyPassword(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return ServiceResult<LoginResponse>.Fail("E-mail ou senha incorretos.", ErrorType.Validation);
            }

            if (user.Active != UserStatus.Active)
            {
                return ServiceResult<LoginResponse>.Fail("Usuário inativo", ErrorType.Validation);
            }

            var token = GenerateJwtToken(user);

            var responseData = new LoginResponse(token, user.Name, user.Email);
            return ServiceResult<LoginResponse>.Ok(responseData, "Login efetuado com sucesso!");

        }
        
        private string GenerateJwtToken(User user)
        {
            if (string.IsNullOrWhiteSpace(_jwtSettings.Secret))
                throw new InvalidOperationException("JWT Secret não configurada.");

            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<List<UserResponse>> GetAllUserResponseAsync()
        {
            var users = await context.Users.Select(user => new UserResponse(
                user.Id,
                user.Name,
                user.Email,
                user.Role,
                user.Active,
                user.RegisterDate))
                .ToListAsync();

            return users;
        }

        public async Task<ServiceResult<UserResponse>> GetUserByIdResponseAsync(int id)
        {
            var userResponse = await context.Users
                .Where(user => user.Id == id)
                .Select(user => new UserResponse(
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Role,
                    user.Active,
                    user.RegisterDate))
                .SingleOrDefaultAsync();

            if (userResponse == null)
            {
                return ServiceResult<UserResponse>.Fail("Usuário não existe", ErrorType.NotFound);
            }

            return ServiceResult<UserResponse>.Ok(userResponse, "");
        }

        public async Task<ServiceResult<UserResponse>> UpdateUserRoleAsync(UpdateRoleRequest request)
        {

            if (!Enum.IsDefined(request.UserRole))
            {
                return ServiceResult<UserResponse>.Fail("Role inválida.", ErrorType.Validation);
            }

            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            if (user == null) {
                return ServiceResult<UserResponse>.Fail("Usuário não existe", ErrorType.NotFound);
            }

            if (user.Role == request.UserRole)
            {
                return ServiceResult<UserResponse>.Fail("Usuário já possui este cargo.", ErrorType.Conflict);
            }

            user.Role = request.UserRole;

            await context.SaveChangesAsync();

            UserResponse response = new UserResponse(
                Id: user.Id,
                Name: user.Name,
                Email: user.Email,
                Role: user.Role,
                Active: user.Active,
                RegisterDate: user.RegisterDate);

            return ServiceResult<UserResponse>.Ok(response, "Role atualizada");

        }
    }
}
