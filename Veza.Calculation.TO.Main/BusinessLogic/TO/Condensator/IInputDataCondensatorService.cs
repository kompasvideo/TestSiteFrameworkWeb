using Veza.HeatExchanger.BusinessLogic;
using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.Interfaces
{
    public interface IInputDataCondensatorService : ICalculateBaseService
    {
        /// <summary>
        /// Первичная установка свойств
        /// </summary>
        void SetPropertiesFirst();
        /// <summary>
        /// Установка входных данных специфичных для Конденсатора МАКК
        /// </summary>
        void SetPropertiesCX_MAKK();
    }
}
