using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;

namespace cleangap.api.Services.Security
{
    public class SimpleRefreshTokenProvider : AuthenticationTokenProvider
    {
        private void UpdateTicket(AuthenticationTokenCreateContext context)
        {
            // Expiration time in minutes
            int expire = 20;
            context.Ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.Now.AddMinutes(expire));
            context.SetToken(context.SerializeTicket());
        }

        public override void Create(AuthenticationTokenCreateContext context)
        {
            UpdateTicket(context);
        }

        public override Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            UpdateTicket(context);

            return Task.FromResult<object>(null);
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }
}