using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.Messages
{
    sealed internal class FanMessage : IMessage
    {
        public FanMessage(FanParams fanParams)
        {
            FanParamsV = fanParams;
        }

        public FanParams FanParamsV { get; set; }
    }
}
