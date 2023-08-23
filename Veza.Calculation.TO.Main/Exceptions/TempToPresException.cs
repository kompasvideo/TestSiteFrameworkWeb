using System;

namespace Veza.HeatExchanger.Exceptions
{
    /// <summary>
    /// класс-исключение для проверки корректностиперевода температуры в давление (конденсатор и испаритель)
    /// </summary>
    sealed internal class TempToPresException : ApplicationException
    {
        private string _messageDetails = string.Empty;
        public TempToPresException() {}
        public TempToPresException(string message)
        {
            _messageDetails = message;
        }

        public override string Message => $"{Calculation.TO.Main.Properties.Resources.ErrorExcepTempToPres} {_messageDetails}";
    }
}
