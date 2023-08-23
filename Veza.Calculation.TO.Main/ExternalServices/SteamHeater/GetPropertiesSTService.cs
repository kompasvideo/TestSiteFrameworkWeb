using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.SteamHeater;

namespace Veza.Calculation.TO.Main.ExternalServices.SteamHeater
{
    public class GetPropertiesSTService : IGetPropertiesSTService
    {
        private readonly IExtInputDataSteamHeaterService service;

        public GetPropertiesSTService(IExtInputDataSteamHeaterService service)
        {
            this.service = service;
        }
        public async Task<object> GetPropertiesST()
        {
            return await Task.Run(() => service.GetProperties());
        }
    }
}
