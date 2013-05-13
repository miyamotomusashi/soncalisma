using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan.Areas.Admin.Filters;
using deneysan.Areas.Admin.Models;
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
            return View();
             //var list = TeklifManager.GetList();
             //if (RouteData.Values["type"] != null)
             //{
             //    ViewBag.Param = "0";
             //    string type = RouteData.Values["type"].ToString();
             //    //if (type == "tumteklifler")
             //    //{
             //    //    ViewBag.Header = "TÜM TEKLİFLER";
             //    //    list = TeklifManager.GetList();
             //    //}
             //    if (type == "onaybekleyenler")
             //    {
             //        ViewBag.Header = "YENİ GELEN TEKLİFLER / ONAY BEKLEYEN TEKLİFLER";
             //        list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylanmadi));
             //    }
             //    else if (type == "onaylananlar")
             //    {
             //        ViewBag.Header = "ONAYLANAN TEKLİFLER";
             //        list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylandi));
             //    }
             //    else if (type == "iptaledilenler")
             //    {
             //        ViewBag.Header = "İPTAL EDİLEN TEKLİFLER";
             //        list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Iptal));
             //    }

             //    return View(list);
             //}
             //else
             //{
             //    ViewBag.Header = "YENİ GELEN TEKLİFLER / ONAY BEKLEYEN TEKLİFLER";
             //    list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylanmadi));
             //    return View(list);
             //}
        }

        //public ActionResult AllList()
        //{
        //    var list = TeklifManager.GetList();
        //    return View(list);
        //}


        //public ActionResult Details(int id)
        //{
        //    int teklifid = 0;
        //    if (RouteData.Values["id"] != null)
        //    {
        //        bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out teklifid);
        //        if (isnumber)
        //        {
        //            var teklif = TeklifManager.GetTeklifById(teklifid);
        //            var urunler = TeklifManager.GetUrunList(teklifid);
        //            TeklifModel modelbind = new TeklifModel(teklif, urunler);
        //            return View(modelbind);
        //        }
        //        else
        //            return View();
           
        //    }
        //    else
        //        return View();
           
           
        //}

        //
        // GET: /Admin/Home/Create

      
    }
}
