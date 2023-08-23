using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Veza.Calculation.TO.Main.Interfaces;
using Veza.HeatExchanger.BusinessLogic.Compressors.InvoTech;
using Veza.HeatExchanger.BusinessLogic.Compressors.Models;
using Veza.HeatExchanger.BusinessLogic.Compressors.Select_8;
using Veza.HeatExchanger.BusinessLogic.MAKK.DTO;
using Veza.HeatExchanger.BusinessLogic.MAKK.Mapper;
using Veza.HeatExchanger.BusinessLogic.MAKK.Models;
using Veza.HeatExchanger.DataBase.Interafces;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.DataBase.Models.DTO;
using Veza.HeatExchanger.DataBase.Models.EquipmentMAKK;
using Veza.HeatExchanger.DataBase.Models.Mappers;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Interfaces.Mappers;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.Main;
using Veza.HeatExchanger.Models.MAKK;
using Veza.HeatExchanger.Services;
using Veza.HeatExchanger.ViewModels.Controls;

namespace Veza.HeatExchanger.BusinessLogic.MAKK
{
    public class MAKKService : IExtMAKKService
    {
        #region Внутренние поля 
        private InputMAKKParams InputMAKKParamsP { get; set; }
        private List<EquipmentMAKKDB> equipmentMAKKs;
        private IMakk makkDb;
        private IFan fanDb;
        private ICompressor compressor;
        private IInputDataCondensatorService calculateService;
        private IInputData inputData;
        private IInputDataService inputDataService;
        private AdvanceParamsViewModel advanceParamsViewModel;
        private ILogs logs;
        /// <summary>
        /// таблица результатов расчёта
        /// </summary>
        private List<MAKKItem> Results { get; set; }
        /// <summary>
        /// выделенный расчёт МАКК
        /// </summary>
        private MAKKItem SelectedResults { get; set; }
        /// <summary>
        /// Текст ошибки при расчёте
        /// </summary>
        private string Error { get; set; } = "";
        /// <summary>
        /// Доступность кнопки "Опции и Доп.оборудов."
        /// </summary>
        private bool IsOption { get; set; }
        /// <summary>
        /// Доступность кнопки "Чертёж"
        /// </summary>
        private bool IsDrawing { get; set; }
        /// <summary>
        /// Доступность кнопки "Печать"
        /// </summary>
        private bool IsPrint { get; set; }
        /// <summary>
        /// Список опций МАКК
        /// </summary>
        private List<MAKKOptions> options;
        /// <summary>
        /// Список дополнительных комплектующих для МАКК
        /// </summary>
        private List<MAKKOptions> addEquip;
        /// <summary>
        /// Оборудование МАКК
        /// </summary>
        private IList<EquipmentMAKKDTO> EquipmentMAKK { get; set; }
        /// <summary>
        /// выбранное оборудование МАКК
        /// </summary>
        private EquipmentMAKKDTO SelectedEquipmentMAKK { set; get; }
        private readonly ICondensatorMapper condensatorMapper;
        /// <summary>
        /// были ли установлены параметры
        /// </summary>
        private bool IsSetParams { get; set; }
        /// <summary>
        /// был ли расчёт
        /// </summary>
        private bool IsCalc { get; set; }
        private InputDataCompressors inputDataCompressors;
        /// <summary>
        /// подобранные вентиляторы
        /// </summary>
        private List<FanDTO> Fans { get; set; } = new List<FanDTO>();
        /// <summary>
        /// выбранный вентилятор
        /// </summary>
        private FanDTO SelectedFan { get; set; }

        /// <summary>
        /// подобранный оптимальный компрессор
        /// </summary>
        private List<SelectCompressors> Compressors { get; set; } = new List<SelectCompressors>();
        /// <summary>
        /// подобранные компрессоры
        /// </summary>
        private List<SelectCompressors> CompressorsOther { get; set; } = new List<SelectCompressors>();
        private List<SearchCompressors> searchCompressors = new List<SearchCompressors>();
        /// <summary>
        /// Сервис для поиска подходящего вентилятора
        /// </summary>
        private IFanSelection fanSelection;
        /// <summary>
        /// Компрессора
        /// </summary>
        private IInvoTech invoTech;
        private ISelect8 select8;
        private bool fLoad = false;
        private const double MASS_FLOW_DIFF_RES = 0.5;
        private const double REFR_CAP_DIFF_RES = 0.1;
        #endregion

        #region Конструктор

        public MAKKService(AdvanceParamsViewModel advanceParamsViewModel, IInputData inputData, ILogs logs,  
           IInputDataService inputDataService, ICondensatorMapper condensatorMapper, ICompressor compressor,
           IInputDataCondensatorService condensatorCalculateService, InputDataCompressors inputDataCompressors,
           IFanSelection fanSelection, IInvoTech invoTech, ISelect8 select8, IMakk makkDb, IFan fanDb)
        {
            this.makkDb = makkDb;
            this.fanDb = fanDb;
            this.compressor = compressor;
            calculateService = condensatorCalculateService;
            this.inputData = inputData;
            this.inputDataService = inputDataService;
            this.advanceParamsViewModel = advanceParamsViewModel;
            this.logs = logs;
            this.condensatorMapper = condensatorMapper;
            this.inputDataCompressors = inputDataCompressors;
            this.fanSelection = fanSelection;
            this.invoTech = invoTech;
            this.select8 = select8;
            options = new List<MAKKOptions>();
            addEquip = new List<MAKKOptions>();
            EquipmentMAKK = new List<EquipmentMAKKDTO>();
            InputMAKKParamsP = new InputMAKKParams()
            {
                Refrigerants = new List<string>() { Calculation.TO.Main.Properties.Resources.R410A },
                SelectRefrigerant = Calculation.TO.Main.Properties.Resources.R410A,
                SeriesMAKKs = new List<SeriesMAKK>
                {
                    new SeriesMAKK
                    {
                        IsChecked = true,
                        Name = Calculation.TO.Main.Properties.Resources.MAKKSeria310List,
                    },
                    new SeriesMAKK
                    {
                        IsChecked = false,
                        Name = Calculation.TO.Main.Properties.Resources.MAKKSeria320List,
                    },
                    new SeriesMAKK
                    {
                        IsChecked = false,
                        Name = Calculation.TO.Main.Properties.Resources.MAKKSeria330List,
                    },
                },
                CoolingCapacity = "0",
                ErrorRate = "20",
                OutTemp = "30",
                EvapTemp = "7"
            };
        }
        #endregion

        #region Публичные методы

        /// <summary>
        /// Получение входных параметров МАКК
        /// </summary>
        public InputMAKKParamsDTO GetInputMAKKParams()
        {
            return InputMAKKParamsMapper.InputDataToDTO(InputMAKKParamsP);
        }

        /// <summary>
        /// Изменение входных параметров МАКК
        /// </summary>
        public bool SetInputMAKKParams(InputMAKKParamsDTO inputDTO)
        {
            InputMAKKParamsP = InputMAKKParamsMapper.DTOToInputData(inputDTO);
            return true;
        }

        /// <summary>
        /// Расчёт МАКК
        /// </summary>
        public void CalcMAKK()
        {
            double power;
            double coolCap;
            double err;
            // получение параметров МАКК из БД
            equipmentMAKKs = makkDb.LoadMAKK();
            calculateService.SetCalcMode("CX");
            calculateService.CalculationInit();
            if (Results == null)
            {
                Results = new List<MAKKItem>();
            }
            else
            {
                Results.Clear();
            }
            for (int i = 0; i < equipmentMAKKs.Count; i++)
            {
                // присваивание параметров конденсатору из МАКК
                SetParamsMAKKToInputData(i);
                // присваивание введенных параметров из окна конденсатору
                SetInputParamsToCondensator();

                // вызов dll для перерасчёта конденсатора на введенные параметры
                calculateService.CalcReverse(Calculation.TO.Main.Properties.Resources.PageCondensator_Reverse);
                if (calculateService.GetResult() == 0)
                {
                    //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strCalculationSuccessful);
                }
                power = equipmentMAKKs[i].CompressorId.PowerInput * equipmentMAKKs[i].CompressorCount
                    + equipmentMAKKs[i].FanModelsId.Power * equipmentMAKKs[i].FanModelsCount;
                err = 0;
                bool res;
                coolCap = GetCoolCapacity(out res);
                if (res)
                {
                    err = coolCap / power;
                }
                MAKKItem calcResult = new MAKKItem
                {
                    Name = equipmentMAKKs[i].Name,
                    NameView = equipmentMAKKs[i].Name,
                    CoolingCapacity = inputData.ProjectsOutView[0].O_TotCap,
                    TotalCapacity = string.Format("{0:F2}", power),
                    EER = string.Format("{0:F2}", err),
                    NumCircuits = "1",
                };
                if (InputMAKKParamsP.CoolingCapacityD != 0)
                {
                    if (InputMAKKParamsP.CoolingCapacityD * (1 + InputMAKKParamsP.ErrorRateD / 100) < coolCap) continue;
                    if (InputMAKKParamsP.CoolingCapacityD * (1 - InputMAKKParamsP.ErrorRateD / 100) > coolCap) continue;
                    double delta = 0;
                    if (InputMAKKParamsP.CoolingCapacityD > coolCap)
                    {
                        delta = -(InputMAKKParamsP.CoolingCapacityD - coolCap) / InputMAKKParamsP.CoolingCapacityD * 100;
                    }
                    else
                    {
                        delta = (coolCap - InputMAKKParamsP.CoolingCapacityD) / InputMAKKParamsP.CoolingCapacityD * 100;
                    }
                    calcResult.Reserve = delta.ToString("F2", CultureInfo.InvariantCulture);
                }
                else
                {
                    calcResult.Reserve = "";
                }
                Results.Add(calcResult);
            }
            if (Results.Count > 0)
            {
                IsOption = true;
                IsDrawing = true;
                IsPrint = true;
                Error = "";
            }
            else
            {
                IsOption = false;
                IsDrawing = false;
                IsPrint = false;
                Error = "Нет подобранных вариантов для данных серий";
            }
        }

        /// <summary>
        /// Возврат результатов расчёта МАКК
        /// </summary>
        public List<MAKKItem> GetResultCalcMAKK()
        {
            if (Results.Count > 0) return Results;
            return new List<MAKKItem>();
        }

        /// <summary>
        /// Вернуть сообщение об ошибке
        /// </summary>
        /// <returns></returns>
        public string GetError()
        {
            return Error;
        }

        /// <summary>
        /// Получить опции МАКК
        /// </summary>
        /// <returns></returns>
        public List<MAKKOptions> GetOptions()
        {
            if (!IsOption) return options;
            string l_nameMAKK = GetName();
            if (string.IsNullOrEmpty(l_nameMAKK)) return options;
            return options = GetOptions(l_nameMAKK);
        }

        /// <summary>
        /// Изменить список Опций МАКК
        /// </summary>
        public bool SetOptions(List<MAKKOptions> inOptions)
        {
            if (options.Count != inOptions.Count) return false;
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].Id != inOptions[i].Id) return false;
                options[i].IsChecked = inOptions[i].IsChecked;
            }
            return true;
        }

        /// <summary>
        /// Получить дополнительные комплектующие для МАКК
        /// </summary>
        /// <returns></returns>
        public List<MAKKOptions> GetAddEquip()
        {
            if (!IsOption) return addEquip;
            string l_nameMAKK = GetName();
            if (string.IsNullOrEmpty(l_nameMAKK)) return addEquip;
            return addEquip = GetAccessories(l_nameMAKK);
        }

        /// <summary>
        /// Отмеченный список дополнительных комплектующих МАКК
        /// </summary>
        /// <param name="inAddEquip"></param>
        /// <returns></returns>
        public bool SetAddEquip(List<MAKKOptions> inAddEquip)
        {
            if (addEquip.Count != inAddEquip.Count) return false;
            for (int i = 0; i < addEquip.Count; i++)
            {
                if (inAddEquip[i].Id != inAddEquip[i].Id) return false;
                addEquip[i].IsChecked = inAddEquip[i].IsChecked;
            }
            return true;
        }

        /// <summary>
        /// получении бланк-заказа МАКК
        /// </summary>
        public void GetOrderForm()
        {

        }

        /// <summary>
        /// Получение списка МАКК 
        /// </summary>
        public List<ListMAKKParamsDTO> GetMAKKs()
        {
            makkDb.InitEqupmentMAKK(EquipmentMAKK);
            return ListMAKKParamsMapper.MAKKParamsToDTO(EquipmentMAKK.ToList());
        }

        /// <summary>
        /// Получить параметры MAKK
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MAKKParamsDTO GetParamsMAKK(string name)
        {
            EquipmentMAKKDTO equipmentMAKKDTO = EquipmentMAKK.Where(m => m.Model == name).FirstOrDefault();
            MAKKParamsDTO mAKKParamsDTO = MAKKParamsDTOMapper.EquipmentToParams(equipmentMAKKDTO, makkDb.GetHeatExchangers(),
                fanDb.GetFans(), compressor.GetCompressors());
            return mAKKParamsDTO;
        }

        /// <summary>
        /// Передать изменённые параметры MAKK
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool SetParamsMAKK(MAKKParamsDTO param)
        {
            EquipmentMAKKDTO equipmentMAKKDTO = EquipmentMAKK.Where(m => m.Id == param.Id).FirstOrDefault();
            equipmentMAKKDTO = MAKKParamsDTOMapper.ParamsToEquipment(param, equipmentMAKKDTO);
            return makkDb.UpdateEquipmentMAKK(equipmentMAKKDTO);
        }

        /// <summary>
        /// Установка свойств
        /// </summary>
        /// <param name="inputParams"></param>
        public void SetParams(InputDataMAKK inputParams)
        {
            inputData.FanReserveAirFlow = inputParams.FanReserveAirFlow;
            inputData.NumberOfFans = inputParams.NumberOfFans;
            condensatorMapper.InputToInputDataClass(inputParams.InputParams);
            inputDataCompressors = inputParams.InputDataCompressor;
            IsSetParams = true;
        }

        /// <summary>
        /// расчёт Конденсатора
        /// </summary>
        /// <returns></returns>
        public bool CalcNewMAKK()
        {
            if (!IsSetParams) return false;
            inputData.SetInputDataCompressors(inputDataCompressors);
            //calculateService.SetPropertiesCX_MAKK();
            calculateService.SetCalcMode("CX");
            calculateService.CalculationInit();
            calculateService.CalcReverse(Calculation.TO.Main.Properties.Resources.PageCondensator_Reverse);
            fLoad = false;
            if (calculateService.GetResult() == 0)
            {
                SelectionFans(inputData);                   // подбор вентиляторов
                SelectionCompressors();                     // подбор компрессоров
                return IsCalc = true;
            }
            return IsCalc = false;
        }

        /// <summary>
        /// Получение результатов расчёта
        /// </summary>
        /// <returns></returns>
        public OutputDataMAKK GetResultsCalcNewMAKK()
        {
            OutputDataMAKK outputDataMAKK = new OutputDataMAKK();          
            OutputDataCondensatorDTO outputParams = new OutputDataCondensatorDTO();
            outputDataMAKK.OutputDataCondensator = outputParams;
            if (!IsCalc) return outputDataMAKK;
            if (calculateService.GetResult() != 0)
            {
                // ошибка
                return outputDataMAKK;
            }
            // если ошибки расчёта нет
            inputData.CalcCondensator = true;
            outputDataMAKK.FanReserveAirFlow = inputData.FanReserveAirFlow;
            outputDataMAKK.NumberOfFans = inputData.NumberOfFans;
            outputDataMAKK.Fans = MapperFanDTOToFanOutDTO.FanDTOToFanOutDTO(Fans);
            outputDataMAKK.SelectedFan = MapperFanDTOToFanOutDTO.FanDTOToFanOutDTO(SelectedFan);
            outputDataMAKK.OutputDataCondensator = GetOutputParams();
            outputDataMAKK.Compressors = Compressors;
            outputDataMAKK.CompressorsOther = CompressorsOther;
            return outputDataMAKK;
        }

        /// <summary>
        /// Получить параметры для расчёта нового МАКК
        /// </summary>
        /// <returns></returns>
        public InputDataMAKK MAKKGetParams()
        {
            if (!IsCalc)
            {
                return new InputDataMAKK
                {
                    FanReserveAirFlow = 0,
                    NumberOfFans = 1,
                    InputParams = new InputDataCondensatorDTO
                    {
                        SelectedMode = "reverse",
                        SelectedDirectMode = "Мощность",
                        //ValueCapacity = "230",
                        SelectGeometry = "2507",
                        ValueWidthFin = "550",
                        ValueHightFin = "1250",
                        TubesN = "50",
                        SelectedPipe = "7.0 x 0.28 мм Медь",
                        SelectedFin = "0.10 мм Алюминий",
                        SelectedStepFin = "2.0 мм",
                        ValueAirFlow = "3400",
                        I_AirTempIn = "-12",
                        ValueBaseHum = "90",
                        Selected_I_RefT = "R410A",
                        Selected_I_FoulingI = "Без масла",
                        //I_THotGasCX = "3",
                        SelectCondTemp = "Температура конденсации",
                        I_TCondCX = "45",
                        CondAbsPresCX = "0",
                        SelectSubCool = "Переохлаждение",
                        I_TSubCCX = "7",
                        LiquidTempCX = "0",
                        ValuePipe = "2",
                        ValueCircuits = "6",
                        NumOfPasses = "16",
                        // характеристики корпуса и коллектора
                        I_MatHdr = "0",
                        Selected_I_MatHdr = "Медь",
                        Selected_I_ConIn = "Оптимизировать",
                        Selected_I_ConOut = "Оптимизировать",
                        Selected_I_ConType = "Заглушенный патрубок",
                        I_CSheetL = "0",
                        SelectedCasingType = "Для установки в кондиционере типа ВЕРОСА 200, 251, 300, 700",
                        Selected_I_CasMat = "Оцинкованная сталь без покрытия",
                        SelectedEsapo = "Вертикальное расположение, противоток, подключение к верхнему патрубку",
                        Select_I_CDir = "Стандартная (перпендикулярно потоку воздуха в сторону)",
                        SelectedAirFlowDirection = "Правый",
                        SelectedConnectingCoolant = "Выход коллекторов на одну и ту же сторону",
                        // Дополнительные параметры
                        BaseBar = "1013.25",
                        SelectGasWorkPressUnit = "мбар",
                        I_BaseDens = "1.2",
                        I_ARes = "0",
                        I_LRes = "0",
                        //Select_I_FoulingI = "Деминерализованная вода",
                        Select_I_FoulingE = "Чистый операционный воздух",
                        SelectAFPV = "Нет",
                    },
                    InputDataCompressor = inputDataCompressors
                };
            }
            return new InputDataMAKK()
            {
                FanReserveAirFlow = inputData.FanReserveAirFlow,
                NumberOfFans = inputData.NumberOfFans,
                //InputParams = condensatorMapper.InputDataClassToInputParams(),
                InputDataCompressor = inputDataCompressors,
            };
        }
        #endregion

        #region Приватные методы

        /// <summary>
        /// Возвращяет холодопроизводительность
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        private double GetCoolCapacity(out bool res)
        {
            double coolCap = 0;
            res = double.TryParse(inputData.ProjectsOutView[0].O_TotCap, NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out coolCap);
            return coolCap;
        }

        /// <summary>
        /// присваивание параметров конденсатору из МАКК
        /// </summary>
        private void SetParamsMAKKToInputData(int i)
        {
            inputData.SelectGeometry = equipmentMAKKs[i].HeatExchangerId.I_Geometry;
            inputData.ValueWidthFin = equipmentMAKKs[i].HeatExchangerId.I_WidthInt;
            inputData.ValueHightFin = equipmentMAKKs[i].HeatExchangerId.I_HeightInt;
            inputData.ValuePipe = equipmentMAKKs[i].HeatExchangerId.I_Rows;
            inputData.ValueCircuits = equipmentMAKKs[i].HeatExchangerId.I_Circuits;
            inputData.ValueAirFlowCX = equipmentMAKKs[i].HeatExchangerId.I_Airflow;
            inputData.ValueBaseHumCX = equipmentMAKKs[i].HeatExchangerId.I_AirHumIn;
            inputData.I_AirTempInCX = equipmentMAKKs[i].HeatExchangerId.I_AirTempIn;
            inputData.I_BaseHum = equipmentMAKKs[i].HeatExchangerId.I_BaseHum;
            inputData.I_BaseMode = equipmentMAKKs[i].HeatExchangerId.I_BaseMode;
            inputData.I_BaseSea = equipmentMAKKs[i].HeatExchangerId.I_BaseSea;
            inputData.I_BaseTemp = equipmentMAKKs[i].HeatExchangerId.I_BaseTemp;
            inputData.I_CasIso = equipmentMAKKs[i].HeatExchangerId.I_CasIso;
            inputData.I_CatCoat = equipmentMAKKs[i].HeatExchangerId.I_CatCoat;
            inputData.I_CBox = equipmentMAKKs[i].HeatExchangerId.I_CBox;
            inputData.I_CertMode = equipmentMAKKs[i].HeatExchangerId.I_CertMode;
            inputData.I_ElimMat = equipmentMAKKs[i].HeatExchangerId.I_ElimMat;
            inputData.I_ElimTp = equipmentMAKKs[i].HeatExchangerId.I_ElimTp;
            inputData.I_Esapo = equipmentMAKKs[i].HeatExchangerId.I_Esapo;
            inputData.I_FanPlen = equipmentMAKKs[i].HeatExchangerId.I_FanPlen;
            inputData.I_FrameTp = equipmentMAKKs[i].HeatExchangerId.I_FrameTp;
            inputData.I_FullH = equipmentMAKKs[i].HeatExchangerId.I_FullH;
            inputData.I_HdrQta = equipmentMAKKs[i].HeatExchangerId.I_HdrQta;
            inputData.I_IseEta = equipmentMAKKs[i].HeatExchangerId.I_IseEta;
            inputData.I_KorrADP = equipmentMAKKs[i].HeatExchangerId.I_KorrADP;
            inputData.I_KorrKDP = equipmentMAKKs[i].HeatExchangerId.I_KorrKDP;
            inputData.I_LamAbsMax = equipmentMAKKs[i].HeatExchangerId.I_LamAbsMax;
            inputData.I_MatFins = equipmentMAKKs[i].HeatExchangerId.I_MatFins;
            inputData.I_MatFrame = equipmentMAKKs[i].HeatExchangerId.I_MatFrame;
            inputData.I_Mode = equipmentMAKKs[i].HeatExchangerId.I_Mode;
            inputData.I_PanMat = equipmentMAKKs[i].HeatExchangerId.I_PanMat;
            inputData.I_PanTp = equipmentMAKKs[i].HeatExchangerId.I_PanTp;
            inputData.I_RCon = equipmentMAKKs[i].HeatExchangerId.I_RCon;
            inputData.Selected_I_RefT = equipmentMAKKs[i].HeatExchangerId.I_RefT;
            inputData.I_SpcSolder = equipmentMAKKs[i].HeatExchangerId.I_SpcSolder;
            inputData.I_TCondCX = equipmentMAKKs[i].HeatExchangerId.I_TCond;
            inputData.I_TEvapCX = equipmentMAKKs[i].HeatExchangerId.I_TEvap;
            inputData.I_THotGasCX = equipmentMAKKs[i].HeatExchangerId.I_THotGas;
            inputData.I_TMaxPCX = equipmentMAKKs[i].HeatExchangerId.I_TMaxP;
            inputData.I_TOvrHCX = equipmentMAKKs[i].HeatExchangerId.I_TOvrH;
            inputData.I_TSubCCX = equipmentMAKKs[i].HeatExchangerId.I_TSubC;
            inputData.I_TSucGasCX = equipmentMAKKs[i].HeatExchangerId.I_TSucGas;
            inputData.I_Type = equipmentMAKKs[i].HeatExchangerId.I_Type;
            inputData.I_Uneven = equipmentMAKKs[i].HeatExchangerId.I_Uneven;
            inputData.I_MatRows = equipmentMAKKs[i].HeatExchangerId.I_MatRows;

            inputDataService.SetI_CasMat(equipmentMAKKs[i].HeatExchangerId.I_CasMat);
            inputDataService.SetI_CDir(equipmentMAKKs[i].HeatExchangerId.I_CDir);
            inputDataService.SetI_ConIn(equipmentMAKKs[i].HeatExchangerId.I_ConIn);
            inputDataService.SetI_ConIn(equipmentMAKKs[i].HeatExchangerId.I_ConOut);
            inputDataService.SetI_ConType(equipmentMAKKs[i].HeatExchangerId.I_ConType);
            inputDataService.SetI_CSheet(equipmentMAKKs[i].HeatExchangerId.I_CSheet);
            inputDataService.SetI_CSheetL(equipmentMAKKs[i].HeatExchangerId.I_CSheetL);
            inputDataService.SetI_MatHdr(equipmentMAKKs[i].HeatExchangerId.I_MatHdr);

            SetI_FinThk(i);
            SetI_LamAbsFix(i);

            #region не используются, всегда 0
            //equipmentMAKKs[i].HeatExchangerId.I_KorrFA;
            //equipmentMAKKs[i].HeatExchangerId.I_KorrFI;
            #endregion

            advanceParamsViewModel.Set_I_FoulingE(equipmentMAKKs[i].HeatExchangerId.I_FoulingE);
            advanceParamsViewModel.Set_I_FoulingI(equipmentMAKKs[i].HeatExchangerId.I_FoulingI);
            advanceParamsViewModel.I_ARes = equipmentMAKKs[i].HeatExchangerId.I_ARes;
            advanceParamsViewModel.I_LRes = equipmentMAKKs[i].HeatExchangerId.I_LRes;
            advanceParamsViewModel.BaseBar = equipmentMAKKs[i].HeatExchangerId.I_BaseBar;
            advanceParamsViewModel.I_BaseDens = equipmentMAKKs[i].HeatExchangerId.I_BaseDens;

            inputData.SelectedEsapo = Calculation.TO.Main.Properties.Resources.EsapoBU;
            inputData.SelectedAirFlowDirection = Calculation.TO.Main.Properties.Resources.AirFlowDirectionRight;
        }

        private void SetI_FinThk(int i)
        {
            List<string> finThks = new List<string>();
            int count = 0;
            foreach (var item in inputData.Fin)
            {
                if (item.Contains(equipmentMAKKs[i].HeatExchangerId.I_FinThk))
                {
                    finThks.Add(item);
                    count++;
                }
            }
            if (finThks.Count == 1)
            {
                inputData.SelectedFin = finThks[0];
            }
            else if (finThks.Count > 1)
            {
                string pipe = equipmentMAKKs[i].HeatExchangerId.I_PipeThk;
                pipe = pipe.Replace('.', ',');
                foreach (var item in inputData.Fin)
                {
                    if (item.Contains("."))
                    {
                        pipe = pipe.Replace('.', ',');
                    }
                    if (item.Contains(pipe))
                    {
                        GetMat(i);
                    }
                }
            }
        }

        private void GetMat(int i)
        {
            switch (equipmentMAKKs[i].HeatExchangerId.I_MatFins)
            {
                case "1":
                    // CuSmall
                    foreach (var item in inputData.Fin)
                    {
                        if (item.Contains(equipmentMAKKs[i].HeatExchangerId.I_FinThk))
                        {
                            if (item.Contains(Calculation.TO.Main.Properties.Resources.CuSmall))
                            {
                                inputData.SelectedFin = item;
                            }
                        }
                    }
                    break;
                case "2":
                    // AlSmall
                    foreach (var item in inputData.Fin)
                    {
                        if (item.Contains(equipmentMAKKs[i].HeatExchangerId.I_FinThk))
                        {
                            if (item.Contains(Calculation.TO.Main.Properties.Resources.AlSmall))
                            {
                                inputData.SelectedFin = item;
                            }
                        }
                    }
                    break;
                case "3":
                    // EpoxySmall
                    foreach (var item in inputData.Fin)
                    {
                        if (item.Contains(equipmentMAKKs[i].HeatExchangerId.I_FinThk))
                        {
                            if (item.Contains(Calculation.TO.Main.Properties.Resources.EpoxySmall))
                            {
                                inputData.SelectedFin = item;
                            }
                        }
                    }
                    break;
                case "13":
                    // stain.steel304Small
                    foreach (var item in inputData.Fin)
                    {
                        if (item.Contains(equipmentMAKKs[i].HeatExchangerId.I_FinThk))
                        {
                            if (item.Contains(Calculation.TO.Main.Properties.Resources.stain_steel304Small))
                            {
                                inputData.SelectedFin = item;
                            }
                        }
                    }
                    break;
            }
        }

        private void SetI_LamAbsFix(int i)
        {
            foreach (var item in inputData.StepFin)
            {
                if (item.Contains(equipmentMAKKs[i].HeatExchangerId.I_LamAbsFix))
                {
                    inputData.SelectedStepFin = item;
                    break;
                }
            }
        }

        /// <summary>
        /// присваивание введенных параметров из окна конденсатору
        /// </summary>
        private void SetInputParamsToCondensator()
        {
            inputData.I_AirTempInCX = InputMAKKParamsP.OutTempD;
            inputData.I_TEvapCX = InputMAKKParamsP.EvapTempD;
        }

        /// <summary>
        /// вернуть список Опции
        /// </summary>
        private List<MAKKOptions> GetOptions(string nameMAKK)
        {
            List<MAKKOptionDB> strings = makkDb.GetOptions(nameMAKK);
            // опции
            List<MAKKOptions> options = new List<MAKKOptions>();
            int k = 0;
            foreach (var item in strings)
            {
                if (GS.IsCultureRU())
                {
                    options.Add(new MAKKOptions
                    {
                        Id = k++,
                        Description = item.NameRu,
                        IsChecked = false,
                    });
                }
                else
                {
                    options.Add(new MAKKOptions
                    {
                        Id = k++,
                        Description = item.NameEn,
                        IsChecked = false,
                    });
                }
            }
            return options;
        }

        /// <summary>
        /// вернуть список Доп. оборудование
        /// </summary>
        private List<MAKKOptions> GetAccessories(string nameMAKK)
        {
            List<MAKKAccessoriesDB> strings = makkDb.GetAccessories(nameMAKK);
            // опции
            List<MAKKOptions> options = new List<MAKKOptions>();
            int k = 0;
            foreach (var item in strings)
            {
                if (GS.IsCultureRU())
                {
                    options.Add(new MAKKOptions
                    {
                        Id = k++,
                        Description = item.NameRu,
                        IsChecked = false,
                    });
                }
                else
                {
                    options.Add(new MAKKOptions
                    {
                        Id = k++,
                        Description = item.NameEn,
                        IsChecked = false,
                    });
                }
            }
            return options;
        }

        /// <summary>
        /// Получить имя МАКК
        /// </summary>
        /// <returns></returns>
        private string GetName()
        {
            string l_nameMAKK = "";
            if (SelectedResults is null)
            {
                if (Results.Count > 0)
                {
                    l_nameMAKK = Results[0].Name;
                }
            }
            else
            {
                l_nameMAKK = SelectedResults.Name;
            }
            return l_nameMAKK;
        }

        /// <summary>
        /// Получить выходные параметры
        /// </summary>
        /// <returns></returns>
        private OutputDataCondensatorDTO GetOutputParams()
        {
            OutputDataCondensatorDTO outputParams = new OutputDataCondensatorDTO();
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

        /// <summary>
        /// Подбор вентиляторов
        /// </summary>
        /// <param name="inputData"></param>
        private void SelectionFans(IInputData inputData)
        {
            Fans.Clear();
            int n = 1; // кол-во вентиляторов
            IList<FanDTO> list = fanSelection.FanSearch(inputData.ValueAirFlowCX / n,
                (inputData.ValueAirFlowCX * (1 + inputData.FanReserveAirFlow / 100)) / n, inputData.ProjectsOutView[0].PresDropDry);
            if (list != null)
            {
                foreach (FanDTO fanDTO in list)
                {
                    Fans.Add(fanDTO);
                }
                if (Fans.Count > 0) SelectedFan = Fans[0];
            }
        }

        /// <summary>
        /// Подбор компрессоров
        /// </summary>
        private void SelectionCompressors()
        {
            if (!fLoad)
            {
                // холодопроизводительность 
                double totCap = GS.StringToDouble(inputData.ProjectsOutView[0].O_TotCap);

                // массовый расход хладогента
                double dxCxMas = GS.StringToDouble(inputData.ProjectsOutView[0].O_DxCxMas);

                // Хладогент
                string refr = inputData.Selected_I_RefT;

                // температура кипения
                double I_TEvap = inputDataCompressors.I_TEvap;
                // перегрев
                double I_TOvrH = inputDataCompressors.I_TOvrH;
                // температура конденсации
                double I_TCond = inputData.I_TCondCX;
                // переохлаждение
                double I_TSubC = inputData.I_TSubCCX;

                List<MAKKRefrCapacitys> capacitys = new List<MAKKRefrCapacitys>();
                MAKKCalcRC makkCalcRC = MAKKCalcRC.SuctGasTemp;
                int delta = 0;

                #region Компрессора InvoTech
                invoTech.LoadDll();
                CalcCompressorInvoTech(totCap, dxCxMas, I_TEvap, I_TOvrH, I_TSubC);
                #endregion

                #region Компрессора Select8
                //select8.LoadDll();
                //CalcCompressorSelect8(totCap, dxCxMas, I_TEvap, I_TOvrH, I_TSubC);
                #endregion

                inputData.I_TSucGasCX = inputDataCompressors.I_TSucGas;
                RecalcCondensator(dxCxMas, I_TCond);
                ////RecalcCondensatorRC(totCap, I_TCond);
                // холодопроизводительность конденсатора 
                totCap = GS.StringToDouble(inputData.ProjectsOutView[0].O_TotCap);
                CalcCompressorInvoTech(totCap, dxCxMas, I_TEvap, I_TOvrH, I_TSubC);
                //CalcCompressorSelect8(totCap, dxCxMas, I_TEvap, I_TOvrH, I_TSubC);
                bool breakF = false;
                int count = 0;
                bool suctGasTemp = false;
                bool subCool = false;
                double refrCapacity = 0;

                while (false)
                {

                    refrCapacity = GS.StringToDouble(Compressors[0].RefrigerationCapacity);
                    double deltaRefrCapacity = totCap - refrCapacity;

                    // выход из цикла while
                    if (Math.Abs(deltaRefrCapacity) < 0.1) break;

                    capacitys.Add(new MAKKRefrCapacitys
                    {
                        O_TotCap = totCap,
                        RefrigerationCapacity = refrCapacity,
                        Delta = deltaRefrCapacity,
                    });
                    if (deltaRefrCapacity < 0)
                    {
                        delta = -1;
                        switch (makkCalcRC)
                        {
                            case MAKKCalcRC.SuctGasTemp:
                                if ((inputData.I_TSucGasCX - inputData.I_TOvrHCX) == 0)
                                {
                                    suctGasTemp = true;
                                }
                                else
                                {
                                    inputData.I_TSucGasCX--;
                                }
                                makkCalcRC = MAKKCalcRC.SubCool;
                                break;
                            case MAKKCalcRC.SubCool:
                                if (inputData.I_TSubCCX == 0)
                                {
                                    subCool = true;
                                }
                                else
                                {
                                    inputData.I_TSubCCX--;
                                }
                                makkCalcRC = MAKKCalcRC.SuctGasTemp;
                                break;
                        }
                        if (suctGasTemp && subCool) break;
                    }

                    // массовый расход хладогента
                    dxCxMas = GS.StringToDouble(inputData.ProjectsOutView[0].O_DxCxMas);
                    // температура кипения
                    I_TEvap = inputDataCompressors.I_TEvap = inputData.I_TEvapCX;
                    // перегрев
                    I_TOvrH = inputDataCompressors.I_TOvrH;
                    // температура конденсации
                    I_TCond = inputData.I_TCondCX;
                    // переохлаждение
                    I_TSubC = inputData.I_TSubCCX;
                    //CalcCondensator();
                    // холодопроизводительность конденсатора 
                    totCap = GS.StringToDouble(inputData.ProjectsOutView[0].O_TotCap);
                    CalcCompressorInvoTech(totCap, dxCxMas, I_TEvap, I_TOvrH, I_TSubC);
                    CalcCondensator();
                    //RecalcCondensator(dxCxMas, I_TCond);
                    if (breakF) break;
                    count++;
                    //if (count == 1) break;
                }

                fLoad = true;
            }
        }

        private void CalcCompressorInvoTech(double totCap, double dxCxMas, double I_TEvap, double I_TOvrH, double I_TSubC)
        {
            invoTech.SetParams(I_TEvap, I_TOvrH, inputData.I_TCondCX, I_TSubC);
            (SelectCompressors optionalCompr, List<SelectCompressors> comprs) = invoTech.SelectCompessors(totCap, dxCxMas);
            Compressors.Clear();
            CompressorsOther.Clear();
            Compressors.Add(optionalCompr);
            foreach (var compr in comprs)
            {
                CompressorsOther.Add(compr);
            }
        }

        private void CalcCompressorSelect8(double totCap, double dxCxMas, double I_TEvap, double I_TOvrH, double I_TSubC)
        {
            (SelectCompressors optionalCompr, List<SelectCompressors> comprs) = select8.SelectCompessors(
                I_TEvap, I_TOvrH, inputData.I_TCondCX, I_TSubC, totCap, dxCxMas);
        }

        /// <summary>
        /// пересчёт параметров кронденсатора и испарителя для выбранного компрессора
        /// </summary>
        /// <param name="dxCxMas"></param>
        /// <param name="I_TCond"></param>
        private void RecalcCondensator(double dxCxMas, double I_TCond)
        {
            double masFlow = GS.StringToDouble(Compressors[Compressors.Count - 1].MassFlow);
            searchCompressors.Clear();
            searchCompressors.Add(
                new SearchCompressors
                {
                    MasFlowCond = dxCxMas,
                    MasFlowCompr = masFlow,
                    MasFlowDiff = dxCxMas - masFlow,
                    I_TCond = I_TCond,
                });
            if (Math.Abs(masFlow - dxCxMas) > MASS_FLOW_DIFF_RES)
            {
                if (masFlow < dxCxMas)
                {
                    UpdateCondensatorBelow(masFlow);
                }
                if (masFlow > dxCxMas)
                {
                    UpdateCondensatorAbowe(masFlow);
                }
            }

            void UpdateCondensatorBelow(double MasFlow)
            {
                double diff = 0;
                while (true)
                {
                    if (searchCompressors.Count > 0)
                    {
                        inputData.I_TCondCX--;
                        calculateService.CalcReverse(Calculation.TO.Main.Properties.Resources.PageCondensator_ReverseOut);
                        if (calculateService.GetResult() == 0)
                        {
                            // холодопроизводительность 
                            double totCap2 = GS.StringToDouble(inputData.ProjectsOutView[0].O_TotCap);
                            // массовый расход хладогента
                            double dxCxMas2 = GS.StringToDouble(inputData.ProjectsOutView[0].O_DxCxMas);

                            diff = dxCxMas2 - MasFlow;
                            searchCompressors.Add(new SearchCompressors
                            {
                                MasFlowCond = dxCxMas2,
                                MasFlowCompr = MasFlow,
                                MasFlowDiff = dxCxMas2 - MasFlow,
                                I_TCond = inputData.I_TCondCX,
                            });
                        }
                        if (diff < 0)
                        {
                            double a = searchCompressors[searchCompressors.Count - 2].MasFlowCond
                                - searchCompressors[searchCompressors.Count - 1].MasFlowCond;
                            double b = searchCompressors[searchCompressors.Count - 1].MasFlowCompr
                                - searchCompressors[searchCompressors.Count - 1].MasFlowCond;
                            double c = b / a;
                            inputData.I_TCondCX += c;
                            calculateService.CalcReverse(Calculation.TO.Main.Properties.Resources.PageCondensator_ReverseOut);
                            break;
                        }
                    }
                }
            }

            void UpdateCondensatorAbowe(double MasFlow)
            {
                double diff = 0;
                while (true)
                {
                    if (searchCompressors.Count > 0)
                    {
                        inputData.I_TCondCX++;
                        calculateService.CalcReverse(Calculation.TO.Main.Properties.Resources.PageCondensator_ReverseOut);
                        if (calculateService.GetResult() == 0)
                        {
                            // холодопроизводительность 
                            double totCap2 = GS.StringToDouble(inputData.ProjectsOutView[0].O_TotCap);
                            // массовый расход хладогента
                            double dxCxMas2 = GS.StringToDouble(inputData.ProjectsOutView[0].O_DxCxMas);

                            diff = dxCxMas2 - MasFlow;
                            searchCompressors.Add(
                                new SearchCompressors
                                {
                                    MasFlowCond = dxCxMas2,
                                    MasFlowCompr = MasFlow,
                                    MasFlowDiff = dxCxMas2 - MasFlow,
                                    I_TCond = inputData.I_TCondCX,
                                });
                        }
                        if (diff > 0)
                        {

                            double a = searchCompressors[searchCompressors.Count - 1].MasFlowCond
                                - searchCompressors[searchCompressors.Count - 2].MasFlowCond;
                            double b = searchCompressors[searchCompressors.Count - 1].MasFlowCond
                                - searchCompressors[searchCompressors.Count - 1].MasFlowCompr;
                            double c = b / a;
                            inputData.I_TCondCX -= c;
                            calculateService.CalcReverse(Calculation.TO.Main.Properties.Resources.PageCondensator_ReverseOut);
                            break;
                        }
                    }
                }
            }
        }
        private void CalcCondensator()
        {
            calculateService.CalcReverse(Calculation.TO.Main.Properties.Resources.PageCondensator_ReverseOut);
        }
        #endregion
    }
}
