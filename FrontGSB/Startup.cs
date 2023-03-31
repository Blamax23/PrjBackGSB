using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FrontGSB.Startup))]
namespace FrontGSB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
