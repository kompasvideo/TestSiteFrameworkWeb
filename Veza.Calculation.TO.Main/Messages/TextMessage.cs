namespace Veza.HeatExchanger.Messages
{
    /// <summary>
    /// в данный момент не используется (зарезервирована при регистрации)
    /// </summary>
    sealed public class TextMessage : IMessage
    {
        public TextMessage(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
