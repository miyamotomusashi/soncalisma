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
        //
        // GET: /Kariyer/

        public ActionResult Index()
        {
            return View();
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
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("cetintozkoparan@gmail.com", "c1e2t3i4n5");
                    var mail = new MailMessage();
                    mail.From = new MailAddress("cetintozkoparan@yahoo.com");
                    mail.To.Add("cetintozkoparan@hotmail.com");
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
