using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using deneysan.Areas.Admin.Models;
using deneysan_DAL.Entities;
using deneysan_BLL.SecurityBL;
using deneysan.AccountBL;
using System.Web.Security;
namespace deneysan.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(LoginModel loginmodel)
        {
            if (ModelState.IsValid)
            {
                //string password = SecurityManager.EncodeMD5(model.Password);
                if (AccountManager.Login(loginmodel.Email, loginmodel.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı!");
                }

                return View(loginmodel);

            }
            else
            {
                return View();
            }
        }

       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Account");

        }

    }
}
