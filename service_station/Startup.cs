using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(service_station.Startup))]
namespace service_station
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
