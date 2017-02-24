[assembly: Microsoft.Owin.OwinStartup(typeof(cleangap.api.Startup))]

namespace cleangap.api
{
    using System.Web.Mvc;
    using NWebsec.Owin;
    using Owin;
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureContainer(app);
        }
    }
}