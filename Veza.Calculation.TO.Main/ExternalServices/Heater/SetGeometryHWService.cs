using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Heater;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Heater
{
    public class SetGeometryHWService : ISetGeometryHWService
    {
        private readonly IExtInputDataFluidHeaterService service;

        public SetGeometryHWService(IExtInputDataFluidHeaterService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Установить новую геометрия и получить изменённые параметры
        /// </summary>
        /// <returns></returns>
        public async Task<object> SetGeometryHW(InputDataFluidHeaterCoolerDTO inputParams)
        {
            service.SetProperties(inputParams);
            service.SetGeometry(inputParams.SelectGeometry);
            return await Task.Run(() => service.GetChangesGeometryParams());
        }
    }
}
