using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan.Areas.Admin.Models;
using deneysan_BLL.TeklifBL;
using deneysan_DAL.Entities;
using System.IO;
using deneysan.Areas.Admin.Filters;
using deneysan_BLL.ProductBL;
using deneysan_BLL.LanguageBL;
using Newtonsoft.Json;

namespace deneysan.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class TeklifController : Controller
    {
        //
        // GET: /Admin/Teklif/

        public ActionResult Add()
        {
            string lang = FillLangList();
            var list = ProductManager.GetProductListAllForTeklif(lang);
            return View(list);
        }

        [HttpPost]
        public ActionResult AddContinue()
        {
            Teklif teklif = new Teklif();
            HttpCookie cookie;
            TeklifUrun[] teklifurun;

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("AdminOfferList"))
            {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["AdminOfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
                teklifurun = new TeklifUrun[values.Count()];

                int i = 0;

                foreach (var item in values)
                {
                    teklifurun[i] = new TeklifUrun();
                    teklifurun[i].UrunId = Convert.ToInt32(item["id"]);
                    teklifurun[i].Adet = Convert.ToInt32(item["count"]);
                    i++;
                }
                values = null;
            }
            else
            {
                teklifurun = new TeklifUrun[0];
            }

            teklif.Kurum = "------";
            teklif.Adsoyad = "------";
            teklif.Eposta = "------";
            teklif.Gsm = "------";

            bool result = TeklifManager.AddTeklif(teklif, teklifurun, null);
            int newID = TeklifManager.GetList().OrderByDescending(d => d.TeklifId).FirstOrDefault().TeklifId;
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("AdminOfferList"))
            {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["AdminOfferList"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Details", "Teklif", new { id = newID });
            //}
            //return RedirectToAction("Add");
        }

        [HttpPost]
        public string AddToList(string id, string count)
        {
            if (!this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("AdminOfferList"))
            {
                HttpCookie cookie = new HttpCookie("AdminOfferList");
                cookie.Value = "[{id:'" + id + "',count:'" + count + "'}]";
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                return "1";
            }
            else
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["AdminOfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
                cookie.Value = "[";

                foreach (var element in values)
                {
                    string currentId = "";
                    foreach (var entry in element)
                    {
                        if (entry.Key == "id")
                            currentId = entry.Value;

                        if (entry.Key == "id" && entry.Value == id)
                            return values.Count().ToString();

                        if (entry.Key == "count")
                            cookie.Value += "{id:'" + currentId + "',count:'" + entry.Value + "'},";
                    }
                }

                cookie.Value += "{id:'" + id + "',count:'" + count + "'}]";

                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                return (values.Count() + 1).ToString();
            }
        }

        [HttpPost]
        public string RemoveFromList(string id)
        {
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["AdminOfferList"];
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
            cookie.Value = "[";

            foreach (var element in values)
            {
                string currentId = "";
                foreach (var entry in element)
                {
                    if (entry.Key == "id")
                        currentId = entry.Value;

                    if (entry.Key == "id" && entry.Value == id)
                        break;

                    if (entry.Key == "count")
                        cookie.Value += "{id:'" + currentId + "',count:'" + entry.Value + "'},";
                }
            }

            if (cookie.Value.Equals("["))
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
            }
            else
            {
                cookie.Value = cookie.Value.Substring(0, cookie.Value.Length - 1) + "]";
            }

            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            return (values.Count() - 1).ToString();
        }

        private string FillLangList()
        {
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
            return lang;
        }

        public ActionResult Delete(int id, string type)
        {
            TeklifManager.DeleteTeklif(id);
            return RedirectToAction("Index", new { @type = type });
        }

        public JsonResult DeleteTeklifUrun(int id)
        {
            bool retval = TeklifManager.DeleteTeklifUrun(id);
            return Json(retval);
        }

        public ActionResult Index()
        {
            var list = TeklifManager.GetList();
            if (RouteData.Values["type"] != null)
            {
                ViewBag.Tur = "0";
                string type = RouteData.Values["type"].ToString();
                if (type == "tumteklifler")
                {
                    ViewBag.Header = "TÜM TEKLİFLER";
                    ViewBag.Tur = "1";
                    list = TeklifManager.GetList();
                }
                else if (type == "onaybekleyenler")
                {
                    ViewBag.Header = "YENİ GELEN TEKLİFLER / ONAY BEKLEYEN TEKLİFLER";
                    list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylanmadi));
                }
                else if (type == "onaylananlar")
                {
                    ViewBag.Header = "ONAYLANAN TEKLİFLER";
                    list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylandi));
                }
                else if (type == "iptaledilenler")
                {
                    ViewBag.Header = "İPTAL EDİLEN TEKLİFLER";
                    list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Iptal));
                }

                return View(list);
            }
            else
            {
                ViewBag.Header = "YENİ GELEN TEKLİFLER / ONAY BEKLEYEN TEKLİFLER";
                list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylanmadi));
                return View(list);
            }
        }

        public ActionResult AllList()
        {
            var list = TeklifManager.GetList();
            return View(list);
        }

        [HttpPost]
        public ActionResult Details(Teklif teklif, string teklifid, string txtcevaptarihi, string TeklifDurum)
        {
            if (ModelState.IsValid)
            {

                teklif.TeklifId = Convert.ToInt32(teklifid);
                teklif.CevapTarihi = Convert.ToDateTime(txtcevaptarihi);
                teklif.Durum = Convert.ToInt32(TeklifDurum);
                if (teklif.Not == null) teklif.Not = " ";
                if (teklif.OdemeNotu == null) teklif.OdemeNotu = " ";
                ViewBag.ProcessMessage = TeklifManager.UpdateTeklif(teklif);
                return RedirectToAction("Details", new { id = teklifid });
            }

            return View();
        }

        public FileStreamResult Proforma(string tekid)
        {
            MemoryStream pdf = TeklifManager.ProformaOnizle(tekid);

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", "1"));
            Response.BinaryWrite(pdf.ToArray());

            return new FileStreamResult(pdf, "application/pdf");
            //return File(pdf, "application/pdf", "DownloadName.pdf");
            //return RedirectToAction("Details", new { id = tekid });
        }

        public ActionResult ProformaGonder(string tekid2)
        {
            ViewBag.ProcessMessage = TeklifManager.ProformaGonder(tekid2);
            return RedirectToAction("Details", new { id = tekid2 });
        }

        public ActionResult Details(int id)
        {
            int teklifid = 0;
            ViewBag.cevaptrh = "";
            if (RouteData.Values["id"] != null)
            {
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out teklifid);
                if (isnumber)
                {
                    var teklif = TeklifManager.GetTeklifById(teklifid);
                    if (teklif.CevapTarihi == null)
                    {
                        ViewBag.cevaptrh = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        ViewBag.cevaptrh = ((DateTime)teklif.CevapTarihi).ToShortDateString();
                    }

                    var TekDurum = new List<SelectListItem>{ 
                                           new SelectListItem { Value = "0", Text = "Onaylandı", Selected = false },
                                           new SelectListItem { Value = "1", Text = "Onaylanmadı", Selected = false },
                                           new SelectListItem { Value = "2", Text = "İptal Edildi", Selected = false }
                                 };
                    TekDurum.FirstOrDefault(d => d.Value == teklif.Durum.ToString()).Selected = true;
                    ViewBag.TeklifDurum = TekDurum;
                    var urunler = TeklifManager.GetUrunList(teklifid);
                    TeklifModel modelbind = new TeklifModel(teklif, urunler);
                    return View(modelbind);
                }
                else
                    return View();

            }
            else
                return View();
        }


        public JsonResult UpdateRecord(int id, string fiyat, int adet, string donanim, int teklifid)
        {
            string[] vals = TeklifManager.HesaplamaYap(id, fiyat, adet, donanim, teklifid);
            return Json(vals);

        }


        public PartialViewResult PartialResult(int teklifid)
        {
            ViewBag.TeklifId = teklifid;
           
            string lang = FillLangList();
            List<Product> list = ProductManager.GetProductListAllForTeklif(lang);
         

            return PartialView(VirtualPathUtility.ToAbsolute("~/Areas/Admin/Views/Teklif/_productchoise.cshtml"),list);
        }



        [HttpPost]
        public void AddNewProduct(string hdnteklifid)
        {
            int teklifId = Convert.ToInt32(hdnteklifid);
            HttpCookie cookie;
            List<TeklifUrun> teklifurun = new List<TeklifUrun>();

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("AdminOfferList"))
            {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["AdminOfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
               

                int i = 0;

                foreach (var item in values)
                {
                    TeklifUrun turun  = new TeklifUrun();
                    turun.UrunId = Convert.ToInt32(item["id"]);
                    turun.Adet = Convert.ToInt32(item["count"]);
                    teklifurun.Add(turun);
                }
                values = null;
            }
            //else
            //{
            //    teklifurun = new TeklifUrun[0];
            //}

            bool result = TeklifManager.DuzenleTeklifUrun(teklifId, teklifurun);

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("AdminOfferList"))
            {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["AdminOfferList"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }


            //return RedirectToAction("Details", "Teklif", new { id = teklifId });
            Response.Redirect("/yonetim/teklifler/detay/"+teklifId);
            //}
            //return RedirectToAction("Add");
        }



      

    }
}

