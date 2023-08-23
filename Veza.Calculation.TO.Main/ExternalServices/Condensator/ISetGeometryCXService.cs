using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Condensator
{
    public interface ISetGeometryCXService
    {
        /// <summary>
        /// Установить новую геометрия и получить изменённые параметры
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<object> SetGeometryCX(InputDataCondensatorDTO dto);
    }
}
