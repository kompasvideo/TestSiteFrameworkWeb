using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Extensions.DependencyInjection;
using Veza.Calculation.TO.Main.ExternalServices.Condensator;
using Veza.Calculation.TO.Main.ExternalServices.Cooler;
using Veza.Calculation.TO.Main.ExternalServices.Evaporater;
using Veza.Calculation.TO.Main.ExternalServices.Heater;
using Veza.Calculation.TO.Main.ExternalServices.SteamHeater;
using Veza.Calculation.TO.Main;
using TestSiteFrameworkWeb.Controllers;
using System.IO;
using System.Reflection;

namespace TestSiteFrameworkWeb
{
    public class WebApiApplication : HttpApplication
    {
        private static ServiceProvider _provider;
        protected void Application_Start()
        {            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // внедрение зависимостей
            Init();
        }

        public static void Init()
        {
            var services = new ServiceCollection();
            services.AddTransient<IGetPropertiesHWService, GetPropertiesHWService>();
            services.AddTransient<ICalcDirectHWService, CalcDirectHWService>();
            services.AddTransient<ICalcReverseHWService, CalcReverseHWService>();
            services.AddTransient<ISetGeometryHWService, SetGeometryHWService>();
            services.AddTransient<IGetPropertiesCWService, GetPropertiesCWService>();
            services.AddTransient<ICalcDirectCWService, CalcDirectCWService>();
            services.AddTransient<ICalcReverseCWService, CalcReverseCWService>();
            services.AddTransient<ISetGeometryCWService, SetGeometryCWService>();
            services.AddTransient<IGetPropertiesSTService, GetPropertiesSTService>();
            services.AddTransient<ICalcDirectSTService, CalcDirectSTService>();
            services.AddTransient<ICalcReverseSTService, CalcReverseSTService>();
            services.AddTransient<ISetGeometrySTService, SetGeometrySTService>();
            services.AddTransient<IGetPropertiesCXService, GetPropertiesCXService>();
            services.AddTransient<ICalcDirectCXService, CalcDirectCXService>();
            services.AddTransient<ICalcReverseCXService, CalcReverseCXService>();
            services.AddTransient<ISetGeometryCXService, SetGeometryCXService>();
            services.AddTransient<IGetPropertiesDXService, GetPropertiesDXService>();
            services.AddTransient<ICalcDirectDXService, CalcDirectDXService>();
            services.AddTransient<ICalcReverseDXService, CalcReverseDXService>();
            services.AddTransient<ISetGeometryDXService, SetGeometryDXService>();
            services.AddTransient<CalcController>();

            ViewModelLocator.Init(services);


            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
    }
}
