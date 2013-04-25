using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deneysan_DAL.Context;
using deneysan_DAL.Entities;

namespace deneysan_BLL.HRBL
{
    public class HumanResourceManager
    {
        public static HumanResource GetHRByLanguage(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                HumanResource instional_info = db.HumanResource.SingleOrDefault(d => d.Language == language);
                return instional_info;
            }
        }

        public static bool EditHumanResource(HumanResource record)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                HumanResource editrecord = db.HumanResource.SingleOrDefault(d =>  d.Language == record.Language);
                if (editrecord != null)
                {
                    //editrecord.TimeUpdated = DateTime.Now;
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
