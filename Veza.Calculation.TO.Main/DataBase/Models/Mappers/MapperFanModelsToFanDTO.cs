using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.DataBase.Models.Mappers
{
    internal class MapperFanModelsToFanDTO
    {
        public static FanDTO FanModelsToFanDTO(FanModelsDB fan)
        {
            FanDTO fanDTO = new FanDTO
            {
                Id = fan.Id,
                Model = fan.Model,
                Voltage = fan.Voltage,
                ConnectionOfMotor = fan.ConnectionOfMotor,
                Size1 = fan.Size1,
                MotorPoles = fan.MotorPoles,
                Direction = fan.Direction.Name,
                Speed = fan.Speed,
                Power = fan.Power,
                Current = fan.Current,
                NoiseLp = fan.NoiseLp,
                AirFlowMin = fan.AirFlow_Min,
                AirFlowMax = fan.AirFlow_Max,
                Materials = fan.Materials.Name,
                NBlades = fan.NBlades,
                IP = fan.IP,
                MountId = fan.Mount.Id,
                MountSize = fan.MountSize,
                Holes = fan.Holes,
                Weight = fan.Weight,
                AStatPres = fan.AStatPres,
                BStatPres = fan.BStatPres,
                CStatPres = fan.CStatPres,
                ATotalPres = fan.ATotalPres,
                BTotalPres = fan.BTotalPres,
                CTotalPres = fan.CTotalPres,
                AEffFactor = fan.AEffFactor,
                BEffFactor = fan.BEffFactor,
                CEffFactor = fan.CEffFactor,
                APowerInput = fan.APowerInput,
                BPowerInput = fan.BPowerInput,
                CPowerInput = fan.CPowerInput,
                Builders = fan.Builder,
                Series = fan.Series,
                Points01 = fan.Points01,
                Points02 = fan.Points02,
                Points03 = fan.Points03,
                Points04 = fan.Points04,
                Points05 = fan.Points05,
                Points06 = fan.Points06,
                Points07 = fan.Points07,
                Points08 = fan.Points08,
                Points09 = fan.Points09,
                Points10 = fan.Points10,
                Points11 = fan.Points11,
                Points12 = fan.Points12,
                Points13 = fan.Points13,
                Points14 = fan.Points14,
                Points15 = fan.Points15,
                Points16 = fan.Points16,
                Points17 = fan.Points17,
                Points18 = fan.Points18,
                Points19 = fan.Points19,
                Points20 = fan.Points20,
                FanStep01 = fan.FanStep01,
                FanStep02 = fan.FanStep02,
                FanStep03 = fan.FanStep03,
                FanStep04 = fan.FanStep04,
                FanStep05 = fan.FanStep05,
                FanStep06 = fan.FanStep06,
                FanStep07 = fan.FanStep07,
                FanStep08 = fan.FanStep08,
                FanStep09 = fan.FanStep09,
                FanStep10 = fan.FanStep10,
                FanStep11 = fan.FanStep11,
                FanStep12 = fan.FanStep12,
                FanStep13 = fan.FanStep13,
                FanStep14 = fan.FanStep14,
                FanStep15 = fan.FanStep15,
                FanStep16 = fan.FanStep16,
                FanStep17 = fan.FanStep17,
                FanStep18 = fan.FanStep18,
                FanStep19 = fan.FanStep19,
                FanStep20 = fan.FanStep20,
                FanStep21 = fan.FanStep21,
            };
            if (fan.Mount.Id == 2)
            {
                fanDTO.Mount = "□";
            }
            else if (fan.Mount.Id == 1)
            {
                fanDTO.Mount = "Ø";
            }
            fanDTO.Mount += fan.MountSize;

            switch (fan.Tipology.Id)
            {
                case 1:
                    fanDTO.Tipology = Tipology.AxialMonophase;
                    break;
                case 2:
                    fanDTO.Tipology = Tipology.AxialTriphase;
                    break;
                case 3:
                    fanDTO.Tipology = Tipology.DirectlyCoupled;
                    break;
                case 4:
                    fanDTO.Tipology = Tipology.Centriphugal;
                    break;
            }
            return fanDTO;
        }
    }
}
