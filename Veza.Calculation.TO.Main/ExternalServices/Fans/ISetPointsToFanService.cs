using System.Collections.Generic;
using System.Threading.Tasks;
using Veza.HeatExchanger.DataBase.Models;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public interface ISetPointsToFanService
    {
        /// <summary>
        /// Сохранить отредактированную таблицу скоростей для выбранного вентилятора
        /// </summary>
        /// <param name="fanPoints"></param>
        Task<object> SetPoints(List<FanPointsDB> fanPoints);
    }
}
