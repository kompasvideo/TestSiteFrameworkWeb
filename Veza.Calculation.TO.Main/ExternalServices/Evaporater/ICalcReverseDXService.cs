using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Evaporater
{
    public interface ICalcReverseDXService
    {
        /// <summary>
        /// обратный расчёт Испаритель
        /// </summary>
        /// <returns></returns>
        Task<object> CalcReverseDX(InputDataEvaporaterDTO dto);
    }
}
