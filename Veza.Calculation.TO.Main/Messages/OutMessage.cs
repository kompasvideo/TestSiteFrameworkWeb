using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.Messages
{
    /// <summary>
    /// содержит параметры при переключении страницы
    /// </summary>
    sealed public class OutMessage : IMessage
    {
        public OutMessage(OutParams outParams)
        {
            OutParamsV = outParams;
        }

        public OutParams OutParamsV { get; set; }
    }
}
