using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Scheduler_MVC.Startup))]
namespace Scheduler_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
