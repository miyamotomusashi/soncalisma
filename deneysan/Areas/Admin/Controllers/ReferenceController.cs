using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using deneysan.Areas.Admin.Filters;
using deneysan.Areas.Admin.Helpers;
using deneysan_BLL.LanguageBL;
using deneysan_BLL.ReferenceBL;
using deneysan_DAL.Entities;

namespace deneysan.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class ReferenceController : Controller
    {
        //
        // GET: /Admin/Reference/

        public ActionResult Index()
        {
            string sellang=FillLanguagesList();

            var referncelist = ReferenceManager.GetReferenceList(sellang);
            return View(referncelist);
        }

        public ActionResult AddReference()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
           
            return View();
        }

        [HttpPost]
        public ActionResult AddReference(References newmodel, HttpPostedFileBase uploadfile)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(240, 240).SaveThumbnail(uploadfile, "/Content/images/references/", Utility.SetPagePlug(newmodel.ReferenceName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    newmodel.Logo = "/Content/images/references/" + Utility.SetPagePlug(newmodel.ReferenceName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    newmodel.Logo = "/Content/images/front/noimage.jpeg";
                }
                newmodel.SortOrder = 9999;
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = ReferenceManager.AddReference(newmodel);
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();
        }

        public ActionResult EditReference()
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
                    References editreference = ReferenceManager.GetReferenceById(nid);
                    return View(editreference);
                }
                else
                    return View();
            }
            else
                return View();
            return View();
        }

        [HttpPost]
        public ActionResult EditReference(References referencemodel, HttpPostedFileBase uploadfile)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(240, 240).SaveThumbnail(uploadfile, "/Content/images/references/", Utility.SetPagePlug(referencemodel.ReferenceName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    referencemodel.Logo = "/Content/images/references/" + Utility.SetPagePlug(referencemodel.ReferenceName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }


                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        referencemodel.ReferenceId = nid;
                        ViewBag.ProcessMessage = ReferenceManager.EditReference(referencemodel);
                        return View(referencemodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(referencemodel);
                    }
                }
                else  return View();
            }
            else
                return View();

            return View();
        }


        public JsonResult ReferenceEditStatus(int id)
        {
            string NowState;
            bool isupdate = ReferenceManager.UpdateStatus(id);
            return Json(isupdate);
        }

        
        public JsonResult DeleteReference(int id)
        {
            bool isdelete = ReferenceManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
          //  else return false;
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

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ReferenceManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
       

    }
}
