using Veza.HeatExchanger.BusinessLogic.TO.Heater;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Heater
{
    public class GetPropertiesHWService : IGetPropertiesHWService
    {
        private readonly IExtInputDataFluidHeaterService service;

        public GetPropertiesHWService(IExtInputDataFluidHeaterService service)
        {
            this.service = service;
        }
        GetDataFluidHeaterCoolerDTO IGetPropertiesHWService.GetPropertiesHW()
        {
            return service.GetProperties();
        }
    }
}
