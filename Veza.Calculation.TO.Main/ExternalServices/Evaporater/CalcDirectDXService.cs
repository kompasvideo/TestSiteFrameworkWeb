using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Evaporater;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Evaporater
{
    public class CalcDirectDXService : ICalcDirectDXService
    {
        private readonly IExtInputDataEvaporaterService service;

        public CalcDirectDXService(IExtInputDataEvaporaterService service)
        {
           this.service = service;
        }

        /// <summary>
        /// прямой расчёт Испаритель
        /// </summary>
        /// <returns></returns>
        public async Task<object> CalcDirectDX(InputDataEvaporaterDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return await Task.Run(() => service.GetResults());
        }
    }
}
