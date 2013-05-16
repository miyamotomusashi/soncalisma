using deneysan_BLL.ProductBL;
using deneysan_BLL.TeklifBL;
using deneysan_DAL.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deneysan.Controllers
{
    public class FOffersController : Controller
    {
        //
        // GET: /FOffers/

        [HttpGet]
        public ActionResult Index()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
                var list = ProductManager.GetProductByIds(values);
                return View(list);
            }
            else
            {
                return View(new List<deneysan_DAL.Entities.Product>());
            }
        }

        [HttpPost]
        public ActionResult Index(Teklif teklif, TeklifUrun urunler)
        {
            teklif.Durum = (int)EnumTeklifTip.Onaylanmadi;
            teklif.TeklifTarihi = DateTime.Now;
            
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            } 
            return View(new List<deneysan_DAL.Entities.Product>());
        }

        [HttpPost]
        public string GetOfferCount()
        {
            if (!this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {
                return "0";
            }
            else
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
                
                return values.Count().ToString();
            }
        }
    }
}
