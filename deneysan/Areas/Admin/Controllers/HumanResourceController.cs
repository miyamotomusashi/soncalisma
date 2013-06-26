using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan_BLL.HRBL;
using deneysan_BLL.LanguageBL;
using deneysan_DAL.Entities;
using deneysan.Areas.Admin.Filters;

namespace deneysan.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class HumanResourceController : Controller
    {
        //
        // GET: /Admin/HumanResource/

        public ActionResult Index()
        {
            string lang = FillLanguagesList();

            var vision_info = HumanResourceManager.GetHRByLanguage(lang);
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(HumanResource record)
        {
           string lang=FillLanguagesList();
            record.Language = lang;
            
            ViewBag.ProcessMessage = HumanResourceManager.EditHumanResource(record);
            

            return View();
        }


        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
            return lang;
        }
    }
}
