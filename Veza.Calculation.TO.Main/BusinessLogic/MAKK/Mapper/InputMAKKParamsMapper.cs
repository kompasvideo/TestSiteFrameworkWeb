using Veza.HeatExchanger.BusinessLogic.MAKK.DTO;
using Veza.HeatExchanger.BusinessLogic.MAKK.Models;

namespace Veza.HeatExchanger.BusinessLogic.MAKK.Mapper
{
    internal static class InputMAKKParamsMapper
    {
        public static InputMAKKParamsDTO InputDataToDTO(InputMAKKParams input)
        {
            return new InputMAKKParamsDTO()
            {
                Refrigerants = input.Refrigerants,
                SelectRefrigerant = input.SelectRefrigerant,
                SeriesMAKKs = input.SeriesMAKKs,
                CoolingCapacity = input.CoolingCapacity,
                ErrorRate = input.ErrorRate,
                OutTemp = input.OutTemp,
                EvapTemp = input.EvapTemp,
            };
        }
        public static InputMAKKParams DTOToInputData(InputMAKKParamsDTO input)
        {
            return new InputMAKKParams()
            {
                Refrigerants = input.Refrigerants,
                SelectRefrigerant = input.SelectRefrigerant,
                SeriesMAKKs = input.SeriesMAKKs,
                CoolingCapacity = input.CoolingCapacity,
                ErrorRate = input.ErrorRate,
                OutTemp = input.OutTemp,
                EvapTemp = input.EvapTemp,
            };
        }
    }
}
