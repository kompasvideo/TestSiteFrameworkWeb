using Veza.HeatExchanger.BusinessLogic.MAKK.DTO;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.MAKK
{
    public interface ISetInputMAKKParamsService
    {
        /// <summary>
        /// Изменение входных параметров МАКК
        /// </summary>
        /// <returns></returns>
        Task<object> SetInputMAKKParams(InputMAKKParamsDTO inputMAKKParams);
    }
}
