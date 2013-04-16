using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan.Areas.Admin.Helpers;
using deneysan_BLL.LanguageBL;
using deneysan_BLL.ProductBL;
//using deneysan_BLL.ProductBL;
using deneysan_DAL.Entities;

namespace deneysan.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {


        public ActionResult Index()
        {
            string id = FillLanguagesListForProductList();

            int groupid = Convert.ToInt32(id);

            var list = ProductManager.GetProductList(groupid);
            return View(list);
        }

        public ActionResult AddProduct()
        {
            FillLanguagesList();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product model, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadtechfile, HttpPostedFileBase uploadvideo, HttpPostedFileBase uploadexperimentfile, HttpPostedFileBase uploadtraining)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {

                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/products/", Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    model.ProductImage = "/Content/images/news/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    model.ProductImage = "/Content/images/front/noimage.jpeg";
                }

                if (uploadtechfile != null && uploadtraining.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtechfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtraining.FileName)));
                    model.filetraining = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtraining.FileName);
                }


                if (uploadtechfile != null && uploadtechfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtechfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtechfile.FileName)));
                    model.filetechnical = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtechfile.FileName);
                }

                if (uploadexperimentfile != null && uploadexperimentfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtechfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadexperimentfile.FileName)));
                    model.filexperiment = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadexperimentfile.FileName);
                }

                if (uploadvideo != null && uploadvideo.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadvideo.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadvideo.FileName)));
                    model.filevideo = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadvideo.FileName);
                }

                ModelState.Clear();
                ViewBag.ProcessMessage = ProductManager.AddProduct(model);
            }
            return View();
        }


        string FillLanguagesListForProductList()
        {
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = ProductManager.GetProductGroupList(lang);

            if (RouteData.Values["id"] == null)
            {
                if (groups != null && groups.Count != 0)
                    id = groups.First().ProductGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();


            var grouplist = new SelectList(groups, "ProductGroupId", "GroupName", id);
            ViewBag.GroupList = grouplist;

            return id;
        }


        public ActionResult EditProduct()
        {

            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Product record = ProductManager.GetProductById(nid);
                    var languages = LanguageManager.GetLanguages();
                    var list = new SelectList(languages, "Culture", "Language", record.Language);
                    ViewBag.LanguageList = list;
                    var groups = ProductManager.GetProductGroupList(record.Language);
                    var grouplist = new SelectList(groups, "ProductGroupId", "GroupName", record.ProductGroupId);
                    ViewBag.GroupList = grouplist;
                    return View(record);
                }
                else
                    return View();
            }
            else
                return View();

        }


        [HttpPost]
        public ActionResult EditProduct(Product model, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadtechfile, HttpPostedFileBase uploadvideo, HttpPostedFileBase uploadexperimentfile, HttpPostedFileBase uploadtraining)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {

                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/products/", Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    model.ProductImage = "/Content/images/news/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    model.ProductImage = "/Content/images/front/noimage.jpeg";
                }

                if (uploadtechfile != null && uploadtechfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtechfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtechfile.FileName)));
                    model.filetechnical = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtechfile.FileName);
                }
                if (uploadtraining != null && uploadtraining.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtechfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtraining.FileName)));
                    model.filetraining = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtraining.FileName);
                }
                if (uploadexperimentfile != null && uploadexperimentfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtechfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadexperimentfile.FileName)));
                    model.filexperiment = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadexperimentfile.FileName);
                }

                if (uploadvideo != null && uploadvideo.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadvideo.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadvideo.FileName)));
                    model.filevideo = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadvideo.FileName);
                }


                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        model.ProductId = nid;
                        ViewBag.ProcessMessage = ProductManager.EditProduct(model);
                        return View(model);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(model);
                    }
                }
                else return View();




            }
            return View();
        }

        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            //var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = ProductManager.GetProductGroupList(lang);
            var grouplist = new SelectList(groups, "ProductGroupId", "GroupName");
            ViewBag.GroupList = grouplist;

            return lang;
        }

        [HttpPost]
        public ActionResult LoadGroup(string lang)
        {
            var grouplst = ProductManager.GetProductGroupList(lang);
            JsonResult result = Json(new SelectList(grouplst, "ProductGroupId", "GroupName"));
            return result;
        }

        public JsonResult EditStatus(int id)
        {
            return Json(ProductManager.UpdateStatus(id));
        }

        public JsonResult DeleteRecord(int id)
        {
            return Json(ProductManager.DeleteProduct(id));
        }

    }
}
