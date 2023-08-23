namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// статически свойства, используемые в программе
    /// </summary>
    sealed public class StaticData
    {
        /// <summary>
        /// Текущая страница
        /// </summary>
        public static CurrentPage currentPage { get; set; }
        public static int ErrorI { get; set; }
        /// <summary>
        /// Текст ошибки возвраящённый из Dll
        /// </summary>
        public static string ErrorDll { get; set; }

        /// <summary>
        /// Режим расчёта
        /// HW - воздухонагреватель
        /// CW - воздухоохладитель
        /// </summary>
        public static string CalcMode { get; set; }

        public static void Initialize()
        {
            currentPage = default(CurrentPage);
            ErrorI = default;
            ErrorDll = default;
            CalcMode = default;
        }
        public static string Path { get; set; }
    }
}
