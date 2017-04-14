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

        public static int? GetCurrentUserInt()
        {
            int intUserID = 0;
            string userID = GetCurrentUser();            
            bool parsed = int.TryParse(userID, out intUserID);

            if (parsed)            
                return intUserID;
            
            return null;
        }
    }
}