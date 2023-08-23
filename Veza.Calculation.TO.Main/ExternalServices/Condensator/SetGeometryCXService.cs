using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Condensator;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Condensator
{
    public class SetGeometryCXService : ISetGeometryCXService
    {
        private readonly IExtInputDataCondensatorService service;

        public SetGeometryCXService(IExtInputDataCondensatorService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Установить новую геометрия и получить изменённые параметры
        /// </summary>
        /// <returns></returns>
        public async Task<object> SetGeometryCX(InputDataCondensatorDTO dto)
        {
            service.SetProperties(dto);
            service.SetGeometry(dto.SelectGeometry);
            return await Task.Run(() => service.GetChangesGeometryParams());
        }
    }
}
