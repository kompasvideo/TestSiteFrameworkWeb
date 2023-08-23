using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Condensator
{
    public interface ICalcReverseCXService
    {
        /// <summary>
        /// обратный расчёт Конденсатор
        /// </summary>
        /// <returns></returns>
        Task<object> CalcReverseCX(InputDataCondensatorDTO dto);
    }
}
