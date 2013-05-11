using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_DAL.Entities
{
    public class TeklifUrun
    {
        public int TeklifUrunId { get; set; }
        public int TeklifId { get; set; }
        public string UrunId { get; set; }
        public decimal Fiyat { get; set; }
        public int Adet { get; set; }
        public string Toplam { get; set; }
      
    }
}
