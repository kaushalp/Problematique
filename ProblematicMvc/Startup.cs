using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProblematicMvc.Startup))]
namespace ProblematicMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
