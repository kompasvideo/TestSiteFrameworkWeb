using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.BusinessLogic.TO.Condensator
{
    public interface IExtInputDataCondensatorService : IExtInputDataService
    {
        /// <summary>
        /// Установить входные параметры
        /// </summary>
        /// <param name="inputParams"></param>
        void SetProperties(InputDataCondensatorDTO inputParams);
        /// <summary>
        /// Получение результатов расчёта
        /// </summary>
        /// <returns></returns>
        OutputDataCondensatorDTO GetResults();

        /// <summary>
        /// Получить входные параметры
        /// </summary>
        /// <returns></returns>
        GetDataCondensatorDTO GetProperties();
    }
}
