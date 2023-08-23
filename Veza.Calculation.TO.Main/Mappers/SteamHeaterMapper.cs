﻿using System.Collections.Generic;
using Veza.Calculation.TO.Main.Interfaces;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Interfaces.Mappers;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.Main;
using Veza.HeatExchanger.ViewModels.Controls;

namespace Veza.HeatExchanger.Mappers
{
    public class SteamHeaterMapper : ISteamHeaterMapper
    {
        #region Внутренние поля
        private readonly IInputData inputData;
        private readonly AdvanceParamsViewModel advanceParamsViewModel;
        private readonly IInputDataService inputDataService;
        #endregion

        #region Конструктор

        public SteamHeaterMapper(AdvanceParamsViewModel advanceParamsViewModel, IInputDataService inputDataService, 
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
        /// ValueCapacityST - значение DirectMode
        /// SelectGeometry - геометрия
        /// ValueWidthFin - длина оребрения
        /// ValueHightFin - высота оребрения
        /// TubesN - число труб
        /// SelectedPipe - трубка
        /// SelectedFin - толщина оребрения
        /// SelectedStepFin - шаг оребрения
        /// ValueAirFlowST - расход воздуха
        /// I_AirTempInST - температура воздуха на входе
        /// ValueBaseHumST - влажность воздуха на входе
        /// I_StmBar - давление пара
        /// I_StmTemp - температура пара
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
        public void InputToInputDataClass(InputDataSteamHeaterDTO inputParams)
        {
            //"SelectedMode"  тип расчёта -
            // direct - прямой
            // reverse - обратный
            inputData.SelectedDirect = inputParams.SelectedDirectMode;
            inputDataService.SetValueCapacityST(inputParams.ValueCapacity);
            inputData.SelectGeometry = inputParams.SelectGeometry.ToString();
            inputDataService.SetValueWidthFin(inputParams.ValueWidthFin);
            inputDataService.SetValueHightFin(inputParams.ValueHightFin);
            inputDataService.SetTubesN(inputParams.TubesN);
            inputData.SelectedPipe = inputParams.SelectedPipe;
            inputData.SelectedFin = inputParams.SelectedFin;
            inputData.SelectedStepFin = inputParams.SelectedStepFin;
            inputDataService.SetValueAirFlowST(inputParams.ValueAirFlow);
            inputDataService.SetI_AirTempInST(inputParams.I_AirTempIn);
            inputDataService.SetValueBaseHumST(inputParams.ValueBaseHum);
            inputDataService.SetI_StmBar(inputParams.I_StmBar);
            inputDataService.SetI_StmTemp(inputParams.I_StmTemp);
            inputDataService.SetValuePipe(inputParams.ValuePipe);
            inputDataService.SetValueCircuits(inputParams.ValueCircuits);
            inputDataService.SetNumOfPasses(inputParams.NumOfPasses);

            // характеристики корпуса и коллектора
            //case "I_MatHdr":
            inputDataService.SetI_MatHdr(inputParams.I_MatHdr.ToString());
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
            advanceParamsViewModel.Select_I_FoulingI = inputParams.Select_I_FoulingI;
            advanceParamsViewModel.Select_I_FoulingE = inputParams.Select_I_FoulingE;
            advanceParamsViewModel.SelectAFPV = inputParams.SelectAFPV;
        }

        /// <summary>
        /// Установка входных параметров
        /// SelectedMode - тип расчёта : direct - прямой, reverse - обратный
        /// ListMode - список тип расчёта : direct - прямой, reverse - обратный
        /// SelectedDirectMode - выбранный элемент из списка (мощность, температура воздуха на выходе)
        /// ListDirectMode - список (мощность, температура воздуха на выходе)
        /// ValueCapacityST - значение DirectMode
        /// ValueCapacityMinMax - Мощность граничные условия
        /// SelectGeometries - геометрия
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
        /// ValueAirFlowST - расход воздуха
        /// ValueAirFlowMinMax- Расход воздуха граничные условия
        /// ListUnitsAirFlow - список единиц измерения расхода воздуха
        /// I_AirTempInST - температура воздуха на входе
        /// I_AirTempInMinMax - Температура воздуха на входе граничные условия
        /// ValueBaseHumST - влажность воздуха на входе
        /// ValueBaseHumMinMax - Влажность воздуха на входе граничные условия        /// 
        /// I_StmBar - давление пара
        /// I_StmBarMinMax - Давление пара граничные условия
        /// I_StmTemp - температура пара
        /// I_StmTempMinMax - Температура пара граничные условия
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
        public GetDataSteamHeaterDTO InputDataClassToInputParams()
        {
            return new GetDataSteamHeaterDTO()
            {
                SelectedMode = "direct",
                ListMode = inputDataService.GetListMode(),
                SelectedDirectMode = inputData.SelectedDirect,
                ListDirectMode = inputDataService.GetListDirectMode(),
                ValueCapacity = inputDataService.GetValueCapacityST(),
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
                ValueAirFlow = inputDataService.GetValueAirFlowST(),
                ListUnitsAirFlow = inputDataService.GetListUnitsAirFlow(),
                ValueAirFlowMinMax = new List<int> { 50, 1_000_000 },
                I_AirTempIn = inputDataService.GetI_AirTempInST(),
                I_AirTempInMinMax = new List<int> { -100, 400 },
                ValueBaseHum = inputDataService.GetValueBaseHumST(),
                ValueBaseHumMinMax = new List<int> { 0, 100 },
                I_StmBar = inputDataService.GetI_StmBar(),
                I_StmBarMinMax = new List<int> { 0, 1_000 },
                I_StmTemp = inputDataService.GetI_StmTemp(),
                I_StmTempMinMax = new List<int> { 0, 1_000 },

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
                Select_I_FoulingI = advanceParamsViewModel.Select_I_FoulingI,
                ListI_FoulingI = advanceParamsViewModel.GetListI_FoulingI(),
                Select_I_FoulingE = advanceParamsViewModel.Select_I_FoulingE,
                ListI_FoulingE = advanceParamsViewModel.GetListI_FoulingE(),
                SelectAFPV = advanceParamsViewModel.SelectAFPV,
                ListAFPV = advanceParamsViewModel.GetListAFPV(),
            };
        }
        #endregion
    }
}