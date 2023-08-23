using Isc.Eng.B0003;

namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// хранит параметры для запуска внешней библиотеки расчёта
    /// </summary>
    sealed public class ParamsCalculationLibrary
    {
        /// <summary>
        /// Секретный ключ для запуска - номер лицензии библиотеки расчёта для Везы
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// Путь к библиотеки Dll
        /// </summary>
        public string DataPath { get; set; }

        /// <summary>
        /// Код организанизации вледельца лицензии
        /// </summary>
        public string SupplierKey { get; set; }

        /// <summary>
        /// 2-х буквенное обозначение типа расчёта
        /// CX - конденсатор
        /// DX - испаритель
        /// CW - воздухоохладитель
        /// HW - воздухонагреватель
        /// ST - паровой нагреватель
        /// </summary>
        public string MyMode { get; set; }

        /// <summary>
        /// Ссылка на внешнюю библиотеку
        /// </summary>
        public EngineB0003 MyEngine { get; set; }
    }
}
