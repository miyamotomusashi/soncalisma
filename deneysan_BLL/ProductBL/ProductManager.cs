using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deneysan_DAL.Context;
using deneysan_DAL.Entities;

namespace deneysan_BLL.ProductBL
{
    public class ProductManager
    {
        #region ProductGroup
        public static List<ProductGroup> GetProductGroupList(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.ProductGroup.Where(d => d.Deleted == false && d.Language == language).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static List<ProductGroup> GetProductGroupListForFront(string language)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.ProductGroup.Where(d => d.Deleted == false && d.Language == language && d.Online==true).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static bool AddProductGroup(ProductGroup record)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    record.SortNumber = 9999;
                    db.ProductGroup.Add(record);
                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.ProductGroupAdded;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.GroupName;
                    //logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }


        public static bool EditProductGroup(ProductGroup record)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    ProductGroup editrecord = db.ProductGroup.Where(d => d.ProductGroupId == record.ProductGroupId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        editrecord.TimeUpdated = DateTime.Now;
                        editrecord.GroupName = record.GroupName;
                        editrecord.PageSlug = record.PageSlug;
                        if (!string.IsNullOrEmpty(record.GroupImage))
                            editrecord.GroupImage = record.GroupImage;

                        db.SaveChanges();

                        //LogtrackManager logkeeper = new LogtrackManager();
                        //logkeeper.LogDate = DateTime.Now;
                        //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                        //logkeeper.Message = LogMessages.ProductGroupAdded;
                        //logkeeper.User = HttpContext.Current.User.Identity.Name;
                        //logkeeper.Data = record.GroupName;
                        //logkeeper.AddInfoLog(logger);


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
        
        public static bool UpdateGroupStatus(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.ProductGroup.SingleOrDefault(d => d.ProductGroupId == id);
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


        public static bool DeleteGroup(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    var record = db.ProductGroup.FirstOrDefault(d => d.ProductGroupId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.ProductGroupDeleted;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.GroupName;
                    //logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static ProductGroup GetProductGroupById(int nid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    ProductGroup record = db.ProductGroup.Where(d => d.ProductGroupId == nid).SingleOrDefault();
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

        public static bool EditProductGroup(int id, string name, string pageslug)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    ProductGroup record = db.ProductGroup.Where(d => d.ProductGroupId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {

                        record.GroupName = name;
                        record.PageSlug = pageslug;
                        record.TimeUpdated = DateTime.Now;
                        db.SaveChanges();

                        //LogtrackManager logkeeper = new LogtrackManager();
                        //logkeeper.LogDate = DateTime.Now;
                        //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                        //logkeeper.Message = LogMessages.ProductGroupEdited;
                        //logkeeper.User = HttpContext.Current.User.Identity.Name;
                        //logkeeper.Data = record.GroupName;
                        //logkeeper.AddInfoLog(logger);

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
                        ProductGroup sortingrecord = db.ProductGroup.SingleOrDefault(d => d.ProductGroupId == mid);
                        sortingrecord.SortNumber = Convert.ToInt32(row);
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

        #endregion ProductGroup





        #region Product

        public static bool SortProducts(string[] idsList)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {

                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        Product sortingrecord = db.Product.SingleOrDefault(d => d.ProductId == mid);
                        sortingrecord.SortNumber = Convert.ToInt32(row);
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

        public static List<Product> GetProductListAll(string lang)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                db.Product.Include("ProductGroup").ToList();
                var list = db.Product.Where(d => d.Deleted == false && d.Language == lang).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static List<Product> GetProductListAllForFront(string lang)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.Product.Include("ProductGroup").Where(d => d.Deleted == false && d.Language == lang && d.Online==true).OrderByDescending(d=>d.TimeCreated).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static List<Product> GetProductList(int gid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.Product.Include("ProductGroup").Where(d => d.Deleted == false && d.ProductGroupId == gid).OrderByDescending(d=>d.TimeCreated).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static List<Product> GetProductListAllForTeklif(string lang)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.Product.Include("ProductGroup").Where(d => d.Deleted == false && d.Online == true && d.Language == lang).OrderBy(d => d.Name).ToList();
                return list;
            }
        }

        public static bool AddProduct(Product record)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {

                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    
                    record.Online = true;
                    record.SortNumber = 9999;
                    db.Product.Add(record);
                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                    //logkeeper.Message = LogMessages.ProductAdded;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.Name;
                    //logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static bool EditProduct(Product data)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == data.ProductId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Name = data.Name;
                        record.Content = data.Content;
                        record.PageSlug = data.PageSlug;
                        record.Language = data.Language;
                        record.Hardware = data.Hardware;
                        record.HardwarePrice = data.HardwarePrice;
                        record.Price = data.Price;
                        record.Code = data.Code;
                        record.ProductGroupId = data.ProductGroupId;
                        if (!string.IsNullOrEmpty(data.ProductImage))
                        {
                            record.ProductImageThumb = data.ProductImageThumb;
                            record.ProductImage = data.ProductImage;
                        }
                        if (!string.IsNullOrEmpty(data.filexperiment))
                        {
                            record.filexperiment = data.filexperiment;
                        }
                        if (!string.IsNullOrEmpty(data.filetechnical))
                        {
                            record.filetechnical = data.filetechnical;
                        }
                        if (!string.IsNullOrEmpty(data.filetraining))
                        {
                            record.filetraining = data.filetraining;
                        }
                        if (!string.IsNullOrEmpty(data.filevideo))
                        {
                            record.filevideo = data.filevideo;
                        }
                       
                        db.SaveChanges();

                        //LogtrackManager logkeeper = new LogtrackManager();
                        //logkeeper.LogDate = DateTime.Now;
                        //logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                        //logkeeper.Message = LogMessages.ProductEdited;
                        //logkeeper.User = HttpContext.Current.User.Identity.Name;
                        //logkeeper.Data = record.Name;
                        //logkeeper.AddInfoLog(logger);


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


        #endregion Product


        public static object UpdateStatus(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var list = db.Product.SingleOrDefault(d => d.ProductId == id);
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

        public static object DeleteProduct(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    var record = db.Product.FirstOrDefault(d => d.ProductId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                    //logkeeper.Message = LogMessages.ProductDeleted;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.Name;
                    //logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Product GetProductById(int nid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    Product record = db.Product.Include("ProductGroup").Where(d => d.ProductId == nid && d.Deleted == false).SingleOrDefault();
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

        public static List<Product> GetProductByIds(Dictionary<string, string>[] ids)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    List<Product> list = new List<Product>();
                    
                    foreach (var element in ids)
                    {
                        foreach (var entry in element)
                        {
                            Product p = ProductManager.GetProductById(Convert.ToInt32(entry.Value));
                            list.Add(p);
                        }
                    }

                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }



        public static bool RemoveTechnic(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.filetechnical = null;
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

        public static object RemoveTraining(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.filetraining = null;
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

        public static object RemoveExperimental(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.filexperiment = null;
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

        public static object RemoveVideo(int id)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    Product record = db.Product.Where(d => d.ProductId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.filevideo = null;
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

        public static ProductGroup GetGroupById(int nid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    ProductGroup record = db.ProductGroup.Where(d => d.ProductGroupId == nid && d.Deleted==false).SingleOrDefault();
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


       
        
    }
}
