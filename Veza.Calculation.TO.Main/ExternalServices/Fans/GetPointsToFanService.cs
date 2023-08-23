using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veza.HeatExchanger.DataBase.Models;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public class GetPointsToFanService : IGetPointsToFanService
    {

        public GetPointsToFanService()
        {
        }

        /// <summary>
        /// Получить таблицу скоростей для выбранного вентилятора
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<object> GetPoints(string name)
        {
            //return await Task.Run(() =>
            //{
            //    return calcTO.GetFanService().GetPoints(name);
            //});
            object ret = null;
            return await Task.Run(() => ret);
        }
    }
}
