using Veza.HeatExchanger.BusinessLogic.TO.Heater;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Heater
{
    public class CalcDirectHWService : ICalcDirectHWService
    {
        private readonly IExtInputDataFluidHeaterService service;

        public CalcDirectHWService(IExtInputDataFluidHeaterService service)
        {
            this.service = service;
        }

        /// <summary>
        /// прямой расчёт Воздухонагревателя
        /// </summary>
        /// <returns></returns>
        public OutputDataFluidHeaterCoolerDTO CalcDirectHW(InputDataFluidHeaterCoolerDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return service.GetResults();
        }
    }
}
