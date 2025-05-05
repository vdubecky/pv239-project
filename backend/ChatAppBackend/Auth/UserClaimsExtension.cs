using System.Security.Claims;

namespace ChatAppBackend.Auth;

public static class UserClaimsExtension
{
    public static int? GetUserIdFromClaims(this ClaimsPrincipal user)
    {
       if (int.TryParse(user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value, out int result))
        {
            return result;
        }

        return null;
    }
}
