namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// класс с полями для хранения названия объекта и адреса
    /// </summary>
    sealed public class PrintDataSave
    {
        /// <summary>
        /// Клиент
        /// </summary>
        public string Client { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Объект
        /// </summary>
        public string Object { get; set; }       
    }
}
