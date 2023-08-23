using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.MAKK;

namespace Veza.HeatExchanger.Messages
{
    sealed public class MAKKMessage : IMessage
    {
        public MAKKMessage(MAKKParams makkParams)
        {
            MakkParamsV = makkParams;
        }

        public MAKKParams MakkParamsV { get; set; }    
    }
}
