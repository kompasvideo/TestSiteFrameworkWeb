using System.Collections.Generic;
using System.Linq;
using Veza.HeatExchanger.Services;

namespace Veza.HeatExchanger.DataBase.Models.Mappers
{
    internal class MapperFanModelsToFanOutDTO
    {
        public static FanOutDTO FanModelsToFanOutDTO(FanModelsDB fan, IList<string> builder, IList<string> series,
            List<string> tipologys)
        {
            FanOutDTO fanOutDTO = new FanOutDTO
            {
                Model = fan.Model,
                Voltage = fan.Voltage,
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
            };
            if (fan.ConnectionOfMotor == 1)
            {
                fanOutDTO.ConnectionOfMotor = "Y";
            }
            else if (fan.ConnectionOfMotor == 2)
            {
                fanOutDTO.ConnectionOfMotor = "Δ";
            }
            if (fan.Mount.Id == 2)
            {
                fanOutDTO.Mount = "□";
            }
            else if (fan.Mount.Id == 1)
            {
                fanOutDTO.Mount = "Ø";
            }
            fanOutDTO.Mount += fan.MountSize;
            if (GS.IsCultureRU())
            {
                fanOutDTO.SelectedTipology = fan.Tipology.NameRus;
            }
            else
            {
                fanOutDTO.SelectedTipology = fan.Tipology.Name;
            }
            fanOutDTO.Tipology = tipologys;
            fanOutDTO.SelectedBuilder = fan.Builder.Name;
            List<string> newBuilder = builder.ToList();
            newBuilder.RemoveAt(0); 
            fanOutDTO.Builders = newBuilder;
            fanOutDTO.SelectedSeries = fan.Series.Name;
            List<string> newSeries = series.ToList();
            newSeries.RemoveAt(0);
            fanOutDTO.Series = newSeries;
            return fanOutDTO;
        }
}
}
