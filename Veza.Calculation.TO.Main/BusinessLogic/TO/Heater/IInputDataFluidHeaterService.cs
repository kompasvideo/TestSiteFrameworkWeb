using Veza.HeatExchanger.BusinessLogic;
using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.Interfaces
{
    public interface IInputDataFluidHeaterService : ICalculateBaseService
    {        
        /// <summary>
        /// Первичная установка свойств
        /// </summary>
        void SetPropertiesFirst();
    }
}
