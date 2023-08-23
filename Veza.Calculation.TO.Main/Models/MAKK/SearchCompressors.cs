namespace Veza.HeatExchanger.Models.MAKK
{
    internal class SearchCompressors
    {        
        /// <summary>
        /// температура конденсации
        /// </summary>
        public double I_TCond { get; set; }

        /// <summary>
        /// Расход компрессора
        /// </summary>
        public double MasFlowCompr { get; set; }

        /// <summary>
        /// Расход конденсатора
        /// </summary>
        public double MasFlowCond { get; set;}

        /// <summary>
        /// Разница
        /// </summary>
        public double MasFlowDiff { get; set;}
    }
}
