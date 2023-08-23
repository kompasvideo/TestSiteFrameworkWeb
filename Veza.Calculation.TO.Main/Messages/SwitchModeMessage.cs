namespace Veza.HeatExchanger.Messages
{
    /// <summary>
    /// содержит параметры для смены языка
    /// </summary>
    sealed public class SwitchModeMessage : IMessage
    {
        public SwitchModeMessage(string text)
        {
            CalcMode = text;
        }

        public string CalcMode { get; set; }
    }
}
