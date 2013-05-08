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
            var product_group_list = ProductManager.GetProductGroupListForFront(lang);
            var product_list = ProductManager.GetProductListAllForFront(lang);
            ProductWrapperModel modelbind = new ProductWrapperModel(product_list, product_group_list);
            return View(modelbind);
        }

        public ActionResult ProductList(int gid)
        {
            var product_group_list = ProductManager.GetProductGroupListForFront(lang);
            var product_list = ProductManager.GetProductList(gid);
            ProductWrapperModel modelbind = new ProductWrapperModel(product_list, product_group_list);
            return View(modelbind);
        }

        public ActionResult ProductDetail(int pid)
        {
            var product = ProductManager.GetProductById(pid);
            return View(product);
        }
    }
}
