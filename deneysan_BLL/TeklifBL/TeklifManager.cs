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
using System.Web;
using deneysan_BLL.MailBL;
using System.Net.Mail;
using System.Net;
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
                             join p in db.Product on t.UrunId equals p.ProductId
                             join tk in db.Teklif on t.TeklifId equals tk.TeklifId
                             where t.TeklifId == teklifid
                             select new
                             {
                                 t.TeklifUrunId,
                                 t.TeklifId,
                                 t.UrunId,
                                 t.Fiyat,
                                 t.Adet,
                                 t.Toplam,
                                 t.Donanim,
                                 t.DonanimFiyat,
                                 t.ParaBirimi,
                                 p.Code,
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

        public static bool UpdateTeklif(Teklif teklif)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    var tek = db.Teklif.Where(d => d.TeklifId == teklif.TeklifId).FirstOrDefault();
                    if (tek != null)
                    {
                        tek.Kurum = teklif.Kurum;
                        tek.Unvan = teklif.Unvan;
                        tek.Adsoyad = teklif.Adsoyad;
                        tek.Gsm = teklif.Gsm;
                        tek.Tel = teklif.Tel;
                        tek.Fax= teklif.Fax;
                        tek.CevapTarihi = teklif.CevapTarihi;
                        tek.TeklifNo = teklif.TeklifNo;
                        tek.GecerlilikSuresi = teklif.GecerlilikSuresi;
                        tek.TeslimatSuresi = teklif.TeslimatSuresi;
                        db.SaveChanges();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool AddTeklif(Teklif teklif, TeklifUrun[] teklifurun, Dictionary<string, string>[] products)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    teklif.Durum = (int)EnumTeklifTip.Onaylanmadi;
                    teklif.TeklifTarihi = DateTime.Now;
                    teklif.ParaBirimi = "TL";

                    db.Teklif.Add(teklif);
                    db.SaveChanges();

                    int[] plist = new int[50];

                    int k = 0;

                    foreach (var element in products)
                    {
                        foreach (var entry in element)
                        {
                            plist[k] = Convert.ToInt32(entry.Value);
                        }
                        k++;
                    }

                    double toplamTutar = 0;
                    for (int i = 0; i < teklifurun.Count(); i++)
                    {
                        int pid = Convert.ToInt32(plist[i]);
                        var prod = db.Product.Where(a => a.ProductId == pid).SingleOrDefault();

                        teklifurun[i].TeklifId = teklif.TeklifId;
                        teklifurun[i].UrunId = plist[i];
                        teklifurun[i].Fiyat = prod.Price;
                        if (teklifurun[i].Donanim && prod.Hardware)
                        {
                            teklifurun[i].Toplam = (Convert.ToDouble(prod.Price * teklifurun[i].Adet) * 1.18 + (Convert.ToDouble(prod.HardwarePrice) * 1.18)).ToString();
                        }
                        else
                        {
                            teklifurun[i].Toplam = (Convert.ToDouble(prod.Price * teklifurun[i].Adet) * 1.18).ToString();
                        }
                        toplamTutar += Convert.ToDouble(teklifurun[i].Toplam);
                        db.TeklifUrun.Add(teklifurun[i]);
                    }

                    teklif.FaturaTutar = Convert.ToDecimal(toplamTutar);
                    teklif.KDV = Convert.ToDecimal(Convert.ToDouble(teklif.FaturaTutar) * 0.18);

                    db.SaveChanges();
                    var mset = MailManager.GetMailSettings();
                    var msend = MailManager.GetMailUsersList(1);

                    using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                    {
                        client.EnableSsl = false;
                        client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                        var mail = new MailMessage();
                        mail.From = new MailAddress(mset.ServerMail);
                        foreach (var item in msend)
                            mail.To.Add(item.MailAddress);
                        mail.Subject = "Teklif Talebi";
                        mail.IsBodyHtml = true;
                        mail.Body = "<p>" + "Teklif detaylarına aşağıdaki linkten ulaşabilirsiniz: " + "</p>";
                        mail.Body += "<a href='http://www.deneysan.com/yonetim'>http://www.deneysan.com/yonetim</a>";

                        if (mail.To.Count > 0) client.Send(mail);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        public static string[] HesaplamaYap(int id, string fiyat, int adet, string donanim, int teklifid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                var urun = db.TeklifUrun.Where(d => d.TeklifId == teklifid && d.UrunId == id).SingleOrDefault();
                urun.Fiyat = Convert.ToDecimal(fiyat);
                urun.Adet = adet;
                if (!string.IsNullOrEmpty(donanim))
                    urun.Toplam = (Convert.ToDecimal(fiyat) * adet + Convert.ToDecimal(donanim)).ToString();
                else urun.Toplam = (Convert.ToDecimal(fiyat) * adet).ToString();

                db.SaveChanges();


                var urunler = db.TeklifUrun.Where(d => d.TeklifId == teklifid).ToList();
                decimal fatura = 0;
                foreach (var item in urunler)
                {
                    fatura += Convert.ToDecimal(item.Toplam);
                }

                var teklif = db.Teklif.Where(d => d.TeklifId == teklifid).SingleOrDefault();
                teklif.FaturaTutar = fatura;
                teklif.KDV = fatura * 18 / 100;
                db.SaveChanges();

                string[] returnvalues = new string[3];
                returnvalues[0] = teklif.FaturaTutar.ToString();
                returnvalues[1] = ";";
                returnvalues[2] = teklif.KDV.ToString();
                return returnvalues;
            }
        }
    }


}
