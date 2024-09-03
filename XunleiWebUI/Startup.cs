using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XunleiWebUI.Startup))]
namespace XunleiWebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
