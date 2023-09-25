using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NovaLite.Todo.Api.Exceptions;
using System.IdentityModel.Tokens.Jwt;

namespace NovaLite.Todo.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected async Task<JwtSecurityToken> GetDecodedToken()
        {
            var rawToken = await HttpContext.GetTokenAsync("access_token");
            Console.WriteLine($"Raw Token: {rawToken}");
            var tokenHandler = new JwtSecurityTokenHandler();
            // Read the token and parse it into a JwtSecurityToken
            var jwtToken = tokenHandler.ReadJwtToken(rawToken);

            return jwtToken;
        }
        protected async Task<string> GetUserEmail()
        {
            // Access the claims
            // Access the claims
            var token = await GetDecodedToken();
            var claims = token.Claims;

            // Display the claims
            foreach (var claim in claims)
            {
                if (claim.Type == "preferred_username")
                {
                    return claim.Value;
                }
            }
            throw new NotFoundException("The email is invalid.");
        }
    }
}
