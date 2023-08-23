using System.Collections.Generic;
using Veza.HeatExchanger.BusinessLogic.Compressors.Models;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.Models.MAKK
{
    public class OutputDataMAKK
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
        /// <summary>
        /// подобранные вентиляторы
        /// </summary>
        public List<FanOutDTO> Fans { get; set; }
        /// <summary>
        /// выбранный вентилятор
        /// </summary>
        public FanOutDTO SelectedFan { get; set; }
        #endregion

        /// <summary>
        /// параметры теплообменника - конденсатора
        /// </summary>
        public OutputDataCondensatorDTO OutputDataCondensator { get; set; }

        #region Компрессор

        /// <summary>
        /// Входные парметры компрессора
        /// </summary>
        public InputDataCompressors InputDataCompressor { get; set; }
        /// <summary>
        /// подобранный оптимальный компрессор
        /// </summary>
        public List<SelectCompressors> Compressors { get; set; }
        /// <summary>
        /// подобранные компрессоры
        /// </summary>
        public List<SelectCompressors> CompressorsOther { get; set; } 

        #endregion
    }
}
