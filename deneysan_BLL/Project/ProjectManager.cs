using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deneysan_DAL.Context;
using deneysan_DAL.Entities;

namespace deneysan_BLL.Project
{
    public class ProjectManager
    {
        public static List<Projects> GetProjectList(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.Projects.Where(d =>  d.Language == language).OrderBy(d=>d.SortOrder).ToList();
                return list;
            }
        }

        public static List<Projects> GetProjectListForFront(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.Projects.Where(d => d.Language == language && d.Online==true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddProject(Projects record)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.SortOrder = 9999;
                    record.Online = true;
                    db.Projects.Add(record);
                    db.SaveChanges();

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
                var list = db.Projects.SingleOrDefault(d => d.ProjectId == id);
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
                    var record = db.Projects.FirstOrDefault(d => d.ProjectId == id);
                    db.Projects.Remove(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Projects GetProjectById(int nid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    Projects record = db.Projects.Where(d => d.ProjectId == nid).SingleOrDefault();
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

        public static bool EditProject(Projects Projectmodel)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    Projects record = db.Projects.Where(d => d.ProjectId == Projectmodel.ProjectId).SingleOrDefault();
                    if (record != null)
                    {
                        record.Content = Projectmodel.Content;
                        record.Name = Projectmodel.Name;
                        
                        record.Language = Projectmodel.Language;
                        if (!string.IsNullOrEmpty(Projectmodel.ProjectFile))
                        {
                            record.ProjectFile = Projectmodel.ProjectFile;
                        }
                        record.Content = Projectmodel.Content;
                        db.SaveChanges();

                       

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


        public static bool SortRecords(string[] idsList)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {

                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        Projects sortingrecord = db.Projects.SingleOrDefault(d => d.ProjectId == mid);
                        sortingrecord.SortOrder = Convert.ToInt32(row);
                        db.SaveChanges();
                        row++;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
