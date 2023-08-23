using System.Collections.ObjectModel;
using System.Windows;

namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// отображаемые свойства для табличного режима расчёта (всех)
    /// </summary>
    sealed public class TableData 
    {
        #region Свойства привязанные к Xaml
        /// <summary>
        /// Ширина ячеек
        /// </summary>
        public static int WidthColumn_1 { get; set; }
        public static int WidthColumn_3 { get; set; }
        public static int WidthColumn_4 { get; set; }

        /// <summary>
        /// Видимость кнопки Сохранить
        /// </summary>
        public static Visibility? saveVisibility;
        public Visibility? SaveVisibility 
        { 
            get
            {
                return saveVisibility;
            }
            set
            {
                saveVisibility = value;
            }
        }

        /// <summary>
        /// Видимость кнопки Печать
        /// </summary>
        public static Visibility? PrintVisibility { get; set; } = Visibility.Hidden;
        
        /// <summary>
        /// Таблица вывода результатов расчёта
        /// </summary>
        public static ObservableCollection<OutView> ProjectsOutTable { get; set; } = new ObservableCollection<OutView>();
        public static OutView SelectProjectsOutTable { get; set; }
        #endregion

        #region Внутренние поля и переменные
        /// <summary>
        /// Геометрия для табличного режима
        /// </summary>
        public string I_Geo { get; set; }
        /// <summary>
        /// Размеры и материал трубки для табличного режима
        /// </summary>
        public string I_PipeThk { get; set; }

        /// <summary>
        /// толщина оребрения для табличного режима
        /// </summary>
        public string I_FinThk { get; set; }
        /// <summary>
        /// шаг оребрения для табличного режима
        /// </summary>
        public string I_LamAbsFix { get; set; }

        /// <summary>
        /// теплоноситель
        /// </summary>
        public string I_MedTyp { get; set; }
        #endregion

        #region Конструктор
        public TableData()
        {
            if (saveVisibility == null)
            {
                saveVisibility = Visibility.Visible;
            }
            WidthColumn_3 = 80;
            WidthColumn_4 = 160;
        }
        #endregion       
    }
}
