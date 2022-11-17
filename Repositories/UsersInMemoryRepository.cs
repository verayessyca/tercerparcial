using PrimerParcial.Models;
using PrimerParcial.Interfaces;


namespace PrimerParcial.Repositories
{
    public class UsersInMemoryRepository: IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new()
            {
                Id = 1,
                Fullname = "Yessyca Vera",
                Username = "yvera",
                Password = "4937540",
            }
        };
        public async Task<User?> GetUserByCredentials(string username, string password)
        {
            return _users.FirstOrDefault(x => x.Username.Equals(username) && x.Password.Equals(password));
        }
    }
}