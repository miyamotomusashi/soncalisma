using deneysan_BLL.HRBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace deneysan.Controllers
{
    public class FCareerController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        
        //
        // GET: /Kariyer/

        public ActionResult Index()
        {
            var content = HumanResourceManager.GetHRByLanguage(lang);
            return View(content);
        }

        [HttpGet]
        public ActionResult ApplicationForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ApplicationForm(string namesurname, HttpPostedFileBase attachedfile)
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
                    mail.Subject = "İş Başvurusu";
                    mail.Body = namesurname;
                    if (attachedfile != null && attachedfile.ContentLength > 0)
                    {
                        var attachment = new Attachment(attachedfile.InputStream, attachedfile.FileName);
                        mail.Attachments.Add(attachment);
                    }
                    client.Send(mail);
                }
                TempData["sent"] = "true";
                return RedirectToAction("ApplicationForm");
            }
            catch (Exception ex)
            {
                TempData["sent"] = "false";
            }

            return View();
        }

    }
}
