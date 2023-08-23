using System.Collections.Generic;
using System.Linq;
using Veza.HeatExchanger.BusinessLogic.MAKK.DTO;
using Veza.HeatExchanger.DataBase.Models.DTO;

namespace Veza.HeatExchanger.BusinessLogic.MAKK.Mapper
{
    public class MAKKParamsDTOMapper
    {
        /// <summary>
        /// Перенести данные из EquipmentMAKKDTO в MAKKParamsDTO
        /// </summary>
        /// <param name="eq"></param>
        /// <param name="heatExchangers"></param>
        /// <param name="fans"></param>
        /// <param name="compressors"></param>
        /// <returns></returns>
        public static MAKKParamsDTO EquipmentToParams(EquipmentMAKKDTO eq, IList<string> heatExchangers,
            IList<string> fans, IList<string> compressors)
        {
            return new MAKKParamsDTO()
            {
                Id = eq.Id,
                Model = eq.Model,
                Seria = eq.Seria,
                HeatExchanger = eq.HeatExchanger,
                HeatExchangerCount = eq.HeatExchangerCount,
                Fan = eq.Fan,
                FanCount = eq.FanCount,
                Compressor = eq.Compressor,
                CompressorCount = eq.CompressorCount,
                NoOfCircuits = eq.NoOfCircuits,
                TotalVolumeReceivers = eq.TotalVolumeReceivers,
                TotalAbsorbedPower = eq.TotalAbsorbedPower,
                TotalOperatingCurrent = eq.TotalOperatingCurrent,
                MaximumOperatingCurrent = eq.MaximumOperatingCurrent,
                LRA = eq.LRA,
                LiquidTubeDiameter = eq.LiquidTubeDiameter,
                GasTubeDiameter = eq.GasTubeDiameter,
                Length = eq.Length,
                Width = eq.Width,
                Height = eq.Height,
                ShippingWeight = eq.ShippingWeight,
                OperatingWeight = eq.OperatingWeight,
                SoundPressure = eq.SoundPressure,
                HeatExchangers = heatExchangers.ToList(),
                Fans = fans.ToList(),
                Compressors = compressors.ToList(),
            };            
        }

        /// <summary>
        /// Перенести данные из MAKKParamsDTO в EquipmentMAKKDTO
        /// </summary>
        /// <param name="mAKKParamsDTO"></param>
        /// <returns></returns>
        public static EquipmentMAKKDTO ParamsToEquipment(MAKKParamsDTO mk, EquipmentMAKKDTO eq)
        {
            eq.Id = mk.Id;
            eq.Model = mk.Model;
            eq.Seria = mk.Seria;
            eq.HeatExchanger = mk.HeatExchanger;
            eq.HeatExchangerCount = mk.HeatExchangerCount;
            eq.Fan = mk.Fan;
            eq.FanCount = mk.FanCount;
            eq.Compressor = mk.Compressor;
            eq.CompressorCount = mk.CompressorCount;
            eq.NoOfCircuits = mk.NoOfCircuits;
            eq.TotalVolumeReceivers = mk.TotalVolumeReceivers;
            eq.TotalAbsorbedPower = mk.TotalAbsorbedPower;
            eq.TotalOperatingCurrent = mk.TotalOperatingCurrent;
            eq.MaximumOperatingCurrent = mk.MaximumOperatingCurrent;
            eq.LRA = mk.LRA;
            eq.LiquidTubeDiameter = mk.LiquidTubeDiameter;
            eq.GasTubeDiameter = mk.GasTubeDiameter;
            eq.Length = mk.Length;
            eq.Width = mk.Width;
            eq.Height = mk.Height;
            eq.ShippingWeight = mk.ShippingWeight;
            eq.OperatingWeight = mk.OperatingWeight;
            eq.SoundPressure = mk.SoundPressure;
            return eq;
        }
    }
}
