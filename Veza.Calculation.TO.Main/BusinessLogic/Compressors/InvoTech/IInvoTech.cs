using System.Collections.Generic;
using Veza.HeatExchanger.Models.MAKK;

namespace Veza.HeatExchanger.BusinessLogic.Compressors.InvoTech
{
    public interface IInvoTech
    {
        void LoadDll();
        void SetParams(double i_TEvap, double i_TOvrH, double i_TCond, double i_TSubC);

        /// <summary>
        /// Подбор компрессора
        /// </summary>
        /// <param name="totCap"></param>
        /// <param name="dxCxMas"></param>
        (SelectCompressors, List<SelectCompressors>) SelectCompessors(double totCap, double dxCxMas);
    }
}
