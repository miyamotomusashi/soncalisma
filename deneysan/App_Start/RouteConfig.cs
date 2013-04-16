using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace deneysan
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            ControllerBuilder.Current.DefaultNamespaces.Add("deneysan.Controllers");

         //   routes.MapRoute("home_default", "/", new { action = "Index", Controller = "Home" });
            routes.MapRoute("home_tr", "tr/anasayfa", new { action = "Index", Controller = "FHome" });
            routes.MapRoute("home_en", "en/homepage", new { action = "Index", Controller = "FHome" });

            routes.MapRoute("aboutus_tr", "tr/hakkimizda", new { action = "Index", Controller = "FInstitutional" });
            routes.MapRoute("aboutus_en", "en/aboutus", new { action = "Index", Controller = "FInstitutional" });

            routes.MapRoute("visionmision_tr", "tr/vizyon-misyon", new { action = "VisionMision", Controller = "FInstitutional" });
            routes.MapRoute("visionmision_en", "en/vision-mision", new { action = "VisionMision", Controller = "FInstitutional" });

            routes.MapRoute("products_tr", "tr/urunler", new { action = "Index", Controller = "FProducts" });
            routes.MapRoute("products_en", "en/products", new { action = "Index", Controller = "FProducts" });

            routes.MapRoute("documentgroups_tr", "tr/dokumanlar", new { action = "Index", Controller = "FDocuments" });
            routes.MapRoute("documentgroups_en", "en/documents", new { action = "Index", Controller = "FDocuments" });

            routes.MapRoute("documents_tr", "tr/dokumanlar/{group}/{gid}", new { action = "DocumentList", Controller = "FDocuments" });
            routes.MapRoute("documents_en", "en/documents/{group}/{gid}", new { action = "DocumentList", Controller = "FDocuments" });

            routes.MapRoute("contactdetails_tr", "tr/iletisim-bilgileri", new { action = "Index", Controller = "FContact" });
            routes.MapRoute("contactdetails_en", "en/contact-details", new { action = "Index", Controller = "FContact" });

            routes.MapRoute("contactform_tr", "tr/iletisim-formu", new { action = "Form", Controller = "FContact" });
            routes.MapRoute("contactform_en", "en/contact-form", new { action = "Form", Controller = "FContact" });

            routes.MapRoute("contactbank_tr", "tr/banka-hesaplari", new { action = "Bank", Controller = "FContact" });
            routes.MapRoute("contactbank_en", "en/bank-accounts", new { action = "Bank", Controller = "FContact" });

            routes.MapRoute("news_tr", "tr/tum-haberler", new { action = "Index", Controller = "FNews" });
            routes.MapRoute("news_en", "en/all-news", new { action = "Index", Controller = "FNews" });

            routes.MapRoute("newscontent_tr", "tr/haberler/{header}/{hid}", new { action = "NewsContent", Controller = "FNews" });
            routes.MapRoute("newscontent_en", "en/news/{header}/{hid}", new { action = "NewsContent", Controller = "FNews" });

            routes.MapRoute("career_tr", "tr/kariyer", new { action = "Index", Controller = "FCareer" });
            routes.MapRoute("career_en", "en/career", new { action = "Index", Controller = "FCareer" });

            routes.MapRoute("career_app_tr", "tr/kariyer/basvuru", new { action = "ApplicationForm", Controller = "FCareer" });
            routes.MapRoute("career_app_en", "en/career/application", new { action = "ApplicationForm", Controller = "FCareer" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FHome", action = "Index", id = UrlParameter.Optional }
            );

           
        }
    }
}