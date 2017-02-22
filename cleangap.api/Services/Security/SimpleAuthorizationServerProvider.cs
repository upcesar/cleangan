using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using cleangap.api.Domain;
using cleangap.api.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace cleangap.api.Services.Security
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            CustomersBO customerBO = new CustomersBO();            

            LoginResponseModel user = await customerBO.Authenticate(context.UserName, context.Password);
            
            if (user == null)
            {
                context.SetError("invalid_grant", "Usuário ou senha não válida.");
                return;
            }
            

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, user.name));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.email));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.ID.ToString()));
            identity.AddClaim(new Claim("userName", context.UserName));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    } ,
                    {
                        "userID", user.ID.ToString()
                    },
                    {
                        "userName", user.name
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {

            //enforce client binding of refresh token
            if (context.Ticket == null || context.Ticket.Identity == null || !context.Ticket.Identity.IsAuthenticated)
            {
                context.SetError("invalid_grant", "Refresh token is not valid");
            }
            else
            {
                var userIdentity = context.Ticket.Identity;

                var authenticationTicket = CreateAuthenticationTicket(userIdentity);

                //Additional claim is needed to separate access token updating from authentication 
                //requests in RefreshTokenProvider.CreateAsync() method
                authenticationTicket.Identity.AddClaim(new Claim("refreshToken", "refreshToken"));

                context.Validated(authenticationTicket);
                
            }

            return Task.FromResult<object>(null);
        }

        private AuthenticationTicket CreateAuthenticationTicket(ClaimsIdentity userIdentity)
        {
            // Expiration time in minutes
            int expire = 20;

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(expire),
            };

            return new AuthenticationTicket(userIdentity, props);

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}