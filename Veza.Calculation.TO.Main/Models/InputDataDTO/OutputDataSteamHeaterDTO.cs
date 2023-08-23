namespace Veza.HeatExchanger.Models.Main
{
    public class OutputDataSteamHeaterDTO
    {
        /// <summary>
        /// Геометрия
        /// </summary>
        public string I_Geo { get; set; }
        /// <summary>
        /// Производственный код
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// Число рядов
        /// </summary>
        public string NoOfRows { get; set; }
        /// <summary>
        /// Количество отводов
        /// </summary>
        public string Circuits { get; set; }
        /// <summary>
        /// Скорость воздуха
        /// </summary>
        public string AirVelocity { get; set; }
        /// <summary>
        /// Перепад давления сухой воздуха
        /// </summary>
        public string PresDropDry { get; set; }
        /// <summary>
        /// Резервная нагрузка
        /// </summary>
        public string ReverseLoad { get; set; }
        /// <summary>
        /// Давление пара
        /// </summary>
        public string O_StmBar { get; set; }
        /// <summary>
        /// Объём пара
        /// </summary>
        public string O_StmFlow { get; set; }
        /// <summary>
        /// Скрытая теплота пара
        /// </summary>
        public string O_StmHeat { get; set; }
        /// <summary>
        /// Скорость движения пара
        /// </summary>
        public string O_StmVelo { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public string AirTempOut { get; set; }
        /// <summary>
        /// Общая производительность
        /// </summary>
        public string O_TotCap { get; set; }
    }
}
