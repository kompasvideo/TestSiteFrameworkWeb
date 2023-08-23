using System.Windows;
using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.BusinessLogic
{
    public interface ICalculateBaseService
    {
        Visibility? WindowCalcV { get; set; }
        /// <summary>
        /// Запуск прямого расчёта
        /// </summary>
        /// <param name="returnPage"></param>
        void CalcDirect(string returnPage);
        /// <summary>
        /// Установка типа расчёта - воздухонагреватель, воздухоохладитель, паровой нагреватель, конденсатор, испаритель
        /// </summary>
        /// <param name="calcMode"></param>
        void CalcReverse(string returnPage);
        void CalcTable();
        /// <summary>
        /// Получение из производственного кода параметров
        /// </summary>
        /// <param name="saveCodes"></param>
        /// <returns></returns>
        bool Decoder(SaveCodes saveCodes);
        /// <summary>
        /// Установка свойств
        /// </summary>
        void SetPropertiesChangeLang();
        void SetCalcMode(string calcMode);
        int GetResult();
        void CalculationInit();
        string GetErrorStr();

        /// <summary>
        /// Добавить расчёт по массову расходу хладогента
        /// </summary>
        void AddCalcMassFlow();

        /// <summary>
        /// Удалить расчёт по массову расходу хладогента
        /// </summary>
        void DelCalcMassFlow();
    }
}
