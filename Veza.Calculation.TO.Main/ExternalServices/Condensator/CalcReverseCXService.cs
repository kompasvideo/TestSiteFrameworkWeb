﻿using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.TO.Condensator;
using Veza.HeatExchanger.Models.Main;

namespace Veza.Calculation.TO.Main.ExternalServices.Condensator
{
    public class CalcReverseCXService : ICalcReverseCXService
    {
        private readonly IExtInputDataCondensatorService service;

        public CalcReverseCXService(IExtInputDataCondensatorService service)
        {
            this.service = service;
        }

        /// <summary>
        /// обратный расчёт Конденсатор
        /// </summary>
        /// <returns></returns>
        public async Task<object> CalcReverseCX(InputDataCondensatorDTO dto)
        {
            service.SetProperties(dto);
            service.Calc();
            return await Task.Run(() => service.GetResults());
        }
    }
}