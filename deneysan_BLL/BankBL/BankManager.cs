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

namespace deneysan_BLL.BankBL
{
    public class BankManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<BankInfo> GetBankInfoList(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.BankInfo.Where(d =>  d.Language == language).ToList();
                return list;
            }
        }


        public static List<BankInfo> GetBankInfoListForFront(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.BankInfo.Where(d => d.Language == language && d.Online==true).ToList();
                return list;
            }
        }

        public static bool AddBankInfo(BankInfo record)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    
                    record.Online = true;
                    db.BankInfo.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.BankaBilgisi.ToString();
                    logkeeper.Message = LogMessages.BankAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.BankName;
                    logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }


        public static bool UpdateStatus(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.BankInfo.SingleOrDefault(d => d.BankId == id);
                try
                {

                    if (list != null)
                    {
                        list.Online = list.Online == true ? false : true;
                        db.SaveChanges();

                    }
                    return list.Online;

                }
                catch (Exception)
                {
                    return list.Online;
                }
            }
        }


        public static bool Delete(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    var record = db.BankInfo.FirstOrDefault(d => d.BankId == id);
                    db.BankInfo.Remove(record);

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.BankaBilgisi.ToString();
                    logkeeper.Message = LogMessages.BankDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.BankName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static BankInfo GetBankInfoById(int nid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    BankInfo record = db.BankInfo.Where(d => d.BankId == nid).SingleOrDefault();
                    if (record != null)
                        return record;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool EditBank(BankInfo model)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    BankInfo record = db.BankInfo.Where(d => d.BankId == model.BankId ).SingleOrDefault();
                    if (record != null)
                    {
                        record.BankName = model.BankName;
                        record.Language = model.Language;
                        record.BankNumber = model.BankNumber;
                        
                        record.IBAN = model.IBAN;
                        if (!string.IsNullOrEmpty(model.Logo))
                        {
                            record.Logo = model.Logo;
                        }
                        
                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.BankaBilgisi.ToString();
                        logkeeper.Message = LogMessages.BankEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.BankName;
                        logkeeper.AddInfoLog(logger);

                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

    }
}
