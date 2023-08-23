using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Condensator;

namespace Veza.Calculation.TO.Main.ExternalServices.Condensator
{
    public class GetPropertiesCXService : IGetPropertiesCXService
    {
        private readonly IExtInputDataCondensatorService service;

        public GetPropertiesCXService(IExtInputDataCondensatorService service)
        {
            this.service = service;
        }
        public async Task<object> GetPropertiesCX()
        {            
            return await Task.Run(() => service.GetProperties());
        }

    }
}
