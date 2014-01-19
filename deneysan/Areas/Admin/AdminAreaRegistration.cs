using System.Web.Mvc;

namespace deneysan.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("loginpage", "yonetim/login", new { action = "Login", Controller = "Account" });
            context.MapRoute("logout", "cikis", new { action = "Logout", Controller = "Account" });
            context.MapRoute("homepage_default", "yonetim", new { action = "Index", Controller = "Home" });
            context.MapRoute("homepage", "yonetim/anasayfa", new { action = "Index", Controller = "Home" });
            //context.MapRoute("homepage_prm", "yonetim/{type}", new { action = "Index", Controller = "Home" });
            //context.MapRoute("homepage_all", "yonetim/teklif/tumteklifler", new { action = "AllList", Controller = "Home" });
            //context.MapRoute("homepage_detail", "yonetim/teklifdetay/{id}", new { action = "Details", Controller = "Home" });
            context.MapRoute("instituional_index", "yonetim/kurumsal", new { action = "Index", Controller = "Institutional" });
            context.MapRoute("instituional_mision_default", "yonetim/kurumsal/misyon", new { action = "Misyon", Controller = "Institutional" });
            context.MapRoute("instituional_vision_default", "yonetim/kurumsal/hakkimizda", new { action = "Vizyon", Controller = "Institutional" });
            context.MapRoute("instituional_vision", "yonetim/kurumsal/hakkimizda/{lang}", new { action = "Vizyon", Controller = "Institutional" });
            context.MapRoute("instituional_mision", "yonetim/kurumsal/misyon/{lang}", new { action = "Misyon", Controller = "Institutional" });

            //HABERLER
            context.MapRoute("news_default", "yonetim/haberler", new { action = "Index", Controller = "News" },null, new[] { "deneysan.Areas.Admin.Controllers" });
            context.MapRoute("news", "yonetim/haberler/{lang}", new { action = "Index", Controller = "News" });
            context.MapRoute("newsadd", "yonetim/haberekle", new { action = "AddNews", Controller = "News" });
            context.MapRoute("newsedit", "yonetim/haberduzenle/{id}", new { action = "EditNews", Controller = "News" });

            //PROJECTS
            context.MapRoute("project_default", "yonetim/projeler", new { action = "Index", Controller = "Project" });
            context.MapRoute("project", "yonetim/projeler/{lang}", new { action = "Index", Controller = "Project" });
            context.MapRoute("projectadd", "yonetim/projeekle", new { action = "AddProject", Controller = "Project" });
            context.MapRoute("projectedit", "yonetim/projeduzenle/{id}", new { action = "EditProject", Controller = "Project" });


            //TEKLİFLER
         //   context.MapRoute("teklif_default", "yonetim/tumteklifler", new { action = "Index", Controller = "Teklif" });
            context.MapRoute("teklif_yeni", "yonetim/teklifler/yeniteklif", new { action = "Add", Controller = "Teklif" });
            context.MapRoute("teklif_yeni2", "yonetim/teklifler/yeniteklif/{lang}", new { action = "Add", Controller = "Teklif" });
            context.MapRoute("teklif", "yonetim/teklifler/{type}", new { action = "Index", Controller = "Teklif" });
            context.MapRoute("teklif_detail", "yonetim/teklifler/detay/{id}", new { action = "Details", Controller = "Teklif" });
            context.MapRoute("teklif_sil", "yonetim/teklifler/sil/{id}/{type}", new { action = "Delete", Controller = "Teklif" });
           
            //LİnKLER
            context.MapRoute("link_default", "yonetim/linkler", new { action = "Index", Controller = "Link" });
            context.MapRoute("linkadd", "yonetim/linkekle", new { action = "AddLink", Controller = "Link" });
            context.MapRoute("linkedit", "yonetim/linkduzenle/{id}", new { action = "EditLink", Controller = "Link" });
            context.MapRoute("links", "yonetim/linkler/{lang}", new { action = "Index", Controller = "Link" });


            //REFERANSLAR
            context.MapRoute("references_default", "yonetim/referanslar", new { action = "Index", Controller = "Reference" });
            context.MapRoute("references", "yonetim/referanslar/{lang}", new { action = "Index", Controller = "Reference" });
            context.MapRoute("referenceadd", "yonetim/referansekle", new { action = "AddReference", Controller = "Reference" });
            context.MapRoute("referenceedit", "yonetim/referansduzenle/{id}", new { action = "EditReference", Controller = "Reference" });

            //MAİL KULLANICILARI
            context.MapRoute("mailuser_def", "yonetim/mailkullanicilari", new { action = "Index", Controller = "Mail" });
            context.MapRoute("mailuser", "yonetim/mailkullanicilari/{type}", new { action = "Index", Controller = "Mail" });
            context.MapRoute("mailuser_add", "yonetim/ekle", new { action = "Add", Controller = "Mail" });
            context.MapRoute("mailuser_edit", "yonetim/duzenle/{id}", new { action = "Edit", Controller = "Mail" });
            context.MapRoute("mail_setting", "yonetim/mailayarlari", new { action = "MailSetting", Controller = "Mail" });


            //DÖKÜMANLAR
            context.MapRoute("documents", "yonetim/dokumanlar", new { action = "Index", Controller = "Documents" });
            context.MapRoute("documentsgroups_default", "yonetim/dokumangruplari", new { action = "Index", Controller = "DocumentGroup" });
            context.MapRoute("documentsgroups", "yonetim/dokumangruplari/{lang}", new { action = "Index", Controller = "DocumentGroup" });
            context.MapRoute("adddocument", "yonetim/dokumanekle", new { action = "AddDocument", Controller = "Documents" });
            context.MapRoute("editdocument", "yonetim/dokumanduzenle/{id}", new { action = "EditDocument", Controller = "Documents" });
            context.MapRoute("documentlist_default", "yonetim/dokumanlistesi", new { action = "Index", Controller = "Documents" }, null, new[] { "deneysan.Areas.Admin.Controllers" });
            context.MapRoute("documentlist", "yonetim/dokumanlistesi/{lang}", new { action = "Index", Controller = "Documents" });
            context.MapRoute("documentlist_twoparam", "yonetim/dokumanlistesi/{lang}/{id}", new { action = "Index", Controller = "Documents" });


            //ÜRÜNLER
            context.MapRoute("products", "yonetim/urunler", new { action = "Index", Controller = "Product" });
            context.MapRoute("productsgroups_default", "yonetim/urungruplari", new { action = "Index", Controller = "ProductGroup" });
            context.MapRoute("productsgroups_edit", "yonetim/urungrubuduzenle/{id}", new { action = "EdtiGroup", Controller = "ProductGroup" });
            context.MapRoute("productsgroups", "yonetim/urungruplari/{lang}", new { action = "Index", Controller = "ProductGroup" });
            context.MapRoute("addproducts", "yonetim/urunekle", new { action = "AddProduct", Controller = "Product" });
            context.MapRoute("editproducts", "yonetim/urunduzenle/{id}", new { action = "EditProduct", Controller = "Product" });
            context.MapRoute("productslist_default", "yonetim/urunlistesi", new { action = "Index", Controller = "Product" }, null, new[] { "deneysan.Areas.Admin.Controllers" });
            context.MapRoute("productslist", "yonetim/urunlistesi/{lang}", new { action = "Index", Controller = "Product" });
            context.MapRoute("productslist_twoparam", "yonetim/urunlistesi/{lang}/{id}", new { action = "Index", Controller = "Product" });

            //RESİM GALERİ
            context.MapRoute("gallerygroup", "yonetim/galeriler", new { action = "Index", Controller = "Gallery" });
            context.MapRoute("gallerygroups_default", "yonetim/galerigruplari", new { action = "Index", Controller = "GalleryGroup" });
            context.MapRoute("gallerygroups", "yonetim/galerigruplari/{lang}", new { action = "Index", Controller = "GalleryGroup" });
            context.MapRoute("galleryimageadd", "yonetim/resimekle", new { action = "AddImage", Controller = "Gallery" });
            context.MapRoute("gallerylist", "yonetim/galeriresimleri", new { action = "GalleryList", Controller = "Gallery" });


            //BANKA BİLGİLERİ
            context.MapRoute("bank_default", "yonetim/bankabilgileri", new { action = "Index", Controller = "Bank" });
            context.MapRoute("bank", "yonetim/bankabilgileri/{lang}", new { action = "Index", Controller = "Bank" });
            context.MapRoute("bankadd", "yonetim/bankabilgisiekle", new { action = "AddBank", Controller = "Bank" });
            context.MapRoute("bankedit", "yonetim/bankabilgisiduzenle/{id}", new { action = "EditBank", Controller = "Bank" });


            context.MapRoute("contact_default", "yonetim/iletisim", new { action = "Index", Controller = "Contact" });
            context.MapRoute("contact", "yonetim/iletisim/{lang}", new { action = "Index", Controller = "Contact" });


            context.MapRoute("ik_index", "yonetim/insankaynaklari", new { action = "Index", Controller = "HumanResource" });
            context.MapRoute("ik_mision_default", "yonetim/insankaynaklari/{lang}", new { action = "Index", Controller = "HumanResource" });
            




            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
