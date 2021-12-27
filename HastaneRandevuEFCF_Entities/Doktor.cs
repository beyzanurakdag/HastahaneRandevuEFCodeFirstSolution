﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuEFCF_Entities
{
    [Table("Doktorlar")]
    public class Doktor:Kisi,IMaasAlabilir
    {
        //Data Annotations
        [Key] 
        [Column(Order =1)] 
        public int DoktorId { get; set; }     
        [Required(ErrorMessage ="Lütfen doktor adı giriniz!")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Doktor adı en az iki en çok elli karakter olmalıdır!")]
        [Display(Name ="Doktor Adı")]
        public string DoktorAdi { get; set; }
        [Column(TypeName ="nvarchar")]
        [Required(ErrorMessage = "Lütfen doktor soyadı giriniz!")]
        [StringLength(50)]
        [Display(Name = "Doktor Soyadı")]
        public string DoktorSoyadi { get; set; }
        [Display(Name = "Doktorun Branşı")]
        public Branslar Brans { get; set; }
        [Display(Name = "Maaş")]
        public decimal Maas { get; set; }
        public virtual List<RandevuBilgileri> RandevuBilgileriList { get; set; }
    }
}
