using Veza.HeatExchanger.DataBase.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public class SetPointsToFanService : ISetPointsToFanService
    {

        public SetPointsToFanService()
        {
        }

        /// <summary>
        /// Редактировать вентилятор 
        /// </summary>
        /// <param name="fan"></param>
        /// <returns></returns>
        public Task<object> SetPoints(List<FanPointsDB> fanPoints)
        {
            //calcTO.GetFanService().SetPoints(fanPoints);            
            return (Task<object>)Task.CompletedTask;
        }
    }
}
