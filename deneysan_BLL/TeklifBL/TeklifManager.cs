using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deneysan_DAL.Context;
using deneysan_DAL.Entities;
using System.Data;
using System.Data.Entity;
using System.Linq;
namespace deneysan_BLL.TeklifBL
{
    public class TeklifManager
    {

        public static List<Teklif> GetList()
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                int status = Convert.ToInt32(EnumTeklifTip.Onaylanmadi);
                var list = db.Teklif.OrderByDescending(d => d.TeklifTarihi).ToList();
                return list;
            }
        }

        public static List<Teklif> GetList(int type)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                
                var list = db.Teklif.Where(d => d.Durum == type).OrderByDescending(d => d.TeklifTarihi).ToList();
                return list;
            }
        }

        public static Teklif GetTeklifById(int teklifid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {

                var model = db.Teklif.SingleOrDefault(d => d.TeklifId == teklifid);
                if (model != null)
                    return model;
                else return null;
            }
        }

        public static List<TeklifUrun_Urun> GetUrunList(int teklifid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {

               // var model = db.TeklifUrun.Where(d => d.TeklifId == teklifid).ToList();
                var model = (from t in db.TeklifUrun
                             join
                                 p in db.Product
                                 on t.UrunId equals p.ProductId
                             select new
                             {
                                 t.TeklifUrunId,
                                 t.TeklifId,
                                 t.UrunId,
                                 t.Fiyat,
                                 t.Adet ,
                                 t.Toplam ,
                                 t.Donanim ,
                                 t.DonanimFiyat ,
                                 t.ParaBirimi,
                                 p.Code ,
                                 p.ProductImageThumb, 
                                 p.Name 
                             }).ToList();

                List<TeklifUrun_Urun> liste = new List<TeklifUrun_Urun>();
                foreach (var item in model)
                {
                    TeklifUrun_Urun m = new TeklifUrun_Urun();
                     m.TeklifUrunId = item.TeklifUrunId;
                     m.TeklifId = item.TeklifId;
                     m.UrunId = item.UrunId;
                     m.Fiyat = item.Fiyat;
                     m.Adet = item.Adet;
                     m.Toplam = item.Toplam;
                     m.Donanim = item.Donanim;
                     m.DonanimFiyat = item.DonanimFiyat;
                     m.ParaBirimi = item.ParaBirimi;
                     m.UrunKod = item.Code;
                     m.UrunResim = item.ProductImageThumb;
                     m.UrunAdi = item.Name;
                     liste.Add(m);
                }

                return liste;
            }
        }

        public static bool AddTeklif(Teklif teklif, IEnumerable<TeklifUrun> teklifurun)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    teklif.Durum = (int)EnumTeklifTip.Onaylanmadi;
                    teklif.TeklifTarihi = DateTime.Now;
                    
                    db.Teklif.Add(teklif);
                    foreach (var item in teklifurun)
                    {
                        db.TeklifUrun.Add(item);
                    }
                    db.SaveChanges();

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
