using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web
{
    public static class ClaimsExtension
    {
        [Obsolete("Support for asp.net core rc 1 code")]
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [Obsolete("Support for asp.net core rc 1 code")]
        public static bool IsSignedIn(this ClaimsPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }
        
    }
}
