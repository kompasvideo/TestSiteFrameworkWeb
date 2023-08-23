using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Evaporater
{
    public interface ISetGeometryDXService
    {
        /// <summary>
        /// Установить новую геометрия и получить изменённые параметры
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<object> SetGeometryDX(InputDataEvaporaterDTO dto);
    }
}
