using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Heater;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Cooler
{
    public class CalcReverseCWService : ICalcReverseCWService
    {
        private readonly IExtInputDataFluidHeaterService service;

        public CalcReverseCWService(IExtInputDataFluidHeaterService service)
        {
            this.service = service;
        }

        /// <summary>
        /// обратный расчёт Воздухоохладителя
        /// </summary>
        /// <returns></returns>
        public async Task<object> CalcReverseCW(InputDataFluidHeaterCoolerDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return await Task.Run(() => service.GetResults());
        }
    }
}
