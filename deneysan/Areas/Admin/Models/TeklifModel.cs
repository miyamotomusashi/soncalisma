using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using deneysan_BLL.TeklifBL;
using deneysan_DAL.Entities;

namespace deneysan.Areas.Admin.Models
{
    public class TeklifModel
    {
       
            public Teklif teklif { get; set; }
            public IEnumerable<TeklifUrun_Urun> teklifurun { get; set; }

            public TeklifModel(Teklif teklif, IEnumerable<TeklifUrun_Urun> teklifurun)
            {
                this.teklif = teklif;
                this.teklifurun = teklifurun;
            }
        
    }
}