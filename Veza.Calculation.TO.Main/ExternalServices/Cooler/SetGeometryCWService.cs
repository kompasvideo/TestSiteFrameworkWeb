using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Heater;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Cooler
{
    public class SetGeometryCWService : ISetGeometryCWService
    {
        private readonly IExtInputDataFluidHeaterService service;

        public SetGeometryCWService(IExtInputDataFluidHeaterService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Установить новую геометрия и получить изменённые параметры
        /// </summary>
        /// <returns></returns>
        public async Task<object> SetGeometryCW(InputDataFluidHeaterCoolerDTO inputParams)
        {
            service.SetProperties(inputParams);
            service.SetGeometry(inputParams.SelectGeometry);
            return await Task.Run(() => service.GetChangesGeometryParams());
        }
    }
}
