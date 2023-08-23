using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.MAKK
{
    public class GetInputMAKKParamsService : IGetInputMAKKParamsService
    {

        public GetInputMAKKParamsService()
        {
        }

        /// <summary>
        /// Получение входных параметров МАКК
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetInputMAKKParams()
        {
            //return await Task.Run(() =>
            //{
            //    return calcTO.GetMAKKService().GetInputMAKKParams();
            //});
            object ret = null;
            return await Task.Run(() => ret);
        }
    }
}
