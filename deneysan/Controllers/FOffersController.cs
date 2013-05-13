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
            if (!this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string namesurname, string email, string subject, string body)
        {
            return View();
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
