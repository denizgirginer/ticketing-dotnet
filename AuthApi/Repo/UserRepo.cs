using AuthApi.Models;
using JwtGenerator.Abstractions;
using JwtGenerator.Types;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;
using Ticket.Common.Utility;

namespace AuthApi.Repo
{
    public class UserRepo : MongoDbRepositoryBase<User>, IUserRepo
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public override string CollectionName => "Users";
        public UserRepo(IOptions<MongoDbSettings> options, IJwtTokenGenerator jwtTokenGenerator) : base(options)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }


        public async Task<TokenWithClaimsPrincipal> SignInUser(User user)
        {
            var found = await GetAsync(x => x.email == user.email);

            if (found == null)
            {
                throw new  Exception("Unauthorized");
            }

            string savedPassword = found.password;

            PasswordHash.ValidatePassword(user.password, savedPassword);

            TokenWithClaimsPrincipal accessTokenResult = _jwtTokenGenerator.GenerateAccessTokenWithClaimsPrincipal(
                    found.email,
                    AddMyClaims(user));

            return accessTokenResult;
        }

        private static IEnumerable<Claim> AddMyClaims(User authenticatedUser)
        {
            var myClaims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, authenticatedUser.email),
                new Claim(ClaimTypes.Surname, authenticatedUser.email),
                new Claim(ClaimTypes.Email, authenticatedUser.email),
                new Claim("HasAdminRights", "bla")
            };

            return myClaims;
        }
    }
}
