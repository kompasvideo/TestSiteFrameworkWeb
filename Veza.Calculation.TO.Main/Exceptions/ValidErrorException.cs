using System;

namespace Veza.HeatExchanger.Exceptions
{
    /// <summary>
    /// класс-исключение для проверки корректности входных свойств для расчёта
    /// </summary>
    sealed public class ValidErrorException : ApplicationException
    {
        public ValidErrorException(string message, string param, string name): base(message) 
        {
            Data.Add("param", param);
            Data.Add("name", name);
        }
    }
}
