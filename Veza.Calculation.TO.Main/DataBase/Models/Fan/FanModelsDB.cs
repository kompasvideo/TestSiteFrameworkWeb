using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Veza.HeatExchanger.DataBase.Models.FanAddEdit;
using System.Diagnostics.CodeAnalysis;

namespace Veza.HeatExchanger.DataBase.Models
{
    sealed public class FanModelsDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// Model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Напряжение (В)
        /// </summary>
        public float Voltage { get; set; }

        /// <summary>
        /// Соединение обмотки двигателя
        /// 1 - Y 
        /// 2 - Δ
        /// </summary>
        public byte ConnectionOfMotor { get; set; }

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
        public FanDirectionDB Direction {get; set; }

        /// <summary>
        /// Скорость (об/мин)
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Мощность (Вт)
        /// </summary>
        public float Power { get; set; }

        /// <summary>
        /// Ток (А)
        /// </summary>
        public float Current { get; set; }

        /// <summary>
        /// Уровень шума
        /// </summary>
        public byte NoiseLp { get; set; }

        /// <summary>
        /// Поток воздуха макс(м3/ч) 
        /// </summary>
        public float AirFlow_Max { get; set; }
        /// <summary>
        /// Поток воздуха мин(м3/ч) 
        /// </summary>
        public float AirFlow_Min { get; set; }

        /// <summary>
        /// Impeller material Id
        /// </summary>
        public FanMaterialsDB Materials { get; set; }

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
        public FanMountDB Mount { get; set; }
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

        ///// <summary>
        ///// Частота в Гц
        ///// </summary>
        //public float Frequency { get; set; }
        ///// <summary>
        ///// Размер 2 (мм) 
        ///// </summary>
        //public float Size_2 { get; set; }
        ///// <summary>
        ///// Размер 3 (мм) 
        ///// </summary>
        //public float Size_3 { get; set; }
        //public int TipologyId { get; set; }
        public FanTipologyDB Tipology { get; set; }

        /// <summary>
        /// manufactures
        /// </summary>
        public FanBuilderDB Builder { get; set; }

        public FanSeriesDB Series { get; set; }
        public FanPointsDB Points01 { get; set; }
        public FanPointsDB Points02 { get; set; }
        public FanPointsDB Points03 { get; set; }
        public FanPointsDB Points04 { get; set; }
        public FanPointsDB Points05 { get; set; }
        public FanPointsDB Points06 { get; set; }
        public FanPointsDB Points07 { get; set; }
        public FanPointsDB Points08 { get; set; }
        public FanPointsDB Points09 { get; set; }
        public FanPointsDB Points10 { get; set; }
        public FanPointsDB Points11 { get; set; }
        public FanPointsDB Points12 { get; set; }
        public FanPointsDB Points13 { get; set; }
        public FanPointsDB Points14 { get; set; }
        public FanPointsDB Points15 { get; set; }
        public FanPointsDB Points16 { get; set; }
        public FanPointsDB Points17 { get; set; }
        public FanPointsDB Points18 { get; set; }         
        public FanPointsDB Points19 { get; set; }         
        public FanPointsDB Points20 { get; set; }
        public FanStepEffPowerDB FanStep01 { get; set; }         
        public FanStepEffPowerDB FanStep02 { get; set; }         
        public FanStepEffPowerDB FanStep03 { get; set; }         
        public FanStepEffPowerDB FanStep04 { get; set; }         
        public FanStepEffPowerDB FanStep05 { get; set; }         
        public FanStepEffPowerDB FanStep06 { get; set; }
         
        public FanStepEffPowerDB FanStep07 { get; set; }
         
        public FanStepEffPowerDB FanStep08 { get; set; }
         
        public FanStepEffPowerDB FanStep09 { get; set; }
         
        public FanStepEffPowerDB FanStep10 { get; set; }
         
        public FanStepEffPowerDB FanStep11 { get; set; }
         
        public FanStepEffPowerDB FanStep12 { get; set; }
         
        public FanStepEffPowerDB FanStep13 { get; set; }
         
        public FanStepEffPowerDB FanStep14 { get; set; }
         
        public FanStepEffPowerDB FanStep15 { get; set; }
         
        public FanStepEffPowerDB FanStep16 { get; set; }
         
        public FanStepEffPowerDB FanStep17 { get; set; }
         
        public FanStepEffPowerDB FanStep18 { get; set; }
         
        public FanStepEffPowerDB FanStep19 { get; set; }
         
        public FanStepEffPowerDB FanStep20 { get; set; }
         
        public FanStepEffPowerDB FanStep21 { get; set; }
    }
}
