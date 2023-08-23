using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.BusinessLogic.TO
{
    public interface IExtInputDataService
    {
        /// <summary>
        /// расчёт Теплообменника
        /// </summary>
        /// <param name="inputParams"></param>
        /// <returns></returns>
        bool Calc();

        /// <summary>
        /// Изменение геометрии
        /// </summary>
        /// <param name="geometry"></param>
        void SetGeometry(string geometry);

        /// <summary>
        /// Получение новых параметров всвязи с изменением геометрии
        /// </summary>
        /// <returns></returns>
        ChangesGeometryParamsTO GetChangesGeometryParams();
    }
}
