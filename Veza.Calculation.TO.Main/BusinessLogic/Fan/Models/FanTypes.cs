namespace Veza.HeatExchanger.BusinessLogic.TO.Models
{
    public class FanTypes
    {
        #region Внутренние поля и переменные  

        /// <summary>
        /// Выбран ли тип
        /// </summary>
        public bool IsSelect { get; set; }
        public string Name { get; set; }
        #endregion

        #region Конструктора
        public FanTypes(bool isSelect, string name)
        {
            IsSelect = isSelect;
            Name = name;
        }

        public FanTypes(){}

        #endregion
    }
}
