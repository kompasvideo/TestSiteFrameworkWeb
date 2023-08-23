using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Condensator;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Condensator
{
    public class CalcDirectCXService : ICalcDirectCXService
    {
        private readonly IExtInputDataCondensatorService service;

        public CalcDirectCXService(IExtInputDataCondensatorService service)
        {
           this.service = service;
        }

        /// <summary>
        /// прямой расчёт Конденсатор
        /// </summary>
        /// <returns></returns>
        public async Task<object> CalcDirectCX(InputDataCondensatorDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return await Task.Run(() => service.GetResults());
        }
    }
}
