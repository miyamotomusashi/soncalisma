using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myBLOGData;
using System.Data;
using System.Web.Security;
using System.Web;
using deneysan_DAL.Context;
using deneysan_DAL.Entities;
namespace deneysan.AccountBL
{
    public class AccountManager
    {
        public static bool Login(string email,string password)
        {
            using(DeneysanContext db=new DeneysanContext())
            {
                AdminUser record = db.AdminUser.SingleOrDefault(d => d.Email == email && d.Password == password);
                if (record != null)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, record.FullName, DateTime.Now, DateTime.Now.AddMinutes(120), false, "Admin", FormsAuthentication.FormsCookiePath);
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    return true;
                }
                else
                    return false;

            }

        }

      
       
    }
}
