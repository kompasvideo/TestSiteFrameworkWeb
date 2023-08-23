using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.Fan.Models;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public class SetFanToEditService : ISetFanToEditService
    {

        public SetFanToEditService()
        {
        }

        /// <summary>
        /// Редактировать вентилятор 
        /// </summary>
        /// <param name="fan"></param>
        /// <returns></returns>
        public async Task<object> SetFanToEdit(FanT fan)
        {
            //return await Task.Run(() =>
            //{
            //    return calcTO.GetFanService().SetFanToEdit(fan);
            //});
            object ret = null;
            return await Task.Run(() => ret);
        }

    }
}
