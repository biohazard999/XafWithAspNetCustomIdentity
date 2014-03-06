using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XafWithAspNetCustomIdentity.Web.Startup))]
namespace XafWithAspNetCustomIdentity.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
