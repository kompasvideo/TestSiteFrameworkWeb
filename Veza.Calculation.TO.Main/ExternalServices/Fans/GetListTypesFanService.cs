using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public class GetListTypesFanService : IGetListTypesFanService
    {

        public GetListTypesFanService()
        {
        }

        /// <summary>
        /// Получить список типов вентиляторов
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetListTypesFan()
        {
            //return await Task.Run(() =>
            //{
            //    return calcTO.GetFanService().GetListTypesFan();
            //});
            object ret = null;
            return await Task.Run(() => ret);
        }
    }
}
