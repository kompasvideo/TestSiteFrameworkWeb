using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Evaporater;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Evaporater
{
    public class CalcReverseDXService : ICalcReverseDXService
    {
        private readonly IExtInputDataEvaporaterService service;

        public CalcReverseDXService(IExtInputDataEvaporaterService service)
        {
            this.service = service;
        }

        /// <summary>
        /// обратный расчёт Испаритель
        /// </summary>
        /// <returns></returns>
        public async Task<object> CalcReverseDX(InputDataEvaporaterDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return await Task.Run(() => service.GetResults());
        }
    }
}
