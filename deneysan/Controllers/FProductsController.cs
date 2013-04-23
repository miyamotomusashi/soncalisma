using deneysan.Models;
using deneysan_BLL.ProductBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deneysan.Controllers
{
    public class FProductsController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        //
        // GET: /Urunler/

        public ActionResult Index()
        {
            var product_group_list = ProductManager.GetProductGroupList(lang);
            var product_list = ProductManager.GetProductListAll(lang);
            ProductWrapperModel modelbind = new ProductWrapperModel(product_list, product_group_list);
            return View(modelbind);
        }

    }
}
