using System.Globalization;
using System.Linq;

namespace Veza.HeatExchanger.Services
{
    internal static class GS
    {
        /// <summary>
        /// Имя культуры
        /// </summary>
        private static string nameCulture;
        private static CultureInfo cultureRu = new CultureInfo("ru-RU");
        private static CultureInfo cultureEn = CultureInfo.InvariantCulture;

        static GS()
        {
            nameCulture = CultureInfo.CurrentCulture.Name;
        }

        public static string DoubleToString(double d)
        {
            return d.ToString();
        }

        public static double StringToDouble(string s)
        {
            double d = 0;
            if (s == null) return 0;
            if (double.TryParse(s, out d))                
            { 
                return d; 
            }
            else if (s.Contains(','))
            {
                if (double.TryParse(s, NumberStyles.AllowDecimalPoint, cultureRu, out d))
                {
                    return d;
                }
            }
            else
            {
                if (double.TryParse(s, NumberStyles.AllowDecimalPoint, cultureEn, out d))
                {
                    return d;
                }
            }
            return double.NaN;
        }
        public static bool IsCultureRU()
        {
            if (Calculation.TO.Main.Properties.Resources.Culture.Name == "ru-RU") return true;
            return false;
        }

    }
}
