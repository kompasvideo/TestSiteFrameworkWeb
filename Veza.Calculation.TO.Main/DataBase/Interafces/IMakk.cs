using System.Collections.Generic;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.DataBase.Models.DTO;
using Veza.HeatExchanger.DataBase.Models.EquipmentMAKK;

namespace Veza.HeatExchanger.DataBase.Interafces
{
    public interface IMakk
    {        
        /// <summary>
        /// Обновление в БД модели оборудования МАКК
        /// </summary>
        /// <param name="equipmentMAKKDTO"></param>
        bool UpdateEquipmentMAKK(EquipmentMAKKDTO equipmentMAKKDTO);

        /// <summary>
        /// Добавление в БД модели оборудования МАКК
        /// </summary>
        /// <param name="equipmentMAKKDTO"></param>
        void AddEquipmentMAKK(EquipmentMAKKDTO equipmentMAKKDTO);

        /// <summary>
        /// Получений списка опций модели МАКК
        /// </summary>
        /// <param name="nameMAKK"></param>
        List<MAKKOptionDB> GetOptions(string nameMAKK);

        /// <summary>
        /// Получений списка "Доп. оборудование" модели МАКК
        /// </summary>
        /// <param name="nameMAKK"></param>
        List<MAKKAccessoriesDB> GetAccessories(string nameMAKK);

        /// <summary>
        /// Создаёт список оборудования МАКК
        /// </summary>
        /// <param name="MAKK"></param>
        void InitEqupmentMAKK(IList<EquipmentMAKKDTO> MAKK);

        /// <summary>
        /// Получить список МАКК с параметрами
        /// </summary>
        /// <returns></returns>
        List<EquipmentMAKKDB> LoadMAKK();

        /// <summary>
        /// получить список Хладогентов
        /// </summary>
        /// <returns></returns>
        IList<string> GetHeatExchangers();
    }
}
