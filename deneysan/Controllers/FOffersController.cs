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
            return View();
        }

        [HttpPost]
        public ActionResult Index(string namesurname, string email, string subject, string body)
        {
            return View();
        }

    }
}
