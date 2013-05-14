using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace deneysan
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            log4net.Config.XmlConfigurator.Configure();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }


     protected void Application_AcquireRequestState(object sender, EventArgs e)
     {
         
         if (Session["culture"] == null)
         {
             CultureInfo ci = new CultureInfo("tr");
             System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
             System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
         }
         else
         {
             CultureInfo ci = new CultureInfo(Session["culture"].ToString());
             System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
             System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
         }

     }


    }
}