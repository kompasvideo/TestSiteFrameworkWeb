using System.Globalization;
using System.Threading;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.Services
{
    /// <summary>
    /// Вызывает перерисовку страницы на «выбранном» языке
    /// </summary>
    sealed public class SwitchLanguageService : ISwitchLanguageService
    {
        #region Внутренние поля и переменные
        //private PageService _pageService;
        private MessageBus _messageBus;
        #endregion

        #region Конструктор
        public SwitchLanguageService( MessageBus messageBus)
        {
            //_pageService = pageService;
            _messageBus = messageBus;
        }
        #endregion

        #region Методы

        /// <summary>
        /// Вызывается при смене языка интерфейса - вызывет метод перерисовки страницы на выбранном языке
        /// </summary>
        /// <param name="culture"></param>
        public async void SwitchLanguage(string culture)
        {
            Calculation.TO.Main.Properties.Resources.Culture = Thread.CurrentThread.CurrentCulture  =
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);            
            OutParams outParams = new OutParams();
            outParams.ChangeLang = true;
        }
        #endregion
    }
}
