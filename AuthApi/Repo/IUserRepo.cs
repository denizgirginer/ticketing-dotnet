using AuthApi.Models;
using JwtGenerator.Types;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace AuthApi.Repo
{
    public interface IUserRepo : IRepository<User, string>
    {
        Task<TokenWithClaimsPrincipal> SignInUser(User user);
    }
}
