using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using deneysan_DAL.Entities;
using myBLOGData.Context;
namespace deneysan_DAL.Context
{
    public class DeneysanContext : DbContext
    {

        public DeneysanContext() : base("name=DeneysanContext") { }


        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<GalleryGroup> GalleryGroup { get; set; }
        public DbSet<Institutional> Institutional { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<References> References { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<DocumentGroup> DocumentGroup { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<BankInfo> BankInfo { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ImportantLinks> ImportantLinks { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<HumanResource> HumanResource { get; set; }
        public DbSet<Teklif> Teklif { get; set; }
        public DbSet<TeklifUrun> TeklifUrun { get; set; }
        public DbSet<MailSetting> MailSetting { get; set; }
        public DbSet<MailUsers> MailUsers { get; set; }
        public DbSet<OfferNumber> OfferNumber { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer(new DatabaseCreatorClass());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DeneysanContext, Configration>());


            modelBuilder.Entity<AdminUser>().ToTable("AdminUser");
            modelBuilder.Entity<Gallery>().ToTable("Gallery");
            modelBuilder.Entity<GalleryGroup>().ToTable("GalleryGroup");
            modelBuilder.Entity<Institutional>().ToTable("Institutional");
            modelBuilder.Entity<Languages>().ToTable("Languages");
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<References>().ToTable("References");
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<DocumentGroup>().ToTable("DocumentGroup");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<BankInfo>().ToTable("BankInfo");
            modelBuilder.Entity<ProductGroup>().ToTable("ProductGroup");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ImportantLinks>().ToTable("ImportantLinks");
            modelBuilder.Entity<Projects>().ToTable("Projects");
            modelBuilder.Entity<HumanResource>().ToTable("HumanResource");
            modelBuilder.Entity<Teklif>().ToTable("Teklif");
            modelBuilder.Entity<TeklifUrun>().ToTable("TeklifUrun");
            modelBuilder.Entity<MailUsers>().ToTable("MailUsers");
            modelBuilder.Entity<MailSetting>().ToTable("MailSetting");
            modelBuilder.Entity<OfferNumber>().ToTable("OfferNumber");
            
        }
    }
}