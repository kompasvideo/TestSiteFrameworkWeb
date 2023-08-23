namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// класс с свойствами, которые отображаются на страницах предварительного просмотра
    /// </summary>
    sealed public class PrintData
    {
        #region Свойства привязанные к Xaml
        /// <summary>
        /// Клиент
        /// </summary>
        public static string Client { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public static string Description { get; set; }
        /// <summary>
        /// Объект
        /// </summary>
        public static string Object { get; set; }
        /// <summary>
        /// входные и выходные данные
        /// </summary>
        public OutView ProjectsOut { get; set; }
        public string Date { get; set; }
        /// <summary>
        /// Жидкость
        /// </summary>
        public string SelectedFluid { get; set; }
        /// <summary>
        /// Тип хладагента
        /// </summary>
        public string Selected_I_RefT { get; set; }
        /// <summary>
        /// Коэффициент загрязнения трубы
        /// </summary>
        public string Selected_I_FoulingI { get; set; }
        /// <summary>
        /// Переохлаждение
        /// </summary>
        public double I_TSubC { get; set; }

        /// <summary>
        /// Перегрев
        /// </summary>
        public double I_TOvrH { get; set; }

        /// <summary>
        /// Температура горячего газа
        /// </summary>
        public double I_THotGas { get; set; }

        /// <summary>
        /// Температура всасываемого газа
        /// </summary>
        public double I_TSucGas { get; set; }


        #endregion

        #region Методы

        /// <summary>
        /// Установка параметров
        /// </summary>
        /// <param name="projectsOut"></param>
        /// <param name="date"></param>
        /// <param name="fluid"></param>
        public void SetParams(OutView projectsOut, string date,string fluid)
        {
            ProjectsOut = projectsOut;
            Date = date;
            SelectedFluid = fluid;
        }

        /// <summary>
        /// Установка параметров
        /// </summary>
        /// <param name="projectsOut"></param>
        /// <param name="date"></param>
        /// <param name="fluid"></param>
        public void SetParams(OutView projectsOut, string date, string selected_I_RefT, string selected_I_FoulingI,
                     double i_TSubC, double i_TOvrH, double i_THotGas, double i_TSucGas )
        {
            ProjectsOut = projectsOut;
            Date = date;
            Selected_I_RefT = selected_I_RefT;
            Selected_I_FoulingI = selected_I_FoulingI;
            I_TSubC = i_TSubC;
            I_TOvrH = i_TOvrH;
            I_THotGas = i_THotGas;
            I_TSucGas = i_TSucGas;
        }
        #endregion
    }
}
