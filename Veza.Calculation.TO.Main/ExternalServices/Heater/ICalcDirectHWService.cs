using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Heater
{
    public interface ICalcDirectHWService
    {
        /// <summary>
        /// прямой расчёт Воздухонагревателя
        /// </summary>
        /// <returns></returns>
        OutputDataFluidHeaterCoolerDTO CalcDirectHW(InputDataFluidHeaterCoolerDTO dto);
    }
}
