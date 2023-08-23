using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Veza.HeatExchanger.DataBase.Interafces;

namespace Veza.HeatExchanger.DataBase
{
    public class Compressor : ICompressor
    {
        /// <summary>
        /// получить список компрессоров
        /// </summary>
        /// <returns></returns>
        public IList<string> GetCompressors()
        {
            IList<string> list = new List<string>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Compressors.Load();
                foreach (var compressor in db.Compressors.ToList())
                {
                    list.Add(compressor.Name);
                }
            }
            return list;
        }
    }
}
