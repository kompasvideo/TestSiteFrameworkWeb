using System;

namespace Veza.HeatExchanger.Models.MAKK
{
    public class OrderParams
    {
        /// <summary>
        /// Номер бланк-заказа
        /// </summary>
        public string TechnicalOffer { get; set; }

        /// <summary>
        /// Номер проекта
        /// </summary>
        public string ProjectNumber { get; set; }

        /// <summary>
        /// Наименование объекта
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Адрес объекта
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Инженер
        /// </summary>
        public string Engineer { get; set; }
        
        /// <summary>
        /// Менеджер проекта
        /// </summary>
        public string ProjectManager { get; set;}

        /// <summary>
        /// Дата подбора
        /// </summary>
        public string SelectionDate { get; set; }

        /// <summary>
        /// Согласовано
        /// </summary>
        public string Approved { get; set; }

        /// <summary>
        /// есть ли спец. требования
        /// </summary>
        public bool CBSpecRequire { get; set; }

        /// <summary>
        /// Спец. требования
        /// </summary>
        public string SpecRequire { get; set; }
    }
}
