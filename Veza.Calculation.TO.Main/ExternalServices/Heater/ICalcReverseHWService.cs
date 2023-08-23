using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Heater
{
    public interface ICalcReverseHWService
    {
        /// <summary>
        /// обратный расчёт Воздухонагревателя
        /// </summary>
        /// <returns></returns>
        Task<object> CalcReverseHW(InputDataFluidHeaterCoolerDTO dto);
    }
}
