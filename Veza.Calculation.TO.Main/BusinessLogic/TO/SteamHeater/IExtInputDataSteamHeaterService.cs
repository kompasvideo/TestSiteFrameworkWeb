using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.BusinessLogic.TO.SteamHeater
{
    public interface IExtInputDataSteamHeaterService :IExtInputDataService
    {
        /// <summary>
        /// Получить входные параметры
        /// </summary>
        /// <returns></returns>
        GetDataSteamHeaterDTO GetProperties();

        /// <summary>
        /// Установить входные параметры
        /// </summary>
        /// <param name="inputParams"></param>
        void SetProperties(InputDataSteamHeaterDTO inputParams);
        /// <summary>
        /// Получение результатов расчёта
        /// </summary>
        /// <returns></returns>
        OutputDataSteamHeaterDTO GetResults();
    }
}
