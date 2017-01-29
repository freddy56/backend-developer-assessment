using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using System.Web.Routing;
using MusicStore.WebApi.App_Start;

namespace MusicStore.WebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            // Pass a delegate to the Configure method.
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Bootstrapper.InitialiseDependencies();
        }
    }
}