using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan_BLL.InstituionalBL;
using deneysan_BLL.LanguageBL;
using deneysan_DAL.Context;
using deneysan_DAL.Entities;
using deneysan.Helpers.Enums;
using deneysan.Areas.Admin.Filters;
namespace deneysan.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class InstitutionalController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Vizyon()
        {
            string lang=FillLanguagesList();
            
            var vision_info = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Hakkimizda));
            return View(vision_info);
        }

       

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Vizyon(Institutional record)
        {
            string lang=FillLanguagesList();
            record.Language = lang;
            record.TypeId=Convert.ToInt32(EnumInstituionalTypes.Hakkimizda);
            ViewBag.ProcessMessage = InstituionalManager.EditInstituional(record);
            

            return View();
        }

        public ActionResult Misyon()
        {
            string lang = FillLanguagesList();
            var vision_info = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Misyon));
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Misyon(Institutional record)
        {
            string lang = FillLanguagesList();
            record.Language = lang;
            record.TypeId = Convert.ToInt32(EnumInstituionalTypes.Misyon);
            ViewBag.ProcessMessage = InstituionalManager.EditInstituional(record);
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
