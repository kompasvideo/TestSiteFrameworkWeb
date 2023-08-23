namespace Veza.HeatExchanger.Models.MAKK
{
    internal class SearchEvaporator
    {
        /// <summary>
        /// расход компрессора
        /// </summary>
        public double MasFlowCompressor { get; set; }

        /// <summary>
        /// расход испарителя
        /// </summary>
        public double MasFlowEvaporater { get; set; }
        
        /// <summary>
        /// температура кипения испарителя
        /// </summary>
        public double I_TEvap { get; set; }

        /// <summary>
        /// Разница
        /// </summary>
        public double MasFlowDiff { get; set; }

    }
}
