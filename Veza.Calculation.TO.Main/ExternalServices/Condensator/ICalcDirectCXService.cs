using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Condensator
{
    public interface ICalcDirectCXService
    {
        /// <summary>
        /// прямой расчёт Конденсатор
        /// </summary>
        /// <returns></returns>
        Task<object> CalcDirectCX(InputDataCondensatorDTO dto);
    }
}
