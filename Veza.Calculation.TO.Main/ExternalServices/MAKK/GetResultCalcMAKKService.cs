using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.MAKK
{
    public class GetResultCalcMAKKService : IGetResultCalcMAKKService
    {

        public GetResultCalcMAKKService()
        {
        }

        /// <summary>
        /// Возврат результатов расчёта МАКК
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetResultCalcMAKK()
        {
            //return await Task.Run(() =>
            //{
            //    return calcTO.GetMAKKService().GetResultCalcMAKK();
            //});            
            object ret = null;
            return await Task.Run(() => ret);
        }
    }
}
