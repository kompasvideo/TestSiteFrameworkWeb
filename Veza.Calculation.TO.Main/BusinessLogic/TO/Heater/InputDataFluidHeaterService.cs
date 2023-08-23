using Veza.HeatExchanger.Exceptions;
using System.Collections.ObjectModel;
using System.Globalization;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.ViewModels.Controls;
using Veza.HeatExchanger.Models.Main;
using Veza.HeatExchanger.BusinessLogic.TO.Heater;
using Veza.HeatExchanger.Interfaces.Mappers;
using System.Security;
using Veza.Calculation.TO.Main.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Veza.HeatExchanger.Services.Heater
{
    sealed public class InputDataFluidHeaterService : CalculateBaseService, IInputDataFluidHeaterService, IExtInputDataFluidHeaterService
    {
        #region Внутренние поля и переменные
        private readonly IFluidHeaterMapper fluidMapper;
        /// <summary>
        /// были ли установлены параметры
        /// </summary>
        private bool IsSetParams { get; set; }
        /// <summary>
        /// был ли расчёт
        /// </summary>
        private bool IsCalc { get; set; }
        private InputDataFluidHeaterCoolerDTO inputParams;
        #endregion

        #region Конструктор
        public InputDataFluidHeaterService(AdvanceParamsViewModel advanceParamsViewModel, IInputData inputData, ICalculate calculate,
            ILogs logs, ITableDataService tableDataService, IInputDataService inputDataService,
            IFinThk finThk, IFluidHeaterMapper fluidMapper) :
            base(advanceParamsViewModel, calculate, logs, inputData, tableDataService, inputDataService, finThk)
        {
            this.fluidMapper = fluidMapper;
            //SetPropertiesFirst();
        }
        #endregion

        #region Методы открытые

        /// <summary>
        /// Получить входные параметры
        /// </summary>
        /// <returns></returns>
        public GetDataFluidHeaterCoolerDTO GetProperties()
        {
            SetPropertiesFirst();
            return fluidMapper.InputDataClassToInputParams();
        }

        /// <summary>
        /// Установить входные параметры
        /// </summary>
        /// <param name="inputParams"></param>
        public void SetProperties(InputDataFluidHeaterCoolerDTO inputParams)
        {
            SetPropertiesFirst();
            fluidMapper.InputToInputDataClass(inputParams);
            this.inputParams = inputParams;
            IsSetParams = true;
        }

        /// <summary>
        /// расчёт Воздухонагревателя
        /// </summary>
        /// <returns></returns>
        public bool Calc()
        {
            if (!IsSetParams) return false;
            SetCalcMode("HW");
            CalculationInit();
            switch (inputParams.SelectedMode)
            {
                case "direct":
                    CalcDirect(Calculation.TO.Main.Properties.Resources.PageFluidHeater_Direct);
                    break;
                case "reverse":
                    CalcReverse(Calculation.TO.Main.Properties.Resources.PageFluidHeater_Reverse);
                    break;
            }
            return IsCalc = true;
        }

        /// <summary>
        /// Получение результатов расчёта
        /// </summary>
        /// <returns></returns>
        public OutputDataFluidHeaterCoolerDTO GetResults()
        {
            OutputDataFluidHeaterCoolerDTO outputParams = new OutputDataFluidHeaterCoolerDTO();
            if (!IsCalc) return outputParams;
            if (GetResult() != 0)
            {
                // ошибка
                return outputParams;
            }
            // если ошибки расчёта нет
            inputData.CalcFluidHeater = true;
            calculate.End();
            return GetOutputParams();
        }

        /// <summary>
        /// Изменение геометрии
        /// </summary>
        /// <param name="geometry"></param>
        public void SetGeometry(string geometry)
        {
            // Проверка геометрии на правильность, что только 
            // 5012, 4816, 3512, 2510, 2507
            if (inputData.Geometry.Contains(geometry))
            {
                inputData.SelectGeometry = geometry;
            }
        }

        /// <summary>
        /// Получение изменённых параметров всвязи с изменением геометрии
        /// </summary>
        /// <returns></returns>
        public ChangesGeometryParamsTO GetChangesGeometryParams()
        {
            return new ChangesGeometryParamsTO()
            {
                Pipe = inputData.Pipe.ToList(),
                SelectedPipe = inputData.SelectedPipe,
                Fin = inputData.Fin.ToList(),
                SelectedFin = inputData.SelectedFin,
                StepFin = inputData.StepFin.ToList(),
                SelectedStepFin = inputData.SelectedStepFin
            };
        }
        public void End()
        {
            calculate.End();
        }

        #endregion

        #region Методы внутренние     

        /// <summary>
        /// Очистка выходных данных перед повторным расчётом
        /// </summary>
        protected override void ClearOutputData()
        {
            if (inputData.ProjectsOutView != null)
            {
                foreach (var item in inputData.ProjectsOutView)
                {
                    // был ли расчёт
                    item.Calc = false;
                    // Скорость воздуха
                    item.AirVelocity = "";
                    // Перепад давления сухой воздуха
                    item.PresDropDry = "";
                    // Резервная нагрузка
                    item.ReverseLoad = "";
                    // скорость жидкости             
                    item.MedVelo = "";
                    // Максимальное падение давления
                    item.MedKPa = "";
                    // Температура воздуха на выходе
                    item.AirTempOut = "";
                    // Расход жидкости
                    item.O_MedFlow = "";
                }
            }
        }

        /// <summary>
        /// установка входных данных специфичных для прямого расчёта - воздухонагреватель
        /// </summary>
        protected override void SetPropToCalc_DirectBase()
        {
            SetPropToCalc();
            SetPropToCalcHeatCooler();
            SetPropToCalcHeater_Direct();
        }

        /// <summary>
        /// Установка свойств специфичных для прямого расчёта - воздухонагреватель
        /// </summary>
        private void SetPropToCalcHeater_Direct()
        {
            foreach (var item in lines)
            {
                if (inputData.SelectedVariantRasch == Calculation.TO.Main.Properties.Resources.I_TempFluidOut)
                {
                    // Температура теплоносителя на выходе
                    if (item.Field == "I_MedTempOut")
                    {
                        item.Content = inputData.ValueVariantRaschHW.ToString("G", CultureInfo.InvariantCulture);
                        continue;
                    }
                    // Расход теплоносителя
                    if (item.Field == "I_MedFlow")
                    {
                        item.Content = "";
                        continue;
                    }
                }
                else
                {
                    // Расход теплоносителя
                    if (item.Field == "I_MedFlow")
                    {
                        item.Content = inputData.I_MedFlowHW.ToString("G", CultureInfo.InvariantCulture);
                        continue;
                    }
                    // Температура теплоносителя на выходе
                    if (item.Field == "I_MedTempOut")
                    {
                        item.Content = "";
                        continue;
                    }
                }

                // требуемая мощность
                if (item.Field == "I_Capacity")
                {
                    if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.Capacity)
                    {
                        item.Content = inputData.ValueCapacityHW.ToString("G", CultureInfo.InvariantCulture);
                        continue;
                    }
                    else
                    {
                        item.Content = "";
                        continue;
                    }
                }
                // температура воздуха на выходе
                if (item.Field == "I_AirTempOut")
                {
                    if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.I_AirTempOut)
                    {
                        item.Content = inputData.DirectAirTempOutHW.ToString("G", CultureInfo.InvariantCulture);
                        continue;
                    }
                    else
                    {
                        item.Content = "";
                        continue;
                    }
                }

                // Макс. перепад давления жидкости
                if (item.Field == "I_MedKPa")
                {
                    if (inputData.ValueMedKPaHW == 0)
                        item.Content = "40";
                    else item.Content = inputData.ValueMedKPaHW.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                /// Тип расчета
                if (item.Field == "I_Type")
                {
                    item.Content = inputData.I_Type = "1";
                    continue;
                }

            }
            SetPropAdvanceParams();
        }

        /// <summary>
        /// установка входных данных специфичных для обратного расчёта - воздухонагреватель
        /// </summary>
        protected override void SetPropToCalc_ReverseBase()
        {
            SetPropToCalc();
            SetPropToCalcHeatCooler();
            SetPropToCalcHeater_Reverse();
        }

        /// <summary>
        /// установка входных данных специфичных для обратного расчёта
        /// </summary>
        private void SetPropToCalcHeater_Reverse()
        {
            foreach (var item in lines)
            {
                if (inputData.SelectedVariantRasch == Calculation.TO.Main.Properties.Resources.I_TempFluidOut)
                {
                    // Температура теплоносителя на выходе
                    if (item.Field == "I_MedTempOut")
                    {
                        item.Content = inputData.ValueVariantRaschHW.ToString("G", CultureInfo.InvariantCulture);
                        continue;
                    }
                    // Расход теплоносителя
                    if (item.Field == "I_MedFlow")
                    {
                        item.Content = "";
                        continue;
                    }
                }
                else
                {
                    // Расход теплоносителя
                    if (item.Field == "I_MedFlow")
                    {
                        item.Content = inputData.ValueVariantRaschHW.ToString("G", CultureInfo.InvariantCulture);
                        continue;
                    }
                    // Температура теплоносителя на выходе
                    if (item.Field == "I_MedTempOut")
                    {
                        item.Content = "";
                        continue;
                    }
                }

                // число рядов
                if (item.Field == "I_Rows")
                {
                    item.Content = inputData.ValuePipe.ToString();
                    continue;
                }

                // Количество отводов
                if (item.Field == "I_Circuits")
                {
                    item.Content = inputData.ValueCircuits.ToString();
                    continue;
                }

                // требуемая мощность
                if (item.Field == "I_Capacity")
                {
                    item.Content = "";
                    continue;
                }

                // температура воздуха на выходе
                if (item.Field == "I_AirTempOut")
                {
                    item.Content = "";
                    continue;
                }

                // Макс. перепад давления жидкости
                if (item.Field == "I_MedKPa")
                {
                    if (inputData.ValueMedKPaHW == 0)
                        item.Content = "40";
                    else item.Content = inputData.ValueMedKPaHW.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                /// Тип расчета
                if (item.Field == "I_Type")
                {
                    item.Content = inputData.I_Type = "1";
                    continue;
                }
            }
            SetPropAdvanceParams();
        }

        /// <summary>
        /// переопределение входных данных для табличного расчёта - воздухонагреватель
        /// </summary>
        protected override void SetPropToCalc_Table(OutView outView)
        {
            foreach (var item in lines)
            {
                /// Геометрия
                if (item.Field == "I_Geometry")
                {
                    switch (outView.I_Geo)
                    {
                        // меняется трубка 
                        case 5012:
                            item.Content = "2";
                            break;
                        case 4816:
                            item.Content = "1";
                            break;
                        case 3512:
                            item.Content = "3";
                            break;
                        case 2510:
                            item.Content = "4";
                            break;
                        default:
                            item.Content = "0";
                            break;
                    }
                    continue;
                }

                // Размеры и материал трубки
                string[] pipes = outView.SelectedPipe.Split(' ');
                if (item.Field == "I_PipeThk")
                {
                    item.Content = pipes[2].ToString();
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
                            outView.I_MatRows = Calculation.TO.Main.Properties.Resources.CuSmall;
                        }
                        else
                        {
                            item.Content = inputData.I_MatRows = "14";
                        }
                    }
                    if (pipes[4].ToLower() == Calculation.TO.Main.Properties.Resources.stain_steel304Small)
                    {
                        item.Content = inputData.I_MatRows = "4";
                        outView.I_MatRows = Calculation.TO.Main.Properties.Resources.stain_steel304Small;
                    }
                    continue;
                }

                // толщина оребрения
                string[] fins = outView.SelectedFin.Split(' ');
                if (item.Field == "I_FinThk")
                {
                    //item.Content = fins[0];
                    item.Content = "0";
                    finThk.SetFinThickness(fins[0]);
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
                        outView.I_MatFins = Calculation.TO.Main.Properties.Resources.CuSmall;
                    }
                    if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources.AlSmall)
                    {
                        if (fins.Length == 3)
                        {
                            item.Content = "2";
                            inputData.I_MatFins = "2";
                            outView.I_MatFins = Calculation.TO.Main.Properties.Resources.AlSmall;
                        }
                        else
                        {
                            item.Content = "3"; // Calculation.TO.Main.Properties.Resources.EpoxySmall
                            inputData.I_MatFins = "3";
                            outView.I_MatFins = Calculation.TO.Main.Properties.Resources.EpoxySmall;
                        }
                    }
                    if (fins[2].ToLower() == Calculation.TO.Main.Properties.Resources.stain_steel304Small)
                    {
                        item.Content = "13";
                        inputData.I_MatFins = "13";
                        outView.I_MatFins = Calculation.TO.Main.Properties.Resources.stain_steel304Small;
                    }
                    continue;
                }

                // Длина оребрения
                if (item.Field == "I_WidthInt")
                {
                    item.Content = outView.I_WidthInt.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                // Высота оребрения
                if (item.Field == "I_HeightInt")
                {
                    item.Content = outView.I_HeightInt.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // число рядов
                if (item.Field == "I_Rows")
                {
                    if (CalcType == CalcEnum.Direct)
                    {
                        item.Content = "";
                    }
                    else
                    {
                        item.Content = outView.NoOfRows.ToString();
                    }
                    continue;
                }

                // Количество отводов
                if (item.Field == "I_Circuits")
                {
                    if (CalcType == CalcEnum.Direct)
                    {
                        item.Content = "";
                    }
                    else
                    {
                        item.Content = outView.Circuits.ToString();
                    }
                    continue;
                }

                // шаг оребрения
                if (item.Field == "I_LamAbsFix")
                {
                    item.Content = outView.FinSpace.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // температура воздуха на входе
                if (item.Field == "I_AirTempIn")
                {
                    item.Content = outView.I_AirTempInHW.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                // владность воздуха на входе
                if (item.Field == "I_AirHumIn")
                {
                    item.Content = outView.I_AirHumInHW.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // расход воздуха
                if (item.Field == "I_Airflow")
                {
                    item.Content = outView.I_AirflowHW.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // Температура теплоносителя на входе
                if (item.Field == "I_MedTempIn")
                {
                    item.Content = outView.I_MedTempInHW.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                // Температура теплоносителя на выходе
                if (item.Field == "I_MedTempOut")
                {
                    item.Content = outView.I_MedTempOutHW.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // 0 - Water
                // 1 - Ethylene Glycol
                // 2 - Propylene Glycol
                // 99 - Special Medium
                // теплоноситель
                if (item.Field == "I_MedTyp")
                {
                    if (outView.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidW100)
                    {
                        item.Content = "0";
                    }
                    else if (outView.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidEthyleneGlycol)
                    {
                        item.Content = "1";
                    }
                    else if (outView.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidPropyleneGlycol)
                    {
                        item.Content = "2";
                    }
                    continue;
                }

                // концентрация гликоля в жидкости
                if (item.Field == "I_SoleCnz")
                {
                    if (outView.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidW100)
                    {
                        item.Content = "";
                    }
                    else if (outView.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidEthyleneGlycol)
                    {
                        item.Content = outView.I_SoleCnz;
                    }
                    else if (outView.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidPropyleneGlycol)
                    {
                        item.Content = outView.I_SoleCnz;
                    }
                    continue;
                }
                /// Конструкция/Подключение змеевика
                if (item.Field == "I_Esapo")
                {
                    item.Content = inputDataService.GetCalc_I_Esapo();
                    continue;
                }
            }
            SetPropAdvanceParams();
        }

        /// <summary>
        /// Установка входных данных специфичных для Воздухонагревателя
        /// </summary>
        /// <exception cref="ValidErrorException"></exception>
        private void SetPropToCalcHeatCooler()
        {
            foreach (var item in lines)
            {
                // 0 - Water
                // 1 - Ethylene Glycol
                // 2 - Propylene Glycol
                // 99 - Special Medium
                // теплоноситель
                if (item.Field == "I_MedTyp")
                {
                    if (inputData.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidW100)
                    {
                        item.Content = "0";
                    }
                    else if (inputData.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidEthyleneGlycol)
                    {
                        item.Content = "1";
                    }
                    else if (inputData.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidPropyleneGlycol)
                    {
                        item.Content = "2";
                    }
                    continue;
                }

                // концентрация гликоля в жидкости
                if (item.Field == "I_SoleCnz")
                {
                    if (inputData.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidW100)
                    {
                        item.Content = "";
                    }
                    else if (inputData.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidEthyleneGlycol)
                    {
                        if (inputData.I_SoleCnz == 0)
                        {
                            throw new ValidErrorException(Calculation.TO.Main.Properties.Resources.ValidationError1,
                                inputData.I_SoleCnz.ToString("G", CultureInfo.InvariantCulture),
                                   Calculation.TO.Main.Properties.Resources.Concentration);
                        }
                        item.Content = inputData.I_SoleCnz.ToString("G", CultureInfo.InvariantCulture);
                    }
                    else if (inputData.SelectedFluid == Calculation.TO.Main.Properties.Resources.FluidPropyleneGlycol)
                    {
                        if (inputData.I_SoleCnz == 0)
                        {
                            throw new ValidErrorException(Calculation.TO.Main.Properties.Resources.ValidationError1,
                                inputData.I_SoleCnz.ToString("G", CultureInfo.InvariantCulture),
                                   Calculation.TO.Main.Properties.Resources.Concentration);
                        }
                        item.Content = inputData.I_SoleCnz.ToString("G", CultureInfo.InvariantCulture);
                    }
                    continue;
                }


                // Температура теплоносителя на входе
                if (item.Field == "I_MedTempIn")
                {
                    item.Content = inputData.ProjectsOut.I_MedTempInStrHW = inputData.ValueMedTempInHW.ToString("G", CultureInfo.InvariantCulture);
                    inputData.ProjectsOut.I_MedTempInStrHW += " " + Calculation.TO.Main.Properties.Resources.degreeСelsius;
                    continue;
                }

                // Количество отводов
                if (item.Field == "I_Circuits")
                {
                    item.Content = "";
                    continue;
                }

                // число рядов
                if (item.Field == "I_Rows")
                {
                    item.Content = "";
                    continue;
                }

                #region Параметры по умолчанию
                /// Удельный вес (гликоль)
                if (item.Field == "I_SoleGew")
                {
                    item.Content = inputData.I_SoleGew;
                    continue;
                }
                /// Теплопроводность гликоля
                if (item.Field == "I_SoleLeit")
                {
                    item.Content = inputData.I_SoleLeit;
                    continue;
                }
                /// Температура замерзания
                if (item.Field == "I_SoleTFrz")
                {
                    item.Content = inputData.I_SoleTFrz;
                    continue;
                }
                /// Вязкость Гликоля
                if (item.Field == "I_SoleVisk")
                {
                    item.Content = inputData.I_SoleVisk;
                    continue;
                }
                /// Теплопередача гликоля
                if (item.Field == "I_SoleWaer")
                {
                    item.Content = inputData.I_SoleWaer;
                    continue;
                }
                /// Раздельные теплообменники
                if (item.Field == "I_SplitC")
                {
                    item.Content = inputData.I_SplitC;
                    continue;
                }
                /// Вентиляционное отверстие/канал
                if (item.Field == "I_VentD")
                {
                    item.Content = inputData.I_VentD;
                    continue;
                }
                #endregion
            }
        }

        /// <summary>
        /// Чтение выходных данных после расчёта
        /// </summary>
        protected override void ReadOutput()
        {
            lines.Clear();
            foreach (var line in calculate.GetListInput())
            {
                lines.Add(line);
            }
            linesOut.Clear();
            OutViewLines[] outViewLines = new OutViewLines[40];
            for (int i = 0; i < 40; i++)
            {
                outViewLines[i] = new OutViewLines();
            }
            foreach (var line in calculate.GetListOutput())
            {
                linesOut.Add(line);
                string str = line.ToString();
                string[] lines = str.Split(',');
                switch (line.Field)
                {
                    case "O_Code":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_Code = lines[i];
                        }
                        break;
                    case "O_Rows":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_Rows = lines[i];
                        }
                        break;
                    case "O_Circuits":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_Circuits = lines[i];
                        }
                        break;
                    case "O_LamAbs":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_LamAbs = lines[i];
                        }
                        break;
                    case "O_AirVelo":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_AirVelo = lines[i];
                        }
                        break;
                    case "O_AirPaT":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_AirPaT = lines[i];
                        }
                        break;
                    case "O_LRes":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_LRes = lines[i];
                        }
                        break;
                    case "O_MedVelo":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_MedVelo = lines[i];
                        }
                        break;
                    case "O_MedKPa":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_MedKPa = lines[i];
                        }
                        break;
                    case "O_AirTempOut":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_AirTempOut = lines[i];
                        }
                        break;
                    case "O_TotCap":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_TotCap = lines[i];
                        }
                        break;
                    case "O_AirHumInAbs":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_AirHumInAbs = lines[i];
                        }
                        break;
                    case "O_AirHumOut":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_AirHumOut = lines[i];
                        }
                        break;
                    case "O_Airkgs":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_Airkgs = lines[i];
                        }
                        break;
                    case "O_MedTempOut":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_MedTempOut = lines[i];
                        }
                        break;
                    case "O_MedFlow":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_MedFlow = lines[i];
                        }
                        break;
                    case "O_Volume":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_Volume = lines[i];
                        }
                        break;
                    case "O_StmBar":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_StmBar = lines[i];
                        }
                        break;
                    case "O_StmFlow":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_StmFlow = lines[i];
                        }
                        break;
                    case "O_StmHeat":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_StmHeat = lines[i];
                        }
                        break;
                    case "O_StmVelo":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_StmVelo = lines[i];
                        }
                        break;
                    case "O_WidthInt":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_WidthInt = lines[i];
                        }
                        break;
                    case "O_HeightInt":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_HeightInt = lines[i];
                        }
                        break;
                    case "O_ConIN":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_ConIN = lines[i];
                        }
                        break;
                    case "O_ConOut":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_ConOut = lines[i];
                        }
                        break;
                    case "O_DepthExt":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_DepthExt = lines[i];
                        }
                        break;
                    case "O_DepthInt":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_DepthInt = lines[i];
                        }
                        break;
                    case "O_FrameB":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_FrameB = lines[i];
                        }
                        break;
                    case "O_FrameC":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_FrameC = lines[i];
                        }
                        break;
                    case "O_FrameL":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_FrameL = lines[i];
                        }
                        break;
                    case "O_FrameR":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_FrameR = lines[i];
                        }
                        break;
                    case "O_FrameT":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_FrameT = lines[i];
                        }
                        break;
                    case "O_FrameThk":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_FrameThk = lines[i];
                        }
                        break;
                    case "O_HeightExt":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_HeightExt = lines[i];
                        }
                        break;
                    case "O_WidthExt":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_WidthExt = lines[i];
                        }
                        break;
                    case "O_RowsH":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_RowsH = lines[i];
                        }
                        break;
                    case "O_HdrQta":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_HdrQta = lines[i];
                        }
                        break;
                    case "O_AirHumOutAbs":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_AirHumOutAbs = lines[i];
                        }
                        break;
                    case "O_KValue":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_KValue = lines[i];
                        }
                        break;
                    case "O_LogTDif":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_LogTDif = lines[i];
                        }
                        break;
                    case "O_SoleGew":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_SoleGew = lines[i];
                        }
                        break;
                    case "O_SoleLeit":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_SoleLeit = lines[i];
                        }
                        break;
                    case "O_SoleTFrz":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_SoleTFrz = lines[i];
                        }
                        break;
                    case "O_SoleVisk":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_SoleVisk = lines[i];
                        }
                        break;
                    case "O_SoleWaer":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_SoleWaer = lines[i];
                        }
                        break;
                }
            }
            List<OutViewLines> outViews2 = new List<OutViewLines>(outViewLines);
            bool[] isM = new bool[40];
            for (int i = 0; i < 40; i++)
            {
                if (!string.IsNullOrEmpty(outViewLines[i].O_Code))
                    isM[i] = true;
            }
            outViews = new List<OutViewLines>();
            for (int i = 0; i < 40; i++)
            {
                if (isM[i])
                {
                    outViews.Add(outViewLines[i]);
                }
            }
            foreach (var outV in outViews)
            {
                //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strOutputData + outV);
            }
        }

        /// <summary>
        /// Филтрация вывода по типоразмеру в таблицу вывода результатов расчёта
        /// вызывается после ReadOutput()
        /// </summary>
        /// <returns>true - найден расчёт с такой геометрией, false - не найден расчёт с такой геометрией</returns>
        protected override bool SelectOut(out bool returnRes)
        {
            bool calc = false;
            foreach (var line in outViews)
            {
                string code = line.O_Code.Substring(4, 2);
                string code2 = line.O_Code.Substring(10, 2);
                if (code2 == "7x")
                {
                    code += "07";
                }
                else
                {
                    code += code2;
                }
                if (inputData.SelectGeometry.Contains(code))
                {
                    if (finThk.IsFinThickness(line))
                    {
                        AddProjectsOutView(line);
                        calc = true;
                    }
                }
            }
            inputData.ProjectsOutView = new ObservableCollection<OutView>();
            returnRes = true;
            if (calc)
            {
                inputData.ProjectsOutView.Add(inputData.ProjectsOut);
                returnRes = false;
                return true;
            }
            else
            {
                if (outViews.Count > 0)
                {
                    foreach (var line in outViews)
                    {
                        AddProjectsOutView(line);
                        inputData.ProjectsOutView.Add(inputData.ProjectsOut);
                        returnRes = false;
                    }
                }
            }
            return false;
        }

        private void AddProjectsOutView(OutViewLines line)
        {
            //inputData.ProjectsOut = new OutView();
            inputData.ProjectsOut.Calc = true;
            inputData.ProjectsOut.Id = inputData.Id++;
            inputData.ProjectsOut.NoOfRows = line.O_Rows;
            inputData.ProjectsOut.Circuits = line.O_Circuits;
            inputData.ProjectsOut.FinSpace = double.Parse(line.O_LamAbs, CultureInfo.InvariantCulture);
            inputData.ProjectsOut.AirVelocity = line.O_AirVelo;
            inputData.ProjectsOut.PresDropDry = line.O_AirPaT;
            inputData.ProjectsOut.ReverseLoad = line.O_LRes;
            inputData.ProjectsOut.MedVelo = line.O_MedVelo;
            inputData.ProjectsOut.MedKPa = line.O_MedKPa;
            inputData.ProjectsOut.AirTempOut = line.O_AirTempOut;
            inputData.ProjectsOut.I_Geo = GetGeometry(line.O_Code);
            inputData.ProjectsOut.O_TotCapPrint = line.O_TotCap + " " + Calculation.TO.Main.Properties.Resources.kW;
            inputData.ProjectsOut.O_TotCap = line.O_TotCap;
            inputData.ProjectsOut.O_AirTempOutStr = line.O_AirTempOut + " " + Calculation.TO.Main.Properties.Resources.degreeСelsius;
            inputData.ProjectsOut.O_AirHumOutAbs = line.O_AirHumInAbs;
            inputData.ProjectsOut.O_AirVelo = line.O_AirVelo;
            inputData.ProjectsOut.O_AirPaT = line.O_AirPaT;
            inputData.ProjectsOut.O_Code = line.O_Code;
            inputData.ProjectsOut.O_MedTempOutStr = Calculation.TO.Main.Properties.Resources.PreviewOut + " " + line.O_MedTempOut + " " + Calculation.TO.Main.Properties.Resources.degreeСelsius;
            inputData.ProjectsOut.O_MedFlow = line.O_MedFlow;
            inputData.ProjectsOut.O_Volume = line.O_Volume;
            inputData.ProjectsOut.O_MedVelo = line.O_MedVelo;
            inputData.ProjectsOut.O_MedKPa = line.O_MedKPa;
            inputData.ProjectsOut.I_MedTyp = inputData.SelectedFluid;
            inputData.ProjectsOut.CalcType = CalcType;
            inputData.ProjectsOut.O_AirHumOut = line.O_AirHumOut;
            inputData.ProjectsOut.O_Airkgs = line.O_Airkgs;
            inputData.ProjectsOut.I_StmTemp = inputData.I_StmTemp.ToString("G", CultureInfo.InvariantCulture);
            inputData.ProjectsOut.I_SoleCnz = inputData.I_SoleCnz.ToString("G", CultureInfo.InvariantCulture);
            inputData.ProjectsOut.I_WidthInt = inputData.ValueWidthFin;
            inputData.ProjectsOut.I_HeightInt = inputData.ValueHightFin;

            double d = GS.StringToDouble(line.O_StmBar);
            inputData.ProjectsOut.O_StmBar = d;
            inputData.ProjectsOut.O_StmFlow = line.O_StmFlow;
            inputData.ProjectsOut.O_StmHeat = line.O_StmHeat;
            inputData.ProjectsOut.O_StmVelo = line.O_StmVelo;
            inputData.ProjectsOut.I_StmBar = line.O_StmBar;
            inputData.ProjectsOut.O_KValue = line.O_KValue;
            inputData.ProjectsOut.O_LogTDif = line.O_LogTDif;


            // корпус и коллектор
            inputData.ProjectsOut.O_ConIN = inputDataService.GetOut_I_ConIn(line.O_ConIN);
            inputData.ProjectsOut.O_ConOut = inputDataService.GetOut_I_ConIn(line.O_ConOut);
            inputData.ProjectsOut.O_DepthExt = line.O_DepthExt;
            inputData.ProjectsOut.O_DepthInt = line.O_DepthInt;
            inputData.ProjectsOut.O_FrameB = line.O_FrameB;
            inputData.ProjectsOut.O_FrameC = line.O_FrameC;
            inputData.ProjectsOut.O_FrameL = line.O_FrameL;
            inputData.ProjectsOut.O_FrameR = line.O_FrameR;
            inputData.ProjectsOut.O_FrameT = line.O_FrameT;
            inputData.ProjectsOut.O_FrameThk = line.O_FrameThk;
            inputData.ProjectsOut.O_HeightExt = line.O_HeightExt;
            inputData.ProjectsOut.O_WidthExt = line.O_WidthExt;
            inputData.ProjectsOut.O_RowsH = line.O_RowsH;
            inputData.ProjectsOut.O_HdrQta = line.O_HdrQta;
            inputData.ProjectsOut.O_MatHdr = inputData.Selected_I_MatHdr;
            inputData.ProjectsOut.I_CSheetL = inputDataService.GetString_I_CSheetL();
            inputData.ProjectsOut.I_ConType = inputData.Selected_I_ConType;
            inputData.ProjectsOut.CasingType = inputData.SelectedCasingType;
            inputData.ProjectsOut.I_CasMat = inputData.Selected_I_CasMat;
            inputData.ProjectsOut.I_Esapo = inputData.SelectedEsapo;
            inputData.ProjectsOut.I_CDir = inputData.Select_I_CDir;
            inputData.ProjectsOut.AirFlowDirection = inputData.SelectedAirFlowDirection;
            inputData.ProjectsOut.ConnectingTheCoolant = inputData.SelectedConnectingCoolant;
            inputData.ProjectsOut.O_WidthInt = line.O_WidthInt;
            inputData.ProjectsOut.O_HeightInt = line.O_HeightInt;

            inputData.ProjectsOut.Name = GetProductionCode(line.O_Code, line); // line.O_Code,
            inputData.ProjectsOut.ShortName = GetProductionShortCode(line.O_Code, line);
        }

        /// <summary>
        /// Филтрация вывода по типоразмеру в таблицу вывода результатов табличного расчёта
        /// </summary>
        protected override void SelectOutTable()
        {
            int IdToCalc = tableDataService.GetIdToCalc();
            foreach (var line in outViews)
            {
                string code = line.O_Code.Substring(4, 2);
                string code2 = line.O_Code.Substring(10, 2);
                if (code2 == "7x")
                {
                    code += "07";
                }
                else
                {
                    code += code2;
                }
                if (finThk.IsFinThickness(line))
                {
                    if (TableData.ProjectsOutTable[IdToCalc].I_Geo.ToString() == code)
                    {
                        TableData.ProjectsOutTable[IdToCalc].Calc = true;
                        TableData.ProjectsOutTable[IdToCalc].Id = inputData.Id++;
                        TableData.ProjectsOutTable[IdToCalc].NoOfRows = line.O_Rows;
                        TableData.ProjectsOutTable[IdToCalc].Circuits = line.O_Circuits;
                        TableData.ProjectsOutTable[IdToCalc].FinSpace = double.Parse(line.O_LamAbs, CultureInfo.InvariantCulture);
                        TableData.ProjectsOutTable[IdToCalc].AirVelocity = line.O_AirVelo;
                        TableData.ProjectsOutTable[IdToCalc].PresDropDry = line.O_AirPaT;
                        TableData.ProjectsOutTable[IdToCalc].ReverseLoad = line.O_LRes;
                        TableData.ProjectsOutTable[IdToCalc].MedVelo = line.O_MedVelo;
                        TableData.ProjectsOutTable[IdToCalc].MedKPa = line.O_MedKPa;
                        TableData.ProjectsOutTable[IdToCalc].AirTempOut = line.O_AirTempOut;
                        TableData.ProjectsOutTable[IdToCalc].I_Geo = GetGeometry(line.O_Code);
                        TableData.ProjectsOutTable[IdToCalc].O_TotCapPrint = line.O_TotCap + " " + Calculation.TO.Main.Properties.Resources.kW;
                        TableData.ProjectsOutTable[IdToCalc].O_TotCap = line.O_TotCap;
                        TableData.ProjectsOutTable[IdToCalc].O_AirTempOutStr = line.O_AirTempOut + " " + Calculation.TO.Main.Properties.Resources.degreeСelsius;
                        TableData.ProjectsOutTable[IdToCalc].O_AirHumOutAbs = line.O_AirHumInAbs;
                        TableData.ProjectsOutTable[IdToCalc].O_AirVelo = line.O_AirVelo;
                        TableData.ProjectsOutTable[IdToCalc].O_AirPaT = line.O_AirPaT;
                        TableData.ProjectsOutTable[IdToCalc].O_Code = line.O_Code;
                        TableData.ProjectsOutTable[IdToCalc].O_MedTempOutStr = Calculation.TO.Main.Properties.Resources.PreviewOut + " " + line.O_MedTempOut + " " + Calculation.TO.Main.Properties.Resources.degreeСelsius;
                        TableData.ProjectsOutTable[IdToCalc].O_MedFlow = line.O_MedFlow;
                        TableData.ProjectsOutTable[IdToCalc].O_Volume = line.O_Volume;
                        TableData.ProjectsOutTable[IdToCalc].O_MedVelo = line.O_MedVelo;
                        TableData.ProjectsOutTable[IdToCalc].O_MedKPa = line.O_MedKPa;
                        //TableData.ProjectsOutTable[IdToCalc].I_MedTyp = inputData.SelectedFluid;
                        TableData.ProjectsOutTable[IdToCalc].CalcType = CalcType;
                        TableData.ProjectsOutTable[IdToCalc].O_AirHumOut = line.O_AirHumOut;
                        TableData.ProjectsOutTable[IdToCalc].O_Airkgs = line.O_Airkgs;
                        //TableData.ProjectsOutTable[IdToCalc].I_StmTemp =inputData.I_StmTemp.ToString("G", CultureInfo.InvariantCulture);
                        //TableData.ProjectsOutTable[IdToCalc].I_SoleCnz = inputData.I_SoleCnz.ToString("G", CultureInfo.InvariantCulture);
                        TableData.ProjectsOutTable[IdToCalc].I_WidthInt = double.Parse(line.O_WidthInt, CultureInfo.InvariantCulture); //inputData.ValueWidthFin;
                        TableData.ProjectsOutTable[IdToCalc].I_HeightInt = double.Parse(line.O_HeightInt, CultureInfo.InvariantCulture); //inputData.ValueHightFin;

                        double d = GS.StringToDouble(line.O_StmBar);
                        TableData.ProjectsOutTable[IdToCalc].O_StmBar = d;
                        TableData.ProjectsOutTable[IdToCalc].O_StmFlow = line.O_StmFlow;
                        TableData.ProjectsOutTable[IdToCalc].O_StmHeat = line.O_StmHeat;
                        TableData.ProjectsOutTable[IdToCalc].O_StmVelo = line.O_StmVelo;
                        TableData.ProjectsOutTable[IdToCalc].I_StmBar = line.O_StmBar;
                        TableData.ProjectsOutTable[IdToCalc].O_SoleGew = line.O_SoleGew;
                        TableData.ProjectsOutTable[IdToCalc].O_SoleLeit = line.O_SoleLeit;
                        TableData.ProjectsOutTable[IdToCalc].O_SoleTFrz = line.O_SoleTFrz;
                        TableData.ProjectsOutTable[IdToCalc].O_SoleVisk = line.O_SoleVisk;
                        TableData.ProjectsOutTable[IdToCalc].O_SoleWaer = line.O_SoleWaer;

                        // корпус и коллектор
                        TableData.ProjectsOutTable[IdToCalc].O_ConIN = inputDataService.GetOut_I_ConIn(line.O_ConIN);
                        TableData.ProjectsOutTable[IdToCalc].O_ConOut = inputDataService.GetOut_I_ConIn(line.O_ConOut);
                        TableData.ProjectsOutTable[IdToCalc].O_DepthExt = line.O_DepthExt;
                        TableData.ProjectsOutTable[IdToCalc].O_DepthInt = line.O_DepthInt;
                        TableData.ProjectsOutTable[IdToCalc].O_FrameB = line.O_FrameB;
                        TableData.ProjectsOutTable[IdToCalc].O_FrameC = line.O_FrameC;
                        TableData.ProjectsOutTable[IdToCalc].O_FrameL = line.O_FrameL;
                        TableData.ProjectsOutTable[IdToCalc].O_FrameR = line.O_FrameR;
                        TableData.ProjectsOutTable[IdToCalc].O_FrameT = line.O_FrameT;
                        TableData.ProjectsOutTable[IdToCalc].O_FrameThk = line.O_FrameThk;
                        TableData.ProjectsOutTable[IdToCalc].O_HeightExt = line.O_HeightExt;
                        TableData.ProjectsOutTable[IdToCalc].O_WidthExt = line.O_WidthExt;
                        TableData.ProjectsOutTable[IdToCalc].O_RowsH = line.O_RowsH;
                        TableData.ProjectsOutTable[IdToCalc].O_HdrQta = line.O_HdrQta;
                        TableData.ProjectsOutTable[IdToCalc].O_MatHdr = inputData.Selected_I_MatHdr;
                        TableData.ProjectsOutTable[IdToCalc].I_CSheetL = inputDataService.GetString_I_CSheetL();
                        TableData.ProjectsOutTable[IdToCalc].I_ConType = inputData.Selected_I_ConType;
                        TableData.ProjectsOutTable[IdToCalc].CasingType = inputData.SelectedCasingType;
                        TableData.ProjectsOutTable[IdToCalc].I_CasMat = inputData.Selected_I_CasMat;
                        TableData.ProjectsOutTable[IdToCalc].I_Esapo = inputData.SelectedEsapo;
                        TableData.ProjectsOutTable[IdToCalc].I_CDir = inputData.Select_I_CDir;
                        TableData.ProjectsOutTable[IdToCalc].AirFlowDirection = inputData.SelectedAirFlowDirection;
                        TableData.ProjectsOutTable[IdToCalc].ConnectingTheCoolant = inputData.SelectedConnectingCoolant;
                        TableData.ProjectsOutTable[IdToCalc].O_WidthInt = line.O_WidthInt;
                        TableData.ProjectsOutTable[IdToCalc].O_HeightInt = line.O_HeightInt;

                        TableData.ProjectsOutTable[IdToCalc].Name = GetProductionCode(line.O_Code, line); // line.O_Code,
                        TableData.ProjectsOutTable[IdToCalc].ShortName = GetProductionShortCode(line.O_Code, line);
                    }
                }
            }
        }

        protected override void SetSelectHeatExchanger(SaveCodes saveCodes)
        {
            // Мощность или температура воздуха на выходе
            inputData.ValueCapacityHW = saveCodes.CapacityHW;
            // Расход воздуха
            inputData.ValueAirFlowHW = saveCodes.ValueAirFlowHW;
            // Температура воздуха на входе
            inputData.I_AirTempInHW = saveCodes.I_AirTempInHW;

            // Влажность воздуха на входе
            inputData.ValueBaseHumHW = saveCodes.ValueBaseHumHW;
            // Температура теплоносителя на входе
            inputData.ValueMedTempInHW = saveCodes.ValueMedTempInHW;

            // Максимальное падение давления
            inputData.ValueMedKPaHW = saveCodes.ValueMedKPaHW;
            // Вариант расчёта 
            inputData.ValueVariantRaschHW = saveCodes.ValueVariantRaschHW;

        }

        protected override void SetSelectHeatExchangerPropToCalc(DBLine item)
        {
            // расход воздуха
            if (item.Field == "I_Airflow")
            {
                inputData.ProjectsOut.I_AirflowHW = inputData.ValueAirFlowHW;
                item.Content = inputDataService.GetString_ValueAirFlowHW();
            }
            // владность воздуха на входе
            if (item.Field == "I_AirHumIn")
            {
                inputData.ProjectsOut.I_AirHumInHW = inputData.ValueBaseHumHW;
                item.Content = inputData.ValueBaseHumHW.ToString("G", CultureInfo.InvariantCulture);
            }
            // температура воздуха на входе
            if (item.Field == "I_AirTempIn")
            {
                item.Content = inputData.ProjectsOut.I_AirTempInStrHW = inputDataService.GetString_I_AirTempInHW();
                inputData.ProjectsOut.I_AirTempInStrHW += " " + Calculation.TO.Main.Properties.Resources.degreeСelsius;
            }
        }

        /// <summary>
        /// Получить выходные параметры
        /// </summary>
        /// <returns></returns>
        private OutputDataFluidHeaterCoolerDTO GetOutputParams()
        {
            OutputDataFluidHeaterCoolerDTO outputParams = new OutputDataFluidHeaterCoolerDTO();
            if (inputData.ProjectsOutView.Count == 0) return outputParams;
            outputParams.I_Geo = inputData.ProjectsOutView[0].I_Geo.ToString();
            outputParams.ShortName = inputData.ProjectsOutView[0].ShortName;
            outputParams.NoOfRows = inputData.ProjectsOutView[0].NoOfRows;
            outputParams.Circuits = inputData.ProjectsOutView[0].Circuits;
            outputParams.AirVelocity = inputData.ProjectsOutView[0].AirVelocity;
            outputParams.PresDropDry = inputData.ProjectsOutView[0].PresDropDry;
            outputParams.ReverseLoad = inputData.ProjectsOutView[0].ReverseLoad;
            outputParams.MedVelo = inputData.ProjectsOutView[0].MedVelo;
            outputParams.MedKPa = inputData.ProjectsOutView[0].MedKPa;
            outputParams.AirTempOut = inputData.ProjectsOutView[0].AirTempOut;
            outputParams.O_TotCap = inputData.ProjectsOutView[0].O_TotCap;
            return outputParams;
        }
        #endregion
    }
}
