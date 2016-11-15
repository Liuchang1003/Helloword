using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookTest.Startup))]
namespace BookTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
