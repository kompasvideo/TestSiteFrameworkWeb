using Veza.HeatExchanger.BusinessLogic.TO.SteamHeater;
using Veza.HeatExchanger.Models.Main;
using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.SteamHeater
{
    public class CalcReverseSTService : ICalcReverseSTService
    {
        private readonly IExtInputDataSteamHeaterService service;

        public CalcReverseSTService(IExtInputDataSteamHeaterService service)
        {
            this.service = service;
        }

        /// <summary>
        /// обратный расчёт Паровой нагреватель
        /// </summary>
        /// <returns></returns>
        public async Task<object> CalcReverseST(InputDataSteamHeaterDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return await Task.Run(() => service.GetResults());
        }
    }
}
