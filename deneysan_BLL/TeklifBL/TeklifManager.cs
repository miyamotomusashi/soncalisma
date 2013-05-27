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
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Web.Hosting;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
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
                    teklif.GecerlilikSuresi = 90;
                    teklif.TeslimatSuresi = "90";
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

        public static MemoryStream ProformaGonder(string tekid)
        {
            using (DeneysanContext db = new DeneysanContext())
            {
                try
                {
                    BaseFont arial = BaseFont.CreateFont("C:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(arial, 12, Font.NORMAL);

                    var teklif = TeklifManager.GetTeklifById(Convert.ToInt32(tekid));
                    
                    var document = new iTextSharp.text.Document(PageSize.A4, 50, 50, 25, 25);

                    // Create a new PdfWrite object, writing the output to a MemoryStream
                    var output = new MemoryStream();
                    var writer = PdfWriter.GetInstance(document, output);
                    
                    // Open the Document for writing
                    document.Open();
                    // Read in the contents of the Receipt.htm HTML template file
                    string contents = File.ReadAllText(HostingEnvironment.MapPath("~/HTMLTemplate/Receipt.htm"));
                    contents = TurkceKarakter(contents);

                    //PdfPTable table = new PdfPTable(2);
                    //table.TotalWidth = 516f;
                    //table.LockedWidth = true;
                    //float[] widths = new float[] { 3f, 5f };
                    //table.SetWidths(widths);
                    
                    //table.HorizontalAlignment = 1;
                    //table.SpacingBefore = 0f;
                    //table.SpacingAfter = 0f;
                    
                    //PdfPCell cell = new PdfPCell();
                    //cell.BorderWidth = 1;

                    //cell.Colspan = 2;
                    //cell.Border = 1;
                    //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                    //table.AddCell(cell);
                    //table.AddCell("1");
                    //table.AddCell("DENEYSAN EĞİTİM CİHAZLARI SAN. TİC. LTD. ŞTİ.");
                    //table.AddCell("2");
                    //table.AddCell("Col 2 Row 2");
                    //table.AddCell("3");
                    //table.AddCell("Col 2 Row 2");
                    //document.Add(table);


                    contents = contents.Replace("[KURUM]", teklif.Kurum);
                    contents = contents.Replace("[CEVAPTARIHI]", DateTime.Now.ToShortDateString());
                    contents = contents.Replace("[TEKLIFNO]", teklif.TeklifNo);
                    contents = contents.Replace("[TEL]", teklif.Tel);
                    contents = contents.Replace("[GSM]", teklif.Gsm);
                    contents = contents.Replace("[FAX]", teklif.Fax);
                    contents = contents.Replace("[DURUM]", ((EnumTeklifTip)teklif.Durum).ToString());
                    contents = contents.Replace("[GECER]", teklif.GecerlilikSuresi.ToString());
                    contents = contents.Replace("[TESL]", teklif.TeslimatSuresi);


                    var logo = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~/Content/images/front/dnysn.jpg"));
                    logo.SetAbsolutePosition(30, 730);
                  

                    document.Add(logo);

                    StyleSheet styles = GetStyles();


                    var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), styles);
                    foreach (var htmlElement in parsedHtmlElements)
                        document.Add(htmlElement as IElement);
                    
                    writer.CloseStream = false;
                    document.Close();

                    var mset = MailManager.GetMailSettings();

                    using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                    {
                        client.EnableSsl = false;
                        client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                        var mail = new MailMessage();
                        mail.From = new MailAddress(mset.ServerMail);
                        mail.To.Add(teklif.Eposta);
                        mail.Subject = "Deneysan - Proforma Faturası";
                        mail.Body = "Proforma faturanız ekte bulunmaktadır.";
                        if (document != null)
                        {
                            output.Position = 0;
                            var attachment = new Attachment(output, "proforma-fatura.pdf");
                            mail.Attachments.Add(attachment);
                        }
                       // client.Send(mail);
                    }
                    
                    return output;

                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static string TurkceKarakter(string text)
        {
            text = text.Replace("İ", "\u0130");
            text = text.Replace("ı", "\u0131");
            text = text.Replace("Ş", "\u015e");
            text = text.Replace("ş", "\u015f");
            text = text.Replace("Ğ", "\u011e");
            text = text.Replace("ğ", "\u011f");
            text = text.Replace("Ö", "\u00d6");
            text = text.Replace("ö", "\u00f6");
            text = text.Replace("ç", "\u00e7");
            text = text.Replace("Ç", "\u00c7");
            text = text.Replace("ü", "\u00fc");
            text = text.Replace("Ü", "\u00dc");
            return text;
        }

        private static StyleSheet GetStyles()
        {
            StyleSheet styles = new StyleSheet();

            string[] elements = {"html","body","div","span","dsds","tbody", "tfoot", "thead", "tr", "th", "td"};

            foreach (var item in elements)
	        {
                styles.LoadTagStyle(item, "margin", "0");
                styles.LoadTagStyle(item, "padding", "0");
                styles.LoadTagStyle(item, "border", "0");
                styles.LoadTagStyle(item, "font-size", "100%");
                styles.LoadTagStyle(item, "font", "inherit");
                styles.LoadTagStyle(item, "vertical-align", "baseline");
	        }

            styles.LoadTagStyle("body", "line-height", "1");
            styles.LoadTagStyle("table", "border-collapse", "collapse");
            styles.LoadTagStyle("table", "border-spacing", "0");
            

            styles.LoadStyle("tnone", "border-top", "none !important");
            styles.LoadStyle("bnone", "border-bottom", "none !important");
            styles.LoadStyle("pbnone", "padding-bottom", "2px !important");
            styles.LoadStyle("ptnone", "padding-top", "2px !important");

            styles.LoadStyle("sagust", "float", "right");
            styles.LoadStyle("sagust", "background-color", "#ccc");
            styles.LoadStyle("sagust", "border", "1px solid #666");
            
            return new StyleSheet();
        }
    }


}
