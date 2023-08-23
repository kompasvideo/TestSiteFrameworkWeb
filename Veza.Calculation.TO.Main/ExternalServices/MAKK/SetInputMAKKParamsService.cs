using Veza.HeatExchanger.BusinessLogic.MAKK.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.MAKK
{
    public class SetInputMAKKParamsService : ISetInputMAKKParamsService
    {
        public SetInputMAKKParamsService()
        {
        }

        /// <summary>
        /// Изменение входных параметров МАКК
        /// </summary>
        /// <returns></returns>
        public async Task<object> SetInputMAKKParams(InputMAKKParamsDTO inputMAKKParams)
        {
            //return await Task.Run(() =>
            //{
            //    return calcTO.GetMAKKService().SetInputMAKKParams(inputMAKKParams);
            //});
            object ret = null;
            return await Task.Run(() => ret);
        }
    }
}
