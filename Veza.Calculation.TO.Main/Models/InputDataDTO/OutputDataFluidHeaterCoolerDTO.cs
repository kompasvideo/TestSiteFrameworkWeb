namespace Veza.HeatExchanger.Models.Main
{
    public class OutputDataFluidHeaterCoolerDTO
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
        /// Скорость жидкости
        /// </summary>
        public string MedVelo { get; set; }
        /// <summary>
        /// Перепад давления жидкости
        /// </summary>
        public string MedKPa { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public string AirTempOut { get; set; }
        /// <summary>
        /// Общая производительность
        /// </summary>
        public string O_TotCap { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
