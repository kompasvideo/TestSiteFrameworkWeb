using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.SteamHeater;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.SteamHeater
{
    public class CalcDirectSTService : ICalcDirectSTService
    {
        private readonly IExtInputDataSteamHeaterService service;

        public CalcDirectSTService(IExtInputDataSteamHeaterService service)
        {
            this.service = service;
        }

        /// <summary>
        /// прямой расчёт Паровой нагреватель
        /// </summary>
        /// <returns></returns>
        public async Task<object> CalcDirectST(InputDataSteamHeaterDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return await Task.Run(() => service.GetResults());
        }
    }
}
