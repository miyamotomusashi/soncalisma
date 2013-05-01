﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using deneysan_BLL.LogBL;
using deneysan_DAL.Context;
using deneysan_DAL.Entities;
using log4net;

namespace deneysan_BLL.ReferenceBL
{
    public class ReferenceManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<References> GetReferenceList(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.References.Where(d => d.Deleted == false && d.Language == language).ToList();
                return list;
            }
        }

        public static List<References> GetReferenceListForFront(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.References.Where(d => d.Deleted == false && d.Language == language && d.Online==true).ToList();
                return list;
            }
        }

        public static bool AddReference(References record)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    if (!record.TimeCreated.HasValue)
                        record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    db.References.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Referans.ToString();
                    logkeeper.Message = LogMessages.ReferenceAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.ReferenceName;
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
                var list = db.References.SingleOrDefault(d => d.ReferenceId == id);
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
                    var record = db.References.FirstOrDefault(d => d.ReferenceId == id);
                    record.Deleted = true;
                    
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Referans.ToString();
                    logkeeper.Message = LogMessages.ReferenceDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.ReferenceName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static References GetReferenceById(int nid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    References record = db.References.Where(d => d.ReferenceId == nid).SingleOrDefault();
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

        public static bool EditReference(References referencemodel)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    References record = db.References.Where(d => d.ReferenceId == referencemodel.ReferenceId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Content = referencemodel.Content;
                        record.Link = referencemodel.Link;
                        record.ReferenceName = referencemodel.ReferenceName;
                        record.Language = referencemodel.Language;
                        if (!string.IsNullOrEmpty(referencemodel.Logo))
                        {
                            record.Logo = referencemodel.Logo;
                        }
                        record.Content = referencemodel.Content;
                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Referans.ToString();
                        logkeeper.Message = LogMessages.ReferenceEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.ReferenceName;
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
