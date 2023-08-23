using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.SteamHeater
{
    public interface ICalcDirectSTService
    {
        /// <summary>
        /// прямой расчёт Паровой нагреватель
        /// </summary>
        /// <returns></returns>
        Task<object> CalcDirectST(InputDataSteamHeaterDTO dto);
    }
}
