using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Evaporater;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Evaporater
{
    public class SetGeometryDXService : ISetGeometryDXService
    {
        private readonly IExtInputDataEvaporaterService service;

        public SetGeometryDXService()
        {
            this.service = service;
        }

        /// <summary>
        /// Установить новую геометрия и получить изменённые параметры
        /// </summary>
        /// <returns></returns>
        public async Task<object> SetGeometryDX(InputDataEvaporaterDTO dto)
        {
            service.SetProperties(dto);
            service.SetGeometry(dto.SelectGeometry);
            return await Task.Run(() => service.GetChangesGeometryParams());
        }
    }
}
