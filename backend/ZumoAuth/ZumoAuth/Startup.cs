using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ZumoAuth.Startup))]

namespace ZumoAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}