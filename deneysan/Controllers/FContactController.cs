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
using deneysan_BLL.MailBL;

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
                if (namesurname == String.Empty || email == String.Empty || subject == String.Empty || body == String.Empty)
                {
                    TempData["required"] = "true";
                    return RedirectToAction("Form");
                }

                var mset = MailManager.GetMailSettings();
                var msend = MailManager.GetMailUsersList(0);

                using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                {
                    client.EnableSsl = false;
                    client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                    var mail = new MailMessage();
                    mail.From = new MailAddress(mset.ServerMail);
                    foreach (var item in msend)
                        mail.To.Add(item.MailAddress);
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;
                    mail.Body = "<h5><b>" + namesurname + " - " + email + "</b></h5>" + "<p>" + body + "</p>";
                   
                    if(mail.To.Count > 0) client.Send(mail);
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
