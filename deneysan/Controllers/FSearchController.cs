using deneysan_BLL.SearchBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deneysan.Controllers
{
    public class FSearchController : Controller
    {
        //
        // GET: /Shared/

        public ActionResult _search()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string SearchText)
        {
            var result = SearchManager.Search(SearchText);
            return View(result);
        }
    }
}
