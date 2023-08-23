using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public interface IGetListTypesFanService
    {
        /// <summary>
        /// Получить список типов вентиляторов
        /// </summary>
        /// <returns></returns>
        Task<object> GetListTypesFan();
    }
}
