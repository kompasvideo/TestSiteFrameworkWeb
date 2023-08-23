using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.Fan.Models;
using Veza.HeatExchanger.DataBase.Models;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public interface ISetFanToEditService
    {
        /// <summary>
        /// Редактировать вентилятор 
        /// </summary>
        /// <param name="fan"></param>
        /// <returns></returns>
        Task<object> SetFanToEdit(FanT fan);
    }
}
