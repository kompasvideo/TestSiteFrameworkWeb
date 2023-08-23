using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.SteamHeater
{
    public interface ISetGeometrySTService
    {
        /// <summary>
        /// Установить новую геометрия и получить изменённые параметры
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<object> SetGeometryST(InputDataSteamHeaterDTO dto);
    }
}
