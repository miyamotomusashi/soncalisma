using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_DAL.Entities
{
    public class Contact
    {
         public int ContactId { get; set; }
        [Display(Name="Adres")]
        [Required(ErrorMessage = "Adres Bilgisini Giriniz")]
        public string Address { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Faks")]
        public string Fax { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Bilgisini Giriniz")]
        
        public string Email { get; set; }
        [Display(Name = "Vergi Dairesi")]
        public string Taxoffice { get; set; }
        [Display(Name = "Vergi Numarası")]
        public string Taxnumber { get; set; }
        
        [Required(ErrorMessage = "Dili Giriniz")]
        public string Language { get; set; }

    }
}
