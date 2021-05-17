using System.Threading.Tasks;
using Webapi.Models;

namespace Webapi.Data
{
    public interface IAuthRepository
    {
         Task<ServiceResponce<int>> Register(User user,string password);

         Task<ServiceResponce<string>> Login(string username ,string password);

         Task<bool> UserExists(string username);
    }
}