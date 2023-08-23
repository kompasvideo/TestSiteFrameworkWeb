using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.Messages
{
    /// <summary>
    /// содержит параметры для загрузки сохранённого расчёта
    /// </summary>
    sealed public class SaveCodesMessage : IMessage
    {
        public SaveCodesMessage(SaveCodes saveCodes)
        {
            SaveCode = saveCodes;
        }

        public SaveCodes SaveCode { get; set; }
    }        
}
