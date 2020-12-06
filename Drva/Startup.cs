using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Drva.Startup))]
namespace Drva
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
