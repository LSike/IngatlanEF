using IngatlanEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngatlanEF.Services
{
    internal class IngatlanService
    {
        public static List<Ingatlan> GetAllIngatlan() 
        {
            using (var context = new MiskolcingatlanContext())
            {
                try
                {
                    List<Ingatlan> result = context.Ingatlans.ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    List<Ingatlan> hibaLista = new List<Ingatlan>();
                    hibaLista.Add(new Ingatlan()
                    {
                        Id = -1,
                        Település = ex.Message
                    });
                    return hibaLista;
                }
            }
        }

        public static void IngatlanInsert(Ingatlan ujIngatlan)
        {
            using(var context = new MiskolcingatlanContext())
            {
                try
                {
                    context.Ingatlans.Add(ujIngatlan);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }
    }
}
