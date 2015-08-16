using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Interests.Startup))]
namespace Interests
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
