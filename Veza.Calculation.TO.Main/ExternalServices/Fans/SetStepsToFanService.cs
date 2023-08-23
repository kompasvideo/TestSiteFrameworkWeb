using Veza.HeatExchanger.DataBase.Models.FanAddEdit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public class SetStepsToFanService : ISetStepsToFanService
    {

        public SetStepsToFanService()
        {
        }

        /// <summary>
        /// Сохранить таблицу "расход воздуха-мощность" для выбранного вентилятора
        /// </summary>
        /// <param name="fanSteps"></param>
        /// <returns></returns>
        public async Task<object> SetStepsToFan(List<FanStepEffPowerDB> fanSteps)
        {
            return await Task.Run(() =>
            {
                //calcTO.GetFanService().SetSteps(fanSteps);
                return (Task<object>)Task.CompletedTask;
            });
        }
    }
}
