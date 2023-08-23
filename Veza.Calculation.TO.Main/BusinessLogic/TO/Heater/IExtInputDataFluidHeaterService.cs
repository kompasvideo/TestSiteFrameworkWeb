using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.BusinessLogic.TO.Heater
{
    public interface IExtInputDataFluidHeaterService : IExtInputDataService
    {
        /// <summary>
        /// Получить входные параметры
        /// </summary>
        /// <returns></returns>
        GetDataFluidHeaterCoolerDTO GetProperties();

        /// <summary>
        /// Установить входные параметры
        /// </summary>
        /// <param name="inputParams"></param>
        void SetProperties(InputDataFluidHeaterCoolerDTO inputParams);

        /// <summary>
        /// Получение результатов расчёта
        /// </summary>
        /// <returns></returns>
        OutputDataFluidHeaterCoolerDTO GetResults();
    }
}
