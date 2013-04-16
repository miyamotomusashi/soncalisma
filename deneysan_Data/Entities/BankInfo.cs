using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_DAL.Entities
{
    public class BankInfo
    {
        [Key]
        public int BankId { get; set; }
        [Display(Name="Banka Adı")]
        [Required(ErrorMessage="Banka Adını Girin.")]
        public string BankName { get; set; }

        [Display(Name = "Banka Logosu")]
        
        public string Logo { get; set; }
        [Display(Name = "Banka Hesap Numarası")]
        [Required(ErrorMessage = "Hesp numarasını Girin.")]
        public string BankNumber { get; set; }
        [Display(Name = "IBAN No")]
        
        public string IBAN { get; set; }
        public bool Online { get; set; }
        [Display(Name = "Dil")]
        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }


    }
}
