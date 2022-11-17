using PrimerParcial.Models;
namespace PrimerParcial.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByCredentials(string username, string password);
    }
}
