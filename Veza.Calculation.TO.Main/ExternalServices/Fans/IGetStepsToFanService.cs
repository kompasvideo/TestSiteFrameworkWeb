using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public interface IGetStepsToFanService
    {
        /// <summary>
        /// Получить вентилятор для редактирования
        /// </summary>
        /// <returns></returns>
        Task<object> GetSteps(string name);
    }
}
