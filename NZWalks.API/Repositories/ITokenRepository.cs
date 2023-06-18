//Interface for Token

using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        //Define Method
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}

//Steps
//Define the name of your method: CreateJWTToken
// Define parameters:
// IdentityUser user
// List<string> roles
// Define return type
// string ...