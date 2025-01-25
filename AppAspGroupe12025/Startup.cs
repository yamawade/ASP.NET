using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppAspGroupe12025.Startup))]
namespace AppAspGroupe12025
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
