using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veza.HeatExchanger.Interfaces.Refrigerants
{
    public interface IConvertTempToPres
    {
        #region Температура кипения
        /// <summary>
        /// Преобразует температуру в абс. давление для кипения   
        /// </summary>
        /// <param name="temperature">температура типа double</param>
        /// <param name="refrigerant">имя хладогента</param>
        /// <returns>абс. давление типа double</returns>
        double ToPressure(double temperature, string refrigerant);

        /// <summary>
        /// Преобразует абс. давление в температуру для кипения 
        /// </summary>
        /// <param name="pressure">абс. давление типа double</param>
        /// <param name="refrigerant">имя хладогента</param>
        /// <returns>температура типа double</returns>
        double ToTemperature(double pressure, string refrigerant);
        #endregion

        #region Температура конденсации
        /// <summary>
        /// Преобразует температуру в абс. давление для конденсации
        /// </summary>
        /// <param name="temperature">температура типа double</param>
        /// <param name="refrigerant">имя хладогента</param>
        /// <returns>абс. давление типа double</returns>
        double ToCondPressure(double temperature, string refrigerant);

        /// <summary>
        /// Преобразует абс. давление в температуру для конденсации 
        /// </summary>
        /// <param name="pressure">абс. давление типа double</param>
        /// <param name="refrigerant">имя хладогента</param>
        /// <returns>температура типа double</returns>
        double ToCondTemperature(double pressure, string refrigerant);
        #endregion

        #region Переохлаждение
        /// <summary>
        /// Преобразует температуру жидкости в переохлаждение
        /// </summary>
        /// <param name="temperature"></param>
        /// <returns></returns>
        double ToSubCol(double tempCond, double temperature, string refrigerant);

        /// <summary>
        /// преобразует переохлаждение в температуру жидкости
        /// </summary>
        /// <param name="tempSubCol"></param>
        /// <returns></returns>
        double ToSubColTemperature(double tempCond, double tempSubCol, string refrigerant);
        #endregion
    }
}
