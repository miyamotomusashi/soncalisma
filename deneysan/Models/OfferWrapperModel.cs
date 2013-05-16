using deneysan_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace deneysan.Models
{
    public class OfferWrapperModel
    {
        public List<Product> products { get; set; }
        public List<TeklifUrun> teklifurun { get; set; }
        public Teklif teklif { get; set; }

        public OfferWrapperModel(List<Product> products, List<TeklifUrun> teklifurun, Teklif teklif)
        {
            this.products = products;
            this.teklifurun = teklifurun;
            this.teklif = teklif;
        }
    }
}