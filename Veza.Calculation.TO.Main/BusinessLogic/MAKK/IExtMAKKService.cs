using System.Collections.Generic;
using Veza.HeatExchanger.BusinessLogic.MAKK.DTO;
using Veza.HeatExchanger.Models.Main;
using Veza.HeatExchanger.Models.MAKK;

namespace Veza.HeatExchanger.BusinessLogic.MAKK
{
    public interface IExtMAKKService
    {
        /// <summary>
        /// Получение входных параметров МАКК
        /// </summary>
        InputMAKKParamsDTO GetInputMAKKParams();
        /// <summary>
        /// Изменение входных параметров МАКК
        /// </summary>
        bool SetInputMAKKParams(InputMAKKParamsDTO inputMAKKParams);

        /// <summary>
        /// Расчёт МАКК
        /// </summary>
        void CalcMAKK();

        /// <summary>
        /// Возврат результатов расчёта МАКК
        /// </summary>
        List<MAKKItem> GetResultCalcMAKK();

        /// <summary>
        /// Вернуть сообщение об ошибке
        /// </summary>
        /// <returns></returns>
        string GetError();

        /// <summary>
        /// Получить опции МАКК
        /// </summary>
        /// <returns></returns>
        List<MAKKOptions> GetOptions();

        /// <summary>
        /// Изменить список Опций МАКК
        /// </summary>
        bool SetOptions(List<MAKKOptions> options);

        /// <summary>
        /// Получить дополнительные комплектующие для МАКК
        /// </summary>
        /// <returns></returns>
        List<MAKKOptions> GetAddEquip();

        /// <summary>
        /// Отмеченный список дополнительных комплектующих МАКК
        /// </summary>
        /// <param name="inAddEquip"></param>
        /// <returns></returns>
        bool SetAddEquip(List<MAKKOptions> inAddEquip);

        /// <summary>
        /// получении бланк-заказа МАКК
        /// </summary>
        void GetOrderForm();

        /// <summary>
        /// Получение списка МАКК 
        /// </summary>
        List<ListMAKKParamsDTO> GetMAKKs();

        /// <summary>
        /// Получить параметры MAKK
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        MAKKParamsDTO GetParamsMAKK(string name);

        /// <summary>
        /// Передать изменённые параметры MAKK
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool SetParamsMAKK(MAKKParamsDTO param);

        /// <summary>
        /// Установка свойств
        /// </summary>
        /// <param name="inputParams"></param>
        void SetParams(InputDataMAKK inputParams);
        /// <summary>
        /// расчёт Конденсатора
        /// </summary>
        /// <returns></returns>
        bool CalcNewMAKK();

        /// <summary>
        /// Получение результатов расчёта
        /// </summary>
        /// <returns></returns>
        OutputDataMAKK GetResultsCalcNewMAKK();

        /// <summary>
        /// Получить параметры для расчёта нового МАКК
        /// </summary>
        /// <returns></returns>
        InputDataMAKK MAKKGetParams();
    }
}
