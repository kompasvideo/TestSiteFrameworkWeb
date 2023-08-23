using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.SteamHeater;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.SteamHeater
{
    public class SetGeometrySTService : ISetGeometrySTService
    {
        private readonly IExtInputDataSteamHeaterService service;

        public SetGeometrySTService(IExtInputDataSteamHeaterService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Установить новую геометрия и получить изменённые параметры
        /// </summary>
        /// <returns></returns>
        public async Task<object> SetGeometryST(InputDataSteamHeaterDTO dto)
        {
            service.SetProperties(dto);
            service.SetGeometry(dto.SelectGeometry);
            return await Task.Run(() => service.GetChangesGeometryParams());
        }
    }
}
