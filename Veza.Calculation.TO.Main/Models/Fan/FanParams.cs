using System.Collections.ObjectModel;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.DataBase.Models.FanAddEdit;

namespace Veza.HeatExchanger.Models
{
    sealed internal class FanParams
    {
        public FanDTO fanDTO { get; set; }
        public float Size { get; set; }
        public float AirFlowMax { get; set; }
        public float AirFlowMin { get; set; }
        public double AStatPres { get; set; }
        public double BStatPres { get; set; }
        public double CStatPres { get; set; }
        public double ATotalPres { get; set; }
        public double BTotalPres { get; set; }
        public double CTotalPres { get; set; }
        public double AEffFactor { get; set; }
        public double BEffFactor { get; set; }
        public double CEffFactor { get; set; }
        public double APowerInput { get; set; }
        public double BPowerInput { get; set; }
        public double CPowerInput { get; set; }
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

