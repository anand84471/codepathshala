using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentDashboard.Startup))]
namespace StudentDashboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
