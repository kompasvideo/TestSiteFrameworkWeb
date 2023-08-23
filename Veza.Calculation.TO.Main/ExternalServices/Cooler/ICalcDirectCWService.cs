using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Cooler
{
    public interface ICalcDirectCWService
    {
        /// <summary>
        /// прямой расчёт Воздухоохладителя
        /// </summary>
        /// <returns></returns>
        Task<object> CalcDirectCW(InputDataFluidHeaterCoolerDTO dto);
    }
}
