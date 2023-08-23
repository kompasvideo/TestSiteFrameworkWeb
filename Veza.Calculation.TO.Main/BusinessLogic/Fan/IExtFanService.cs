using System.Collections.Generic;
using Veza.HeatExchanger.BusinessLogic.Fan.Models;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.DataBase.Models.FanAddEdit;

namespace Veza.HeatExchanger.BusinessLogic.Fan
{
    public interface IExtFanService
    {
        /// <summary>
        /// Возвраящяет список вентиляторов, список типов, список производителей, список снрий
        /// </summary>
        /// <returns></returns>
        Fans GetListTypesFan();

        /// <summary>
        /// Возвраящяет вентилятор для редактирования
        /// </summary>
        /// <returns></returns>
        FanT GetFanToEdit(string name);

        /// <summary>
        /// Редактировать вентилятор 
        /// </summary>
        /// <param name="fan"></param>
        /// <returns></returns>
        ErrorEdit SetFanToEdit(FanT fan);

        /// <summary>
        /// Получить таблицу скоростей для выбранного вентилятора
        /// </summary>
        /// <param name="name"></param>
        List<FanPointsDB> GetPoints(string name);

        /// <summary>
        /// Сохранить отредактированную таблицу скоростей для выбранного вентилятора
        /// </summary>
        /// <param name="fanPoints"></param>
        void SetPoints(List<FanPointsDB> fanPoints);

        /// <summary>
        /// Получить таблицу "расход воздуха-мощность" для выбранного вентилятора
        /// </summary>
        /// <param name="name"></param>
        List<FanStepEffPowerDB> GetSteps(string name);

        /// <summary>
        /// Сохранить таблицу "расход воздуха-мощность" для выбранного вентилятора
        /// </summary>
        /// <param name="fanSteps"></param>
        void SetSteps(List<FanStepEffPowerDB> fanSteps);

    }
}
