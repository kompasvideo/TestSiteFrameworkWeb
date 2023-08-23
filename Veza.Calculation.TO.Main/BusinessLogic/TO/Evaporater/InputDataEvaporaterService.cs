using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Veza.Calculation.TO.Main.Interfaces;
using Veza.HeatExchanger.BusinessLogic.TO.Evaporater;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Interfaces.Mappers;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.Main;
using Veza.HeatExchanger.ViewModels.Controls;

namespace Veza.HeatExchanger.Services.Cooler
{
    sealed public class InputDataEvaporaterService : CalculateBaseService, IInputDataEvaporaterService, IExtInputDataEvaporaterService
    {
        #region Внутренние поля и переменные

        /// <summary>
        /// Индекс к TableData.ProjectsOutTable
        /// </summary>
        private int index { get; set; }
        private readonly IEvaporaterMapper evaporaterMapper;
        /// <summary>
        /// были ли установлены параметры
        /// </summary>
        private bool IsSetParams { get; set; }
        /// <summary>
        /// был ли расчёт
        /// </summary>
        private bool IsCalc { get; set; }
        private InputDataEvaporaterDTO inputParams;
        #endregion

        #region Конструктор
        public InputDataEvaporaterService(AdvanceParamsViewModel advanceParamsViewModel, IInputData inputData, ICalculate calculate,
            ILogs logs, ITableDataService tableDataService, IInputDataService inputDataService,
            IFinThk finThk, IEvaporaterMapper evaporaterMapper) :
            base(advanceParamsViewModel, calculate, logs, inputData, tableDataService, inputDataService, finThk)
        {
            //SetPropertiesFirst();
            this.evaporaterMapper = evaporaterMapper;
        }
        #endregion

        #region Методы открытые

        /// <summary>
        /// Установка входных данных специфичных для испарителя
        /// </summary>
        public void SetPropertiesDX()
        {
            if (!inputData.InitDX)
            {
                inputData.ValueCapacityDX = 200;
                inputData.ValueBaseHumDX = 50;
                inputData.I_AirTempInDX = 25;
                foreach (var item in TableData.ProjectsOutTable)
                {
                    item.I_AirHumInDX = 50;
                    item.I_AirTempInDX = 25;
                    item.I_CapacityDX = "200";
                }
                inputData.ValueAirFlowDX = inputData.ValueAirFlowHW;
                inputData.InitDX = true;
            }
            inputDataService.SetEsapoEvaporater();
            SelectMatHdr();
        }

        /// <summary>
        /// Установка значений в ProjectsOutView для печати 
        /// </summary>
        public void SetPropToPrint()
        {
            inputData.ProjectsOut.I_TCondDX = inputData.I_TCondDX.ToString("G", CultureInfo.InvariantCulture);
            inputData.ProjectsOut.I_TEvapDX = inputData.I_TEvapDX.ToString("G", CultureInfo.InvariantCulture);
            inputData.ProjectsOut.I_TSubCDX = inputData.I_TSubCDX.ToString("G", CultureInfo.InvariantCulture);
            inputData.ProjectsOut.I_TOvrHDX = inputData.I_TOvrHDX.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// считывание сохранённого расчёта в входные и выходные параметры
        /// </summary>
        /// <param name="saveCodes"></param>
        /// <returns></returns>
        public override bool Decoder(SaveCodes saveCodes)
        {
            try
            {
                if (DecoderBase(saveCodes) == false)
                {
                    return false;
                }

                #region Испаритель
                // Тип хладогента
                inputData.Selected_I_RefT = saveCodes.Selected_I_RefT;
                // Коэффициент загрязнения трубы
                inputData.Selected_I_FoulingI = saveCodes.Selected_I_FoulingI;
                // Максимальное падение давление хладогента
                inputData.I_TMaxPDX = saveCodes.I_TMaxPDX;
                // Температура горячего газа
                inputData.I_THotGasDX = saveCodes.I_THotGasDX;
                // Температура всасываемого газа
                inputData.I_TSucGasDX = saveCodes.I_TSucGas;
                // Температура конденсации
                inputData.I_TCondDX = saveCodes.I_TCond;
                // Температура кипения 
                inputData.I_TEvapDX = saveCodes.I_TEvapDX;
                // Переохлаждение
                inputData.I_TSubCDX = saveCodes.I_TSubC;
                // Перегрев
                inputData.I_TOvrHDX = saveCodes.I_TOvrHDX;
                // Количество контуров хладагента
                inputData.I_RCircDX = saveCodes.I_RCircDX;
                // Пропускная способность байпаса
                inputData.I_DxBypDX = saveCodes.I_DxBypDX;
                #endregion
                ProjectsOut_Clean();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Получить входные параметры
        /// </summary>
        /// <returns></returns>
        public GetDataEvaporaterDTO GetProperties()
        {
            SetPropertiesFirst();
            SetPropertiesDX();
            return evaporaterMapper.InputDataClassToInputParams();
        }

        /// <summary>
        /// Установить входные параметры
        /// </summary>
        /// <param name="inputParams"></param>
        public void SetProperties(InputDataEvaporaterDTO inputParams)
        {
            SetPropertiesFirst();
            evaporaterMapper.InputToInputDataClass(inputParams);
            this.inputParams = inputParams;
            IsSetParams = true;
        }
        /// <summary>
        /// Расчёт Испапрителя
        /// </summary>
        /// <returns></returns>        
        public bool Calc()
        {
            if (!IsSetParams) return false;
            SetCalcMode("DX");
            CalculationInit();
            switch (inputParams.SelectedMode)
            {
                case "direct":
                    CalcDirect(Calculation.TO.Main.Properties.Resources.PageCondensator_Direct);
                    break;
                case "reverse":
                    CalcReverse(Calculation.TO.Main.Properties.Resources.PageCondensator_Reverse);
                    break;
            }
            return IsCalc = true;
        }
        /// <summary>
        /// Получение результатов расчёта
        /// </summary>
        /// <returns></returns>
        public OutputDataEvaporaterDTO GetResults()
        {
            OutputDataEvaporaterDTO outputParams = new OutputDataEvaporaterDTO();
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
        #endregion

        #region Методы внутренние
        /// <summary>
        /// Изменение материала корпуса на медь,
        /// во внутренних таблицах расчёта нет данных для материала корпуса - сталь 
        /// </summary>
        private void SelectMatHdr()
        {
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Steel)
            {
                inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_Cu;
            }
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_316)
            {
                inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_Cu;
            }
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SP)
            {
                inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_Cu;
            }
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SPC)
            {
                inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_Cu;
            }
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Steel)
            {
                inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_Cu;
            }
        }

        /// <summary>
        /// Очистка парметров при неудачном считывании сохранённого расчёта
        /// </summary>
        protected override void ProjectsOut_Clean()
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
                inputData.ProjectsOut.AirTempOut = "";
            }
        }

        /// <summary>
        /// Установка свойств
        /// </summary>
        private void SetPropertiesFirst()
        {
            SetPropertiesChangeLang();
            // Температура конденсации
            inputData.I_TCondDX = 45;
            // Температура кипениия
            inputData.I_TEvapDX = 5;
            /// Переохлаждение
            inputData.I_TSubCDX = 7;
            // Перегрев
            inputData.I_TOvrHDX = 4;
            // Максимальное падение давления хладагента
            inputData.I_TMaxPDX = 3;
            // Температура горячего газа
            inputData.I_THotGasDX = 0;
            // Температура всасываемого газа
            inputData.I_TSucGasDX = 20;
            // Изэнтропийная эффективность
            inputData.I_IseEta = "0.8";
        }

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
                    // падение давления хладогента, bar
                    item.O_DxCxBar = "";
                    // падение давления хладогента, K
                    item.O_DxCxK = "";
                    // Температура горячего газа
                    item.O_THotGas = "";
                    // Количество контуров хладогента
                    item.O_RCirc = "";
                    // Объём хладогента
                    item.O_DxCxVol = "";
                    // Общая производительность
                    item.O_TotCap = "";
                    // Массовый расход хладагента
                    item.O_DxCxMas = "";
                }
            }
        }

        /// <summary>
        /// переопределение входных данных для табличного расчёта - конденсатор
        /// </summary>
        protected override void SetPropToCalc_Table(OutView outView)
        {
            SetPropToCalc();
            SetPropToCalcEvaporater();
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
                    item.Content = fins[0];
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
                    item.Content = outView.I_AirTempInDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                // владность воздуха на входе
                if (item.Field == "I_AirHumIn")
                {
                    item.Content = outView.I_AirHumInDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // расход воздуха
                if (item.Field == "I_Airflow")
                {
                    item.Content = outView.I_AirflowDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // Температура конденсации
                if (item.Field == "I_TCond")
                {
                    item.Content = outView.I_TCondDX;
                    continue;
                }

                // Температура кипениия
                if (item.Field == "I_TEvap")
                {
                    item.Content = outView.I_TEvapDX;
                    continue;
                }

                // Переохлаждение
                if (item.Field == "I_TSubC")
                {
                    item.Content = outView.I_TSubCDX;
                    continue;
                }

                // Перегрев всас. газа
                if (item.Field == "I_TOvrH")
                {
                    item.Content = outView.I_TOvrHDX;
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
        /// Установка свойств специфичных для прямого расчёта - конденсатор
        /// </summary>
        private void SetPropToCalcEvaporater_Direct()
        {
            foreach (var item in lines)
            {
                // требуемая мощность
                if (item.Field == "I_Capacity")
                {
                    if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.Capacity)
                    {
                        item.Content = inputData.ValueCapacityDX.ToString("G", CultureInfo.InvariantCulture);
                        continue;
                    }
                    // массовый расход
                    else if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.I_MedFlowCalc)
                    {
                        item.Content = inputData.CapacityMassFlow.ToString("G", CultureInfo.InvariantCulture);
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
                        item.Content = inputData.DirectAirTempOutDX.ToString("G", CultureInfo.InvariantCulture);
                        continue;
                    }
                    else
                    {
                        item.Content = "";
                        continue;
                    }
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
            }
            SetPropAdvanceParams();
        }

        /// <summary>
        /// Установка свойств специфичных для обратного расчёта - конденсатор
        /// </summary>
        private void SetPropToCalcEvaporater_Reverse()
        {
            foreach (var item in lines)
            {
                // требуемая мощность
                if (item.Field == "I_Capacity")
                {
                    item.Content = "";
                    continue;
                }

                // Температура воздуха на выходе
                if (item.Field == "I_AirTempOut")
                {
                    item.Content = "";
                    continue;
                }
                // Количество отводов
                if (item.Field == "I_Circuits")
                {
                    item.Content = inputData.ValueCircuits.ToString();
                    continue;
                }

                // число рядов
                if (item.Field == "I_Rows")
                {
                    item.Content = inputData.ValuePipe.ToString();
                    continue;
                }
            }
            SetPropAdvanceParams();
        }

        /// <summary>
        /// установка входных данных специфичных для прямого расчёта - конденсатор
        /// </summary>
        protected override void SetPropToCalc_DirectBase()
        {
            SetPropToCalc();
            SetPropToCalcEvaporater();
            SetPropToCalcEvaporater_Direct();
        }

        /// <summary>
        /// установка входных данных специфичных для расчёта конденсатора
        /// </summary>
        private void SetPropToCalcEvaporater()
        {
            foreach (var item in lines)
            {
                #region Параметры по умолчанию 
                // Тип присоединения/подключения
                if (item.Field == "I_ConType")
                {
                    item.Content = "8"; //inputData.I_ConType;
                    continue;
                }
                // Тип хладагента
                if (item.Field == "I_RefT")
                {
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R407C)
                    {
                        item.Content = "1";
                        continue;
                    }
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R134a)
                    {
                        item.Content = "2";
                        continue;
                    }
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R404A)
                    {
                        item.Content = "3";
                        continue;
                    }
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R22)
                    {
                        item.Content = "4";
                        continue;
                    }
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R507)
                    {
                        item.Content = "5";
                        continue;
                    }
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R410A)
                    {
                        item.Content = "6";
                        continue;
                    }
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R32)
                    {
                        item.Content = "7";
                        continue;
                    }
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R125)
                    {
                        item.Content = "8";
                        continue;
                    }
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R143a)
                    {
                        item.Content = "9";
                        continue;
                    }
                    if (inputData.Selected_I_RefT == Calculation.TO.Main.Properties.Resources.R513A)
                    {
                        item.Content = "10";
                        continue;
                    }
                }

                // Коэффициент загрязнения трубы
                if (item.Field == "I_FoulingI")
                {
                    if (inputData.Selected_I_FoulingI == Calculation.TO.Main.Properties.Resources.WithoutOil)
                    {
                        item.Content = "0";
                        continue;
                    }
                    if (inputData.Selected_I_FoulingI == Calculation.TO.Main.Properties.Resources._12MineralOil)
                    {
                        item.Content = "1";
                        continue;
                    }
                    if (inputData.Selected_I_FoulingI == Calculation.TO.Main.Properties.Resources._23MineralOil)
                    {
                        item.Content = "2";
                        continue;
                    }
                    if (inputData.Selected_I_FoulingI == Calculation.TO.Main.Properties.Resources._12SyntheticOil)
                    {
                        item.Content = "3";
                        continue;
                    }
                    if (inputData.Selected_I_FoulingI == Calculation.TO.Main.Properties.Resources._23SyntheticOil)
                    {
                        item.Content = "4";
                        continue;
                    }
                }

                // Температура конденсации
                if (item.Field == "I_TCond")
                {
                    item.Content = inputData.I_TCondDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // Температура кипениия
                if (item.Field == "I_TEvap")
                {
                    item.Content = inputData.I_TEvapDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // Переохлаждение
                if (item.Field == "I_TSubC")
                {
                    item.Content = inputData.I_TSubCDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // Перегрев
                if (item.Field == "I_TOvrH")
                {
                    item.Content = inputData.I_TOvrHDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // Максимальное падение давления хладагента
                if (item.Field == "I_TMaxP")
                {
                    item.Content = inputData.I_TMaxPDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // Температура горячего газа
                if (item.Field == "I_THotGas")
                {
                    item.Content = inputData.I_THotGasDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // Температура всасываемого газа
                if (item.Field == "I_TSucGas")
                {
                    item.Content = inputData.I_TSucGasDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }

                // Тип расчета
                if (item.Field == "I_Type")
                {
                    item.Content = inputData.I_Type = "5";
                    continue;
                }

                // Количество контуров хладагента
                if (item.Field == "I_RCirc")
                {
                    item.Content = inputData.I_RCircDX.ToString();
                    continue;
                }

                // Пропускная способность байпаса
                if (item.Field == "I_DxByp")
                {
                    item.Content = inputData.I_DxBypDX.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
                #endregion
            }
        }

        /// <summary>
        /// установка входных данных специфичных для обратного расчёта - конденсатор
        /// </summary>
        protected override void SetPropToCalc_ReverseBase()
        {
            SetPropToCalc();
            SetPropToCalcEvaporater();
            SetPropToCalcEvaporater_Reverse();
        }

        /// <summary>
        /// Связывание входных данных в классе Calculate с этим классом
        /// </summary>
        protected override void ReadOutput()
        {
            lines.Clear();
            foreach (var line in calculate.GetListInput())
            {
                lines.Add(line);
            }
            linesOut.Clear();
            //outViews = new List<OutViewLines>();
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
                    case "O_Volume":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_Volume = lines[i];
                        }
                        break;
                    case "O_DxCxBar":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_DxCxBar = lines[i];
                        }
                        break;
                    case "O_DxCxK":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_DxCxK = lines[i];
                        }
                        break;
                    case "O_DxCxMas":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_DxCxMas = lines[i];
                        }
                        break;
                    case "O_DxCxPres":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_DxCxPres = lines[i];
                        }
                        break;
                    case "O_DxCxVol":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_DxCxVol = lines[i];
                        }
                        break;
                    case "O_THotGas":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_THotGas = lines[i];
                        }
                        break;
                    case "O_TOvrH":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_TOvrH = lines[i];
                        }
                        break;
                    case "O_RCirc":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_RCirc = lines[i];
                        }
                        break;
                    case "O_Capilar":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_Capilar = lines[i];
                        }
                        break;
                    case "O_CapLen":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_CapLen = lines[i];
                        }
                        break;
                    case "O_CondQta":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_CondQta = lines[i];
                        }
                        break;
                    case "O_DxDist":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_DxDist = lines[i];
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
                    case "O_SenCap":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_SenCap = lines[i];
                        }
                        break;
                    case "O_AirPaF":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_AirPaF = lines[i];
                        }
                        break;
                    case "O_SHR":
                        for (int i = 0; i < 40; i++)
                        {
                            outViewLines[i].O_SHR = lines[i];
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
            inputData.ProjectsOut.AirTempOut = line.O_AirTempOut;
            inputData.ProjectsOut.I_Geo = GetGeometry(line.O_Code);
            inputData.ProjectsOut.O_TotCapPrint = line.O_TotCap + " " + Calculation.TO.Main.Properties.Resources.kW;
            inputData.ProjectsOut.O_TotCap = line.O_TotCap;
            inputData.ProjectsOut.AirTempOut = line.O_AirTempOut;
            inputData.ProjectsOut.O_AirTempOutStr = line.O_AirTempOut + " " + Calculation.TO.Main.Properties.Resources.degreeСelsius;
            inputData.ProjectsOut.O_AirHumOutAbs = line.O_AirHumInAbs;
            inputData.ProjectsOut.O_AirVelo = line.O_AirVelo;
            inputData.ProjectsOut.O_AirPaT = line.O_AirPaT;
            inputData.ProjectsOut.O_Code = line.O_Code;
            inputData.ProjectsOut.O_Volume = line.O_Volume;
            inputData.ProjectsOut.CalcType = CalcType;
            inputData.ProjectsOut.O_AirHumOut = line.O_AirHumOut;
            inputData.ProjectsOut.O_Airkgs = line.O_Airkgs;

            if (CalcType == CalcEnum.Table)
            {
                inputData.ProjectsOut.FinSpace = tableDataService.GetFinSpace(index);
            }
            else
            {
                inputData.ProjectsOut.FinSpace = GetFinSpace();
            }
            inputData.ProjectsOut.O_DxCxBar = line.O_DxCxBar;
            inputData.ProjectsOut.O_DxCxK = line.O_DxCxK;
            inputData.ProjectsOut.O_DxCxMas = line.O_DxCxMas;
            inputData.ProjectsOut.O_DxCxPres = line.O_DxCxPres;
            inputData.ProjectsOut.O_DxCxVol = line.O_DxCxVol;
            inputData.ProjectsOut.O_THotGas = line.O_THotGas;
            inputData.ProjectsOut.O_TOvrH = line.O_TOvrH;
            inputData.ProjectsOut.O_RCirc = line.O_RCirc;
            inputData.ProjectsOut.O_Capilar = line.O_Capilar;
            inputData.ProjectsOut.O_CapLen = line.O_CapLen;
            inputData.ProjectsOut.O_CondQta = line.O_CondQta;
            inputData.ProjectsOut.O_DxDist = line.O_DxDist;

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
                        TableData.ProjectsOutTable[IdToCalc].I_WidthInt = double.Parse(line.O_WidthInt, CultureInfo.InvariantCulture); //inputData.ValueWidthFin;
                        TableData.ProjectsOutTable[IdToCalc].I_HeightInt = double.Parse(line.O_HeightInt, CultureInfo.InvariantCulture); //inputData.ValueHightFin;

                        double d = GS.StringToDouble(line.O_StmBar);
                        TableData.ProjectsOutTable[IdToCalc].O_StmBar = d;
                        TableData.ProjectsOutTable[IdToCalc].O_StmFlow = line.O_StmFlow;
                        TableData.ProjectsOutTable[IdToCalc].O_StmHeat = line.O_StmHeat;
                        TableData.ProjectsOutTable[IdToCalc].O_StmVelo = line.O_StmVelo;
                        TableData.ProjectsOutTable[IdToCalc].I_StmBar = line.O_StmBar;

                        TableData.ProjectsOutTable[IdToCalc].O_RCirc = line.O_RCirc;
                        TableData.ProjectsOutTable[IdToCalc].O_DxCxVol = line.O_DxCxVol;
                        TableData.ProjectsOutTable[IdToCalc].O_DxCxBar = line.O_DxCxBar;
                        TableData.ProjectsOutTable[IdToCalc].O_DxCxK = line.O_DxCxK;
                        TableData.ProjectsOutTable[IdToCalc].O_THotGas = line.O_THotGas;
                        TableData.ProjectsOutTable[IdToCalc].O_DxCxMas = line.O_DxCxMas;
                        TableData.ProjectsOutTable[IdToCalc].O_DxCxPres = line.O_DxCxPres;
                        TableData.ProjectsOutTable[IdToCalc].O_Capilar = line.O_Capilar;
                        TableData.ProjectsOutTable[IdToCalc].O_CapLen = line.O_CapLen;
                        TableData.ProjectsOutTable[IdToCalc].O_SenCap = line.O_SenCap;
                        TableData.ProjectsOutTable[IdToCalc].O_CondQta = line.O_CondQta;
                        TableData.ProjectsOutTable[IdToCalc].O_DxDist = line.O_DxDist;
                        TableData.ProjectsOutTable[IdToCalc].O_AirPaF = line.O_AirPaF;
                        TableData.ProjectsOutTable[IdToCalc].O_SHR = line.O_SHR;

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

        /// <summary>
        /// Возвращяет шаг оребрения
        /// </summary>
        /// <returns>Значение поля FinSpase</returns>
        private double GetFinSpace()
        {
            string[] stepFin = inputData.SelectedStepFin.Split(' ');
            return double.Parse(stepFin[0], CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Проверка совпадает ли массовый расход
        /// </summary>
        /// <returns></returns>
        protected override bool CheckMassFlow()
        {
            double masFlow = double.Parse(inputData.ProjectsOut.O_DxCxMas, CultureInfo.InvariantCulture);

            // ели еединица измерения массового расхода "г/с" 
            // умножить  на 3.6 для перевода в "кг/ч"
            if (inputData.VisibilityDirectMassFlowGS == Visibility.Visible)
            {
                masFlow /= 3.6;
            }
            double delta = (masFlow - inputData.DirectMassFlowDX) /
                inputData.DirectMassFlowDX;
            if (Math.Abs(delta) < 0.01)
            {
                return true;
            }
            else
            {
                inputData.CapacityMassFlow = inputData.DirectMassFlowDX / masFlow * inputData.CapacityMassFlow;
                SetCapacity();
                return false;
            }
        }

        /// <summary>
        /// Изменение мощности для расчёта по массовому расходу
        /// </summary>
        protected override void SetCapacity()
        {
            foreach (var item in lines)
            {
                // требуемая мощность
                if (item.Field == "I_Capacity")
                {
                    item.Content = inputData.CapacityMassFlow.ToString("G", CultureInfo.InvariantCulture);
                    continue;
                }
            }
        }

        /// <summary>
        /// В зависимости от выбранного теплообменика
        /// </summary>
        /// <param name="saveCodes"></param>
        protected override void SetSelectHeatExchanger(SaveCodes saveCodes)
        {
            // Мощность или температура воздуха на выходе
            inputData.ValueCapacityDX = saveCodes.CapacityDX;
            // Расход воздуха
            inputData.ValueAirFlowDX = saveCodes.ValueAirFlowDX;
            // Температура воздуха на входе
            inputData.I_AirTempInDX = saveCodes.I_AirTempInDX;

            // Влажность воздуха на входе
            inputData.ValueBaseHumDX = saveCodes.ValueBaseHumDX;

        }

        /// <summary>
        /// Получить выходные параметры
        /// </summary>
        /// <returns></returns>
        private OutputDataEvaporaterDTO GetOutputParams()
        {
            OutputDataEvaporaterDTO outputParams = new OutputDataEvaporaterDTO();
            if (inputData.ProjectsOutView.Count == 0) return outputParams;
            outputParams.I_Geo = inputData.ProjectsOutView[0].I_Geo.ToString();
            outputParams.ShortName = inputData.ProjectsOutView[0].ShortName;
            outputParams.NoOfRows = inputData.ProjectsOutView[0].NoOfRows;
            outputParams.Circuits = inputData.ProjectsOutView[0].Circuits;
            outputParams.AirVelocity = inputData.ProjectsOutView[0].AirVelocity;
            outputParams.PresDropDry = inputData.ProjectsOutView[0].PresDropDry;
            outputParams.ReverseLoad = inputData.ProjectsOutView[0].ReverseLoad;
            outputParams.O_DxCxBar = inputData.ProjectsOutView[0].O_DxCxBar;
            outputParams.O_DxCxK = inputData.ProjectsOutView[0].O_DxCxK;
            outputParams.O_THotGas = inputData.ProjectsOutView[0].O_THotGas;
            outputParams.O_DxCxMas = inputData.ProjectsOutView[0].O_DxCxMas;
            outputParams.O_DxCxVol = inputData.ProjectsOutView[0].O_DxCxVol;
            outputParams.O_Volume = inputData.ProjectsOutView[0].O_Volume;
            outputParams.AirTempOut = inputData.ProjectsOutView[0].AirTempOut;
            outputParams.O_TotCap = inputData.ProjectsOutView[0].O_TotCap;
            return outputParams;
        }

        #endregion
    }
}
