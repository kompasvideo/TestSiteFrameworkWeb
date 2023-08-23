using System.Collections.Generic;
using Veza.HeatExchanger.Models.MAKK;

namespace Veza.HeatExchanger.BusinessLogic.Compressors.Select_8
{
    public interface ISelect8
    {
        void LoadDll();

        /// <summary>
        /// загружена ли dll
        /// </summary>
        /// <returns></returns>
        bool IsLoadDll();        

        /// <summary>
        /// Подбор компрессора
        /// </summary>
        /// <param name="totCap"></param>
        /// <param name="dxCxMas"></param>
        (SelectCompressors, List<SelectCompressors>) SelectCompessors(double i_TEvap, double i_TOvrH, double i_TCond, 
            double i_TSubC, double totCap, double dxCxMas);
    }
}
