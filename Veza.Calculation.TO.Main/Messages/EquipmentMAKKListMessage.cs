using Veza.HeatExchanger.BusinessLogic.Compressors.Models;

namespace Veza.HeatExchanger.Messages
{
    sealed public class EquipmentMAKKListMessage : IMessage
    {
        public EquipmentMAKKListMessage(EquipmentMAKKLists equipmentMAKKLists)
        {
            EquipmentMAKKListsM = equipmentMAKKLists;
        }
        public EquipmentMAKKLists EquipmentMAKKListsM { get; set; }
    }
}
