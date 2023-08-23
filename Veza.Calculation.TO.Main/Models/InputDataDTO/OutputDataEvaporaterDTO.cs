namespace Veza.HeatExchanger.Models.Main
{
    public class OutputDataEvaporaterDTO
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
        /// Падение давления хладогента, бар
        /// </summary>
        public string O_DxCxBar { get; set; }
        /// <summary>
        /// Падение давления хладогента, К
        /// </summary>
        public string O_DxCxK { get; set; }
        /// <summary>
        /// Температура горячего газа
        /// </summary>
        public string O_THotGas { get; set; }
        /// <summary>
        /// Массовый расход хладогента
        /// </summary>
        public string O_DxCxMas { get; set; }
        /// <summary>
        /// Объём хладогента
        /// </summary>
        public string O_DxCxVol { get; set; }
        /// <summary>
        /// Объём
        /// </summary>
        public string O_Volume { get; set; }
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
