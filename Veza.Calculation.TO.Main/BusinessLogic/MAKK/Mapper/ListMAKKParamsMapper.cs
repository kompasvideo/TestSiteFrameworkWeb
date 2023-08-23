using System.Collections.Generic;
using Veza.HeatExchanger.BusinessLogic.MAKK.DTO;
using Veza.HeatExchanger.DataBase.Models.DTO;

namespace Veza.HeatExchanger.BusinessLogic.MAKK.Mapper
{
    public static class ListMAKKParamsMapper
    {
        public static ListMAKKParamsDTO MAKKParamsToDTO(EquipmentMAKKDTO eMAKKDTO)
        {
            return new ListMAKKParamsDTO()
            {
                Id = eMAKKDTO.Id,
                Model = eMAKKDTO.Model,
                HeatExchanger = eMAKKDTO.HeatExchanger,
                HeatExchangerCount = eMAKKDTO.HeatExchangerCount,
                Fan = eMAKKDTO.Fan,
                FanCount = eMAKKDTO.FanCount,
                Compressor = eMAKKDTO.Compressor,   
                CompressorCount = eMAKKDTO.CompressorCount,
                NoOfCircuits = eMAKKDTO.NoOfCircuits,
            };
        }
        public static List<ListMAKKParamsDTO> MAKKParamsToDTO(List<EquipmentMAKKDTO> eMAKKDTO)
        {
            List<ListMAKKParamsDTO> list = new List<ListMAKKParamsDTO>();
            foreach (var item in eMAKKDTO)
            {
                list.Add(MAKKParamsToDTO(item));
            }
            return list;
        }
    }
}
