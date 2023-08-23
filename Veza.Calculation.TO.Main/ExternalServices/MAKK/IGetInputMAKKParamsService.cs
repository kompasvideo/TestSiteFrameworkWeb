using System.Collections.Generic;
using System.Threading.Tasks;

using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.MAKK
{
    public interface IGetInputMAKKParamsService
    {
        /// <summary>
        /// Получение входных параметров МАКК
        /// </summary>
        /// <returns></returns>
        Task<object> GetInputMAKKParams();
    }
}
