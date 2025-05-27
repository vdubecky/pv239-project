using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace pv239_project.Helpers;

public static class JwtHelpers
{
    public static int GetUserId()
    {
        var token = SecureStorage.GetAsync("jwt_token").Result;
        if (token is null)
        {
            throw new NullReferenceException("JWT token not found.");
        }
        
        var jwt = new JsonWebToken(token);
        var idClaim = jwt.GetClaim(ClaimTypes.NameIdentifier);
        if (idClaim is null)
        {
            throw new NullReferenceException("JWT token does not contain an id claim.");
        }
        
        return Convert.ToInt32(idClaim.Value);
    }
}
