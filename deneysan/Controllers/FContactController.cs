using deneysan_BLL.ContactBL;
using deneysan_BLL.BankBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace deneysan.Controllers
{
    public class FContactController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        //
        // GET: /Iletisim/
       
        public ActionResult Index()
        {
            var contact = ContactManager.GetContact(lang);
            return View(contact);
        }

        public ActionResult Bank()
        {
            var banks = BankManager.GetBankInfoList(lang);
            return View(banks);
        }

        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(string namesurname, string email, string subject, string body)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.Password = "c1e2t3i4n5";
                WebMail.UserName = "cetintozkoparan@gmail.com";
                WebMail.SmtpUseDefaultCredentials = false;
                WebMail.EnableSsl = true;
                WebMail.From = email;
                body = "<h5><b>" + namesurname + " - " + email + "</b></h5>" + "<p>" + body + "</p>";
                WebMail.Send(
                        "cetintozkoparan@hotmail.com",
                        subject,
                        body
                    );
                TempData["sent"] = "true";
                return RedirectToAction("Form");
            }
            catch (Exception ex)
            {
                TempData["sent"] = "false";
            }

            return View();
        }
    }
}
