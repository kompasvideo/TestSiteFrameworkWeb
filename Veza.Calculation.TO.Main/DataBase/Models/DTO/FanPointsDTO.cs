using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veza.HeatExchanger.DataBase.Models.DTO
{
    sealed public class FanPointsDTO
    {
        public float Voltage { get; set; }
        public byte Frequency { get; set; }
        public float Speed { get; set; }
        public float Power { get; set; }
        public float Current { get; set; }
        public uint Airflow { get; set; }
        public ushort StatPressure { get; set; }
        public ushort DynamicPressure { get; set; }
        public ushort TotalPressure { get; set; }
        public float AirflowS { get; set; }
        public float EffFactorProcent { get; set; }
        public float PerfFactor { get; set; }
        public float TotalPresFactor { get; set; }
        public float StatPresFactor { get; set; }
        public float DynPresFactor { get; set; }
        public float PowerFactor { get; set; }
        public float EffFactor { get; set; }
        public float SpeedFactor1 { get; set; }
        public float SpeedFactor2 { get; set; }
        public float SizeFactor1 { get; set; }
        public float SizeFactor2 { get; set; }

        public FanPointsDB FanPoint { get; set; }
    }
}
