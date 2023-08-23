using System.Collections.Generic;
using System.Globalization;
using Veza.HeatExchanger.Services;

namespace Veza.HeatExchanger.BusinessLogic.MAKK.Models
{
    public class InputMAKKParams
    {        
        /// <summary>
        /// Список хладогентов
        /// </summary>
        public List<string> Refrigerants { get; set; }
        /// <summary>
        /// Выбранный хладогент
        /// </summary>
        public string SelectRefrigerant { get; set; }
        /// <summary>
        /// Список серий МАКК
        /// </summary>
        public List<SeriesMAKK> SeriesMAKKs { get; set; }

        private string coolingCapacity;
        /// <summary>
        /// Холодопроизводительность в кВт, типа string
        /// </summary>
        public string CoolingCapacity 
        { 
            get => coolingCapacity ?? string.Empty;
            set
            {
                coolingCapacity = value;
                coolingCapacityD = GS.StringToDouble(value);
            }
        }
        private double coolingCapacityD;
        /// <summary>
        /// Холодопроизводительность в кВт, типа double
        /// </summary>
        public double CoolingCapacityD 
        {
            get => coolingCapacityD; 
            set
            {
                coolingCapacityD = value;
                coolingCapacity = value.ToString();
            }
        }

        private string errorRate;
        /// <summary>
        /// Погрешность в %, типа string
        /// </summary>
        public string ErrorRate 
        { 
            get => errorRate ?? string.Empty;
            set
            {
                errorRate = value;
                errorRateD = GS.StringToDouble(value);
            }
        }
        private double errorRateD;
        /// <summary>
        /// Погрешность в %, типа double
        /// </summary>
        public double ErrorRateD
        {
            get => errorRateD;
            set
            {
                errorRateD = value;
                errorRate = value.ToString();
            }
        }

        private string outTemp;
        /// <summary>
        /// Температура окружающей среды, типа string
        /// </summary>
        public string OutTemp 
        { 
            get => outTemp ?? string.Empty;
            set
            {
                outTemp = value;
                outTempD = GS.StringToDouble(value);
            }
        }
        private double outTempD;
        /// <summary>
        /// Температура окружающей среды, типа double
        /// </summary>
        public double OutTempD 
        { 
            get => outTempD;
            set
            {
                outTempD= value;
                outTemp = value.ToString();
            }
        }

        private string evapTemp;
        /// <summary>
        /// Температура кипения, типа string
        /// </summary>
        public string EvapTemp 
        { 
            get => evapTemp ?? string.Empty;
            set
            {
                evapTemp = value;
                evapTempD = GS.StringToDouble(value);
            }
        }
        private double evapTempD;
        /// <summary>
        /// Температура кипения, типа double
        /// </summary>
        public double EvapTempD
        {
            get => evapTempD;
            set
            {
                evapTempD = value;   
                evapTemp =value.ToString();
            }
        }
    }
}
