using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.MAKK
{
    public interface IGetResultCalcMAKKService
    {
        /// <summary>
        /// Расчёт МАКК
        /// </summary>
        /// <returns></returns>
        Task<object> GetResultCalcMAKK();
    }
}
