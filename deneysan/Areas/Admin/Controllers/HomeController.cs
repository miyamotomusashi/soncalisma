using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan.Areas.Admin.Filters;
using deneysan_BLL.TeklifBL;

namespace deneysan.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
             var list = TeklifManager.GetList();
             if (RouteData.Values["type"] != null)
             {
                 ViewBag.Param = "0";
                 string type = RouteData.Values["type"].ToString();
                 //if (type == "tumteklifler")
                 //{
                 //    ViewBag.Header = "TÜM TEKLİFLER";
                 //    list = TeklifManager.GetList();
                 //}
                 if (type == "onaybekleyenler")
                 {
                     ViewBag.Header = "YENİ GELEN TEKLİFLER / ONAY BEKLEYEN TEKLİFLER";
                     list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylanmadi));
                 }
                 else if (type == "onaylananlar")
                 {
                     ViewBag.Header = "ONAYLANAN TEKLİFLER";
                     list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylandi));
                 }
                 else if (type == "iptaledilenler")
                 {
                     ViewBag.Header = "İPTAL EDİLEN TEKLİFLER";
                     list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Iptal));
                 }

                 return View(list);
             }
             else
             {
                 ViewBag.Header = "YENİ GELEN TEKLİFLER / ONAY BEKLEYEN TEKLİFLER";
                 list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylanmadi));
                 return View(list);
             }
        }

        public ActionResult AllList()
        {
            var list = TeklifManager.GetList();
            return View(list);
        }


        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
