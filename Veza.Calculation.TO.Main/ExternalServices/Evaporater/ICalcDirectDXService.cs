using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Evaporater
{
    public interface ICalcDirectDXService
    {
        /// <summary>
        /// прямой расчёт Испаритель
        /// </summary>
        /// <returns></returns>
        Task<object> CalcDirectDX(InputDataEvaporaterDTO dto);
    }
}
