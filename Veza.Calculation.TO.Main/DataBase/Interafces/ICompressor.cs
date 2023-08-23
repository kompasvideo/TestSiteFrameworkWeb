using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veza.HeatExchanger.DataBase.Interafces
{
    public interface ICompressor
    {
        /// <summary>
        /// получить список компрессоров
        /// </summary>
        /// <returns></returns>
        IList<string> GetCompressors();
    }
}
