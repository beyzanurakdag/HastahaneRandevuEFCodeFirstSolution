﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HastaneRandevuEFCF_Entities;

namespace HastahaneRandevuEFCF_DAL
{
    public class MyContext:DbContext
    {
        public MyContext()
            :base("name=MyCon")
        {
            
        }
        //tablolarımızı entities'den alacağız.
        public virtual DbSet<Doktor> Doktorlar { get; set; }
        public virtual DbSet<Hasta> Hastalar { get; set; }
        public virtual DbSet<RandevuBilgileri> RandevuBilgileri { get; set; }
    }
}
