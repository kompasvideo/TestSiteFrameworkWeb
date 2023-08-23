using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.BusinessLogic.TO.Evaporater
{
    public interface IExtInputDataEvaporaterService : IExtInputDataService
    {
        /// <summary>
        /// Получить входные параметры
        /// </summary>
        /// <returns></returns>
        GetDataEvaporaterDTO GetProperties();

        /// <summary>
        /// Установить входные параметры
        /// </summary>
        /// <param name="inputParams"></param>
        void SetProperties(InputDataEvaporaterDTO inputParams);
        /// <summary>
        /// Получение результатов расчёта
        /// </summary>
        /// <returns></returns>
        OutputDataEvaporaterDTO GetResults();
    }
}
