using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Heater;

namespace Veza.Calculation.TO.Main.ExternalServices.Evaporater
{
    public class GetPropertiesDXService : IGetPropertiesDXService
    {
        private readonly IExtInputDataFluidHeaterService service;

        public GetPropertiesDXService(IExtInputDataFluidHeaterService service)
        {
            this.service = service;
        }
        public async Task<object> GetPropertiesDX()
        {
            return await Task.Run(() => service.GetProperties());
        }

    }
}
