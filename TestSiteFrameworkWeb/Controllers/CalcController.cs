using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Veza.HeatExchanger.Models.Main;
using Veza.Calculation.TO.Main.ExternalServices.Heater;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Veza.HeatExchanger.Models.MAKK;

namespace TestSiteFrameworkWeb.Controllers
{
    public class CalcController : ApiController
    {
        private static ICalcDirectHWService _service;

        public CalcController(ICalcDirectHWService service)
        {
            _service = service;
        }

        public CalcController()
        {
            
        }

        // GET: Calc
        public string Get()
        {
            //Server.MapPath("~/");
            InputDataFluidHeaterCoolerDTO testParams = new InputDataFluidHeaterCoolerDTO
            {
                SelectedMode = "direct",
                SelectedDirectMode = "Мощность",
                ValueCapacity = "230",
                SelectGeometry = "5012",
                ValueWidthFin = "1400",
                ValueHightFin = "1200",
                TubesN = "24",
                SelectedPipe = "12.00 x 0.32 мм Медь",
                SelectedFin = "0.12 мм Алюминий",
                SelectedStepFin = "2.5 мм",
                ValueAirFlow = "21700",
                I_AirTempIn = "-2",
                ValueBaseHum = "85",
                SelectedFluid = "Вода",
                I_SoleCnz = "0",
                ValueMedTempIn = "90",
                SelectedVariantRasch = "Температура теплоносителя на выходе",
                ValueVariantRasch = "70",
                ValueMedKPa = "50",
                // характеристики корпуса и коллектора
                I_HdrQta = "1",
                Selected_I_MatHdr = "Медь",
                Selected_I_ConIn = "Оптимизировать",
                Selected_I_ConOut = "Оптимизировать",
                Selected_I_ConType = "Заглушенный патрубок",
                I_CSheetL = "1",
                SelectedCasingType = "Для установки в кондиционере типа ВЕРОСА 200, 251, 300, 700",
                Selected_I_CasMat = "Оцинкованная сталь без покрытия",
                SelectedEsapo = "Вертикальное расположение, противоток, подключение к нижнему патрубку",
                Select_I_CDir = "Стандартная (перпендикулярно потоку воздуха в сторону)",
                SelectedAirFlowDirection = "Правый",
                SelectedConnectingCoolant = "Выход коллекторов на одну и ту же сторону",
                // Дополнительные параметры
                BaseBar = "1013.25",
                SelectGasWorkPressUnit = "мбар",
                I_BaseDens = "1.2",
                I_ARes = "0",
                I_LRes = "0",
                Select_I_FoulingI = "Деминерализованная вода",
                Select_I_FoulingE = "Чистый операционный воздух",
                SelectAFPV = "Нет",
            };
            Veza.HeatExchanger.Models.StaticData.Path = Static.Path;
            OutputDataFluidHeaterCoolerDTO res = _service.CalcDirectHW(testParams);            
            return JsonConvert.SerializeObject(res);
        }
    }
}