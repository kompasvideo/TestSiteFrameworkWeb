using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Resources;
using System.Reflection;
using Veza.HeatExchanger.Exceptions;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Threading;
using System.Timers;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.ViewModels.Controls;
using Veza.Calculation.TO.Main.Interfaces;

namespace Veza.HeatExchanger.Services
{
    public class CalculateBaseService
    {
        #region Внутренние поля и переменные    
        /// <summary>
        /// таблица Input
        /// </summary>
        protected ObservableCollection<DBLine> lines { get; set; }
        /// <summary>
        /// таблица Output
        /// </summary>
        protected ObservableCollection<DBLineOut> linesOut { get; set; }
        /// <summary>
        /// выходные данные построчно ( как в dll)
        /// </summary>
        protected List<OutViewLines> outViews { get; set; }

        protected ICalculate calculate;
        protected OutParams messageS;
        protected ILogs logs;
        /// <summary>
        /// видимость фрейма с фразой "Идёт расчёт ... "
        /// </summary>
        protected Visibility? windowCalcV;
        public Visibility? WindowCalcV
        {
            get
            {
                return windowCalcV;
            }
            set
            {
                windowCalcV = value;
            }
        }
        protected int result { get; set; }
        /// <summary>
        /// строка с ошибкой
        /// </summary>
        protected string errorStr { get; set; }
        /// <summary>
        /// Тип расчёта - прямой или обратный
        /// </summary>
        protected CalcEnum CalcType
        {
            get;
            set;
        }
        /// <summary>
        /// класс для табличного расчёта
        /// </summary>
        protected ITableDataService tableDataService;
        /// <summary>
        /// класс с входными данными
        /// </summary>
        protected IInputData inputData;
        /// <summary>
        /// класс с дополнительными параметрами
        /// </summary>
        protected AdvanceParamsViewModel advanceParamsViewModel;

        protected IInputDataService inputDataService;
        /// <summary>
        /// Тип расчёта Теплонагреватель, паровой нагреватель, Испаритель
        /// </summary>
        protected CalcMode CalcModeV { get; set; }

        protected IFinThk finThk;
        #endregion

        #region Конструкторы

        public CalculateBaseService(AdvanceParamsViewModel advanceParamsViewModel, ICalculate calculate, ILogs logs,
            IInputData inputData, ITableDataService tableDataService, IInputDataService inputDataService, IFinThk finThk)
        {
            this.advanceParamsViewModel = advanceParamsViewModel;
            this.calculate = calculate;
            this.logs = logs;
            this.inputData = inputData;
            this.tableDataService = tableDataService;
            this.inputDataService = inputDataService;
            this.finThk = finThk;
        }
        #endregion

        #region Методы открытые            

        /// <summary>
        /// Первичная установка свойств
        /// </summary>
        public virtual void SetPropertiesFirst()
        {
            StaticData.Initialize();
            SetPropertiesChangeLang();
            inputData.Geometry = new ObservableCollection<string>();
            inputData.Geometry.Add(Calculation.TO.Main.Properties.Resources._5012);
            inputData.Geometry.Add(Calculation.TO.Main.Properties.Resources._4816);
            inputData.Geometry.Add(Calculation.TO.Main.Properties.Resources._3512);
            inputData.Geometry.Add(Calculation.TO.Main.Properties.Resources._2510);
            inputData.Geometry.Add(Calculation.TO.Main.Properties.Resources._2507);
            inputData.SelectGeometry = Calculation.TO.Main.Properties.Resources._5012;

            inputData.I_RefT = new ObservableCollection<string>();
            inputData.I_RefT.Add(Calculation.TO.Main.Properties.Resources.R407C);
            inputData.I_RefT.Add(Calculation.TO.Main.Properties.Resources.R134a);
            inputData.I_RefT.Add(Calculation.TO.Main.Properties.Resources.R410A);
            inputData.I_RefT.Add(Calculation.TO.Main.Properties.Resources.R404A);
            inputData.I_RefT.Add(Calculation.TO.Main.Properties.Resources.R22);
            inputData.I_RefT.Add(Calculation.TO.Main.Properties.Resources.R513A);
            inputData.Selected_I_RefT = Calculation.TO.Main.Properties.Resources.R410A;

            inputData.ValueCircuits = 12;
            inputData.ValueHightFin = 1200;
            inputData.ValueWidthFin = 1400;
            inputData.ValuePipe = 3;
            inputData.ValueCapacityHW = 230;
            inputData.ValueBaseHumHW = 85;
            inputData.ValueAirFlowHW = 21700;
            inputData.ValueMedTempInHW = 90;
            inputData.ValueVariantRaschHW = 70;
            inputData.ValueMedKPaHW = 50;
            inputData.I_AirTempInHW = -2;

            inputData.bLanguge = new ObservableCollection<string>();
            inputData.lang = new List<string>();
            inputData.lang.Add(Calculation.TO.Main.Properties.Resources.Eng);
            inputData.lang.Add(Calculation.TO.Main.Properties.Resources.Ru);
            if (GS.IsCultureRU())
            {
                inputData.bLanguge.Add(Calculation.TO.Main.Properties.Resources.Ru);
                inputData.bLanguge.Add(Calculation.TO.Main.Properties.Resources.Eng);
                inputData.SelectedLanguage = Calculation.TO.Main.Properties.Resources.Ru;
            }
            else
            {
                inputData.bLanguge.Add(Calculation.TO.Main.Properties.Resources.Eng);
                inputData.bLanguge.Add(Calculation.TO.Main.Properties.Resources.Ru);
                inputData.SelectedLanguage = Calculation.TO.Main.Properties.Resources.Eng;
            }
        }

        /// <summary>
        /// Запуск прямого расчёта
        /// </summary>
        /// <param name="returnPage"></param>
        public void CalcDirect(string returnPage)
        {
            try
            {
                bool error = false;
                OutParams outParams = new OutParams
                {
                    ReturnPage = returnPage,
                };
                try
                {
                    SetCalcType(CalcEnum.Direct);
                    StaticData.ErrorI = 0;
                    /// установка входных данных
                    SetPropToCalc_DirectBase();
                    if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.Capacity)
                    {
                        error = CalcDirect();
                    }
                    else
                    {
                        error = CalcDirectMassFlow();
                    }
                }
                catch (ValidErrorException ex)
                {
                    //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.ValidErrorException);
                    //logs.Logger.Info(ex.Source);
                    //logs.Logger.Info(ex.Message);
                    //logs.Logger.Info(ex.StackTrace);
                    errorStr = "'" + (string)ex.Data["name"] + " : " + (string)ex.Data["param"] + "'\r\n" + ex.Message;
                    result = 1;
                    return;
                }
                SetErrorStr(error);
            }
            catch (Exception ex)
            {
                //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.Exception);
                //logs.Logger.Info(ex.Source);
                //logs.Logger.Info(ex.Message);
                //logs.Logger.Info(ex.StackTrace);
                //MessageBox.Show(ex.StackTrace);
            }
        }

        /// <summary>
        /// Обратный расчёт
        /// </summary>
        public void CalcReverse(string returnPage)
        {
            try
            {
                bool error = false;
                bool bErr;
                OutParams outParams = new OutParams
                {
                    ReturnPage = returnPage,
                };
                try
                {
                    SetCalcType(CalcEnum.Reverse);
                    SetPropToCalc_ReverseBase();
                    calculate.Calc();
                    bErr = calculate.GetReturn();
                    if (bErr)
                    {
                        result = 1;
                        error = true;
                        StaticData.ErrorI = 3;
                        StaticData.ErrorDll = calculate.GetTxtErr();
                    }
                    else
                    {
                        ReadOutput();
                        // если не найден расчёт на заданную геометрию
                        bool returnRes;
                        if (!SelectOut(out returnRes))
                        {
                            if (returnRes)
                            {
                                error = true;
                                StaticData.ErrorI = 4;
                            }
                        }
                    }
                    SetErrorStr(error);
                }
                catch (ValidErrorException ex)
                {
                    //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.ValidErrorException);
                    //logs.Logger.Info(ex.Source);
                    //logs.Logger.Info(ex.Message);
                    //logs.Logger.Info(ex.StackTrace);
                    string param = (string)ex.Data["param"];
                    string name = (string)ex.Data["name"];
                    errorStr = "'" + name + " : " + param + "'\r\n" + ex.Message;
                    result = 1;
                    return;
                }
            }
            catch (Exception ex)
            {
                //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.Exception);
                //logs.Logger.Info(ex.Source);
                //logs.Logger.Info(ex.Message);
                //logs.Logger.Info(ex.StackTrace);
                //MessageBox.Show(ex.StackTrace);
            }
        }

        /// <summary>
        /// Табличный режим
        /// </summary>
        public void CalcTable()
        {
            try
            {
                OutParams outParams = new OutParams
                {
                    ReturnPage = Calculation.TO.Main.Properties.Resources.PageFluidCooler_Table,
                    SaveVisibility = true,
                };
                int index = 0;
                tableDataService.ClearOutputData();
                bool error = false;
                foreach (var item in TableData.ProjectsOutTable)
                {
                    try
                    {
                        //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strInputString + item);
                        tableDataService.SetIdToCalc(index);
                        item.CalcType = CalcEnum.Reverse;
                        SetCalcType(CalcEnum.Table);
                        SetPropToCalc_ReverseBase();
                        SetPropToCalc_Table(tableDataService.GetOutView());
                        calculate.Calc();
                        bool bErr = calculate.GetReturn();
                        if (!bErr)
                        {
                            ReadOutput();
                            SelectOutTable();
                            tableDataService.GetProjectOut();
                            item.CalcError = false;
                        }
                        else
                        {
                            error = true;
                            item.CalcError = true;
                            result = 1;
                            tableDataService.ClearOutputData(index);
                            StaticData.ErrorI = 3;
                            StaticData.ErrorDll = calculate.GetTxtErr();
                        }
                        index++;
                    }
                    catch (ValidErrorException ex)
                    {
                        item.CalcError = true;
                        //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.ValidErrorException);
                        //logs.Logger.Info(ex.Source);
                        //logs.Logger.Info(ex.Message);
                        //logs.Logger.Info(ex.StackTrace);
                        index++;
                    }
                }
                if (error)
                {
                    if (TableData.ProjectsOutTable.Count < 2)
                    {
                        //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.strError);
                        switch (StaticData.ErrorI)
                        {
                            case 0:
                                //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.Error_0);
                                errorStr = Calculation.TO.Main.Properties.Resources.Error_0;
                                break;
                            case 1:
                                //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.Error_1);
                                errorStr = Calculation.TO.Main.Properties.Resources.Error_1;
                                break;
                            case 2:
                                //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.Error_2);
                                errorStr = Calculation.TO.Main.Properties.Resources.Error_2;
                                break;
                            case 3:
                                //logs.Logger.Error(StaticData.ErrorDll);
                                errorStr = StaticData.ErrorDll;
                                break;
                        }
                        result = 1;
                        return;
                    }
                }
                result = 0;
            }
            catch (Exception ex)
            {
                //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.Exception);
                //logs.Logger.Info(ex.Source);
                //logs.Logger.Info(ex.Message);
                //logs.Logger.Info(ex.StackTrace);
                //MessageBox.Show(ex.StackTrace);
            }
        }

        /// <summary>
        /// Получение из производственного кода параметров
        /// </summary>
        /// <param name="saveCodes"></param>
        /// <returns></returns>
        public virtual bool Decoder(SaveCodes saveCodes)
        {
            try
            {
                if (DecoderBase(saveCodes) == false)
                {
                    return false;
                }
                ProjectsOut_Clean();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Первоначальная настройка внешней библиотеки для расчёта
        /// </summary>
        public void CalculationInit()
        {
            calculate.Init();
        }

        /// <summary>
        /// получение результата расчёта из внешней библиотеки
        /// </summary>
        /// <returns></returns>
        public int GetResult()
        {
            return result;
        }

        /// <summary>
        /// Получение строки ошибки из внешней библиотеки расчёта
        /// </summary>
        /// <returns></returns>
        public string GetErrorStr()
        {
            return errorStr;
        }

        /// <summary>
        /// Установка типа расчёта - воздухонагреватель, воздухоохладитель, паровой нагреватель, конденсатор, испаритель
        /// DX - испаритель
        /// </summary>
        /// <param name="calcMode"></param>
        public void SetCalcMode(string calcMode)
        {
            switch (calcMode)
            {
                case "HW":
                    CalcModeV = CalcMode.Heater;
                    break;
                case "CW":
                    CalcModeV = CalcMode.Cooler;
                    break;
                case "ST":
                    CalcModeV = CalcMode.SteamHeater;
                    break;
                case "DX":
                    CalcModeV = CalcMode.Evaporater;
                    break;
                case "CX":
                    CalcModeV = CalcMode.Condensator;
                    break;
            }
            if (calcMode != StaticData.CalcMode)
            {
                calculate.GetParams(calcMode);
                StaticData.CalcMode = calcMode;
            }
        }

        /// <summary>
        /// Установка свойств при смене языка
        /// </summary>
        public void SetPropertiesChangeLang()
        {
            string lang = "ru-RU";
            Calculation.TO.Main.Properties.Resources.Culture = new CultureInfo(lang);
            inputData.Esapo = new ObservableCollection<string>();
            switch (CalcModeV)
            {
                case CalcMode.Heater:      // ВНВ
                    inputDataService.SetEsapoHeater();
                    break;
                case CalcMode.Cooler:      // ВОВ
                    inputDataService.SetEsapoCooler();
                    break;
                case CalcMode.SteamHeater: // ВНП
                    inputDataService.SetEsapoSteamHeater();
                    break;
                case CalcMode.Condensator: // ВНФ
                    inputDataService.SetEsapoCondensator();
                    break;
                case CalcMode.Evaporater:  // ВОФ
                    inputDataService.SetEsapoEvaporater();
                    break;
            }

            int i2 = 0;
            if (inputData.Direct != null)
            {
                i2 = inputData.Direct.Count;
            }
            inputData.Direct = new ObservableCollection<string>();
            inputData.Direct.Add(Calculation.TO.Main.Properties.Resources.Capacity);
            inputData.Direct.Add(Calculation.TO.Main.Properties.Resources.I_AirTempOut);
            if (i2 == 3)
            {
                inputData.Direct.Add(Calculation.TO.Main.Properties.Resources.I_MedFlowCalc);
            }
            inputData.SelectedDirect = Calculation.TO.Main.Properties.Resources.Capacity;

            inputData.DirectMassFlowUnits = new ObservableCollection<string>();
            inputData.DirectMassFlowUnits.Add(Calculation.TO.Main.Properties.Resources.kgh);
            inputData.DirectMassFlowUnits.Add(Calculation.TO.Main.Properties.Resources.gs);
            inputData.SelectDirectMassFlowUnits = Calculation.TO.Main.Properties.Resources.kgh;

            inputData.Pipe = new ObservableCollection<string>();
            inputData.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu);
            inputData.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu);
            //inputData.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0.35CuG);
            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;

            inputData.StepFin = new ObservableCollection<string>();
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._1_8mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._2_0mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._2_2mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._2_5mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._3_0mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._3_5mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._4_0mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._4_5mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._5_0mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._7_0mm);
            inputData.StepFin.Add(Calculation.TO.Main.Properties.Resources._12_0mm);
            inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._2_5mm;
            inputData.Fin = new ObservableCollection<string>();
            inputData.Fin.Add(Calculation.TO.Main.Properties.Resources._0_12mmAl);
            inputData.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
            inputData.Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
            //inputData.Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
            //inputData.Fin.Add(Calculation.TO.Main.Properties.Resources.0.25mmAl);
            inputData.Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
            inputData.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
            inputData.Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_12mmAl;
            inputData.AirFlow = new ObservableCollection<string>();
            inputData.AirFlow.Add(Calculation.TO.Main.Properties.Resources.m3h);
            inputData.SelectedAirFlow = Calculation.TO.Main.Properties.Resources.m3h;
            inputData.Fluid = new ObservableCollection<string>();
            inputData.Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidW100);
            inputData.Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidEthyleneGlycol);
            inputData.Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidPropyleneGlycol);
            inputData.SelectedFluid = Calculation.TO.Main.Properties.Resources.FluidW100;
            inputData.VariantRasch = new ObservableCollection<string>();
            inputData.VariantRasch.Add(Calculation.TO.Main.Properties.Resources.I_MedFlow);
            inputData.VariantRasch.Add(Calculation.TO.Main.Properties.Resources.I_TempFluidOut);
            inputData.SelectedVariantRasch = Calculation.TO.Main.Properties.Resources.I_TempFluidOut;

            double I_TEvapDX = inputData.I_TEvapDX;
            double I_TEvapCX = inputData.I_TEvapCX;
            inputData.EvaporatingTemperature = new ObservableCollection<string>();
            inputData.EvaporatingTemperature.Add(Calculation.TO.Main.Properties.Resources.EvaporationTemperature);
            inputData.EvaporatingTemperature.Add(Calculation.TO.Main.Properties.Resources.EvaporationAbsPres);
            inputData.SelectEvapTemp = Calculation.TO.Main.Properties.Resources.EvaporationTemperature;
            inputData.I_TEvapDX = I_TEvapDX;
            inputData.I_TEvapCX = I_TEvapCX;
            double I_TOvrHDX = inputData.I_TOvrHDX;
            double I_TOvrHCX = inputData.I_TOvrHCX;
            inputData.SuctOvrheat = new ObservableCollection<string>();
            inputData.SuctOvrheat.Add(Calculation.TO.Main.Properties.Resources.Overheating);
            inputData.SuctOvrheat.Add(Calculation.TO.Main.Properties.Resources.SuctGasReturn);
            inputData.SelectSuctOvrheat = Calculation.TO.Main.Properties.Resources.Overheating;
            inputData.I_TOvrHDX = I_TOvrHDX;
            inputData.I_TOvrHCX = I_TOvrHCX;
            double I_TCond = inputData.I_TCondDX;
            double I_TCondCX = inputData.I_TCondCX;
            inputData.CondensingTemperature = new ObservableCollection<string>();
            inputData.CondensingTemperature.Add(Calculation.TO.Main.Properties.Resources.CondensingTemperature);
            inputData.CondensingTemperature.Add(Calculation.TO.Main.Properties.Resources.AbsCondensingPressure);
            inputData.SelectCondTemp = Calculation.TO.Main.Properties.Resources.CondensingTemperature;
            inputData.I_TCondDX = I_TCond;
            inputData.I_TCondCX = I_TCondCX;
            double I_TSubC = inputData.I_TSubCDX;
            double I_TSubCCX = inputData.I_TSubCCX;
            inputData.SubCooling = new ObservableCollection<string>();
            inputData.SubCooling.Add(Calculation.TO.Main.Properties.Resources.SubCooling);
            inputData.SubCooling.Add(Calculation.TO.Main.Properties.Resources.LiquidTemp);
            inputData.SelectSubCool = Calculation.TO.Main.Properties.Resources.SubCooling;
            inputData.I_TSubCDX = I_TSubC;
            inputData.I_TSubCCX = I_TSubCCX;

            inputData.I_FoulingI = new ObservableCollection<string>();
            inputData.I_FoulingI.Add(Calculation.TO.Main.Properties.Resources.WithoutOil);
            inputData.I_FoulingI.Add(Calculation.TO.Main.Properties.Resources._12MineralOil);
            inputData.I_FoulingI.Add(Calculation.TO.Main.Properties.Resources._23MineralOil);
            inputData.I_FoulingI.Add(Calculation.TO.Main.Properties.Resources._12SyntheticOil);
            inputData.I_FoulingI.Add(Calculation.TO.Main.Properties.Resources._23SyntheticOil);
            inputData.Selected_I_FoulingI = Calculation.TO.Main.Properties.Resources.WithoutOil;

            inputData.I_CSheet = new ObservableCollection<string>();
            inputData.I_CSheet.Add(Calculation.TO.Main.Properties.Resources.CSHEET_Without);
            inputData.I_CSheet.Add(Calculation.TO.Main.Properties.Resources.CSHEET_SingleAirIn);
            inputData.I_CSheet.Add(Calculation.TO.Main.Properties.Resources.CSHEET_SingleAirOut);
            inputData.Selected_I_CSheet = Calculation.TO.Main.Properties.Resources.CSHEET_Without;
            inputData.I_MatHdr = new ObservableCollection<string>();
            inputData.I_MatHdr.Add(Calculation.TO.Main.Properties.Resources.MATHDR_Cu);
            inputData.I_MatHdr.Add(Calculation.TO.Main.Properties.Resources.MATHDR_Steel);
            inputData.I_MatHdr.Add(Calculation.TO.Main.Properties.Resources.MATHDR_CuBl);
            inputData.I_MatHdr.Add(Calculation.TO.Main.Properties.Resources.MATHDR_316);
            inputData.I_MatHdr.Add(Calculation.TO.Main.Properties.Resources.MATHDR_CuNi);
            inputData.I_MatHdr.Add(Calculation.TO.Main.Properties.Resources.MATHDR_CUPC);
            inputData.I_MatHdr.Add(Calculation.TO.Main.Properties.Resources.MATHDR_SP);
            inputData.I_MatHdr.Add(Calculation.TO.Main.Properties.Resources.MATHDR_SPC);
            inputData.I_MatHdr.Add(Calculation.TO.Main.Properties.Resources.MATHDR_SSPC);
            inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_SP;
            //inputData.I_HdrQta = "0";
            inputData.I_ConIn = new ObservableCollection<string>();
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_0);
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN10);
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN15);
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN20);
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN25);
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN32);
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN40);
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN50);
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN65);
            inputData.I_ConIn.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN80);
            inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_0;
            inputData.I_ConOut = new ObservableCollection<string>();
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_0);
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN10);
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN15);
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN20);
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN25);
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN32);
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN40);
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN50);
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN65);
            inputData.I_ConOut.Add(Calculation.TO.Main.Properties.Resources.CONINF_DN80);
            inputData.Selected_I_ConOut = Calculation.TO.Main.Properties.Resources.CONINF_0;
            inputData.I_ConType = new ObservableCollection<string>();
            inputData.I_ConType.Add(Calculation.TO.Main.Properties.Resources.CONTPA_1);
            inputData.I_ConType.Add(Calculation.TO.Main.Properties.Resources.CONTPA_2);
            inputData.I_ConType.Add(Calculation.TO.Main.Properties.Resources.CONTPA_3);
            inputData.I_ConType.Add(Calculation.TO.Main.Properties.Resources.CONTPA_4);
            inputData.I_ConType.Add(Calculation.TO.Main.Properties.Resources.CONTPA_5);
            inputData.I_ConType.Add(Calculation.TO.Main.Properties.Resources.CONTPA_6);
            inputData.I_ConType.Add(Calculation.TO.Main.Properties.Resources.CONTPA_7);
            inputData.I_ConType.Add(Calculation.TO.Main.Properties.Resources.CONTPA_8);
            inputData.Selected_I_ConType = Calculation.TO.Main.Properties.Resources.CONTPA_7;
            inputData.CasingType = new ObservableCollection<string>();
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_1);
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_2);
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_3);
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_4);
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_5);
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_6);
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_7);
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_9);
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_10);
            inputData.CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_11);
            inputData.SelectedCasingType = Calculation.TO.Main.Properties.Resources.CasingType_1;
            inputData.I_CasMat = new ObservableCollection<string>();
            inputData.I_CasMat.Add(Calculation.TO.Main.Properties.Resources.CASMAT_OSBP);
            inputData.I_CasMat.Add(Calculation.TO.Main.Properties.Resources.CASMAT_AL);
            inputData.I_CasMat.Add(Calculation.TO.Main.Properties.Resources.CASMAT_OSSPP);
            inputData.I_CasMat.Add(Calculation.TO.Main.Properties.Resources.CASMAT_NSSPP);
            inputData.I_CasMat.Add(Calculation.TO.Main.Properties.Resources.CASMAT_NSNO316);
            inputData.I_CasMat.Add(Calculation.TO.Main.Properties.Resources.CASMAT_NSBP);
            inputData.I_CasMat.Add(Calculation.TO.Main.Properties.Resources.CASMAT_CU);
            inputData.Selected_I_CasMat = Calculation.TO.Main.Properties.Resources.CASMAT_OSBP;
            inputData.AirFlowDirection = new ObservableCollection<string>();
            inputData.AirFlowDirection.Add(Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft);
            inputData.AirFlowDirection.Add(Calculation.TO.Main.Properties.Resources.AirFlowDirectionRight);
            inputData.SelectedAirFlowDirection = Calculation.TO.Main.Properties.Resources.AirFlowDirectionRight;
            inputData.ConnectingCoolant = new ObservableCollection<string>();
            inputData.ConnectingCoolant.Add(Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne);
            inputData.ConnectingCoolant.Add(Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantTwo);
            inputData.SelectedConnectingCoolant = Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne;
            inputData.I_CDir = new ObservableCollection<string>();
            inputData.I_CDir.Add(Calculation.TO.Main.Properties.Resources.ConnectionDirection_0);
            inputData.I_CDir.Add(Calculation.TO.Main.Properties.Resources.ConnectionDirection_1);
            inputData.I_CDir.Add(Calculation.TO.Main.Properties.Resources.ConnectionDirection_2);
            inputData.I_CDir.Add(Calculation.TO.Main.Properties.Resources.ConnectionDirection_3);
            inputData.I_CDir.Add(Calculation.TO.Main.Properties.Resources.ConnectionDirection_4);
            inputData.I_CDir.Add(Calculation.TO.Main.Properties.Resources.ConnectionDirection_5);
            inputData.I_CDir.Add(Calculation.TO.Main.Properties.Resources.ConnectionDirection_6);
            inputData.I_CDir.Add(Calculation.TO.Main.Properties.Resources.ConnectionDirection_7);
            inputData.I_CDir.Add(Calculation.TO.Main.Properties.Resources.ConnectionDirection_8);
            inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_0;

            for (int i = 0; i < TableData.ProjectsOutTable.Count; i++)
            {
                if (TableData.ProjectsOutTable[i].Geometry != null)
                {
                    TableData.ProjectsOutTable[i].SelectGeometry = Calculation.TO.Main.Properties.Resources._5012;
                }
                if (TableData.ProjectsOutTable[i].Pipe != null)
                {
                    TableData.ProjectsOutTable[i].Pipe.Clear();
                    TableData.ProjectsOutTable[i].Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu);
                    TableData.ProjectsOutTable[i].Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu);
                    //TableData.ProjectsOutTable[i].Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0.35CuG);
                    TableData.ProjectsOutTable[i].SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
                }
                if (TableData.ProjectsOutTable[i].Fin != null)
                {
                    TableData.ProjectsOutTable[i].Fin.Clear();
                    TableData.ProjectsOutTable[i].Fin.Add(Calculation.TO.Main.Properties.Resources._0_12mmAl);
                    TableData.ProjectsOutTable[i].Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                    TableData.ProjectsOutTable[i].Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                    TableData.ProjectsOutTable[i].Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmAl);
                    TableData.ProjectsOutTable[i].Fin.Add(Calculation.TO.Main.Properties.Resources._0_25mmAl);
                    TableData.ProjectsOutTable[i].Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                    TableData.ProjectsOutTable[i].Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                    TableData.ProjectsOutTable[i].Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                    TableData.ProjectsOutTable[i].SelectedFin = Calculation.TO.Main.Properties.Resources._0_12mmAl;
                }
                if (TableData.ProjectsOutTable[i].StepFin != null)
                {
                    TableData.ProjectsOutTable[i].StepFin.Clear();
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._1_8mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._2_0mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._2_2mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._2_5mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._3_0mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._3_5mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._4_0mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._4_5mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._5_0mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._7_0mm);
                    TableData.ProjectsOutTable[i].StepFin.Add(Calculation.TO.Main.Properties.Resources._12_0mm);
                    TableData.ProjectsOutTable[i].SelectedStepFin = Calculation.TO.Main.Properties.Resources._2_5mm;
                }
                if (TableData.ProjectsOutTable[i].Fluid != null)
                {
                    TableData.ProjectsOutTable[i].Fluid.Clear();
                    TableData.ProjectsOutTable[i].Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidW100);
                    TableData.ProjectsOutTable[i].Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidEthyleneGlycol);
                    TableData.ProjectsOutTable[i].Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidPropyleneGlycol);
                    TableData.ProjectsOutTable[i].SelectedFluid = Calculation.TO.Main.Properties.Resources.FluidW100;
                }
            }
            advanceParamsViewModel.SetPropertiesChangeLang();
        }

        /// <summary>
        /// Добавить расчёт по массову расходу хладогента
        /// </summary>
        public void AddCalcMassFlow()
        {
            if (inputData.Direct.Count < 3)
            {
                inputData.Direct.Add(Calculation.TO.Main.Properties.Resources.I_MedFlowCalc);
            }
        }

        /// <summary>
        /// Удалить расчёт по массову расходу хладогента
        /// </summary>
        public void DelCalcMassFlow()
        {
            if (inputData.Direct.Count > 2)
            {
                inputData.Direct.Remove(Calculation.TO.Main.Properties.Resources.I_MedFlowCalc);
                inputData.SelectedDirect = Calculation.TO.Main.Properties.Resources.Capacity;
            }
        }

        #endregion

        #region Методы внутренние

        /// <summary>
        /// Очистка данных при ошибке 
        /// </summary>
        protected virtual void ProjectsOut_Clean()
        {
            if (inputData.ProjectsOut != null)
            {
                inputData.ProjectsOut.Name = "";
                inputData.ProjectsOut.NoOfRows = "";
                inputData.ProjectsOut.Circuits = "";
                inputData.ProjectsOut.FinSpace = 0;
                inputData.ProjectsOut.AirVelocity = "";
                inputData.ProjectsOut.PresDropDry = "";
                inputData.ProjectsOut.ReverseLoad = "";
                inputData.ProjectsOut.MedVelo = "";
                inputData.ProjectsOut.MedKPa = "";
                inputData.ProjectsOut.AirTempOut = "";
                inputData.ProjectsOut.I_MedTyp = "";
            }
        }

        /// <summary>
        /// считывание сохранённого расчёта в входные и выходные параметры
        /// </summary>
        /// <param name="saveCodes"></param>
        /// <returns></returns>
        protected bool DecoderBase(SaveCodes saveCodes)
        {
            string[] codes = saveCodes.Code.Split('/');

            // Геометрия
            string I_Geo = codes[1].Substring(1, 2);
            I_Geo += codes[2].Substring(0, 2);
            switch (I_Geo)
            {
                case "5012":
                case "4816":
                case "3512":
                case "2520":
                case "2507":
                    inputData.SelectGeometry = I_Geo;
                    break;
                default:
                    return false;
            }

            // Длина оребрения
            string str = codes[4].Split('-')[1];
            inputData.ValueWidthFin = double.Parse(str, CultureInfo.InvariantCulture);

            // Высота оребрения
            inputData.ValueHightFin = saveCodes.ValueHightFin;
            str = codes[2].Substring(0, 2);
            // Расход воздуха - единица измерения
            inputData.SelectedAirFlow = saveCodes.SelectedAirFlow;

            SetSelectHeatExchanger(saveCodes);
            switch (saveCodes.SelectedDirect)
            {
                case 1:
                    inputData.SelectedDirect = Calculation.TO.Main.Properties.Resources.Capacity;
                    break;
                case 2:
                    inputData.SelectedDirect = Calculation.TO.Main.Properties.Resources.I_AirTempOut;
                    break;
            }

            // Трубка
            String str2 = codes[2].Substring(3, 2);
            String str3 = codes[2].Substring(5, codes[2].Length - 5);
            switch (str)
            {
                case "12":
                    switch (str2)
                    {
                        case "32":
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
                            break;
                        case "50":
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu;
                            break;
                        case "35":
                            //inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0.35CuG;
                            break;
                    }
                    break;
                case "16":
                    switch (str2)
                    {
                        case "40":
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe16_0_40Cu;
                            break;
                        case "50":
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe16_0_50Cu;
                            break;
                        case "70":
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe16_0_70G;
                            break;
                        case "100":
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe16_1_00G;
                            break;
                    }
                    break;
                case "9.52":
                    switch (str2)
                    {
                        case "30":
                            if (str3 == "М")
                                inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe9_0_30Cu;
                            else
                                inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe9_0_30CuG;
                            break;
                        case "50":
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe9_0_50Cu;
                            break;
                    }
                    break;
                case "7":
                    switch (str2)
                    {
                        case "28":
                            //if (str3 == "М")
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe7_0_28Cu;
                            //else
                            //inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe7_0.28CuG;
                            break;
                        case "40":
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe7_0_40Cu;
                            break;
                    }
                    break;
            }
            // Толщина оребрения
            str = codes[3].Substring(1, 1);
            string[] mas = codes[3].Split('x', 'х');
            if (str == "П" || str == "О")
            {
                // Шаг оребрения
                str = mas[0].Substring(2, 3);
                // Толщина оребрения
                str2 = mas[1].Substring(0, 2);
                // Материал оребрения
                str3 = mas[1].Substring(2, 4);
            }
            else
            {

                // Шаг оребрения
                str = mas[0].Substring(1, mas[0].Length - 1);
                // Толщина оребрения
                str2 = mas[1].Substring(0, 2);
                // Материал оребрения
                str3 = mas[1].Substring(2, mas[1].Length - 2);
            }
            switch (str2)
            {
                case "12":
                    switch (str3)
                    {
                        case "АЛ":
                            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_12mmAl;
                            break;
                        case "АЛЭ":
                            break;
                        case "АЛБГ":
                            break;
                        case "АЛГФ":
                            break;
                        case "М":
                            break;
                        case "МБГ":
                            break;
                        case "НЖ2":
                            break;
                        case "НЖ3":
                            break;
                    }
                    break;
                case "15":
                    switch (str3)
                    {
                        case "АЛ":
                            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_15mmAl;
                            break;
                        case "АЛЭ":
                            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_15mmEpoxy;
                            break;
                        case "АЛБГ":
                            break;
                        case "АЛГФ":
                            break;
                        case "М":
                            break;
                        case "МБГ":
                            break;
                        case "НЖ2":
                            break;
                        case "НЖ3":
                            break;
                    }
                    break;
                case "16":
                    inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_16mmCu;
                    break;
                case "18":
                    switch (str3)
                    {
                        case "АЛ":
                            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_18mmAl;
                            break;
                        case "АЛЭ":
                            break;
                        case "АЛБГ":
                            break;
                        case "АЛГФ":
                            break;
                        case "М":
                            break;
                        case "МБГ":
                            break;
                        case "НЖ2":
                            break;
                        case "НЖ3":
                            break;
                    }
                    break;
                case "20":
                    switch (str3)
                    {
                        case "АЛ":
                            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_20mmAl;
                            break;
                        case "АЛЭ":
                            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_20mmEpoxy;
                            break;
                        case "АЛБГ":
                            break;
                        case "АЛГФ":
                            break;
                        case "М":
                            break;
                        case "МБГ":
                            break;
                        case "НЖ2":
                            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_20mm304;
                            break;
                        case "НЖ3":
                            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_20mm304;
                            break;
                    }
                    break;
                case "25":
                    switch (str3)
                    {
                        case "АЛ":
                            inputData.SelectedFin = Calculation.TO.Main.Properties.Resources._0_25mmAl;
                            break;
                        case "АЛЭ":
                            break;
                        case "АЛБГ":
                            break;
                        case "АЛГФ":
                            break;
                        case "М":
                            break;
                        case "МБГ":
                            break;
                        case "НЖ2":
                            break;
                        case "НЖ3":
                            break;
                    }
                    break;
            }

            // Шаг оребрения
            switch (str)
            {
                case "18":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._1_8mm;
                    break;
                case "20":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._2_0mm;
                    break;
                case "22":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._2_2mm;
                    break;
                case "25":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._2_5mm;
                    break;
                case "30":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._3_0mm;
                    break;
                case "35":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._3_5mm;
                    break;
                case "40":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._4_0mm;
                    break;
                case "45":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._4_5mm;
                    break;
                case "50":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._5_0mm;
                    break;
                case "70":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._7_0mm;
                    break;
                case "120":
                    inputData.SelectedStepFin = Calculation.TO.Main.Properties.Resources._12_0mm;
                    break;
            }

            // Число рядов
            mas = codes[4].Split('x', 'х');
            str = mas[0].Substring(1, mas[0].Length - 1);
            inputData.ValuePipe = int.Parse(str);

            // Теплоноситель
            inputData.SelectedFluid = saveCodes.SelectedFluid;

            // Концентрация гликоля
            inputData.I_SoleCnz = saveCodes.I_SoleCnz;

            // Вариант расчёта - текст
            inputData.SelectedVariantRasch = saveCodes.SelectedVariantRasch;

            // Колическтво отводов
            inputData.ValueCircuits = saveCodes.ValueCircuits;
            return true;
        }

        /// <summary>
        /// В зависимости от выбранного теплообменика
        /// </summary>
        /// <param name="saveCodes"></param>
        protected virtual void SetSelectHeatExchanger(SaveCodes saveCodes)
        {
        }

        /// <summary>
        /// Прямой расчёт
        /// </summary>
        /// <returns>Расчёт успешен ? true - да/ false - нет</returns>
        private bool CalcDirect()
        {
            bool error = false;
            //  errorI = 1 - коэффициент запаса O_LRes < 0
            //  errorI = 2 - не возможно использовать эту геометрию, измените на другую
            //  errorI = 3 - сообщение об ощибке из Dll
            bool bErr;
            StaticData.ErrorDll = "";
            bool isCorrect = false;
            ClearOutputData();
            while (true)
            {
                // вызывается (для расчёта) dll в новом потоке 
                calculate.Calc(); // вызов метода расчёта в dll
                                  //if (!threadErrorResult)   // если расчёт удачен
                                  //{
                bErr = calculate.GetReturn();
                if (bErr)
                {
                    error = true;
                    StaticData.ErrorI = 3;
                    StaticData.ErrorDll = calculate.GetTxtErr();
                    break;
                }
                ReadOutput();
                //if (Check_O_LRes())
                //{
                isCorrect = true;
                break;
                //}
                //}
                //else // если dll зависло
                //{
                //    error = true;
                //    StaticData.ErrorI = 5;
                //    break;
                //}
            }
            if (isCorrect)
            {
                // если не найден расчёт на заданную геометрию
                bool returnRes;
                if (!SelectOut(out returnRes))
                {
                    if (returnRes)
                    {
                        error = true;
                        StaticData.ErrorI = 4;
                    }
                }
            }
            return error;
        }

        /// <summary>
        /// Прямой расчёт по масовому расходу
        /// </summary>
        /// <returns>Расчёт успешен ? true - да/ false - нет</returns>
        private bool CalcDirectMassFlow()
        {
            bool error = false;
            while (true)
            {
                error = false;
                //  errorI = 1 - коэффициент запаса O_LRes < 0
                //  errorI = 2 - не возможно использовать эту геометрию, измените на другую
                //  errorI = 3 - сообщение об ощибке из Dll
                bool bErr;
                StaticData.ErrorDll = "";
                bool isCorrect = false;
                ClearOutputData();
                while (true)
                {
                    calculate.Calc();
                    bErr = calculate.GetReturn();
                    if (bErr)
                    {
                        error = true;
                        StaticData.ErrorI = 3;
                        StaticData.ErrorDll = calculate.GetTxtErr();
                        break;
                    }
                    ReadOutput();
                    //if (Check_O_LRes())
                    //{
                    isCorrect = true;
                    break;
                    //}
                }
                if (isCorrect)
                {
                    bool returnRes;
                    if (!SelectOut(out returnRes))
                    {
                        if (returnRes)
                        {
                            error = true;
                            StaticData.ErrorI = 4;
                        }
                    }
                    if (CheckMassFlow())
                    {
                        break;
                    }
                }
                else
                {
                    inputData.CapacityMassFlow = 1;
                    SetCapacity();
                }
            }
            return error;
        }

        /// <summary>
        /// Проверка совпадает ли массовый расход
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckMassFlow()
        {
            return true;
        }

        /// <summary>
        /// Изменение мощности для расчёта по массовому расходу
        /// </summary>
        protected virtual void SetCapacity()
        {
        }

        /// <summary>
        /// Сохраняет тип расчёта - прямой, обратный
        /// </summary>
        /// <param name="calcEnum"></param>
        protected void SetCalcType(CalcEnum calcEnum)
        {
            CalcType = calcEnum;
        }

        /// <summary>
        /// Установка свойств для прямого расчёта
        /// </summary>
        protected virtual void SetPropToCalc_DirectBase() { }

        /// <summary>
        /// Очистка выходных данных перед повторным расчётом
        /// </summary>
        protected virtual void ClearOutputData() { }

        /// <summary>
        /// Связывание входных данных в классе Calculate с этим классом
        /// </summary>
        protected virtual void ReadOutput() { }

        /// <summary>
        /// Филтрация вывода по типоразмеру в таблицу вывода результатов расчёта
        /// </summary>
        /// <returns>true - найден расчёт с такой геометрией, false - не найден расчёт с такой геометрией</returns>
        protected virtual bool SelectOut(out bool returnRes)
        {
            returnRes = true;
            return false;
        }

        /// <summary>
        /// установка входных данных специфичных для обратного расчёта - воздухоохладитель
        /// </summary>
        protected virtual void SetPropToCalc_ReverseBase() { }

        /// <summary>
        /// переопределение входных данных для табличного расчёта - воздухоохладитель
        /// </summary>
        protected virtual void SetPropToCalc_Table(OutView outView) { }

        /// <summary>
        /// Установка свойств из окна "Дополнительные параметры" 
        /// </summary>
        protected void SetPropAdvanceParams()
        {
            foreach (var item in lines)
            {
                /// Давление окружающего воздуха
                if (item.Field == "I_BaseBar")
                {
                    item.Content = inputData.ProjectsOut.I_BaseBar = advanceParamsViewModel.I_BaseBar.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                /// коэффициент загрязнения воздуха
                if (item.Field == "I_FoulingE")
                {
                    item.Content = advanceParamsViewModel.Get_I_FoulingE().ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                /// Коэффициент загрязнения трубы
                if (item.Field == "I_FoulingI")
                {
                    item.Content = advanceParamsViewModel.Get_I_FoulingI().ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                /// запас поверхности
                if (item.Field == "I_ARes")
                {
                    item.Content = advanceParamsViewModel.I_ARes.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                /// Резерв мощности
                if (item.Field == "I_LRes")
                {
                    item.Content = advanceParamsViewModel.I_LRes.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                /// Неравное число ходов в контурах
                if (item.Field == "I_Uneven")
                {
                    //item.Content = advanceParamsViewModel.I_Uneven;
                    item.Content = inputData.I_Uneven = "0";
                    continue;
                }
                /// Плотность окружающего воздуха
                if (item.Field == "I_BaseDens")
                {
                    item.Content = advanceParamsViewModel.I_BaseDens.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
            }
        }       

        /// <summary>
        /// генерация производственного кода
        /// </summary>
        /// <param name="code">строка кода из dll</param>
        /// <param name="line"></param>
        /// <returns> строка производственного кода</returns>
        protected string GetProductionCode(string code, OutViewLines line)
        {
            string productionCode = "";
            try
            {
                string[] codes = code.Split('/');
                switch (code.Substring(0, 3))
                {
                    case "VWH":
                        // 1 - назначение теплоносителя
                        productionCode += "ВНВ";
                        break;
                    case "VWC":
                        // 1 - назначение теплоносителя
                        productionCode += "ВОВ";
                        break;
                    case "VSH":
                        // 1 - назначение теплоносителя
                        productionCode += "ВНП";
                        break;
                    case "VCO":
                        // 1 - назначение теплоносителя
                        productionCode += "ВНФ";
                        break;
                    case "VDX":
                        // 1 - назначение теплоносителя
                        productionCode += "ВОФ";
                        break;
                }
                productionCode += "/";
                string tubeSystem = code.Substring(3, 1);
                switch (tubeSystem)
                {
                    case "A":
                        // 2 - расположение трубок
                        productionCode += "П";
                        break;
                    case "C":
                        // 2 - расположение трубок
                        productionCode += "Ш";
                        break;
                }
                string highCode = code.Substring(4, 2);
                if (inputData.SelectGeometry.Contains(highCode))
                {
                    // 3 - расстояние между трубками по вертикали, мм
                    productionCode += highCode;
                }
                productionCode += "х";
                // 4 - расстояние между трубками по горизонтали, мм
                productionCode += code.Substring(7, 2);
                productionCode += "/";
                string[] str2 = codes[1].Split('x');
                string lowCode = "";
                int lowCodeLen = 0;
                if (str2[0].Length == 2)
                {
                    lowCode = codes[1].Substring(0, 2);
                    lowCodeLen = 3;
                }
                if (str2[0].Length == 1)
                {
                    lowCode = "0";
                    lowCode += codes[1].Substring(0, 1);
                    lowCodeLen = 2;
                }
                if (inputData.SelectGeometry.Contains(lowCode))
                {
                    // 5 - диаметр трубки, мм
                    productionCode += lowCode;
                    productionCode += "x";
                    // 6 - стократная толщина стенки трубки
                    productionCode += codes[1].Substring(lowCodeLen, 2);
                    string mat = codes[1].Substring(lowCodeLen + 2, 3);
                    // 7 - материал трубки
                    switch (mat)
                    {
                        case "CUS":
                            productionCode += "М/";
                            break;
                        case "CUI":
                            productionCode += "МО/";
                            break;
                        case "CNS":
                            productionCode += "МН/";
                            break;
                        case "SS1":
                            productionCode += "НЖ1/";
                            break;
                        case "SS2":
                            productionCode += "НЖ2/";
                            break;
                    }
                }
                // 8 - тип ламели
                productionCode += "П";
                // 9 - десятикратное расстояние между ламелями (шаг оребрения)
                productionCode += codes[2].Substring(1, 2);

                productionCode += "x";
                string[] fins = inputData.SelectedFin.Split(' ');
                //double finWidth = Double.Parse(fins[0].ToString(), CultureInfo.InvariantCulture) * 100;
                // 10 - стократная толщина ламели (оребрения)
                productionCode += codes[2].Substring(4, 2);

                if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources.AlSmall)
                {
                    // 11 - материал ламелей (оребрения)
                    productionCode += "АЛ";
                }
                if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources.CuSmall)
                {
                    // 11 - материал ламелей (оребрения)
                    productionCode += "М";
                }
                productionCode += "/Т";
                // 12 - количество рядов трубок в направлении воздушного потока
                if (codes[3].Substring(1, 1) != "0")
                {
                    productionCode += codes[3].Substring(1, 1);
                }
                productionCode += codes[3].Substring(2, 1);
                productionCode += "x";
                // 13 - количество трубок в одном ряду по вертикали
                productionCode += codes[3].Substring(4, 2);
                productionCode += "-";
                // 14 - ширина проходного (живого) сечения, мм
                productionCode += codes[3].Substring(7, codes[3].Length - 7);
                productionCode += "/ГК";
                // 15 - количество гидравлических независимых контуров (количество пар коллекторов) от 1 до 9
                // 16 - количество циркуляционных контуров в одном гидравлическом контуре
                // 17 - количество трубок в одном циркуляционном контуре
                productionCode += codes[4].Substring(2, codes[4].Length - 2);
                productionCode += "/";
                string str = codes[5].Substring(0, 1);
                // 18 - количество пар коллекторов
                productionCode += "1";
                // 19 - тип коллектора
                switch (str)
                {
                    case "I":
                        productionCode += "П";
                        break;
                    case "L":
                        productionCode += "Г";
                        break;
                    case "T":
                        productionCode += "Т";
                        break;
                }
                str = codes[5].Substring(1, 1);
                // 20 - количество патрубков в одном коллекторе (см. п. 19)
                productionCode += str;
                productionCode += "Д";
                str = codes[5].Substring(3, 1);
                // 21 - диаметр коллекторов и патрубков на входе
                productionCode += str;
                productionCode += "-";
                // 22 - диаметр коллекторов и патрубков на выходе
                productionCode += codes[5].Substring(5, 1);
                str = codes[5].Substring(6, codes[5].Length - 6);
                // 23 - материал коллекторов и патрубков                               
                productionCode += inputDataService.GetProdCode_I_MatHdr();
                // 24 - тип патрубков (присоединение патрубков к трубопроводам)
                ///productionCode += codes[5].Substring(i, 1);
                productionCode += inputDataService.GetProdCode_I_ConType();

                // Капилляры только для Испарителя
                if (CalcModeV == CalcMode.Evaporater)
                {
                    // 25 - диаметр капилляра, мм
                    productionCode += inputDataService.GetProdCode_O_Capilar();
                    // 26 - длина капилляра, мм
                    productionCode += inputDataService.GetProdCode_O_CapLen();
                }

                productionCode += "/";
                // 27 - обозначение в соответствии с расположением в пространстве и подключением теплоносителя
                productionCode += inputDataService.GetProdCode_I_Esapo(CalcModeV);
                // 28 - направление потока воздуха
                productionCode += inputDataService.GetProdCode_AirFlowDirection();
                // 29 - подключение теплоносителя
                productionCode += inputDataService.GetProdCode_ConnectingCoolant();
                // 30 - ориентация патрубков
                productionCode += inputDataService.GetProdCode_I_CDir();
                //productionCode += codes[6];
                productionCode += "/";
                // 31 - исполнение корпуса
                productionCode += inputDataService.GetProdCode_CasingType();
                // 32 - материал корпуса                
                string casMat = inputDataService.GetProdCode_I_CasMat();
                if (!string.IsNullOrEmpty(casMat))
                {
                    productionCode += casMat;
                }
                else
                {
                    productionCode += "-";
                }
                // 33 - размер отгиба нижней крышки, мм
                productionCode += GetProdCode_O_FrameB();// ".30";
                // 34 - размер отгиба верхней крышки, мм
                productionCode += GetProdCode_O_FrameT();//".30";
                // 35 - десятикратная толщина материала крышек
                productionCode += GetProdCode_O_FrameThk();//"х15";
                // 36 - размер отгиба передней решётки (возле коллектора), мм
                productionCode += GetProdCode_O_FrameL(); //"-210";
                // 37 - размер отгиба задней решётки, мм
                productionCode += GetProdCode_O_FrameR(); //".100";
                // 38 - десятикратная толщина материала решёток
                productionCode += "х30";
                // 39 - Маркировка специального исполнения - Х. Используется в случаях, когда теплообменник невозможно 
                //productionCode += "Х";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return productionCode;
        }

        /// <summary>
        /// генерация клинтского короткого кода
        /// </summary>
        /// <param name="code">строка кода из dll</param>
        /// <param name="line"></param>
        /// <returns>строка клинтского короткого кода</returns>
        protected string GetProductionShortCode(string code, OutViewLines line)
        {
            string shortCode = "";
            try
            {
                #region Наименование   1
                string[] codes = code.Split('/');
                switch (code.Substring(0, 3))
                {
                    case "VWH":
                        // 1 - назначение теплоносителя
                        shortCode += "ВНВ";
                        break;
                    case "VWC":
                        // 1 - назначение теплоносителя
                        shortCode += "ВОВ";
                        break;
                    case "VSH":
                        // 1 - назначение теплоносителя
                        shortCode += "ВНП";
                        break;
                    case "VCO":
                        // 1 - назначение теплоносителя
                        shortCode += "ВНФ";
                        break;
                    case "VDX":
                        // 1 - назначение теплоносителя
                        shortCode += "ВОФ";
                        break;
                }
                #endregion
                #region Трубный пучок   2
                string highCode = code.Substring(4, 2);
                int lowCodeLen = 3;
                switch (highCode)
                {
                    case "50":
                        shortCode += "2";
                        break;
                    case "48":
                        shortCode += "1";
                        break;
                    case "35":
                        shortCode += "3";
                        break;
                    case "25":
                        string lowCode = codes[1].Substring(0, 2);
                        switch (lowCode)
                        {
                            case "10":
                                shortCode += "4";
                                break;
                            case "07":
                            case "7x":
                                lowCodeLen = 2;
                                shortCode += "5";
                                break;
                        }
                        break;
                }
                #endregion
                #region Материал труб   3
                // стократная толщина стенки трубки
                string smallCode = codes[1].Substring(lowCodeLen, 2);
                // материал трубки
                string mat = codes[1].Substring(lowCodeLen + 2, 3);
                switch (highCode)
                {
                    case "50":
                        switch (smallCode)
                        {
                            case "32":
                                // Медь гладкая
                                // толщиной 0,3 мм для пучка 50 * 25 * 12;
                                shortCode += "4";
                                break;
                            case "50":
                                // Медь гладкая утолщенная
                                // (толщиной 0,5 мм для всех пучков, кроме 25*22*7 - 0,4 мм)
                                shortCode += "6";
                                break;
                        }
                        break;
                    case "48":
                        switch (smallCode)
                        {
                            case "70":
                                // Сталь нержавеющая без покрытия AISI 304
                                // (толщиной 0,7 мм для пучка 48*42*16;
                                if (mat == "SS1")
                                {
                                    shortCode += "2";
                                }
                                break;
                            case "40":
                                // Медь гладкая
                                // (толщиной 0,4 мм для пучка 48*42*16;
                                shortCode += "4";
                                break;
                            case "50":
                                // Медь гладкая утолщенная
                                // (толщиной 0,5 мм для всех пучков, кроме 25*22*7 - 0,4 мм)
                                shortCode += "6";
                                break;
                            case "100":
                                if (mat == "SS1")
                                {
                                    // Сталь нержавеющая утолщенная без покрытия AISI 304
                                    // (толщиной 1,0 мм для пучка 48*42*16;
                                    shortCode += "7";
                                }
                                else if (mat == "CNS")
                                {
                                    // Медно-никелевый сплав
                                    // (толщиной 1,0 мм для всех пучков)
                                    shortCode += "8";
                                }
                                else if (mat == "SS2")
                                {
                                    // Сталь нержавеющая утолщенная без покрытия AISI 316
                                    // (толщиной 1,0 мм для пучка 48*42*16;
                                    shortCode += "9";
                                }
                                break;
                        }
                        break;
                    case "35":
                        switch (smallCode)
                        {
                            case "30":
                                // Медь гладкая
                                // толщиной 0,3 мм для пучка 35 * 30 * 12;
                                shortCode += "4";
                                break;
                            case "32":
                                // Медь оребренная
                                // (толщиной 0,32 мм для пучка 35 * 30 * 12
                                shortCode += "5";
                                break;
                            case "50":
                                // Медь гладкая утолщенная
                                // (толщиной 0,5 мм для всех пучков, кроме 25*22*7 - 0,4 мм)
                                shortCode += "6";
                                break;
                            case "100":
                                if (mat == "CNS")
                                {
                                    // Медно-никелевый сплав
                                    // (толщиной 1,0 мм для всех пучков)
                                    shortCode += "8";
                                }
                                break;
                        }
                        break;
                    case "25":
                        switch (smallCode)
                        {
                            case "30":
                                if (mat == "CUS")
                                {
                                    // Медь гладкая
                                    // толщиной 0,3 мм для пучка 25*22*10;
                                    shortCode += "4";
                                }
                                if (mat == "CUI")
                                {
                                    // Медь оребренная
                                    // толщиной 0,3 мм для пучка 25*22*10;
                                    shortCode += "5";
                                }
                                break;
                            case "28":
                                if (mat == "CUS")
                                {
                                    // Медь гладкая
                                    // толщиной 0,28 мм для пучка 25 * 22 * 7;
                                    // толщиной 0,28 мм для пучка 25 * 22 * 10;
                                    shortCode += "4";
                                }
                                if (mat == "CUI")
                                {
                                    // Медь оребренная
                                    // толщиной 0,28 мм для пучка 25 * 22 * 7;
                                    shortCode += "5";
                                }
                                break;
                            case "50":
                                // Медь гладкая утолщенная
                                // (толщиной 0,5 мм для всех пучков, кроме 25*22*7 - 0,4 мм)
                                shortCode += "6";
                                break;
                        }
                        break;
                }
                #endregion
                #region Материал оребрения   4
                string[] fins = inputData.SelectedFin.Split(' ');

                if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources.AlSmall)
                {
                    // 11 - материал ламелей (оребрения)
                    if (fins.Length == 3)
                    {
                        // Алюминий без покрытия
                        shortCode += 3;
                    }
                    else
                    {
                        string[] mat_str = Calculation.TO.Main.Properties.Resources._0_15mmEpoxy.Split(' ');
                        if (fins[3].ToLower() == mat_str[3].ToLower())
                        {
                            // Алюминий с эпоксидным покрытием
                            shortCode += 9;
                        }
                    }
                }
                else if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources._0_20mm304.Split(' ')[2])
                {
                    // 11 - материал ламелей (оребрения)
                    shortCode += 1;
                }
                else if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources.CuSmall)
                {
                    // 11 - материал ламелей (оребрения)
                    shortCode += 4;
                }
                #endregion
                #region Исполнение корпуса   5
                shortCode += ".";
                string casingType = inputDataService.GetShortCode_CasingType();
                if (!string.IsNullOrEmpty(casingType))
                {
                    shortCode += casingType;
                    shortCode += "-";
                }
                else
                {
                    shortCode += "1-";
                }
                #endregion
                #region Ширина ЖС, см   6

                string str2 = codes[3].Substring(7, codes[3].Length - 7);
                if (str2.Length == 4)
                {
                    shortCode += str2.Substring(0, 3);
                }
                else if (str2.Length == 3)
                {
                    shortCode += "0";
                    shortCode += str2.Substring(0, 2);
                }
                shortCode += "-";
                #endregion
                #region Высота ЖС, см   7
                if (line.O_HeightInt.Length == 4)
                {
                    shortCode += line.O_HeightInt.Substring(0, 3);
                }
                else if (line.O_HeightInt.Length == 3)
                {
                    shortCode += "0";
                    shortCode += line.O_HeightInt.Substring(0, 2);
                }
                shortCode += "-";
                #endregion
                #region Трубок в направление потока   8
                shortCode += codes[3].Substring(1, 2);
                shortCode += "-";
                #endregion
                #region Шаг оребрения   9
                shortCode += codes[2].Substring(1, 2);
                shortCode += "-";
                #endregion
                #region Трубок в одном контуре   10
                string[] str = codes[4].Substring(2, codes[4].Length - 2).Split('-', 'x');
                if (str[2].Length == 1)
                {
                    shortCode += "0";
                }
                shortCode += str[2];
                shortCode += "-";
                #endregion
                #region Ориентация ТО   11
                //shortCode += "1";
                shortCode += inputDataService.GetProdCode_I_Esapo(CalcModeV);
                shortCode += "-";
                #endregion
                #region Материал коллекторов и патрубков   12
                string matHdr = inputDataService.GetShortCode_I_MatHdr();
                if (!string.IsNullOrEmpty(matHdr))
                {
                    shortCode += matHdr;
                }
                else
                {
                    shortCode += "1";
                }
                #endregion
                #region Резьба на патрубках   13
                string conType = inputDataService.GetShortCode_I_ConType();
                if (!string.IsNullOrEmpty(conType))
                {
                    shortCode += conType;
                }
                else
                {
                    shortCode += "1";
                }
                #endregion
                #region Материал корпуса   14
                string casMat = inputDataService.GetShortCode_I_CasMat();
                if (!string.IsNullOrEmpty(casMat))
                {
                    shortCode += casMat;
                    shortCode += "-";
                }
                else
                {
                    shortCode += "1";
                    shortCode += "-";
                }
                #endregion
                #region Кол. гидравлических независимых контуров   15
                str = codes[4].Substring(2, codes[4].Length - 2).Split('-', 'x');
                shortCode += str[0];
                shortCode += "-";
                #endregion
                #region Тип и количество патрубков   16
                string str1 = codes[5].Substring(0, 1);
                switch (str1)
                {
                    case "I":
                        // "П";
                        shortCode += "4";
                        break;
                    case "L":
                        // "Г";
                        shortCode += "1";
                        break;
                    case "T":
                        // "Т";
                        shortCode += "2";
                        break;
                }
                shortCode += "-";
                #endregion
                #region Диаметр коллектора вход   17               
                shortCode += GetShortCode_O_ConIN();
                shortCode += "-";
                #endregion
                #region Диаметр коллектора выход   18
                shortCode += GetShortCode_O_ConOut();
                #endregion
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return shortCode;
        }

        /// <summary>
        /// Получает типоразмер из производственного кода (из Dll)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected int GetGeometry(string code)
        {
            string geometry = "";
            string[] codes = code.Split('/');
            geometry += codes[0].Substring(4, 2);
            string[] codes2 = codes[1].Split('x');
            if (codes2[0].Length == 2)
            {
                geometry += codes[1].Substring(0, 2);
            }
            else if (codes2[0].Length == 1)
            {
                geometry += "0";
                geometry += codes[1].Substring(0, 1);
            }
            return int.Parse(geometry);
        }

        /// <summary>
        /// Установка строки ошибки в зависимости от типа ошибки
        /// </summary>
        /// <param name="error"></param>
        protected void SetErrorStr(bool error)
        {
            if (error)
            {
                //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.strError);
                switch (StaticData.ErrorI)
                {
                    case 0:
                        //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.Error_0);
                        errorStr = Calculation.TO.Main.Properties.Resources.Error_0;
                        break;
                    case 1:
                        //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.Error_1);
                        errorStr = Calculation.TO.Main.Properties.Resources.Error_1;
                        break;
                    case 2:
                        //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.Error_2);
                        errorStr = Calculation.TO.Main.Properties.Resources.Error_2;
                        break;
                    case 3:
                        //logs.Logger.Error(StaticData.ErrorDll);
                        errorStr = StaticData.ErrorDll;
                        break;
                    case 4:
                        //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.Error_6);
                        errorStr = Calculation.TO.Main.Properties.Resources.Error_6;
                        break;
                    case 5:
                        //logs.Logger.Error(Calculation.TO.Main.Properties.Resources.Error_7);
                        errorStr = Calculation.TO.Main.Properties.Resources.Error_7;
                        break;
                }
                result = 1;
            }
            else
            {
                result = 0;
            }
        }

        /// <summary>
        /// Филтрация вывода по типоразмеру в таблицу вывода результатов табличного расчёта
        /// </summary>
        protected virtual void SelectOutTable() { }

        /// <summary>
        /// Получение значения поля O_FrameB
        /// </summary>
        /// <returns></returns>
        protected string GetProdCode_O_FrameB()
        {
            switch (CalcType)
            {
                case CalcEnum.Direct:
                case CalcEnum.Reverse:
                    return inputDataService.GetProdCode_O_FrameB();
                case CalcEnum.Table:
                    return tableDataService.GetProdCode_O_FrameB();
            }
            return "";
        }

        /// <summary>
        /// Получение значения поля O_FrameT
        /// </summary>
        /// <returns></returns>
        protected string GetProdCode_O_FrameT()
        {
            switch (CalcType)
            {
                case CalcEnum.Direct:
                case CalcEnum.Reverse:
                    return inputDataService.GetProdCode_O_FrameT();
                case CalcEnum.Table:
                    return tableDataService.GetProdCode_O_FrameT();
            }
            return "";
        }

        /// <summary>
        /// Получение значения поля O_FrameThk
        /// </summary>
        /// <returns></returns>
        protected string GetProdCode_O_FrameThk()
        {
            switch (CalcType)
            {
                case CalcEnum.Direct:
                case CalcEnum.Reverse:
                    return inputDataService.GetProdCode_O_FrameThk();
                case CalcEnum.Table:
                    return tableDataService.GetProdCode_O_FrameThk();
            }
            return "";
        }

        /// <summary>
        /// Получение значения поля O_FrameL
        /// </summary>
        /// <returns></returns>
        protected string GetProdCode_O_FrameL()
        {
            switch (CalcType)
            {
                case CalcEnum.Direct:
                case CalcEnum.Reverse:
                    return inputDataService.GetProdCode_O_FrameL();
                case CalcEnum.Table:
                    return tableDataService.GetProdCode_O_FrameL();
            }
            return "";
        }

        /// <summary>
        /// Получение значения поля O_FrameR
        /// </summary>
        /// <returns></returns>
        protected string GetProdCode_O_FrameR()
        {
            switch (CalcType)
            {
                case CalcEnum.Direct:
                case CalcEnum.Reverse:
                    return inputDataService.GetProdCode_O_FrameR();
                case CalcEnum.Table:
                    return tableDataService.GetProdCode_O_FrameR();
            }
            return "";
        }

        /// <summary>
        /// установка входных данных
        /// </summary>
        protected void SetPropToCalc()
        {
            lines = new ObservableCollection<DBLine>(calculate.GetListInput());
            linesOut = new ObservableCollection<DBLineOut>(calculate.GetListOutput());
            foreach (var item in lines)
            {
                // 0 - optimize
                // 1 - 4842
                // 2 - 5025
                // 3 - 3530
                // геометрия
                if (item.Field == "I_Geometry")
                {
                    if (inputData.SelectGeometry == Calculation.TO.Main.Properties.Resources._5012)
                    {
                        item.Content = "2";
                    }
                    else if (inputData.SelectGeometry == Calculation.TO.Main.Properties.Resources._4816)
                    {
                        item.Content = "1";
                    }
                    else if (inputData.SelectGeometry == Calculation.TO.Main.Properties.Resources._3512)
                    {
                        item.Content = "3";
                    }
                    else if (inputData.SelectGeometry == Calculation.TO.Main.Properties.Resources._2510)
                    {
                        item.Content = "4";
                    }
                    else
                    {
                        item.Content = "0";
                        //if (inputData.SelectGeometry == Calculation.TO.Main.Properties.Resources.2507)
                        //{
                        //    throw new ValidErrorException(Calculation.TO.Main.Properties.Resources.ValidationError4, inputData.SelectGeometry,
                        //            Calculation.TO.Main.Properties.Resources.I_Geometry);
                        //}
                    }
                    continue;
                }

                SetSelectHeatExchangerPropToCalc(item);


                // толщина оребрения
                string[] fins = inputData.SelectedFin.Split(' ');
                if (item.Field == "I_FinThk")
                {
                    item.Content = "0";
                    finThk.SetFinThickness(fins[0]);
                    //item.Content = inputData.ProjectsOut.I_FinThk = fins[0];
                    inputData.ProjectsOut.I_FinThk += " " + Calculation.TO.Main.Properties.Resources.mm;
                    continue;
                }
                // 1 - Copper
                // 2 - Aluminium
                // 3 - Aluminium Epoxy
                // 12 - Aluminium Hydrophil
                // 13 - Stainless steel 321
                if (item.Field == "I_MatFins") // 
                {
                    if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources.CuSmall)
                    {
                        item.Content = "1";
                        inputData.I_MatFins = "1";
                        inputData.ProjectsOut.I_MatFins = Calculation.TO.Main.Properties.Resources.CuSmall;
                    }
                    if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources.AlSmall)
                    {
                        if (fins.Length == 3)
                        {
                            item.Content = "2";
                            inputData.I_MatFins = "2";
                            inputData.ProjectsOut.I_MatFins = Calculation.TO.Main.Properties.Resources.AlSmall;
                            if (fins[0] == "0.20")
                            {
                                throw new ValidErrorException(Calculation.TO.Main.Properties.Resources.ValidationError4, inputData.SelectedFin,
                                    Calculation.TO.Main.Properties.Resources.I_FinThk);
                            }
                            if (fins[0] == "0.25")
                            {
                                throw new ValidErrorException(Calculation.TO.Main.Properties.Resources.ValidationError4, inputData.SelectedFin,
                                    Calculation.TO.Main.Properties.Resources.I_FinThk);
                            }
                        }
                        else
                        {
                            item.Content = "3"; // Calculation.TO.Main.Properties.Resources.EpoxySmall
                            inputData.I_MatFins = "3";
                            inputData.ProjectsOut.I_MatFins = Calculation.TO.Main.Properties.Resources.EpoxySmall;
                        }
                    }
                    if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources.stain_steel304Small)
                    {
                        item.Content = "13";
                        inputData.I_MatFins = "13";
                        inputData.ProjectsOut.I_MatFins = Calculation.TO.Main.Properties.Resources.stain_steel304Small;
                        throw new ValidErrorException(Calculation.TO.Main.Properties.Resources.ValidationError4, inputData.SelectedFin,
                                    Calculation.TO.Main.Properties.Resources.I_FinThk);
                    }
                    continue;
                }

                // Высота оребрения
                if (item.Field == "I_HeightInt")
                {
                    item.Content = inputData.ProjectsOut.I_HeightIntStr = inputData.ValueHightFin.ToString("G", CultureInfo.InvariantCulture);
                    inputData.ProjectsOut.I_HeightIntStr += " " + Calculation.TO.Main.Properties.Resources.mm;
                    continue;
                }

                // Длина оребрения
                if (item.Field == "I_WidthInt")
                {
                    item.Content = inputData.ProjectsOut.I_WidthIntStr = inputData.ValueWidthFin.ToString("G", CultureInfo.InvariantCulture);
                    inputData.ProjectsOut.I_WidthIntStr += " " + Calculation.TO.Main.Properties.Resources.mm;
                    continue;
                }


                // Размеры и материал трубки
                string[] pipes = inputData.SelectedPipe.Split(' ');
                inputData.ProjectsOut.I_Pipe = pipes[0] + " " + Calculation.TO.Main.Properties.Resources.mm;
                if (item.Field == "I_PipeThk")
                {
                    item.Content = inputData.ProjectsOut.I_PipeThk = pipes[2].ToString();
                    inputData.ProjectsOut.I_PipeThk += " " + Calculation.TO.Main.Properties.Resources.mm;
                    continue;
                }
                // 1 - Copper
                // 14 - Copper Inner Groved
                // 11 - Copper Nickel
                // 4 - Stainless steel SS304
                // 5 - Stainless steel SS316
                if (item.Field == "I_MatRows") // 
                {
                    if (pipes[4].ToLower() == Calculation.TO.Main.Properties.Resources.CuSmall)
                    {
                        if (pipes.Length == 5)
                        {
                            item.Content = inputData.I_MatRows = "1";
                            inputData.ProjectsOut.I_MatRows = Calculation.TO.Main.Properties.Resources.CuSmall;
                        }
                        else
                        {
                            item.Content = inputData.I_MatRows = "14";
                            throw new ValidErrorException(Calculation.TO.Main.Properties.Resources.ValidationError4, inputData.SelectedPipe,
                                Calculation.TO.Main.Properties.Resources.I_Pipe);
                            //I_MatRows = Calculation.TO.Main.Properties.Resources.CuSmall;
                        }
                    }
                    if (pipes[4].ToLower() == Calculation.TO.Main.Properties.Resources.stain_steel304Small)
                    {
                        item.Content = inputData.I_MatRows = "4";
                        inputData.ProjectsOut.I_MatRows = Calculation.TO.Main.Properties.Resources.stain_steel304Small;
                        throw new ValidErrorException(Calculation.TO.Main.Properties.Resources.ValidationError4, inputData.SelectedPipe,
                                Calculation.TO.Main.Properties.Resources.I_Pipe);
                    }
                    continue;
                }

                if (advanceParamsViewModel.SelectAFPV == Calculation.TO.Main.Properties.Resources.True)
                {
                    // шаг оребрения
                    if (item.Field == "I_LamAbsFix")
                    {
                        item.Content = "";
                        continue;
                    }
                    if (item.Field == "I_LamAbsMax")
                    {
                        if (inputData.SelectGeometry == Calculation.TO.Main.Properties.Resources._5012)
                        {
                            // для типоразмера 5012 макс шаг оребрения = 12
                            item.Content = inputData.I_LamAbsMax = "12";

                            continue;
                        }
                        else
                        {
                            item.Content = inputData.I_LamAbsMax = "5";
                            continue;
                        }
                    }
                }
                else
                {
                    // шаг оребрения
                    if (item.Field == "I_LamAbsFix")
                    {
                        string[] stepFin = inputData.SelectedStepFin.Split(' ');
                        item.Content = stepFin[0];
                        double d = double.Parse(stepFin[0], CultureInfo.InvariantCulture);
                        if (d > 4)
                        {
                            throw new ValidErrorException(Calculation.TO.Main.Properties.Resources.ValidationError4, inputData.SelectedStepFin,
                                Calculation.TO.Main.Properties.Resources.I_FinStep);
                        }
                        continue;
                    }

                    if (item.Field == "I_LamAbsMax")
                    {
                        if (inputData.SelectGeometry == Calculation.TO.Main.Properties.Resources._5012)
                        {
                            // для типоразмера 5012 макс шаг оребрения = 12
                            item.Content = inputData.I_LamAbsMax = "12";
                            continue;
                        }
                        else
                        {
                            item.Content = inputData.I_LamAbsMax = "5";
                            continue;
                        }
                    }
                }

                #region Корпус и коллектор
                /// Материал корпуса
                if (item.Field == "I_CasMat")
                {
                    item.Content = inputDataService.GetShortCode_I_CasMat();
                    continue;
                }
                /// Присоединительный размер на входе в дюймах
                if (item.Field == "I_ConIn")
                {
                    item.Content = inputDataService.GetCalc_I_ConIn();
                    continue;
                }
                /// Присоединительный размер на выходе в дюймах
                if (item.Field == "I_ConOut")
                {
                    item.Content = inputDataService.GetCalc_I_ConIn();
                    continue;
                }
                /// Наличие резьбы на патрубке
                if (item.Field == "I_ConType")
                {
                    item.Content = inputDataService.GetCalc_I_ConType();
                    continue;
                }
                /// Покрытие коллектора
                if (item.Field == "I_CSheet")
                {
                    item.Content = inputDataService.GetShortCode_I_CSheet();
                    continue;
                }
                if (item.Field == "I_CSheetL")
                {
                    item.Content = inputDataService.GetShortCode_I_CSheetL();
                    continue;
                }
                /// материал коллектора
                if (item.Field == "I_MatHdr")
                {
                    item.Content = inputDataService.GetCalc_I_MatHdr();
                    continue;
                }
                if (item.Field == "I_CDir")
                {
                    item.Content = inputDataService.GetShortCode_I_CDir();
                    continue;
                }
                /// Конструкция/Подключение змеевика
                if (item.Field == "I_Esapo")
                {
                    item.Content = inputDataService.GetCalc_I_Esapo();
                    continue;
                }
                #endregion

                #region Параметры по умолчанию
                /// Влажность окружающего воздуха
                if (item.Field == "I_BaseHum")
                {
                    item.Content = inputData.I_BaseHum;
                    continue;
                }
                /// Режим расчёта влажности
                if (item.Field == "I_BaseMode")
                {
                    item.Content = inputData.I_BaseMode;
                    continue;
                }
                /// Высота над уровнем моря
                if (item.Field == "I_BaseSea")
                {
                    item.Content = inputData.I_BaseSea;
                    continue;
                }
                /// Исходная температура
                if (item.Field == "I_BaseTemp")
                {
                    item.Content = inputData.I_BaseTemp;
                    continue;
                }
                /// Изоляция корпуса
                if (item.Field == "I_CasIso")
                {
                    item.Content = inputData.I_CasIso;
                    continue;
                }
                /// Катодное покрытие - окраска окунанием
                if (item.Field == "I_CatCoat")
                {
                    item.Content = inputData.I_CatCoat;
                    continue;
                }
                /// Крышка отсека трубопровода коллектора и U-образного трубного колена
                if (item.Field == "I_CBox")
                {
                    item.Content = inputData.I_CBox;
                    continue;
                }
                /// Сертифицированный
                if (item.Field == "I_CertMode")
                {
                    item.Content = inputData.I_CertMode;
                    continue;
                }
                /// Качество корпуса каплеуловителя
                if (item.Field == "I_ElimMat")
                {
                    item.Content = inputData.I_ElimMat;
                    continue;
                }
                /// Тип каплеуловителя
                if (item.Field == "I_ElimTp")
                {
                    item.Content = inputData.I_ElimTp;
                    continue;
                }
                if (item.Field == "I_IseEta")
                {
                    item.Content = inputData.I_IseEta;
                    continue;
                }

                /// Режим работоспособности
                if (item.Field == "I_EtaMax")
                {
                    item.Content = inputData.I_EtaMax;
                    continue;
                }
                /// Звуконепроницаемое помещение для вентиляторов
                if (item.Field == "I_FanPlen")
                {
                    item.Content = inputData.I_FanPlen;
                    continue;
                }
                /// Тип рамы
                if (item.Field == "I_FrameTp")
                {
                    item.Content = inputData.I_FrameTp;
                    continue;
                }
                /// Использование полной высоты теплообменника
                if (item.Field == "I_FullH")
                {
                    item.Content = inputData.I_FullH;
                    continue;
                }
                /// Коль-во коллекторов
                if (item.Field == "I_HdrQta")
                {
                    item.Content = inputData.I_HdrQta;
                    continue;
                }
                /// Коррекция перепада давления воздуха
                if (item.Field == "I_KorrADP")
                {
                    item.Content = inputData.I_KorrADP;
                    continue;
                }
                /// Коррекция перепада среднего давления
                if (item.Field == "I_KorrKDP")
                {
                    item.Content = inputData.I_KorrKDP;
                    continue;
                }
                /// Максимальный шаг оребрения
                if (item.Field == "I_LamAbsMax")
                {
                    item.Content = inputData.I_LamAbsMax;
                    continue;
                }
                /// Качество рамы
                if (item.Field == "I_MatFrame")
                {
                    item.Content = inputData.I_MatFrame;
                    continue;
                }
                /// Качество рядов
                if (item.Field == "I_Mode")
                {
                    item.Content = inputData.I_Mode;
                    continue;
                }
                /// Качество дренажного поддона
                if (item.Field == "I_PanMat")
                {
                    item.Content = inputData.I_PanMat;
                    continue;
                }
                /// Тип дренажного поддона
                if (item.Field == "I_PanTp")
                {
                    item.Content = inputData.I_PanTp;
                    continue;
                }
                /// Переход на сечение круглой формы
                if (item.Field == "I_RCon")
                {
                    item.Content = inputData.I_RCon;
                    continue;
                }
                /// Специальный припой
                if (item.Field == "I_SpcSolder")
                {
                    item.Content = inputData.I_SpcSolder;
                    continue;
                }

                /// Корректировка внешней поверхности
                if (item.Field == "I_KorrFA")
                {
                    item.Content = "0";
                    continue;
                }

                /// Коррекция внутренней поверхности
                if (item.Field == "I_KorrFI")
                {
                    item.Content = "0";
                    continue;
                }
                #endregion
            }
        }

        /// <summary>
        /// Получение значения поля O_ConIN
        /// </summary>
        /// <returns></returns>
        protected string GetShortCode_O_ConIN()
        {
            switch (CalcType)
            {
                case CalcEnum.Direct:
                case CalcEnum.Reverse:
                    return inputDataService.GetShortCode_O_ConIN();
                case CalcEnum.Table:
                    return tableDataService.GetShortCode_O_ConIN();
            }
            return "";
        }

        /// <summary>
        /// Получение значения поля O_ConOut
        /// </summary>
        /// <returns></returns>I_MedTempIn
        protected string GetShortCode_O_ConOut()
        {
            switch (CalcType)
            {
                case CalcEnum.Direct:
                case CalcEnum.Reverse:
                    return inputDataService.GetShortCode_O_ConOut();
                case CalcEnum.Table:
                    return tableDataService.GetShortCode_O_ConOut();
            }
            return "";
        }

        protected virtual void SetSelectHeatExchangerPropToCalc(DBLine item)
        {
            // расход воздуха
            if (item.Field == "I_Airflow")
            {
                inputData.ProjectsOut.I_AirflowDX = inputData.ValueAirFlowDX;
                item.Content = inputDataService.GetString_ValueAirFlowDX();
            }
            // владность воздуха на входе
            if (item.Field == "I_AirHumIn")
            {
                inputData.ProjectsOut.I_AirHumInDX = inputData.ValueBaseHumDX;
                item.Content = inputData.ValueBaseHumDX.ToString("G", CultureInfo.InvariantCulture);
            }
            // температура воздуха на входе
            if (item.Field == "I_AirTempIn")
            {
                item.Content = inputData.ProjectsOut.I_AirTempInStrDX = inputDataService.GetString_I_AirTempInDX();
                inputData.ProjectsOut.I_AirTempInStrDX += " " + Calculation.TO.Main.Properties.Resources.degreeСelsius;
            }
        }
        #endregion
    }
}