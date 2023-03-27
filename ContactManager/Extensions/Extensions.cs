using System;
using System.Security.Claims;
using System.Security.Principal;

namespace ContactManager.Extensions
{
    public static class Extensions
    {
        public static Guid GetOwnerId(this IPrincipal principal)
        {
            
            var identity = principal.Identity as ClaimsIdentity;

            Guid OwnerId = Guid.Empty;
            if (identity != null)
            {
                OwnerId = Guid.Parse(identity.FindFirst("UserId").Value);
            }

            return OwnerId;
        }

        public static string GetCountry(this IPrincipal principal)
        {

            var identity = principal.Identity as ClaimsIdentity;

            string countryCode = "";
            if (identity != null)
            {
                countryCode = identity.FindFirst("Country").Value;
            }

            return countryCode;
        }
    }
}
