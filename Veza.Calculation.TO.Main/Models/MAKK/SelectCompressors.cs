using System;
using Veza.HeatExchanger.Services;

namespace Veza.HeatExchanger.Models.MAKK
{
    public class SelectCompressors : IComparable<SelectCompressors>
    {
        public string Manufacturer { get; set; }
        public string Compessor { get; set; }
        public string RefrigerationCapacity { get; set; }
        public string PowerInput { get; set; }
        public string COP { get; set; }
        public string Current { get; set; }
        public string MassFlow { get; set; }
        public string HeatRejection { get; set; }
        public string Voltage { get; set; }

        public int CompareTo(SelectCompressors other)
        {
            if(other == null)
                return 1;
            double d2 = GS.StringToDouble(other.RefrigerationCapacity);
            double d1 = GS.StringToDouble(RefrigerationCapacity);
            return d1.CompareTo(d2);
        }
    }
}
