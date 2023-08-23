using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Heater
{
    public interface IGetPropertiesHWService
    {
        /// <summary>
        /// Получение списка входных параметров
        /// </summary>
        GetDataFluidHeaterCoolerDTO GetPropertiesHW();
        //object GetPropertiesHW();
    }
}
