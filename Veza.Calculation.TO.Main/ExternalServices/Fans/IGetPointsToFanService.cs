using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public interface IGetPointsToFanService
    {
        /// <summary>
        /// Получить таблицу скоростей для выбранного вентилятора
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<object> GetPoints(string name);
    }
}
