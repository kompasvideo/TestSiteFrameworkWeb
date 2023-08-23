using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veza.HeatExchanger.DataBase.Models
{
    sealed internal class SelectedFans
    {
        /// <summary>
        /// модель из БД
        /// </summary>
        public FanModelsDB fans { get; set; }

        /// <summary>
        /// Разница между максимальным расходом воздуха вентилятора и необходимым
        /// </summary>
        public double MaxAirFlowDifference { get; set; }

        /// <summary>
        /// расход воздуха при заданном давлении
        /// </summary>
        public double AirFlow { get; set; }

        /// <summary>
        /// Разница между расходом воздуха вентилятора и необходимым для заданного давления
        /// </summary>
        public double AirFlowDifference { get; set; }
        
        /// <summary>
        /// Давление
        /// </summary>
        public double Pressure { get; set; }
    }
}
