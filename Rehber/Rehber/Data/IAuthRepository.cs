using Rehber.Models;
using System.Threading.Tasks;

namespace Rehber.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);
        User GetUserById(int userId);
    }
}
