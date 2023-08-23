using System.Collections.Generic;
using Veza.Calculation.TO.Main.Interfaces;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Interfaces.Mappers;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.Main;
using Veza.HeatExchanger.ViewModels.Controls;

namespace Veza.HeatExchanger.Mappers
{
    public class EvaporaterMapper : IEvaporaterMapper
    {
        #region Внутренние поля
        private readonly IInputData inputData;
        private readonly AdvanceParamsViewModel advanceParamsViewModel;
        private readonly IInputDataService inputDataService;
        #endregion

        #region Конструктор

        public EvaporaterMapper(AdvanceParamsViewModel advanceParamsViewModel, IInputDataService inputDataService, 
            IInputData inputData)
        {
            this.inputData = inputData;
            this.advanceParamsViewModel = advanceParamsViewModel;
            this.inputDataService = inputDataService;
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Установка входных параметров
        /// SelectedMode - тип расчёта : direct - прямой, reverse - обратный
        /// DirectMode - список (мощность, температура воздуха на выходе)
        /// ValueCapacityHW - значение DirectMode
        /// SelectGeometry - геометрия
        /// ValueWidthFin - длина оребрения
        /// ValueHightFin - высота оребрения
        /// TubesN - число труб
        /// SelectedPipe - трубка
        /// SelectedFin - толщина оребрения
        /// SelectedStepFin - шаг оребрения
        /// ValueAirFlowHW - расход воздуха
        /// I_AirTempInHW - температура воздуха на входе
        /// ValueBaseHumHW - влажность воздуха на входе
        /// Selected_I_RefT - Тип хладогента
        /// Selected_I_FoulingI  - Коэффициент загрязнения трубы
        /// I_THotGasDX - Температура горячего газа
        /// SelectEvapTemp - Список температура кипения или давление кипения абс.
        /// I_TEvapDX - Температура кипения
        /// EvapAbsPresDX - давление кипения абс.
        /// SelectSuctOvrheat - Список перегрев всас. газа или температура всас. газа
        /// I_TOvrHDX - температура всас. газа
        /// SuctGasReturnDX - перегрев всас. газа
        /// SelectCondTemp - список - температура конденсации или давление конденсации абс.
        /// I_TCondDX - температура конденсации - значение
        /// CondAbsPresDX - давление конденсации абс. - значение 
        /// SelectSubCool - список переохлаждение или температура жидкости
        /// I_TSubCDX - переохлаждение 
        /// LiquidTempDX - температура жидкости
        /// I_RCircDX - количество контуров хладогента
        /// I_DxBypDX - пропускная способность байпаса
        /// ValuePipe - число рядов
        /// ValueCircuits - число отводов
        /// NumOfPasses - число ходов
        /// 
        /// --------   характеристики корпуса и коллектора   ---------
        /// I_MatHdr - количество коллекторов
        /// Selected_I_MatHdr - материал коллектора
        /// Selected_I_ConIn - диаметр коллектора на входе
        /// Selected_I_ConOut - диаметр коллектора на выходе
        /// Selected_I_ConType - тип патрубков
        /// I_CSheetL - припуск на обработку по длине коллектора
        /// SelectedCasingType - исполнение корпуса
        /// Selected_I_CasMat - материал корпуса
        /// SelectedEsapo = ориентация ткплообменника
        /// Select_I_CDir - ориентация патрубков
        /// SelectedAirFlowDirection - направление потока воздуха
        /// SelectedConnectingCoolant - подключение теплоносителя
        /// 
        /// -----------  Дополнительные параметры ------------
        /// BaseBar - давление окружающего воздуха
        /// SelectGasWorkPressUnit - ед. низмерения давление окружающего воздуха
        /// I_BaseDens - плотность окружающего овздуха
        /// I_ARes - запас поверхности
        /// I_LRes - резерв мощности
        /// Select_I_FoulingI - коэффициент загрязнения трубы
        /// Select_I_FoulingE - коэффициент загрязнения воздуха
        /// SelectAFPV - автоматический шаг оребрения
        /// </summary>
        /// <returns></returns>
        public void InputToInputDataClass(InputDataEvaporaterDTO inputParams)
        {
            //"SelectedMode"  тип расчёта -
            // direct - прямой
            // reverse - обратный
            inputData.SelectedDirect = inputParams.SelectedDirectMode;
            if (!string.IsNullOrEmpty(inputParams.SelectedMode))
                inputDataService.SetValueCapacityDX(inputParams.ValueCapacity);
            else
                inputData.SelectGeometry = inputParams.SelectGeometry;
            inputDataService.SetValueWidthFin(inputParams.ValueWidthFin);
            inputDataService.SetValueHightFin(inputParams.ValueHightFin);
            inputDataService.SetTubesN(inputParams.TubesN);
            inputData.SelectedPipe = inputParams.SelectedPipe;
            inputData.SelectedFin = inputParams.SelectedFin;
            inputData.SelectedStepFin = inputParams.SelectedStepFin;
            inputDataService.SetValueAirFlowDX(inputParams.ValueAirFlow);
            inputDataService.SetI_AirTempInDX(inputParams.I_AirTempIn);
            inputDataService.SetValueBaseHumDX(inputParams.ValueBaseHum);
            inputData.Selected_I_RefT = inputParams.Selected_I_RefT;
            inputData.Selected_I_FoulingI = inputParams.Selected_I_FoulingI;
            //inputDataService.SetI_THotGasDX(inputParams.I_THotGasDX);
            inputData.SelectEvapTemp = inputParams.SelectEvapTemp;
            inputDataService.SetI_TEvapDX(inputParams.I_TEvapDX);
            inputDataService.SetEvapAbsPresDX(inputParams.EvapAbsPresDX);
            inputData.SelectSuctOvrheat = inputParams.SelectSuctOvrheat;
            inputDataService.SetI_TOvrHDX(inputParams.I_TOvrHDX);
            inputDataService.SetSuctGasReturnDX(inputParams.SuctGasReturnDX);
            inputData.SelectCondTemp = inputParams.SelectEvapTemp;
            inputDataService.SetI_TCondDX(inputParams.I_TCondDX);
            inputDataService.SetCondAbsPresDX(inputParams.CondAbsPresDX);
            inputData.SelectSubCool = inputParams.SelectSubCool;
            inputDataService.SetI_TSubCDX(inputParams.I_TSubCDX);
            inputDataService.SetLiquidTempDX(inputParams.LiquidTempDX);
            inputDataService.SetI_RCircDX(inputParams.I_RCircDX);
            inputDataService.SetI_DxBypDX(inputParams.I_DxBypDX);
            inputDataService.SetValuePipe(inputParams.ValuePipe);
            inputDataService.SetValueCircuits(inputParams.ValueCircuits);
            inputDataService.SetNumOfPasses(inputParams.NumOfPasses);

            // характеристики корпуса и коллектора
            inputDataService.SetI_MatHdr(inputParams.I_MatHdr);
            inputData.Selected_I_MatHdr = inputParams.Selected_I_MatHdr;
            inputData.Selected_I_ConIn = inputParams.Selected_I_ConIn;
            inputData.Selected_I_ConOut = inputParams.Selected_I_ConOut;
            inputData.Selected_I_ConType = inputParams.Selected_I_ConType;
            inputDataService.SetI_CSheetL(inputParams.I_CSheetL);
            inputData.SelectedCasingType = inputParams.SelectedCasingType;
            inputData.Selected_I_CasMat = inputParams.Selected_I_CasMat;
            inputData.SelectedEsapo = inputParams.SelectedEsapo;
            inputData.Select_I_CDir = inputParams.Select_I_CDir;
            inputData.SelectedAirFlowDirection = inputParams.SelectedAirFlowDirection;
            inputData.SelectedConnectingCoolant = inputParams.SelectedConnectingCoolant;

            // Дополнительные параметры
            advanceParamsViewModel.SetBaseBar(inputParams.BaseBar);
            advanceParamsViewModel.SelectGasWorkPressUnit = inputParams.SelectGasWorkPressUnit;
            advanceParamsViewModel.SetI_BaseDens(inputParams.I_BaseDens);
            advanceParamsViewModel.SetI_ARes(inputParams.I_ARes);
            advanceParamsViewModel.SetI_LRes(inputParams.I_LRes);
            //advanceParamsViewModel.Select_I_FoulingI = inputParams.Select_I_FoulingI;
            advanceParamsViewModel.Select_I_FoulingE = inputParams.Select_I_FoulingE;
            advanceParamsViewModel.SelectAFPV = inputParams.SelectAFPV;
        }
        /// <summary>
        /// Установка входных параметров
        /// SelectedMode - тип расчёта : direct - прямой, reverse - обратный
        /// ListMode - список тип расчёта : direct - прямой, reverse - обратный
        /// SelectedDirectMode - выбранный элемент из списка (мощность, температура воздуха на выходе)
        /// ValueCapacityDX - значение DirectMode
        /// ValueCapacityMinMax - Мощность граничные условия
        /// SelectGeometry - геометрия
        /// ListGeometry - список геометрий
        /// ValueWidthFin - длина оребрения
        /// ValueWidthFinMinMax - Длина оребрения граничные условия
        /// ValueHightFin - высота оребрения
        /// ValueHightFinMinMax - Высота оребрения граничные условия
        /// TubesN - число труб
        /// TubesNMinMax - Число труб граничные условия
        /// SelectedPipe - трубка
        /// ListPipes - список трубок
        /// SelectedFin - толщина оребрения
        /// ListFins - список толщин оребрения
        /// SelectedStepFin - шаг оребрения
        /// ListStepsFin - список шагов оребрения
        /// ValuePipe - число рядов
        /// ValuePipeMinMax - Число рядов граничные условия
        /// ValueCircuits - число отводов
        /// ValueCircuitsMinMax - Количество отводов граничные условия
        /// NumOfPasses - число ходов
        /// NumOfPassesMinMax - Число ходов граничные условия
        /// ValueAirFlowDX - расход воздуха
        /// ValueAirFlowMinMax- Расход воздуха граничные условия
        /// ListUnitsAirFlow - список единиц измерения расхода воздуха
        /// I_AirTempInDX - температура воздуха на входе
        /// I_AirTempInMinMax - Температура воздуха на входе граничные условия
        /// ValueBaseHumDX - влажность воздуха на входе
        /// ValueBaseHumMinMax - Влажность воздуха на входе граничные условия
        /// Selected_I_RefT - Тип хладогента
        /// ListI_RefT - Список хладогентов 
        /// Selected_I_FoulingI  - Коэффициент загрязнения трубы
        /// ListI_FoulingI - Список коэффициентов загрязнения трубы
        /// SelectEvapTemp - Список температура кипения или давление кипения абс.
        /// ListEvaporatingTemperature - список - температура кипения или давление кипения абс.
        /// I_TEvapDX - Температура кипения
        /// I_TEvapDXMinMax - Температура кипения граничные условия
        /// EvapAbsPresDX - давление кипения абс.
        /// EvapAbsPresDXMinMax - давление кипения граничные условия
        /// SelectSuctOvrheat - Список перегрев всас. газа или температура всас. газа
        /// ListSuctOvrheat - Список перегрев всас. газа или температура всас. газа
        /// I_TOvrHDX - температура всас. газа
        /// I_TOvrHDXMinMax - Температура всас газа граничные условия
        /// SuctGasReturnDX - перегрев всас. газа
        /// SuctGasReturnDXMinMax - Перегрев всас газа граничные условия
        /// SelectCondTemp - список - температура конденсации или давление конденсации абс.
        /// ListCondensingTemperature - список - температа конденсации, давление конденсации абс
        /// I_TCondDX - температура конденсации - значение
        /// I_TCondDXMinMax - Температура конденсации граничные условия
        /// CondAbsPresDX - давление конденсации абс. - значение 
        /// CondAbsPresDXMinMax - Давление конденсации граничные условия
        /// SelectSubCool - список переохлаждение или температура жидкости
        /// ListSubCooling - список переохлаждение или температура жидкости
        /// I_TSubCDX - переохлаждение 
        /// I_TSubCDXMinMax - Температура переохлаждения граничные условия
        /// LiquidTempDX - температура жидкости
        /// LiquidTempDXMinMax - Температура жидкости граничные условия
        /// I_RCircDX - Количество контуров хладогента
        /// I_RCircDXMinMax - Количество контуров хладогента граничные условия
        /// I_DxBypDX - Пропускная способность байпаса
        /// I_DxBypDXMinMax - пропускная способность байпаса граничные условия
        /// 
        /// --------   характеристики корпуса и коллектора   ---------
        /// I_MatHdr - количество коллекторов
        /// I_MatHdrMinMax - количество коллекторов граничные условия
        /// Selected_I_MatHdr - материал коллектора
        /// ListI_MatHdr - список материалов коллектора
        /// Selected_I_ConIn - диаметр коллектора на входе
        /// ListI_ConIn - список диаметров коллектора на входе
        /// Selected_I_ConOut - диаметр коллектора на выходе
        /// ListI_ConOut - список диаметров коллектора на выходе
        /// Selected_I_ConType - тип патрубков
        /// ListI_ConType - список типов патрубков
        /// I_CSheetL - припуск на обработку по длине коллектора
        /// SelectedCasingType - исполнение корпуса
        /// ListCasingType - список исполения корпуса
        /// Selected_I_CasMat - материал корпуса
        /// ListI_CasMat - список материалов корпуса
        /// SelectedEsapo = ориентация ткплообменника
        /// ListEsapo - список ориентаций теплообменника
        /// Select_I_CDir - ориентация патрубков
        /// ListI_CDir - список ориентаций патрубков
        /// SelectedAirFlowDirection - направление потока воздуха
        /// ListAirFlowDirection - список направлений потока воздуха
        /// SelectedConnectingCoolant - подключение теплоносителя
        /// ListConnectingCoolant - список подключений теплоносителя
        /// 
        /// -----------  Дополнительные параметры ------------
        /// BaseBar - давление окружающего воздуха
        /// BaseBarMinMax -  Давление окружающего воздуха граничные условия
        /// SelectGasWorkPressUnit - ед. низмерения давление окружающего воздуха
        /// ListGasWorkPressUnit - список единиц измерения давления окр. воздуха
        /// I_BaseDens - плотность окружающего овздуха
        /// I_BaseDensMinMax - Плотность окружающего воздуха граничные условия
        /// I_ARes - запас поверхности
        /// I_AResMinMax - Запас поверхности граничные условия
        /// I_LRes - резерв мощности
        /// I_LResMinMax - Резерв мощности граничные условия
        /// Select_I_FoulingI - коэффициент загрязнения трубы
        /// ListI_FoulingI - список коэффициентов загрязнения трубы
        /// Select_I_FoulingE - коэффициент загрязнения воздуха
        /// ListI_FoulingE - сСписок коэффициентов загрязнения воздуха
        /// SelectAFPV - автоматический шаг оребрения
        /// ListAFPV - список автоматический шаг рёбер
        /// </summary>
        /// <returns></returns>
        public GetDataEvaporaterDTO InputDataClassToInputParams()
        {
            return new GetDataEvaporaterDTO()
            {
                SelectedMode = "direct",
                ListMode = inputDataService.GetListMode(),
                SelectedDirectMode = inputData.SelectedDirect,
                ListDirectMode = inputDataService.GetListDirectMode(),
                ValueCapacity = inputDataService.GetValueCapacityDX(),
                ValueCapacityMinMax = new List<double> { 0.5, 50_000 },
                SelectGeometry = inputData.SelectGeometry,
                ListGeometries = inputDataService.GetListGeometries(),
                ValueWidthFin = inputDataService.GetValueWidthFin(),
                ValueWidthFinMinMax = new List<int> { 100, 12_000 },
                ValueHightFin = inputDataService.GetValueHightFin(),
                ValueHightFinMinMax = new List<int> { 100, 2_500 },
                TubesN = inputDataService.GetTubesN(),
                TubesNMinMax = new List<int> { 1, 1_000 },
                SelectedPipe = inputData.SelectedPipe,
                ListPipes = inputDataService.GetListPipes(),
                SelectedFin = inputData.SelectedFin,
                ListFins = inputDataService.GetListFins(),
                SelectedStepFin = inputData.SelectedStepFin,
                ListStepsFin = inputDataService.GetListStepsFin(),
                ValuePipe = inputDataService.GetValuePipe(),
                ValuePipeMinMax = new List<int> { 1, 1_000 },
                ValueCircuits = inputDataService.GetValueCircuits(),
                ValueCircuitsMinMax = new List<int> { 1, 1_000 },
                NumOfPasses = inputDataService.GetNumOfPasses(),
                NumOfPassesMinMax = new List<int> { 1, 1_000 },
                ValueAirFlow = inputDataService.GetValueAirFlowDX(),
                ListUnitsAirFlow = inputDataService.GetListUnitsAirFlow(),
                ValueAirFlowMinMax = new List<int> { 50, 1_000_000 },
                I_AirTempIn = inputDataService.GetI_AirTempInDX(),
                I_AirTempInMinMax = new List<int> { -100, 400 },
                ValueBaseHum = inputDataService.GetValueBaseHumCX(),
                ValueBaseHumMinMax = new List<int> { 0, 100 },
                Selected_I_RefT = inputData.Selected_I_RefT,
                ListI_RefT = inputDataService.GetListI_RefT(),
                Selected_I_FoulingI = inputData.Selected_I_FoulingI,
                ListI_FoulingI = inputDataService.GetListI_FoulingI(),
                SelectEvapTemp = inputData.SelectEvapTemp,
                ListEvaporatingTemperature = inputDataService.GetListEvaporatingTemperature(),
                I_TEvapDX = inputDataService.GetI_TEvapDX(),
                I_TEvapDXMinMax = new List<int> { -100, 200 },
                EvapAbsPresDX = inputDataService.GetEvapAbsPresDX(),
                EvapAbsPresDXMinMax = new List<int> { -100, 200 },
                SelectSuctOvrheat = inputData.SelectSuctOvrheat,
                ListSuctOvrheat = inputDataService.GetListSuctOvrheat(),
                I_TOvrHDX = inputDataService.GetI_TOvrHDX(),
                I_TOvrHDXMinMax = new List<int> { -100, 200 },
                SuctGasReturnDX = inputDataService.GetSuctGasReturnDX(),
                SuctGasReturnDXMinMax = new List<int> { -100, 200 },
                SelectCondTemp = inputData.SelectCondTemp,
                ListCondensingTemperature = inputDataService.GetListCondensingTemperature(),
                I_TCondDX = inputDataService.GetI_TCondDX(),
                I_TCondDXMinMax  = new List<int> { -100, 200 },
                CondAbsPresDX = inputDataService.GetCondAbsPresDX(),
                CondAbsPresDXMinMax = new List<int> { -100, 200 },
                SelectSubCool = inputData.SelectSubCool,
                ListSubCooling = inputDataService.GetListSubCooling(),
                I_TSubCDX = inputDataService.GetI_TSubCDX(),
                I_TSubCDXMinMax = new List<int> { -100, 200 },
                LiquidTempDX = inputDataService.GetLiquidTempDX(),
                LiquidTempDXMinMax = new List<int> { -100, 200 },
                I_RCircDX = inputDataService.GetI_RCircDX(),
                I_RCircDXMinMax = new List<int> { 0, 100 },
                I_DxBypDX =inputDataService.GetI_DxBypDX(),
                I_DxBypDXMinMax = new List<int> { 0, 100 },

                // характеристики корпуса и коллектора
                I_MatHdr = inputDataService.GetI_MatHdr(),
                I_MatHdrMinMax = new List<int> { 0, 100 },
                Selected_I_MatHdr = inputData.Selected_I_MatHdr,
                ListI_MatHdr = inputDataService.GetListI_MatHdr(),
                Selected_I_ConIn = inputData.Selected_I_ConIn,
                ListI_ConIn = inputDataService.GetListI_ConIn(),
                Selected_I_ConOut = inputData.Selected_I_ConOut,
                ListI_ConOut = inputDataService.GetListI_ConOut(),
                Selected_I_ConType = inputData.Selected_I_ConType,
                ListI_ConType = inputDataService.GetListI_ConType(),
                I_CSheetL = inputDataService.GetI_CSheetL(),
                SelectedCasingType = inputData.SelectedCasingType,
                ListCasingType = inputDataService.GetListCasingType(),
                Selected_I_CasMat = inputData.Selected_I_CasMat,
                ListI_CasMat = inputDataService.GetListI_CasMat(),
                SelectedEsapo = inputData.SelectedEsapo,
                ListEsapo = inputDataService.GetListEsapo(),
                Select_I_CDir = inputData.Select_I_CDir,
                ListI_CDir = inputDataService.GetListI_CDir(),
                SelectedAirFlowDirection = inputData.SelectedAirFlowDirection,
                ListAirFlowDirection = inputDataService.GetListAirFlowDirection(),
                SelectedConnectingCoolant = inputData.SelectedConnectingCoolant,
                ListConnectingCoolant = inputDataService.GetListConnectingCoolant(),

                // Дополнительные параметры
                BaseBar = advanceParamsViewModel.GetBaseBar(),
                BaseBarMinMax = new List<double> { 0, 100_000 },
                SelectGasWorkPressUnit = advanceParamsViewModel.SelectGasWorkPressUnit,
                ListGasWorkPressUnit = advanceParamsViewModel.GetListGasWorkPressUnit(),
                I_BaseDens = advanceParamsViewModel.GetI_BaseDens(),
                I_BaseDensMinMax = new List<double> { 0, 100 },
                I_ARes = advanceParamsViewModel.GetI_ARes(),
                I_AResMinMax = new List<int> { 0, 100 },
                I_LRes = advanceParamsViewModel.GetI_LRes(),
                I_LResMinMax = new List<int> { 0, 100 },
                //Select_I_FoulingI = advanceParamsViewModel.Select_I_FoulingI,
                //ListI_FoulingI = advanceParamsViewModel.GetListI_FoulingI(),
                Select_I_FoulingE = advanceParamsViewModel.Select_I_FoulingE,
                ListI_FoulingE = advanceParamsViewModel.GetListI_FoulingE(),
                SelectAFPV = advanceParamsViewModel.SelectAFPV,
                ListAFPV = advanceParamsViewModel.GetListAFPV(),
            };
        }
        #endregion
    }
}
