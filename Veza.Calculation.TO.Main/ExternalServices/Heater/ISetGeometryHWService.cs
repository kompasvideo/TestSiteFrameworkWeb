using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Heater
{
    public interface ISetGeometryHWService
    {
        /// <summary>
        /// Установить новую геометрия и получить изменённые параметры
        /// </summary>
        /// <param name="inputParams"></param>
        /// <returns></returns>
        Task<object> SetGeometryHW(InputDataFluidHeaterCoolerDTO inputParams);
    }
}
