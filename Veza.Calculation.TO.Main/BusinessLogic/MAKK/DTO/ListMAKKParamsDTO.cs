namespace Veza.HeatExchanger.BusinessLogic.MAKK.DTO
{
    public class ListMAKKParamsDTO
    {
        public int Id;
        /// <summary>
        /// Модель
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Теплообменник
        /// </summary>
        public string HeatExchanger { get; set; }
        /// <summary>
        /// Кол-во теплообменников
        /// </summary>
        public int HeatExchangerCount { get; set; }
        /// <summary>
        /// Вентилятор
        /// </summary>
        public string Fan { get; set; }
        /// <summary>
        /// Кол-во вентиляторов
        /// </summary>
        public int FanCount { get; set; }
        /// <summary>
        /// Компрессор
        /// </summary>
        public string Compressor { get; set; }
        /// <summary>
        /// Кол-во компрессоров
        /// </summary>
        public int CompressorCount { get; set; }
        /// <summary>
        /// Кол-во контуров
        /// </summary>
        public int NoOfCircuits { get; set; }
    }
}
