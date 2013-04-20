using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan.Areas.Admin.Filters;
using deneysan_BLL.ContactBL;
using deneysan_BLL.LanguageBL;
using deneysan_DAL.Entities;

namespace deneysan.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class ContactController : Controller
    {
        //
        // GET: /Admin/Contact/

        public ActionResult Index()
        {
            string lang = FillLanguagesList();

            var contact = ContactManager.GetContact(lang);
            return View(contact);
        }

        [HttpPost]
        public ActionResult Index(Contact record)
        {
            string lang = FillLanguagesList();
            ViewBag.ProcessMessage = ContactManager.EditContact(record);
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
