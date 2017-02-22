using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace cleangap.api.Services.Security
{
    public class AccountIdentity
    {
        public static string GetCurrentUser()
        {
            ClaimsIdentity identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            return claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
        }
    }
}