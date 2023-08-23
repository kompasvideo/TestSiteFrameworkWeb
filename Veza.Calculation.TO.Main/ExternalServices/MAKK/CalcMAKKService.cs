using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.MAKK
{
    public class CalcMAKKService : ICalcMAKKService
    {

        public CalcMAKKService()
        {
        }

        /// <summary>
        /// Расчёт МАКК
        /// </summary>
        /// <returns></returns>
        public Task<object> CalcMAKK()
        {
             //calcTO.GetMAKKService().CalcMAKK();
             return (Task<object>)Task.CompletedTask;
        }
    }
}
