using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AptitudeTestOnline.Startup))]
namespace AptitudeTestOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
