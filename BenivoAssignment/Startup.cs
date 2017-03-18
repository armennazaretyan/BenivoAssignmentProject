using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BenivoAssignment.Startup))]
namespace BenivoAssignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
