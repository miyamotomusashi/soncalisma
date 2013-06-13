﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan_BLL.Gallery;
using deneysan_BLL.LanguageBL;

namespace deneysan.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Admin/Gallery/

        public ActionResult AddImage()
        {
            FillLanguagesList();
            return View();
        }


        string FillLanguagesListForGalleryList()
        {
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = GalleryManager.GetGalleryGroupList(lang);

            if (RouteData.Values["id"] == null)
            {
                if (groups != null && groups.Count != 0)
                    id = groups.First().GalleryGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();


            var grouplist = new SelectList(groups, "GalleryGroupId", "GroupName", id);
            ViewBag.GroupList = grouplist;

            return id;
        }


        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            //var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = GalleryManager.GetGalleryGroupList(lang);
            var grouplist = new SelectList(groups, "GalleryGroupId", "GroupName");
            ViewBag.GroupList = grouplist;

            return lang;
        }

        [HttpPost]
        public ActionResult LoadGroup(string lang)
        {
            var grouplst = GalleryManager.GetGalleryGroupList(lang);
            JsonResult result = Json(new SelectList(grouplst, "GalleryGroupId", "GroupName"));
            return result;
        }

        public void Upload(HttpPostedFileBase files)
        {

        }

    }
}
