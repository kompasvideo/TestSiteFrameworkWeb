using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public class GetFanToEditService : IGetFanToEditService
    {

        public GetFanToEditService()
        {
        }

        /// <summary>
        /// Получить вентилятор для редактирования
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetFanToEdit(string name)
        {
            //return await Task.Run(() =>
            //{
            //    return calcTO.GetFanService().GetFanToEdit(name);
            //});
            object ret = null;
            return await Task.Run(() => ret);
        }

    }
}
