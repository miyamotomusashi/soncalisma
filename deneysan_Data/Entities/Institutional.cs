using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_DAL.Entities
{
    public class Institutional
    {
        public int InstitutionalId{get;set;}
        [DisplayName("İçerik")]
        public string Content { get; set; }
        public DateTime TimeUpdated{get;set;}
        public string Language { get; set; }
        public int TypeId{get;set;}
    }
}
