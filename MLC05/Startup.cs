using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MLC05.Startup))]
namespace MLC05
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
