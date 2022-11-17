using PrimerParcial.Models;

namespace PrimerParcial.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string username, string password);
        Task<string> GenerateJwt(User user);
    }
}
