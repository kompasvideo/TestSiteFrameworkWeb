using Veza.HeatExchanger.BusinessLogic.Compressors.Models;
using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.Models.MAKK
{
    public class InputDataMAKK
    {
        #region Вентилятор

        /// <summary>
        /// Запас вентилятора по расходу воздуха
        /// </summary>
        public double FanReserveAirFlow { get; set; }
        /// <summary>
        /// Количество вентиляторов
        /// </summary>
        public int NumberOfFans { get; set; }
        #endregion

        /// <summary>
        /// параметры теплообменника - конденсатора
        /// </summary>
        public InputDataCondensatorDTO InputParams { get; set; }

        #region Компрессор
        /// <summary>
        /// Входные парметры компрессора
        /// </summary>
        public InputDataCompressors InputDataCompressor { get; set; }
        #endregion
    }
}
