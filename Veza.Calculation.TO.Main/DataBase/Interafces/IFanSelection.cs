using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veza.HeatExchanger.DataBase.Models;

namespace Veza.HeatExchanger.Interfaces
{
    public interface IFanSelection
    {
        /// <summary>
        /// Поиск подходящих вентиляторов
        /// </summary>
        /// <param name="I_AirFlow"></param>
        /// <param name="I_AirFlowMax"></param>
        /// <param name="PresDropDry"></param>
        /// <returns></returns>
        IList<FanDTO> FanSearch(double I_AirFlow, double I_AirFlowMax, string PresDropDry);

        /// <summary>
        /// расхожд воздуха для выбранного вентилятора при перепаде давления
        /// </summary>
        /// <param name="fanName"></param>
        /// <returns></returns>
        string GetFanAirFlowAtPresDrop(string fanName);
    }
}
