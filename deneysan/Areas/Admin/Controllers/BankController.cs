using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan.Areas.Admin.Filters;
using deneysan.Areas.Admin.Helpers;
using deneysan_BLL.BankBL;
using deneysan_BLL.LanguageBL;
using deneysan_DAL.Entities;

namespace deneysan.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class BankController : Controller
    {
        //
        // GET: /Admin/Bank/

        public ActionResult Index()
        {
            string lang = FillLanguagesList();
            var banklist = BankManager.GetBankInfoList(lang);
            return View(banklist);
        }

        public ActionResult AddBank()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            return View();
        }

        [HttpPost]
        public ActionResult AddBank(BankInfo bank,HttpPostedFileBase uploadfile)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(280, 80).SaveThumbnail(uploadfile, "/Content/images/bankinfo/", Utility.SetPagePlug(bank.BankName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    bank.Logo = "/Content/images/bankinfo/" + Utility.SetPagePlug(bank.BankName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    bank.Logo = "/Content/images/front/noimage.jpeg";
                }

                
                ViewBag.ProcessMessage = BankManager.AddBankInfo(bank);
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();
        }



        public ActionResult EditBank()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    BankInfo bank = BankManager.GetBankInfoById(nid);
                    return View(bank);
                }
                else
                    return View();
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult EditBank(BankInfo bank, HttpPostedFileBase uploadfile)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/bankinfo/", Utility.SetPagePlug(bank.BankName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    bank.Logo = "/Content/images/bankinfo/" + Utility.SetPagePlug(bank.BankName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        bank.BankId = nid;
                        ViewBag.ProcessMessage = BankManager.EditBank(bank); 
                        return View(bank);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(bank);
                    }
                }
                else       
                    return View();
            }
            else
                return View();



        }




        public JsonResult EditStatus(int id)
        {
            string NowState;
            bool isupdate = BankManager.UpdateStatus(id);
            return Json(isupdate);
        }

        [AllowAnonymous]
        public JsonResult DeleteBankInfo(int id)
        {
            bool isdelete = BankManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }


        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
            return lang;
        }


    }
}
