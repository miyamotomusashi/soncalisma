﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using deneysan.Areas.Admin.Filters;
using deneysan.Areas.Admin.Helpers;
using deneysan_BLL.DocumentsBL;
using deneysan_BLL.LanguageBL;
using deneysan_DAL.Entities;

namespace deneysan.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class DocumentGroupController : Controller
    {
        //
        // GET: /Admin/DocumentGroup/

        public ActionResult Index()
        {
            string lang=FillLanguagesList();
            var grouplist = DocumentManager.GetDocumentGroupList(lang);
            return View(grouplist);
        }


        [HttpPost]
        public ActionResult Index(string drplanguage, string txtname)
        {
                  string lang = FillLanguagesList();
            if (ModelState.IsValid)
            {
                DocumentGroup model = new DocumentGroup();
                model.GroupName = txtname;
                model.Language = drplanguage;
                model.PageSlug = Utility.SetPagePlug(txtname);
                ViewBag.ProcessMessage = DocumentManager.AddDocumentGroup(model);
          
                var grouplist = DocumentManager.GetDocumentGroupList(lang);
               
                return View(grouplist);


            }
            return View();
        }

        public void UpdateRecord(int id, string name)
        {
            string clearname = name.Replace("%47", "\'");
            string pageslug = Utility.SetPagePlug(clearname);
            DocumentManager.EditDocumentGroup(id, clearname,pageslug);
        }


        public JsonResult GroupEditStatus(int id)
        {
            return Json(DocumentManager.UpdateGroupStatus(id));
        }

        public JsonResult DeleteRecord(int id)
        {
            return Json(DocumentManager.DeleteGroup(id));
        }


        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = DocumentManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
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
