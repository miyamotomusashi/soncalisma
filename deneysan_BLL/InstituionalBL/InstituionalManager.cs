using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deneysan_DAL.Context;
using deneysan_DAL.Entities;

namespace deneysan_BLL.InstituionalBL
{
    public class InstituionalManager
    {
        public static Institutional GetInstationalByLanguage(string language,int typeid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                Institutional instional_info = db.Institutional.SingleOrDefault(d => d.TypeId == typeid && d.Language == language);
                return instional_info;
            }
        }

        public static bool EditInstituional(Institutional record)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                Institutional editrecord = db.Institutional.SingleOrDefault(d => d.TypeId == record.TypeId && d.Language == record.Language);
                if (editrecord != null)
                {
                    editrecord.TimeUpdated = DateTime.Now;
                    editrecord.Content = record.Content;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
                
            }
        }
    }
}
