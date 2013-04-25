using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_DAL.Entities
{
    public class HumanResource
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Dili Seçin")]
        [Display(Name="Dil")]
        public string Language { get; set; }
        
        [Display(Name = "İçerik")]
        public string Content { get; set; }
    }
}
