using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Heater;

namespace Veza.Calculation.TO.Main.ExternalServices.Cooler
{
    public class GetPropertiesCWService : IGetPropertiesCWService
    {
        private readonly IExtInputDataFluidHeaterService service;

        public GetPropertiesCWService(IExtInputDataFluidHeaterService service)
        {
            this.service = service;
        }
        public async Task<object> GetPropertiesCW()
        {
            return await Task.Run(() => service.GetProperties());
        }
    }
}
