namespace EmbarcaPro.API.Services.Interfaces
{
    public interface ICurrentUser
    {
        int CompanyId { get; }
        int UserId { get; }

        bool isAuthenticated { get; }
    }
}
