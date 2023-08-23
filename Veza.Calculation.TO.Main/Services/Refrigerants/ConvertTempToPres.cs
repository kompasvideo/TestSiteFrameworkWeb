using Veza.HeatExchanger.Exceptions;
using Veza.HeatExchanger.Interfaces.Refrigerants;
using Veza.HeatExchanger.Services.Refrigerants;
using Veza.HeatExchanger.Services.Refrigerants.R410A;
using Veza.HeatExchanger.Services.Refrigerants.R513A;

namespace Veza.HeatExchanger.Services.Refrigerant
{
    sealed public class ConvertTempToPres : IConvertTempToPres
    {
        #region Температура кипения
        /// <summary>
        /// Преобразует температуру в абс. давление  
        /// </summary>
        /// <param name="temperature">температура типа double</param>
        /// <param name="refrigerant">имя хладогента</param>
        /// <returns>абс. давление типа double</returns>
        public double ToPressure(double temperature, string refrigerant)
        {
            IRefrigerantFactory factory = GetIRefrigerantFactory(refrigerant);
            IRefrigerant refr = factory.GetRefrigerant();
            return refr.ToPressure(temperature);
        }

        /// <summary>
        /// Преобразует абс. давление в температуру
        /// </summary>
        /// <param name="pressure">абс. давление типа double</param>
        /// <param name="refrigerant">имя хладогента</param>
        /// <returns>температура типа double</returns>
        public double ToTemperature(double pressure, string refrigerant)
        {
            IRefrigerantFactory factory = GetIRefrigerantFactory(refrigerant);
            IRefrigerant refr = factory.GetRefrigerant();
            return refr.ToTemperature(pressure);
        }
        #endregion

        #region Температура конденсации
        /// <summary>
        /// Преобразует температуру в абс. давление  
        /// </summary>
        /// <param name="temperature">температура типа double</param>
        /// <param name="refrigerant">имя хладогента</param>
        /// <returns>абс. давление типа double</returns>
        public double ToCondPressure(double temperature, string refrigerant)
        {
            IRefrigerantFactory factory = GetIRefrigerantFactory(refrigerant);
            IRefrigerant refr = factory.GetRefrigerant();
            return refr.ToCondPressure(temperature);
        }

        /// <summary>
        /// Преобразует абс. давление в температуру
        /// </summary>
        /// <param name="pressure">абс. давление типа double</param>
        /// <param name="refrigerant">имя хладогента</param>
        /// <returns>температура типа double</returns>
        public double ToCondTemperature(double pressure, string refrigerant)
        {
            IRefrigerantFactory factory = GetIRefrigerantFactory(refrigerant);
            IRefrigerant refr = factory.GetRefrigerant();
            return refr.ToCondTemperature(pressure);
        }
        #endregion

        #region Переохлаждение
        public double ToSubCol(double tempCond, double temperature, string refrigerant)
        {
            IRefrigerantFactory factory = GetIRefrigerantFactory(refrigerant);
            IRefrigerant refr = factory.GetRefrigerant();
            return refr.ToSubCol(tempCond, temperature);
        }


        public double ToSubColTemperature(double tempCond, double tempSubCol, string refrigerant)
        {
            IRefrigerantFactory factory = GetIRefrigerantFactory(refrigerant);
            IRefrigerant refr = factory.GetRefrigerant();
            return refr.ToSubColTemperature(tempCond, tempSubCol);
        }
        #endregion

        private IRefrigerantFactory GetIRefrigerantFactory(string refrigerant)
        {
            switch (refrigerant)
            {
                case "R407C":
                    return new RefrigerantFactoryR407C();
                case "R404A":
                    return new RefrigerantFactoryR404A();
                case "R134a":
                    return new RefrigerantFactoryR134a();
                case "R22":
                    return new RefrigerantFactoryR22();
                case "R410A":
                    return new RefrigerantFactoryR410A();
                case "R513A":
                    return new RefrigerantFactoryR513A();
                default:
                    throw new TempToPresException("неверное название хладогента"); 
            }
        }
    }
}
