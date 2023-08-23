using System.Collections.Generic;

namespace Veza.HeatExchanger.DataBase.Models
{
    sealed public class FanOutDTO
    {
        /// <summary>
        /// Наименование 
        /// </summary>
        public string Model { get; set; } 
        /// <summary>
        /// Напряжение
        /// </summary>
        public float Voltage { get; set; }
        /// <summary>
        /// Соединение обмотки двигателя
        /// 1 - Y 
        /// 2 - Δ
        /// </summary>
        public string ConnectionOfMotor { get; set; }
        /// <summary>
        /// Размер (мм) 
        /// </summary>
        public float Size1 { get; set; }
        /// <summary>
        /// Количество полюсов
        /// </summary>
        public byte MotorPoles { get; set; }
        /// <summary>
        /// Направление
        /// </summary>
        public string  Direction { get; set; }
        /// <summary>
        /// Скорость
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// Мощность
        /// </summary>
        public float Power { get; set; }
        /// <summary>
        /// Ток
        /// </summary>
        public float Current { get; set; }
        /// <summary>
        /// Уровень шума
        /// </summary>
        public byte NoiseLp { get; set; }
        /// <summary>
        /// Минимальный объём
        /// </summary>
        public float AirFlowMin { get; set; }
        /// <summary>
        /// Максимальный объём
        /// </summary>
        public float AirFlowMax { get; set; }
        /// <summary>
        /// Impeller material Id
        /// </summary>
        public string Materials { get; set; }
        /// <summary>
        /// Количество лопастей
        /// </summary>
        public byte NBlades { get; set; }
        /// <summary>
        /// IP (влагозащита, пылезащита)
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// Вид для монтажа (2 - квадрат, 1 - круг)
        /// </summary>
        public int MountId { get; set; }
        public string Mount { get; set; }
        /// <summary>
        /// Размер для монтажа
        /// </summary>
        public int MountSize { get; set; }
        /// <summary>
        /// Отверстия
        /// </summary>
        public string Holes { get; set; }
        /// <summary>
        /// Масса (кг)
        /// </summary>
        public float Weight { get; set; }
        /// <summary>
        /// A Static pressure, Pa 
        /// </summary>
        public double AStatPres { get; set; }
        /// <summary>
        /// B Static pressure, Pa 
        /// </summary>
        public double BStatPres { get; set; }
        /// <summary>
        /// C Static pressure, Pa 
        /// </summary>
        public double CStatPres { get; set; }
        /// <summary>
        /// A Total pressure, Pa 
        /// </summary>
        public double ATotalPres { get; set; }
        /// <summary>
        /// B Total pressure, Pa 
        /// </summary>
        public double BTotalPres { get; set; }
        /// <summary>
        /// C Total pressure, Pa 
        /// </summary>
        public double CTotalPres { get; set; }
        /// <summary>
        /// A Eff factor, %
        /// </summary>
        public double AEffFactor { get; set; }
        /// <summary>
        /// B Eff factor, %
        /// </summary>
        public double BEffFactor { get; set; }
        /// <summary>
        /// C Eff factor, %
        /// </summary>
        public double CEffFactor { get; set; }
        /// <summary>
        /// A Power input, W
        /// </summary>
        public double APowerInput { get; set; }
        /// <summary>
        /// B Power input, W
        /// </summary>
        public double BPowerInput { get; set; }
        /// <summary>
        /// C Power input, W
        /// </summary>
        public double CPowerInput { get; set; }

        /// <summary>
        /// Тип 
        /// </summary>
        public string SelectedTipology { get; set; }
        /// <summary>
        /// Список типов
        /// </summary>
        public List<string> Tipology { get; set; }
        /// <summary>
        /// Выбранный производитель
        /// </summary>
        public string SelectedBuilder { get; set; }
        /// <summary>
        /// Выбранная серия
        /// </summary>
        public string SelectedSeries { get; set; }
        /// <summary>
        /// Список всех производителей
        /// </summary>
        public List<string> Builders { get;set; }
        /// <summary>
        /// Список всех серий
        /// </summary>
        public List<string> Series { get; set; }
    }
}
