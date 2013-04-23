using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using deneysan.Areas.Admin.Filters;
using deneysan_BLL.LanguageBL;
using deneysan_BLL.LinkBL;
using deneysan_DAL.Entities;

namespace deneysan.Areas.Admin.Controllers
{
     [AuthenticateUser]
    public class LinkController : Controller
    {
        //
        // GET: /Admin/Link/

        public ActionResult Index()
        {
            string sellang = FillLanguagesForList();

            var list = LinkManager.GetImportantLinksList(sellang);
            return View(list);
        }

        public ActionResult AddLink()
        {
            FillLanguagesList();
            return View();
        }


        [HttpPost]
        public ActionResult AddLink(ImportantLinks model)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {
                ModelState.Clear();
                ViewBag.ProcessMessage = LinkManager.AddImportantLinks(model);
            }
            return View();
        }


        public ActionResult EditLink()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    ImportantLinks editrecord = LinkManager.GetImportantLinksById(nid);
                    return View(editrecord);
                }
                else
                    return View();
            }
            else
                return View();
            return View();
        }



        [HttpPost]
        public ActionResult EditLink(ImportantLinks model)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            if (ModelState.IsValid)
            {
               
                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        model.LinkId = nid;
                        ViewBag.ProcessMessage = LinkManager.EditImportantLink(model);
                        return View(model);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(model);
                    }
                }
                else return View();
            }
            else
                return View();

            return View();
        }






        public JsonResult EditStatus(int id)
        {
            string NowState;
            bool isupdate = LinkManager.UpdateStatus(id);
            return Json(isupdate);
        }


        public JsonResult DeleteRecord(int id)
        {
            bool isdelete = LinkManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = LinkManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
        string FillLanguagesForList()
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

        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            return lang;
        }


      


    }
}
