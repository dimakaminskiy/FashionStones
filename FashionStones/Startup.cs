using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FashionStones.Startup))]
namespace FashionStones
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
