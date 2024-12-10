using IngatlanEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngatlanEF.Services
{
    internal class UgyintezoService
    {
        public static List<Ugyintezo> GetAllUgyintezo()
        {
            using (var context = new MiskolcingatlanContext())
            {
                try
                {
                    var result = context.Ugyintezos.ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    List<Ugyintezo> hibalista = new List<Ugyintezo>();
                    hibalista.Add(new Ugyintezo()
                    {
                        Id = -1,
                        Nev = ex.Message
                    });
                    return hibalista;
                    
                }
            }
        }
    }
}
