using System.Globalization;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Services;

namespace Veza.HeatExchanger.Models
{
    public class FinThk : IFinThk
    {
        int FinThickness { get; set; }

        /// <summary>
        /// Сохраняем толщину трубки для проверки с ней выходных результатов
        /// если указать её в входных параметрах получаем ошиибку
        /// 8: Fin thickness wrong
        /// </summary>
        /// <param name="fins"></param>
        public void SetFinThickness(string fins)
        {
            double d = GS.StringToDouble(fins);
            FinThickness = (int)(d * 100);
        }

        /// <summary>
        /// Проверяем толщину трубки из подобранных результатов с заданной
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool IsFinThickness(OutViewLines line)
        {
            string str = line.O_Code.Split('/')[2].Split('x')[1].Substring(0, 2);
            int res;
            if (int.TryParse(str, out res))
            {
                if (res == FinThickness)
                    return true;
            }
            return false;
        }
    }
}
