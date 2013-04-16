using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan.Models;
using deneysan_BLL.NewsBL;
using deneysan_BLL.ReferenceBL;
using deneysan_DAL;
using deneysan_DAL.Entities;
namespace deneysan.Controllers
{
    public class FHomeController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        
        public ActionResult Index()
        {
            var news = NewsManager.GetNewsList(lang);
            var references = ReferenceManager.GetReferenceList(lang);
            HomePageWrapperModel modelbind = new HomePageWrapperModel(news, references);
            return View(modelbind);
        }

        public ActionResult ChangeCulture(string lang,string returnUrl)
        {
            Session["culture"] = lang;
            if(lang=="en")
                return Redirect("/en/homepage");
            return Redirect("/tr/anasayfa");
        }
    }
}
