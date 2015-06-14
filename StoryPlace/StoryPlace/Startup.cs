using Microsoft.Owin;
using Owin;
using StoryPlace.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace StoryPlace.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
