using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_DAL.Entities
{
    public class ImportantLinks
    {
        [Key]
        public int LinkId { get; set; }
        [Required(ErrorMessage="Link İsmini Giriniz.")]
        [Display(Name="Link Adı")]
        public string LinkName { get; set; }
        [Display(Name = "Link URL")]
        [Required(ErrorMessage = "Link Url'sini Giriniz.")]
        [Url(ErrorMessage = "Url formatı doğru değil.")]
        public string LinkUrl { get; set; }
        public int SortNumber { get; set; }
        public bool Online { get; set; }
        [Display(Name = "Dil")]
         [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }

    }
}
