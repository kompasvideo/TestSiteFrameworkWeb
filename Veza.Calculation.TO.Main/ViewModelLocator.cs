using Microsoft.Extensions.DependencyInjection;
using Veza.HeatExchanger.Services.Refrigerant;
using Veza.HeatExchanger.DataBase;
using Veza.HeatExchanger.DataBase.Interafces;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Services.Heater;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Services;
using Veza.HeatExchanger.Interfaces.Refrigerants;
using Veza.HeatExchanger.ViewModels.Controls;
using Veza.HeatExchanger.BusinessLogic.Compressors.InvoTech;
using Veza.HeatExchanger.BusinessLogic.Compressors.Models;
using Veza.HeatExchanger.BusinessLogic.Compressors.Select_8;
using Veza.HeatExchanger.Services.Cooler;
using Veza.HeatExchanger.Services.SteamHeater;
using Veza.HeatExchanger.Mappers;
using Veza.HeatExchanger.BusinessLogic.TO.Heater;
using Veza.HeatExchanger.BusinessLogic.TO.Cooler;
using Veza.HeatExchanger.Interfaces.Mappers;
using Veza.HeatExchanger.BusinessLogic.TO.SteamHeater;
using Veza.HeatExchanger.BusinessLogic.TO.Condensator;
using Veza.HeatExchanger.BusinessLogic.TO.Evaporater;
using Veza.HeatExchanger.BusinessLogic.Fan;
using Veza.HeatExchanger.BusinessLogic.MAKK;
using Veza.Calculation.TO.Main.Interfaces;

namespace Veza.Calculation.TO.Main
{
    sealed public class ViewModelLocator
    {
        public static void Init(IServiceCollection services)
        {
            services.AddScoped<AdvanceParamsViewModel>();
            services.AddTransient<MessageBus>();
            services.AddTransient<ICalculate, Calculate>();
            services.AddTransient<ITableDataService, TableDataService>();
            services.AddScoped<IInputData, InputData>();             
            services.AddTransient<TableData>();
            services.AddTransient<PrintData>();
            services.AddTransient<ISwitchLanguageService, SwitchLanguageService>();
            services.AddTransient<IInputDataFluidHeaterService, InputDataFluidHeaterService>();
            services.AddTransient<IInputDataFluidCoolerService, InputDataFluidCoolerService>();
            services.AddTransient<IInputDataSteamHeaterService, InputDataSteamHeaterService>();
            services.AddTransient<IInputDataCondensatorService, InputDataCondensatorService>();
            services.AddTransient<IInputDataEvaporaterService, InputDataEvaporaterService>();
            services.AddTransient<IInputDataService, InputDataService>();
            services.AddScoped<OutView>();
            services.AddTransient<IConvertTempToPres, ConvertTempToPres>();
            services.AddTransient<IFan, Fan>();
            services.AddTransient<IMakk, Makk>();
            services.AddTransient<IFanSelection, FanSelection>();
            services.AddScoped<ILogs, Logs>();
            services.AddTransient<IInvoTech, InvoTech>();
            services.AddTransient<ISelect8, Select8>();
            services.AddTransient<IFinThk, FinThk>();
            services.AddTransient<InputDataCompressors>();

            services.AddTransient<IFluidHeaterMapper, FluidHeaterMapper>();
            services.AddTransient<IFluidCoolerMapper, FluidCoolerMapper>();
            services.AddTransient<ISteamHeaterMapper, SteamHeaterMapper>();
            services.AddTransient<ICondensatorMapper, CondensatorMapper>();
            services.AddTransient<IEvaporaterMapper, EvaporaterMapper>();
            services.AddTransient<IExtInputDataFluidHeaterService, InputDataFluidHeaterService>();
            services.AddTransient<IExtInputDataFluidCoolerService, InputDataFluidCoolerService>();
            services.AddTransient<IExtInputDataSteamHeaterService, InputDataSteamHeaterService>();
            services.AddTransient<IExtInputDataCondensatorService, InputDataCondensatorService>();
            services.AddTransient<IExtInputDataEvaporaterService, InputDataEvaporaterService>();
            services.AddTransient<IExtFanService, FanService>();
            services.AddTransient<IExtMAKKService, MAKKService>();
            services.AddTransient<ICompressor, Compressor>();           
        }

        public static void Init()
        {
        }
    }
}
