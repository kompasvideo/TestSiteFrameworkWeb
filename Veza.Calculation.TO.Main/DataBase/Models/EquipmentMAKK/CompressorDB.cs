using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Veza.HeatExchanger.DataBase.Models
{
    sealed public class CompressorDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Температура кипения
        /// </summary>
        public double I_TEvap { get; set; }

        /// <summary>
        /// Температура всасываемого газа
        /// </summary>
        public double I_TSucGas { get; set; }

        /// <summary>
        /// Перегрев всвс. газа
        /// </summary>
        public double I_TOvrH { get; set; }
        
        /// <summary>
        /// Температура конденсации
        /// </summary>
        public double I_TCond { get; set; }
        
        /// <summary>
        /// переохлаждение
        /// </summary>
        public double I_TSubC { get; set; }
        
        /// <summary>
        /// Температура жидкости
        /// </summary>
        public double LiquidTemp { get; set; }

        /// <summary>
        /// производитель
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Холодопроизводительность
        /// </summary>
        public double RefrigerationCapacity { get; set; }
        
        /// <summary>
        /// Массовый расход на всасывание
        /// </summary>
        public double MassFlow { get; set; }

        /// <summary>
        /// Напряжение/частота/кол-во полюсов
        /// </summary>
        public string Voltage { get; set; }

        /// <summary>
        /// Мощность, кВт
        /// </summary>
        public double PowerInput { get; set; }

        /// <summary>
        /// ток, А
        /// </summary>
        public double Current { get; set; }

        /// <summary>
        /// Холод. коэффициент
        /// </summary>
        public double COP { get; set; }

        /// <summary>
        /// Теплопроизводительность, кВт
        /// </summary>
        public double HeatRejection { get; set; }
    }
}
