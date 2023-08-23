using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veza.HeatExchanger.DataBase.Models.DTO
{
    sealed public class FanStepsDTO
    {
        public ushort Step { get; set; }
        public ushort StatPressure { get; set; }
        public ushort TotalPressure { get; set; }
        public float EffFacto1 { get; set; }
        public float EffFacto2 { get; set; }
        public float PowerInput { get; set; }
    }
}
