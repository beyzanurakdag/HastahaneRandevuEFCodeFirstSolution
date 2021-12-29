using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastahaneRandevuEFCF_DAL;
using HastaneRandevuEFCF_Entities;

namespace HastahaneRandevuEFCF_BLL
{
    public class RandevuManager
    {
        //Global Alan
        MyContext myDBContext = new MyContext();
        public List<RandevuBilgileri> DoktorunRandevulariniTariheGoreGetir(Doktor dr, DateTime trh)
        {
            try
            {
                List<RandevuBilgileri> rndList = new List<RandevuBilgileri>();
                rndList = myDBContext.RandevuBilgileri.Where(x => x.DoktorId == dr.DoktorId).ToList();
                //Burada foreach kullanmadık çünkü foreach döngüzünde koleksiyon bozulamaz.Oysa biz burada tarihi eşleşmeyen randevuları listemizden sileceğiz.
                for (int i = 0; i < rndList.Count; i++)
                {
                    if (rndList[i].RandevuTarihi.ToShortDateString() != trh.ToShortDateString()) 
                    {
                        rndList.Remove(rndList[i]);
                    }
                }
                return rndList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool HastaninSecilenTarihveSaatteRandevusuVarMi(Hasta hasta, DateTime trh)
        {
            try
            {
                bool sonuc = false;
                var liste = myDBContext.RandevuBilgileri.Where(x => x.HastaId == hasta.HastaId && x.RandevuTarihi == trh).ToList();

                sonuc = liste.Count > 0 ? true : false;
                return sonuc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RandevuyuAl(RandevuBilgileri rnd)
        {
            try
            {
                myDBContext.RandevuBilgileri.Add(rnd);
                if(myDBContext.SaveChanges()>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
