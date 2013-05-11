﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_DAL.Entities
{
    public class Teklif
    {
        public int TeklifId { get; set; }
        public string Kurum { get; set; }
        public string Unvan { get; set; }
        public string Adsoyad { get; set; }
        public string Gsm { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
       
        public string TeslimatSuresi { get; set; }
        public DateTime TeklifTarihi { get; set; }
        public DateTime CevapTarihi { get; set; }
        public string TeklifNo { get; set; }
        public int GecerlilikSuresi { get; set; }
        public bool Durum { get; set; }
        public decimal FaturaTutar { get; set; }
    
    }

}