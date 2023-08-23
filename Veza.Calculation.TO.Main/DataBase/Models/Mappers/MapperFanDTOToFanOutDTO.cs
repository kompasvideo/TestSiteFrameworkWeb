using System.Collections.Generic;

namespace Veza.HeatExchanger.DataBase.Models.Mappers
{
    public class MapperFanDTOToFanOutDTO
    {
        public static List<FanOutDTO> FanDTOToFanOutDTO(List<FanDTO> fanDTO)
        {
            List<FanOutDTO> fanOutDTO = new List<FanOutDTO>();
            foreach (FanDTO fan in fanDTO)
            {
                string mount = "";
                if (fan.MountId == 2) mount = "□";
                else if (fan.MountId == 1) mount = "Ø";
                mount += fan.MountSize.ToString();
                string connectionOfMotor = "";
                if (fan.ConnectionOfMotor == 1) connectionOfMotor = "Y";
                else if (fan.ConnectionOfMotor == 2) connectionOfMotor = "Δ";
                fanOutDTO.Add(new FanOutDTO()
                {
                    Model = fan.Model,
                    Voltage = fan.Voltage,
                    ConnectionOfMotor = connectionOfMotor,
                    Size1 = fan.Size1,
                    MotorPoles = fan.MotorPoles,
                    Direction = fan.Direction,
                    Speed = fan.Speed,
                    Power = fan.Power,
                    Current = fan.Current,
                    NoiseLp = fan.NoiseLp,
                    AirFlowMin = fan.AirFlowMin,
                    AirFlowMax = fan.AirFlowMax,
                    Materials = fan.Materials,
                    NBlades = fan.NBlades,
                    IP = fan.IP,
                    Mount = mount,
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
                });
            }
            return fanOutDTO;
        }

        public static FanOutDTO FanDTOToFanOutDTO(FanDTO fan)
        {
            string mount = "";
            if (fan.MountId == 2) mount = "□";
            else if (fan.MountId == 1) mount = "Ø";
            mount += fan.MountSize.ToString();
            string connectionOfMotor = "";
            if (fan.ConnectionOfMotor == 1) connectionOfMotor = "Y";
            else if (fan.ConnectionOfMotor == 2) connectionOfMotor = "Δ";
            return new FanOutDTO()
            {
                Model = fan.Model,
                Voltage = fan.Voltage,
                ConnectionOfMotor = connectionOfMotor,
                Size1 = fan.Size1,
                MotorPoles = fan.MotorPoles,
                Direction = fan.Direction,
                Speed = fan.Speed,
                Power = fan.Power,
                Current = fan.Current,
                NoiseLp = fan.NoiseLp,
                AirFlowMin = fan.AirFlowMin,
                AirFlowMax = fan.AirFlowMax,
                Materials = fan.Materials,
                NBlades = fan.NBlades,
                IP = fan.IP,
                Mount = mount,
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
        }
    }
}
