namespace cleangap.api
{
    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using Services.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public partial class Startup
    {
        private static void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AuthorizeEndpointPath = new PathString("/api/account/auth"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20), //TimeSpan.FromSeconds(45),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider(),

            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //Allow from all headers
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

        }

        public static void ConfigureContainer(IAppBuilder app)
        {
            //Configure API Authentication
            ConfigureOAuth(app);
        }
    }
}