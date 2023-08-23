using System.Collections.Generic;
using System.Threading.Tasks;
using Veza.HeatExchanger.DataBase.Models.FanAddEdit;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public interface ISetStepsToFanService
    {
        /// <summary>
        /// Сохранить таблицу "расход воздуха-мощность" для выбранного вентилятора
        /// </summary>
        /// <param name="fanSteps"></param>
        /// <returns></returns>
        Task<object> SetStepsToFan(List<FanStepEffPowerDB> fanSteps);
    }
}
