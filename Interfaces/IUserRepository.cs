using PrimerParcial.Models;

namespace PrimerParcial.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByCredentials(string username, string password);
    }
}
