using deneysan_BLL.LinkBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deneysan.Controllers
{
    public class FLinksController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        //
        // GET: /FLinks/

        public ActionResult Index()
        {
            var news = LinkManager.GetImportantLinksListForFront(lang);
            return View(news);
        }

    }
}
