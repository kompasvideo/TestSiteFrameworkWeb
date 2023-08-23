using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veza.HeatExchanger.Interfaces.Refrigerants
{
    interface IRefrigerant
    {
        #region Температура кипения
        double ToPressure(double temperature);
        double ToTemperature(double pressure);
        #endregion

        #region Температура конденсации
        double ToCondPressure(double temperature);
        double ToCondTemperature(double pressure);
        #endregion

        #region Переохлаждение
        double ToSubCol(double tempCond, double temperature);
        double ToSubColTemperature(double tempCond, double tempSubCol);
        #endregion
    }
}
