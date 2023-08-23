using System.Collections.ObjectModel;
using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// Класс с параметрами, которые передаются при переключении страницы
    /// </summary>
    sealed public class OutParams
    {
        /// <summary>
        /// Тип расчёта - Воздухнагреватель, воздухохладитель, паровой нагреватель и ...
        /// </summary>
        public CalcMode CalcMode { get; set; }
        /// <summary>
        /// Страница возврата
        /// </summary>
        public string ReturnPage { get; set; }

        /// <summary>
        /// Строка с ошибкой
        /// </summary>
        public string ErrorStr { get; set; }

        /// <summary>
        /// Видимость кнопки Сохранить в табличном расчёте
        /// </summary>
        public bool SaveVisibility { get; set; }
        /// <summary>
        /// Страница печати
        /// </summary>
        public PrintEnum PrintEnum { get; set; }
        /// <summary>
        /// Изменился ли язык
        /// </summary>
        public bool ChangeLang { get; set; }
        /// <summary>
        /// ошибка при расчёте
        /// </summary>
        public bool CalcError { get; set; }
    }
}
