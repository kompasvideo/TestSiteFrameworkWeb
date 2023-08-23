using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Heater;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Cooler
{
    public class CalcDirectCWService : ICalcDirectCWService
    {
        private readonly IExtInputDataFluidHeaterService service;

        public CalcDirectCWService(IExtInputDataFluidHeaterService service)
        {
            this.service = service;
        }

        /// <summary>
        /// прямой расчёт Воздухоохладителя
        /// </summary>
        /// <returns></returns>
        public async Task<object> CalcDirectCW(InputDataFluidHeaterCoolerDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return await Task.Run(() => service.GetResults());
        }
    }
}
