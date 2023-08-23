using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.SteamHeater
{
    public interface IGetPropertiesSTService
    {
        /// <summary>
        /// Получение списка входных параметров
        /// </summary>
        Task<object> GetPropertiesST();
    }
}
