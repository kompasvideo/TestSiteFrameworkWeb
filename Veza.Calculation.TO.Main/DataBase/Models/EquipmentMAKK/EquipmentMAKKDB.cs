using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Veza.HeatExchanger.DataBase.Models.EquipmentMAKK;
using System.Diagnostics.CodeAnalysis;

namespace Veza.HeatExchanger.DataBase.Models
{
    sealed public class EquipmentMAKKDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Модель МАКК
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Серия МАКК
        /// </summary>
        public string Seria { get; set; }
        /// <summary>
        /// Теплообменник
        /// </summary>
        public HeatExchangerDB HeatExchangerId { get; set; }
        /// <summary>
        /// Кол-во теплообменников
        /// </summary>
        public int HeatExchangerCount { get; set; }
        /// <summary>
        /// Вентилятор
        /// </summary>
        public FanModelsDB FanModelsId { get; set; }
        /// <summary>
        /// Кол-во вентиляторов
        /// </summary>
        public int FanModelsCount { get; set; }
        /// <summary>
        /// Компрессор
        /// </summary>
        public CompressorDB CompressorId { get; set; }
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
        public double TotVolumeRefr { get; set; }
        /// <summary>
        /// Общая потребляемая мощность / 
        /// Total absorbed power
        /// </summary>
        public double TotalAbsorbedPower { get; set; }
        /// <summary>
        /// Рабочий ток
        /// </summary>
        public double Current { get; set; }
        /// <summary>
        /// Максимальный рабочий ток
        /// </summary>
        public double MaxCurrent { get; set; }
        /// <summary>
        /// пусковой ток
        /// </summary>
        public double LRA { get; set; }
        /// <summary>
        /// диаметр жидкостной трубы
        /// </summary>
        public string LiquidTubeDiam { get; set; }
        /// <summary>
        /// диаметр газовой трубы
        /// </summary>
        public string GasTubeDiam { get; set; }
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
        public double ShipWeight { get; set; }
        /// <summary>
        /// эксплуатационная масса
        /// </summary>
        public double OperWeight { get; set; }
        /// <summary>
        /// уровень звукового давления
        /// </summary>
        public double SoundPressure { get; set; }

        /// <summary>
        /// Опция МАКК
        /// </summary>
        public MAKKOptionDB MAKKOption_1 { get; set; }
        public MAKKOptionDB MAKKOption_2 { get; set; }
        public MAKKOptionDB MAKKOption_3 { get; set; }
        public MAKKOptionDB MAKKOption_4 { get; set; }
        public MAKKOptionDB MAKKOption_5 { get; set; }

        /// <summary>
        /// Доп. комплектующее
        /// </summary>
        public MAKKAccessoriesDB MAKKAccessories_1 { get; set; }
        public MAKKAccessoriesDB MAKKAccessories_2 { get; set; }
        public MAKKAccessoriesDB MAKKAccessories_3 { get; set; }
        public MAKKAccessoriesDB MAKKAccessories_4 { get; set; }
        public MAKKAccessoriesDB MAKKAccessories_5 { get; set; }
    }
}
