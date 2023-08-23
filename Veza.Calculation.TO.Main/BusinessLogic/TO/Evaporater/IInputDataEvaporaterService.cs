using Veza.HeatExchanger.BusinessLogic;
using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.Interfaces
{
    public interface IInputDataEvaporaterService : ICalculateBaseService
    {
        void SetPropToPrint();

        /// <summary>
        /// Установка свойств специфичных для теплоохладителя
        /// </summary>
        void SetPropertiesDX();

        /// <summary>
        /// Первичная установка свойств
        /// </summary>
        void SetPropertiesFirst();
    }
}
