namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// Режим расчёта
    /// </summary>
    public enum CalcMode
    {
        /// <summary>
        /// Режим расчёта - воздухонагреватель
        /// </summary>
        Heater,
        /// <summary>
        /// Режим расчёта - воздухоохладитель
        /// </summary>
        Cooler,
        /// <summary>
        /// Режим расчёта - паровой нагреватель
        /// </summary>
        SteamHeater,
        /// <summary>
        /// Режим расчёта - конденсатор
        /// </summary>
        Condensator,
        /// <summary>
        /// Режим расчёта - испаритель
        /// </summary>
        Evaporater,
    }
}
