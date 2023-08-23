using System.Collections.Generic;
using Veza.HeatExchanger.Models.MAKK;

namespace Veza.HeatExchanger.Models.MAKK
{
    sealed public class MAKKParams
    {
        /// <summary>
        /// имя МАКК
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// серия МАКК
        /// </summary>
        public string Serie { get; set; }

        /// <summary>
        /// Опции
        /// </summary>
        public List<MAKKOptions> Options { get; set; }

        /// <summary>
        /// Дополнительное оборудование
        /// </summary>
        public List<MAKKOptions> AddEquip { get; set; }

        /// <summary>
        /// Отступ снизу страницы 4
        /// </summary>
        public double Page4MarginBottom { get; set; }

        /// <summary>
        /// Отступ снизу страницы 5
        /// </summary>
        public double Page5MarginBottom { get; set; }

        /// <summary>
        /// опции или доп. комплектующие        
        /// </summary>
        public OptionOrAddAccess OptionOrAddAccessV { get; set; }

        /// <summary>
        /// данные для бланк-заказа
        /// </summary>
        public OrderParams OrderParamsV { get; set; }

        /// <summary>
        /// Холодопроизводительность
        /// </summary>
        public string CoolingCapacity { get; set; }

        /// <summary>
        /// энергетическая эффективность
        /// </summary>
        public string ERR { get; set; }

        /// <summary>
        /// Температура конденсации
        /// </summary>
        public string CondTemp { get; set; }

        /// <summary>
        /// Температура кипения
        /// </summary>
        public string EvapTemp { get; set; }
        /// <summary>
        /// Кол-во компрессоров
        /// </summary>
        public int CountCompres { get; set; }
        /// <summary>
        /// Кол-во холодильных контуров
        /// </summary>
        public int RefrCircuits { get; set; }
        /// <summary>
        /// Кол-во теплообменников
        /// </summary>
        public int CountHeatExch { get; set; }
        /// <summary>
        /// Суммарный объём конденсатора
        /// </summary>
        public string TotVolume { get; set; }
        /// <summary>
        /// Кол-во вентиляторов
        /// </summary>
        public int CountFans { get; set; }
        /// <summary>
        /// Суммарный объём ресиверов
        /// </summary>
        public double TotVolumeRefr { get; set; }
        /// <summary>
        /// Общая потребляемая мощность
        /// </summary>
        public double TotalAbsorPower { get; set; }
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
    }
}
