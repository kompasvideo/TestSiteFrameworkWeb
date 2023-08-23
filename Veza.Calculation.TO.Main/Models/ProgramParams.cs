using Veza.HeatExchanger.Interfaces;

namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// Параметры программы ( сохраняются в xml-файл в ExternParamsFileService.cs)
    /// </summary>
    public class ProgramParams 
    {
        public string Version { get; set; } = "1";
        /// <summary>
        /// язык интерфейса
        /// </summary>
        public string Language { get; set; }
    }
}
