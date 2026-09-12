using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Services.Interfaces;
using System.Security.Claims;

namespace EmbarcaPro.API.Services
{
    public class CurrentUser : ICurrentUser
    {

        public const string CompanyIdClaim = "company_id";

        private readonly IHttpContextAccessor _accessor;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public int CompanyId => ReadInt(CompanyIdClaim);

        public int UserId => ReadInt(ClaimTypes.NameIdentifier);

        public UserRole Role => ReadRole();

        public bool isAuthenticated =>
            _accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

        private int ReadInt(string claimType)
        {
            var valor = _accessor.HttpContext?.User.FindFirstValue(claimType);

            return int.TryParse(valor, out var resultado) ? resultado : 0;
        }

        private UserRole ReadRole()
        {
            var valor = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            return Enum.TryParse<UserRole>(valor, true, out var resultado)
                ? resultado
                : UserRole.Consulta;
        }

    }
}
