using Veza.HeatExchanger.BusinessLogic;
using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.Interfaces
{
    public interface IInputDataSteamHeaterService : ICalculateBaseService
    {
        /// <summary>
        /// Первичная установка свойств
        /// </summary>
        void SetPropertiesFirst();
    }
}
