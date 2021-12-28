using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastahaneRandevuEFCF_DAL;
using HastaneRandevuEFCF_Entities;

namespace HastahaneRandevuEFCF_BLL
{
    public class HastaManager
    {
        //Global Alan
        MyContext myDbContext = new MyContext();
        public List<Hasta> TumHastalariGetir()
        {
            try
            {
                return myDbContext.Hastalar.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Hasta> TcNumarasinaGoreHastayiBul(string tc)
        {
            try
            {
                return myDbContext.Hastalar.Where(x => x.TCNumarasi.StartsWith(tc) || x.TCNumarasi.EndsWith(tc)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
