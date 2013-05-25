using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using deneysan_BLL.LogBL;
using deneysan_DAL.Context;
using deneysan_DAL.Entities;
using log4net;

namespace deneysan_BLL.ContactBL
{
    public class ContactManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static Contact GetContact(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.Contact.Where(d=>d.Language == language).SingleOrDefault();
                return list;
            }
        }

        public static dynamic EditContact(Contact record)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    Contact contact = db.Contact.Where(d => d.Language == record.Language).SingleOrDefault();
                    if (contact == null)
                    {
                        contact = new Contact();
                        contact.Address = record.Address;
                        contact.Phone = record.Phone;
                        contact.Fax = record.Fax;
                        contact.Taxnumber = record.Taxnumber;
                        contact.Taxoffice = record.Taxoffice;
                        contact.Email = record.Email;
                        contact.Language = record.Language;
                        db.Contact.Add(contact);
                    }
                    else
                    {
                        contact.Address = record.Address;
                        contact.Phone = record.Phone;
                        contact.Fax = record.Fax;
                        contact.Taxnumber = record.Taxnumber;
                        contact.Taxoffice = record.Taxoffice;
                        contact.Email = record.Email;
                    }

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Contact.ToString();
                    logkeeper.Message = LogMessages.ContactEdited;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Address;
                    logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
