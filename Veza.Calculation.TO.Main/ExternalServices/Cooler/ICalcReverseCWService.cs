using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Cooler
{
    public interface ICalcReverseCWService
    {
        /// <summary>
        /// обратный расчёт Воздухоохладителя
        /// </summary>
        /// <returns></returns>
        Task<object> CalcReverseCW(InputDataFluidHeaterCoolerDTO dto);
    }
}
