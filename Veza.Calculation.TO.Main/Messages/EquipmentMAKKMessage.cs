using Veza.HeatExchanger.DataBase.Models.DTO;

namespace Veza.HeatExchanger.Messages
{
    sealed public class EquipmentMAKKMessage : IMessage
    {
        public EquipmentMAKKMessage(EquipmentMAKKDTO equipmentMAKKDTO )
        {
            EquipmentMAKKDTOV = equipmentMAKKDTO;
        }
        public EquipmentMAKKDTO EquipmentMAKKDTOV { get; set; }
    }
}
