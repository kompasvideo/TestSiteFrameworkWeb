using System.Collections.Generic;

namespace Veza.HeatExchanger.BusinessLogic.MAKK.DTO
{
    public class MAKKParamsDTO
    {
        public int Id;
        /// <summary>
        /// Модель
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Серия
        /// </summary>
        public string Seria { get; set; }
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
        /// <summary>
        /// Суммарный объём ресиверов
        /// </summary>
        public double TotalVolumeReceivers { get; set; }
        /// <summary>
        /// Общая потребляемая мощность / 
        /// Total absorbed power
        /// </summary>
        public double TotalAbsorbedPower { get; set; }
        /// <summary>
        /// Рабочий ток
        /// </summary>
        public double TotalOperatingCurrent { get; set; }
        /// <summary>
        /// Максимальный рабочий ток
        /// </summary>
        public double MaximumOperatingCurrent { get; set; }
        /// <summary>
        /// пусковой ток
        /// </summary>
        public double LRA { get; set; }
        /// <summary>
        /// диаметр жидкостной трубы
        /// </summary>
        public string LiquidTubeDiameter { get; set; }
        /// <summary>
        /// диаметр газовой трубы
        /// </summary>
        public string GasTubeDiameter { get; set; }
        /// <summary>
        /// Длина
        /// </summary>
        public double Length { get; set; }
        /// <summary>
        /// Ширина
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Высота
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// транспортировочная масса
        /// </summary>
        public double ShippingWeight { get; set; }
        /// <summary>
        /// эксплуатационная масса
        /// </summary>
        public double OperatingWeight { get; set; }
        /// <summary>
        /// уровень звукового давления
        /// </summary>
        public double SoundPressure { get; set; }
        /// <summary>
        /// Список теплообменников
        /// </summary>
        public List<string> HeatExchangers { get; set; }
        /// <summary>
        /// Список вентиляторов
        /// </summary>
        public List<string> Fans { get; set; }
        /// <summary>
        /// Список компрессоров
        /// </summary>
        public List<string> Compressors { get; set; }

    }
}
