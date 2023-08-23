namespace Veza.HeatExchanger.Interfaces
{
    /// <summary>
    /// Интерфейс для класса SwitchLanguageService
    /// </summary>
    public interface ISwitchLanguageService
    {
        /// <summary>
        /// Вызывается при смене языка интерфейса - вызывет метод перерисовки страницы на выбранном языке
        /// </summary>
        /// <param name="culture"></param>
        void SwitchLanguage(string value);
    }
}
