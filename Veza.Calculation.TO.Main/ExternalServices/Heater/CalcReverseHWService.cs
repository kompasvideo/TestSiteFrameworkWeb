using Veza.HeatExchanger.BusinessLogic.TO.Heater;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.Main;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Heater
{
    public class CalcReverseHWService : ICalcReverseHWService
    {
        private readonly IExtInputDataFluidHeaterService service;
        public CalcReverseHWService(IExtInputDataFluidHeaterService service)
        {
           this.service = service;
        }

        /// <summary>
        /// обратный расчёт Воздухонагревателя
        /// </summary>
        /// <returns></returns>
        public async Task<object> CalcReverseHW(InputDataFluidHeaterCoolerDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return await Task.Run(() => service.GetResults());
        }
    }
}
