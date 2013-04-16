using deneysan_BLL.ContactBL;
using deneysan_BLL.BankBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

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
            var banks = BankManager.GetBankInfoListForFront(lang);
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
                using (var client = new SmtpClient("mail.deneysan.com.tr", 587))
                {
                    client.EnableSsl = false;
                    client.Credentials = new NetworkCredential("info@deneysan.com.tr", "Deneysan2013");
                    var mail = new MailMessage();
                    mail.From = new MailAddress("info@deneysan.com.tr");
                    mail.To.Add("info@deneysan.com.tr");
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;
                    mail.Body = "<h5><b>" + namesurname + " - " + email + "</b></h5>" + "<p>" + body + "</p>";
                   
                    client.Send(mail);
                }
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
