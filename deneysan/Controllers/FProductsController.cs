using deneysan.Models;
using deneysan_BLL.ProductBL;
using Newtonsoft.Json;
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

        [HttpPost]
        public string AddToList(string id)
        {
            if (!this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {
                HttpCookie cookie = new HttpCookie("OfferList");
                cookie.Value = "[{id:'" + id + "'}]";
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                return "1";
            }
            else
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
                cookie.Value = "[";

                foreach (var element in values)
                {
                    foreach (var entry in element)
                    {
                        if (entry.Value == id)
                            return values.Count().ToString();

                        cookie.Value += "{id:'" + entry.Value + "'},";
                    }
                }

                cookie.Value += "{id:'" + id + "'}]";

                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                return (values.Count() + 1).ToString();
            }
        }

        [HttpPost]
        public string RemoveFromList(string id)
        {
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
            cookie.Value = "[";

            foreach (var element in values)
            {
                foreach (var entry in element)
                {
                    if (entry.Value == id)
                        continue;

                    cookie.Value += "{id:'" + entry.Value + "'},";
                }
            }
            if (cookie.Value.Equals("["))
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
            }
            else
            {
                cookie.Value = cookie.Value.Substring(0, cookie.Value.Length-1) + "]";
            }

            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            return (values.Count() - 1).ToString();
        }

    }
}
