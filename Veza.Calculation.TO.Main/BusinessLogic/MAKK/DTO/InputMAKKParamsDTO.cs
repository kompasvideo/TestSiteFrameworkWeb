using System.Collections.Generic;
using Veza.HeatExchanger.BusinessLogic.MAKK.Models;

namespace Veza.HeatExchanger.BusinessLogic.MAKK.DTO
{
    public class InputMAKKParamsDTO
    {
        /// <summary>
        /// Список хладогентов
        /// </summary>
        public List<string> Refrigerants { get; set; }
        /// <summary>
        /// Выбранный хладогент
        /// </summary>
        public string SelectRefrigerant { get; set; }
        /// <summary>
        /// Список серий МАКК
        /// </summary>
        public List<SeriesMAKK> SeriesMAKKs { get; set; }
        /// <summary>
        /// Холодопроизводительность в кВт, типа string
        /// </summary>
        public string CoolingCapacity { get; set; }
        /// <summary>
        /// Погрешность в %, типа string
        /// </summary>
        public string ErrorRate { get; set; }
        /// <summary>
        /// Температура окружающей среды
        /// </summary>
        public string OutTemp { get; set; }
        /// <summary>
        /// Температура кипения
        /// </summary>
        public string EvapTemp { get; set; }
    }
}
