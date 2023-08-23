using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.SteamHeater
{
    public interface ICalcReverseSTService
    {
        /// <summary>
        /// обратный расчёт Паровой нагреватель
        /// </summary>
        /// <returns></returns>
        Task<object> CalcReverseST(InputDataSteamHeaterDTO dto);
    }
}
