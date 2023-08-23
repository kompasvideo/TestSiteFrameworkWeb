using System.Collections.Generic;
using Veza.HeatExchanger.BusinessLogic.TO.Models;
using Veza.HeatExchanger.DataBase.Models;

namespace Veza.HeatExchanger.BusinessLogic.Fan.Models
{
    public class Fans
    {
        #region Внутренние поля 
        /// <summary>
        /// Типы вентиляторов
        /// </summary>
        public List<FanTypes> FanTypesP { get; set; }
        /// <summary>
        /// Производители
        /// </summary>
        public List<string> Builder { get; set; }
        /// <summary>
        /// Выбранный производитель
        /// </summary>
        public string SelectedBuilder { get; set; }
        /// <summary>
        /// Серия вентиляторов
        /// </summary>
        public List<string> Series { get; set; }
        /// <summary>
        /// Выбранная серия
        /// </summary>
        public string SelectedSeries { get; set; }
        /// <summary>
        /// Список вентиляторов
        /// </summary>
        public List<FanOutDTO> FansP { get; set; }
        #endregion

        #region Конструктор
        public Fans(List<FanTypes> fanTypes, List<string> builder, string selectedBuilder, List<string> series,
            string selectedSeries, List<FanOutDTO> fans)
        {
            FanTypesP = fanTypes;
            Builder = builder;
            SelectedBuilder = selectedBuilder;
            Series = series;
            SelectedSeries = selectedSeries;
            FansP = fans;
        }
        #endregion
    }
}
