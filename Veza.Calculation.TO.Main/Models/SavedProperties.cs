using System;
using System.Windows;

namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// класс со свойством, которое передаётся между страницами.     
    /// </summary>
    sealed public class SavedProperties
    {
        #region Свойства привязанные к Xaml
        /// <summary>
        /// Контролирует свёрнут или нет Expander ( отображение свойств корпуса и коллектора)
        /// </summary>
        public static Boolean? ExpanderFrame { get; set; }
        /// <summary>
        /// Контролирует свёрнут или нет Expander ( дополнительные параметры в табличном режиме)
        /// </summary>
        public static Boolean? ExpanderFrameTable { get; set; }
        
        /// <summary>
        /// видимость списка Характирестики корпуса и коллектора
        /// </summary>
        public static Visibility? CasingHeadersVisibility { get; set; } = Visibility.Collapsed;

        /// <summary>
        /// видимость кнопки Показать характирестики корпуса и коллектора
        /// </summary>
        public static Visibility? CasingHeadersHideVisibility { get; set; } = Visibility.Visible;

        /// <summary>
        /// видимость кнопки Показать дополнительные поля
        /// </summary>
        public static Visibility? AdvPropShowVisibility { get; set; } = Visibility.Visible;

        /// <summary>
        /// видимость кнопки Скрыть дополнительные поля
        /// </summary>
        public static Visibility? AdvPropHideVisibility { get; set; } = Visibility.Collapsed;


        #endregion

        #region Внутренние поля и переменные
        private static bool init = false;
        #endregion

        #region Конструкторы
        public SavedProperties()
        {
            if (!init)
            {
                ExpanderFrame = false;
                ExpanderFrameTable = false;
                init = true;
            }
        }
        #endregion
    }
}
