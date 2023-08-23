using Veza.HeatExchanger.Exceptions;
using System.Collections.ObjectModel;
using Veza.HeatExchanger.Interfaces.Refrigerants;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Services;
using Veza.HeatExchanger.BusinessLogic.Compressors.Models;
using Veza.Calculation.TO.Main.Interfaces;
using System;
using System.Collections.Generic;

namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// класс с входными данными, которые вводятся на страницах
    /// </summary>
    public class InputData : IInputData
    {
        #region Свойства привязанные к Xaml

        /// <summary>
        /// Геометрия O_Code, пример 5012
        /// </summary>
        public ObservableCollection<string> Geometry { get; set; }
        private string selectGeometry;
        public string SelectGeometry
        {
            get
            {
                return selectGeometry;
            }

            set
            {
                selectGeometry = value;
                SetSelectGeometry(value);
            }
        }

        /// <summary>
        /// Размер и материал трубки 
        /// </summary>
        public ObservableCollection<string> Pipe { get; set; }
        private string selectedPipe;
        public string SelectedPipe
        {
            get
            {
                return selectedPipe;
            }
            set
            {
                selectedPipe = value;
                //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strTubeChanged + value); // Трубка изменена на - 
            }
        }

        /// <summary>
        /// Шаг оребрения
        /// </summary>
        public ObservableCollection<string> StepFin { get; set; }
        private string selectedStepFin;
        public string SelectedStepFin
        {
            get
            {
                return selectedStepFin;
            }
            set
            {
                selectedStepFin = value;
                //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strFinChanged + value);// Шаг оребрения изменен на -
            }
        }

        /// <summary>
        /// толщина оребрения и материал
        /// </summary>
        public ObservableCollection<string> Fin { get; set; }
        private string selectedFin;
        public string SelectedFin
        {
            get
            {
                return selectedFin;
            }
            set
            {
                selectedFin = value;
                //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strThicknessFinChanged + value); // "Толщина оребрения изменена на - "
            }
        }

        /// <summary>
        /// длина оребрения
        /// </summary>
        public double ValueWidthFin { get; set; }

        /// <summary>
        /// высота оребрения
        /// </summary>
        private double valueHightFin;
        private double setValueHightFin;
        private bool setValueHight;
        public double ValueHightFin
        {
            get
            {
                return valueHightFin;
            }
            set
            {
                valueHightFin = value;
                if (setValueHightFin == 0)
                {
                    setValueHightFin = value;
                }
                else
                {
                    if (!setValueHight)
                    {
                        setValueHightFin = value;
                        SetValueHeight(value);
                    }
                    else
                    {
                        string old;
                        // Меняет поле "I_HeightInt" через DevExpress
                        //SetPropertyCore<string>("ValueHightFin", value.ToString(), out old);
                    }
                }
                if(!isTubesN) SetTubesN();
            }
        }

        private int valueCircuits;
        private bool setValueCircuits;
        /// <summary>
        /// Кол-во отводов
        /// </summary>
        public int ValueCircuits
        {
            get => valueCircuits;
            set
            {
                valueCircuits = value;
                if (!setNumOfPasses)
                {
                    setValueCircuits = true;
                    NumOfPasses = Math.Round((double)((double)TubesN * ValuePipe / value), numDecimalPlaces);
                    setValueCircuits = false;
                }
            }
        }

        private bool setNumOfPasses;
        private double numOfPasses;
        /// <summary>
        /// Число ходов
        /// </summary>
        public double NumOfPasses
        {
            get => numOfPasses;
            set
            {
                numOfPasses = value;
                if (!setValueCircuits)
                {
                    setNumOfPasses = true;
                    ValueCircuits = (int)(TubesN * ValuePipe / value);
                    setNumOfPasses = false;
                }
            }
        }

        private int valuePipe;
        /// <summary>
        /// Число рядов
        /// </summary>
        public int ValuePipe 
        {
            get => valuePipe;
            set
            {
                valuePipe = value;
                NumOfPasses = Math.Round((double)((double)TubesN * value / valueCircuits), numDecimalPlaces);
            }
        }

        /// <summary>
        /// Расход воздуха
        /// </summary>
        public ObservableCollection<string> AirFlow { get; set; }
        public string SelectedAirFlow { get; set; }

        /// <summary>
        /// Теплоноситель
        /// </summary>
        public ObservableCollection<string> Fluid { get; set; }
        public string selectedFluid;
        public string SelectedFluid
        {
            get
            {
                return selectedFluid;
            }
            set
            {
                selectedFluid = value;
                SetSelectedFluid(value);
            }
        }
        /// <summary>
        /// Температура теплоносителя на входе
        /// </summary>
        public double ValueMedTempIn { get; set; }
        /// <summary>
        /// Максимальное падение давления жидкости
        /// </summary>
        public double ValueMedKPa { get; set; }

        /// <summary>
        /// Концентрация гликоля
        /// </summary>
        public double I_SoleCnz { get; set; }

        private bool isTubesN = false;
        /// <summary>
        /// Количество труб (вычисляемое поле)
        /// </summary>
        private int tubesN;
        public int TubesN
        {
            get
            {
                return tubesN;
            }
            set
            {
                isTubesN = true;
                tubesN = value;
                ValueHightFin = value * GetGeometryHeight();
                NumOfPasses = Math.Round((double)((double)valuePipe * value / valueCircuits), numDecimalPlaces);
                isTubesN = false;
            }
        }

        /// <summary>
        /// Доступность для ввода значения свойства "Концентрация гликоля"
        /// </summary>
        public bool EnableTextBoxSC { get; set; } = false;

        /// <summary>
        /// Выбор языка интерфейса программы
        /// </summary>
        public List<string> lang { get; set; }
        public ObservableCollection<string> bLanguge { get; set; } //"Русский";
        public string selectedLanguage;
        public string SelectedLanguage
        {
            get
            {
                return selectedLanguage;
            }
            set
            {
                selectedLanguage = value;
                SetSelectedLanguage(value);                
            }
        }

        /// <summary>
        /// Вариант расчёта - температура теплоносителя на выходе или расход теплоносителя
        /// </summary>
        public ObservableCollection<string> VariantRasch { get; set; }

        /// <summary>
        /// Температура теплоносителя на выходе
        /// </summary>
        public double ValueVariantRasch { get; set; }
        private string selectedVariantRasch;
        public string SelectedVariantRasch
        {
            get
            {
                return selectedVariantRasch;
            }
            set
            {
                selectedVariantRasch = value;
                //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strCalculationOption + value); // "Вариант расчёта(температура на выходе или расход жидкости) изменен на -
            }
        }

        /// <summary>
        /// Номер строки при табличном расчёте
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ComboBox - мощность или температура воздуха на выходе (для прямого расчёта)
        /// </summary>
        public ObservableCollection<string> Direct { get; set; }
        private string selectedDirect;
        public string SelectedDirect
        {
            get
            {
                return selectedDirect;
            }
            set
            {
                selectedDirect = value;
            }
        }
        // Рама, коллектор
        /// <summary>
        /// тип коллектора - CSHEET
        /// - Без
        /// - Одиночный вход воздуха
        /// - Одиночный выход воздуха
        /// </summary>
        public ObservableCollection<string> I_CSheet { get; set; }
        public string Selected_I_CSheet { get; set; }
        /// <summary>
        /// Коль-во коллекторов
        /// </summary>
        public string I_HdrQta { get; set; } = "0";
        /// <summary>
        /// материал коллектора
        /// </summary>
        public ObservableCollection<string> I_MatHdr { get; set; }
        public string Selected_I_MatHdr { get; set; }
        /// <summary>
        /// Припуск на обработку по длине покрытия трубопровода коллектора ( в мм)
        /// </summary>
        public int I_CSheetL { get; set; }
        /// <summary>
        /// Присоединительный размер на входе в дюймах
        /// </summary>
        public ObservableCollection<string> I_ConIn { get; set; }
        public string Selected_I_ConIn { get; set; }
        /// <summary>
        /// Присоединительный размер на выходе в дюймах
        /// </summary>
        public ObservableCollection<string> I_ConOut { get; set; }
        public string Selected_I_ConOut { get; set; }
        /// <summary>
        /// Тип патрубков
        /// </summary>
        public ObservableCollection<string> I_ConType { get; set; }
        public string Selected_I_ConType { get; set; }
        /// <summary>
        /// Исполнение корпуса
        /// 1 - Для установки в кондиционере типа ВЕРОСА 200, 251, 300, 700 (для пучка 50*25*12) и
        /// ВЕРОСА 200, 251, 300, 350, 500, 700 (для пучков 48*42*16 и 35*30*12);
        /// 2 - Для подсоединения к воздуховоду;
        /// 3 - Для установки в кондиционере типа ВЕРОСА 350 и 500 (стенки с четырьмя гибами - только пучок 50*25*12);
        /// 4 - Для установки в кондиционере типа Airmate
        /// 5 - Для установки в кондиционере типа КЦКП-Г (1, 2, 3, Н) типоразмеров 1,6...6,3 (стенки с двумя гибами - только пучок 50*25*12);
        /// 6 - Для кондиционеров ВЕРОСА-600; 
        /// 7 - Для установок БОКС, ТОРС (горизонтальное расположение);
        /// 9 - По индивидуальному заказу.
        /// </summary>
        public ObservableCollection<string> CasingType { get; set; }
        public string SelectedCasingType { get; set; }
        /// <summary>
        /// Материал корпуса
        /// </summary>
        public ObservableCollection<string> I_CasMat { get; set; }
        public string Selected_I_CasMat { get; set; }
        /// <summary>
        /// Ориентация теплообменника 
        /// </summary>
        public ObservableCollection<string> AirFlowDirection { get; set; }
        public string SelectedAirFlowDirection { get; set; }
        /// <summary>
        /// подключение теплоносителя 
        /// </summary>
        public ObservableCollection<string> ConnectingCoolant { get; set; }
        public string SelectedConnectingCoolant { get; set; }
        /// <summary>
        /// ориентация патрубков
        /// </summary>
        public ObservableCollection<string> I_CDir { get; set; }
        public string Select_I_CDir { get; set; }
        /// <summary>
        /// Конструкция/Подключение змеевика I_Esapo
        /// </summary>
        public ObservableCollection<string> Esapo { get; set; }
        public string SelectedEsapo { get; set; }
        /// <summary>
        /// Конструкция/Подключение змеевика
        /// </summary>
        public string I_Esapo { get; set; } = "V1";
        /// <summary>
        /// Таблица вывода результатов расчёта
        /// </summary>
        public OutView ProjectsOut { get; set; } = new OutView();
        /// <summary>
        /// ProjectsOutView нужен для вывода в Page -> ListView данных из ProjectsOut
        /// </summary>
        public ObservableCollection<OutView> ProjectsOutView { get; set; }
        /// <summary>
        /// Значение мощности для прямого расчёта по массовому расходу
        /// </summary>
        public double CapacityMassFlow { get; set; }

        /// <summary>
        /// Прямой расчёт по массову расходу ?
        /// true - да
        /// </summary>
        public bool IsMassFlowCalc { get; set; }

        public string I_MatFins { get; set; }

        public string I_MatRows { get; set; }
        public string I_Uneven { get; set; }

        public bool Equipment { get; set; } = false;



        #region Воздухонагреватель    HW
        public bool InitHW { get; set; }
        /// <summary>
        /// Мощность Воздухонагреватель
        /// </summary>
        public double ValueCapacityHW { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
        public double DirectAirTempOutHW { get; set; } = 24;
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double ValueAirFlowHW { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInHW { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double ValueBaseHumHW { get; set; }

        /// <summary>
        /// Температура теплоносителя на входе
        /// </summary>
        public double ValueMedTempInHW { get; set; }
        /// <summary>
        /// Расход теплоносителя
        /// </summary>
        public double I_MedFlowHW { get; set; } = 2.8;
        /// <summary>
        /// Температура теплоносителя на выходе
        /// </summary>
        public double ValueVariantRaschHW { get; set; }
        /// <summary>
        /// Максимальное падение давления жидкости
        /// </summary>
        public double ValueMedKPaHW { get; set; }

        #endregion


        #region Воздухоохладитель     CW

        public bool InitCW { get; set; }
        /// <summary>
        /// Мощность Воздухоохладитель
        /// </summary>
        public double ValueCapacityCW { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
        public double DirectAirTempOutCW { get; set; } = 20;
        public double ValueAirFlowCW { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInCW { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public double I_AirTempOutCW { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double ValueBaseHumCW { get; set; }
        /// <summary>
        /// Температура теплоносителя на входе
        /// </summary>
        public double ValueMedTempInCW { get; set; }
        /// <summary>
        /// Расход теплоносителя
        /// </summary>
        public double I_MedFlowCW { get; set; } = 2.8;
        /// <summary>
        /// Температура теплоносителя на выходе
        /// </summary>
        public double ValueVariantRaschCW { get; set; }
        /// <summary>
        /// Максимальное падение давления жидкости
        /// </summary>
        public double ValueMedKPaCW { get; set; }
        #endregion


        #region Паровой испаритель    ST
        public bool InitST { get; set; }
        /// <summary>
        /// Давление пара
        /// </summary>
        public double I_StmBar { get; set; }

        /// <summary>
        /// Температура пара
        /// </summary>
        public double I_StmTemp { get; set; }
        /// <summary>
        /// Мощность Паровой испаритель
        /// </summary>
        public double ValueCapacityST { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double ValueAirFlowST { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
        public double DirectAirTempOutST { get; set; } = 24;
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInST { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public double I_AirTempOutST { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double ValueBaseHumST { get; set; }
        #endregion

        #region Конденсатор           CX    
        /// <summary>
        /// Тип хладагента
        /// </summary>
        public ObservableCollection<string> I_RefT { get; set; }
        private string selected_I_RefT;
        public string Selected_I_RefT
        {
            get
            {
                return selected_I_RefT;
            }
            set
            {
                selected_I_RefT = value;
                if (value != null)
                {
                    SetSelected_I_RefT(value);
                }
            }
        }

        /// <summary>
        /// Коэффициент загрязнения трубы
        /// </summary>
        public ObservableCollection<string> I_FoulingI { get; set; }
        private string selected_I_FoulingI;
        public string Selected_I_FoulingI
        {
            get
            {
                return selected_I_FoulingI;
            }
            set
            {
                selected_I_FoulingI = value;
                SetSelected_I_FoulingI(value);
            }
        }

        public string I_FoulingIP { get; set; }


        public bool InitCX { get; set; }
        /// <summary>
        /// Мощность Конденсатор
        /// </summary>
        public double ValueCapacityCX { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
        public double DirectAirTempOutCX { get; set; } = 24;
        public double ValueAirFlowCX { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInCX { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public double I_AirTempOutCX { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double ValueBaseHumCX { get; set; }

        #region температкра кипения
        /// <summary>
        /// Температура кипениия
        /// </summary>
        public double I_TEvapCX { get; set; }
        public ObservableCollection<string> EvaporatingTemperature { get; set; }
        private string selectEvapTemp;
        public string SelectEvapTemp
        {
            get => selectEvapTemp;
            set
            {
                selectEvapTemp = value;
                try
                {
                    SetSelectEvapTemp(value);
                }
                catch (TempToPresException ex)
                {
                    OutParams outParams = new OutParams
                    {
                        ErrorStr = ex.Message,
                    };
                    //_messageBus.SendTo<PageErrorViewModel>(new OutMessage(outParams));
                    //_pageService.ChangePage(new PageError());
                }
            }
        }
        /// <summary>
        /// давление кипения
        /// </summary>
        private double evapAbsPresCX;
        public double EvapAbsPresCX
        {
            get
            {
                return evapAbsPresCX;
            }
            set
            {
                evapAbsPresCX = value;
                try
                {
                    I_TEvapCX = _convertTempToPres.ToTemperature(evapAbsPresCX, Selected_I_RefT);
                }
                catch (TempToPresException ex)
                {
                    OutParams outParams = new OutParams
                    {
                        ErrorStr = ex.Message,
                    };
                    //_messageBus.SendTo<PageErrorViewModel>(new OutMessage(outParams));
                    //_pageService.ChangePage(new PageError());
                }
            }
        }
        /// <summary>
        /// Видимость слова "°С" для температуры кипения
        /// </summary>
        public Visibility? VisibilityEvapTemp { get; set; } = Visibility.Visible;
        /// <summary>
        /// Видимость слова "бар" для давления кипения
        /// </summary>
        public Visibility? VisibilityEvapPres { get; set; } = Visibility.Hidden;
        #endregion

        #region перегрев
        /// <summary>
        /// Перегрев
        /// </summary>
        public double I_TOvrHCX { get; set; }
        public ObservableCollection<string> SuctOvrheat { get; set; }
        private string selectSuctOvrheat;
        public string SelectSuctOvrheat
        {
            get => selectSuctOvrheat;
            set
            {
                selectSuctOvrheat = value;
                SetSelectSuctOvrheat(value);
            }
        }
        private double suctGasReturnCX;
        public double SuctGasReturnCX
        {
            get
            {
                return suctGasReturnCX;
            }
            set
            {
                suctGasReturnCX = value;
                I_TOvrHCX = suctGasReturnCX - I_TEvapCX;
            }
        }
        /// <summary>
        /// Видимость слова "К" для перегрева всас. газа
        /// </summary>
        public Visibility? VisibilityOverH { get; set; } = Visibility.Visible;
        /// <summary>
        /// Видимость слова "°С" для температуры всас газа
        /// </summary>
        public Visibility? VisibilitySuctGas { get; set; } = Visibility.Hidden;
        #endregion

        #region температура конденсации
        private double i_TCondCX;
        /// <summary>
        /// Температура конденсации
        /// </summary>
        public double I_TCondCX 
        {
            get => i_TCondCX;
            set
            {
                i_TCondCX = value;
                if (Equipment)
                {
                    if (!ExternSet)
                    {
                        _inputDataCompressors.SetI_TCond(value);
                    }
                }
            }
        }
        public ObservableCollection<string> CondensingTemperature { get; set; }
        /// <summary>
        /// Давление конденсации
        /// </summary>
        private string selectCondTemp;
        public string SelectCondTemp
        {
            get => selectCondTemp;
            set
            {
                selectCondTemp = value;
                try
                {
                    SetSelectCondTemp(value);
                }
                catch (TempToPresException ex)
                {
                    OutParams outParams = new OutParams
                    {
                        ErrorStr = ex.Message,
                    };
                    //_messageBus.SendTo<PageErrorViewModel>(new OutMessage(outParams));
                    //_pageService.ChangePage(new PageError());
                }
            }
        }
        /// <summary>
        /// давление конденскации
        /// </summary>
        private double condAbsPresCX;
        public double CondAbsPresCX
        {
            get
            {
                return condAbsPresCX;
            }
            set
            {
                condAbsPresCX = value;
                try
                {
                    I_TCondCX = _convertTempToPres.ToCondTemperature(condAbsPresCX, Selected_I_RefT);
                }
                catch (TempToPresException ex)
                {
                    OutParams outParams = new OutParams
                    {
                        ErrorStr = ex.Message,
                    };
                    //_messageBus.SendTo<PageErrorViewModel>(new OutMessage(outParams));
                    //_pageService.ChangePage(new PageError());
                }
            }
        }
        /// <summary>
        /// Видимость слова "°С" для температуры конденсации
        /// </summary>
        public Visibility? VisibilityCondTemp { get; set; } = Visibility.Visible;
        /// <summary>
        /// Видимость слова "бар" для давления конденсации
        /// </summary>
        public Visibility? VisibilityCondPres { get; set; } = Visibility.Hidden;
        #endregion

        #region Переохлаждение
        private double i_TSubCCX;
        
           
        /// <summary>
        /// Переохлаждение
        /// </summary>
        public double I_TSubCCX 
        {
            get => i_TSubCCX;
            set
            { 
                i_TSubCCX = value;
                if (Equipment)
                {
                    if (!ExternSet)
                    {
                        _inputDataCompressors.SetI_TSubC(value);
                    }
                }
            } 
        }
        public ObservableCollection<string> SubCooling { get; set; }
        private string selectSubCool;
        public string SelectSubCool
        {
            get => selectSubCool;
            set
            {
                selectSubCool = value;
                try
                {
                    SetSelectSubCooling(value);
                }
                catch (TempToPresException ex)
                {
                    OutParams outParams = new OutParams
                    {
                        ErrorStr = ex.Message,
                    };
                    //_messageBus.SendTo<PageErrorViewModel>(new OutMessage(outParams));
                    //_pageService.ChangePage(new PageError());
                }
            }
        }
        /// <summary>
        /// температура жидкости
        /// </summary>
        private double liquidTempCX;
        public double LiquidTempCX
        {
            get
            {
                return liquidTempCX;
            }
            set
            {
                liquidTempCX = value;
                try
                {
                    I_TSubCCX = _convertTempToPres.ToSubColTemperature(I_TCondCX, liquidTempCX, Selected_I_RefT);
                }
                catch (TempToPresException ex)
                {
                    OutParams outParams = new OutParams
                    {
                        ErrorStr = ex.Message,
                    };
                    //_messageBus.SendTo<PageErrorViewModel>(new OutMessage(outParams));
                    //_pageService.ChangePage(new PageError());
                }
            }
        }
        /// <summary>
        /// Видимость слова "K" для переохлаждения
        /// </summary>
        public Visibility? VisibilitySubCool { get; set; } = Visibility.Visible;
        /// <summary>
        /// Видимость слова "°С" для температуры жидкости
        /// </summary>
        public Visibility? VisibilityLiquidTemp { get; set; } = Visibility.Hidden;
        #endregion

        /// <summary>
        /// Максимальное падение давления хладагента
        /// </summary>
        public double I_TMaxPCX { get; set; }

        /// <summary>
        /// Температура горячего газа
        /// </summary>
        public double I_THotGasCX { get; set; }

        /// <summary>
        /// Температура всасываемого газа
        /// </summary>
        public double I_TSucGasCX { get; set; }

        /// <summary>
        /// Выбор размерности "кг/ч" или "г/с" при прямом расчёте по массовому расхлду
        /// </summary>
        public ObservableCollection<string> DirectMassFlowUnits { get; set; }
        public string selectDirectMassFlowUnits;
        public string SelectDirectMassFlowUnits
        {
            get => selectDirectMassFlowUnits;
            set
            {
                selectDirectMassFlowUnits = value;
                SetSelectDirectMassFlowUnits(value);
            }
        }
        /// <summary>
        /// вариант "кг/ч"
        /// </summary>
        public Visibility? VisibilityDirectMassFlowKgH { get; set; } = Visibility.Visible;
        /// <summary>
        /// вариант "г/с"
        /// </summary>
        public Visibility? VisibilityDirectMassFlowGS { get; set; } = Visibility.Hidden;

        /// <summary>
        /// Прямой расчёт по массовому расходу
        /// </summary>
        public double DirectMassFlowCX { get; set; } = 1000;

        /// <summary>
        /// Значение мощности для прямого расчёта по массовому расходу
        /// </summary>
        public double CapacityMassFlowCX { get; set; }
        #endregion

        #region  Испаритель            DX  
        public bool InitDX { get; set; }
        /// <summary>
        /// Количество контуров хладагента
        /// </summary>
        public int I_RCircDX { get; set; }
        /// <summary>
        /// Пропускная способность байпаса
        /// </summary>
        public double I_DxBypDX { get; set; }

        /// <summary>
        /// Прямой расчёт по массовому расходу
        /// </summary>
        public double DirectMassFlowDX { get; set; } = 1000;

        /// <summary>
        /// Максимальное падение давления хладагента
        /// </summary>
        public double I_TMaxPDX { get; set; }

        /// <summary>
        /// Температура горячего газа
        /// </summary>
        public double I_THotGasDX { get; set; }

        /// <summary>
        /// Температура кипениия
        /// </summary>
        public double I_TEvapDX { get; set; }

        /// <summary>
        /// давление кипения
        /// </summary>
        private double evapAbsPresDX;
        public double EvapAbsPresDX
        {
            get
            {
                return evapAbsPresDX;
            }
            set
            {
                evapAbsPresDX = value;
                try
                {
                    I_TEvapDX = _convertTempToPres.ToTemperature(evapAbsPresDX, Selected_I_RefT);
                }
                catch (TempToPresException ex)
                {
                    OutParams outParams = new OutParams
                    {
                        ErrorStr = ex.Message,
                    };
                    //_messageBus.SendTo<PageErrorViewModel>(new OutMessage(outParams));
                    //_pageService.ChangePage(new PageError());
                }
            }
        }

        /// <summary>
        /// Перегрев
        /// </summary>
        public double I_TOvrHDX { get; set; }

        private double suctGasReturnDX;
        public double SuctGasReturnDX
        {
            get
            {
                return suctGasReturnDX;
            }
            set
            {
                suctGasReturnDX = value;
                I_TOvrHDX = suctGasReturnDX - I_TEvapDX;
            }
        }

        /// <summary>
        /// Температура конденсации
        /// </summary>
        public double I_TCondDX { get; set; }

        /// <summary>
        /// давление конденскации
        /// </summary>
        private double condAbsPresDX;
        public double CondAbsPresDX
        {
            get
            {
                return condAbsPresDX;
            }
            set
            {
                condAbsPresDX = value;
                try
                {
                    I_TCondDX = _convertTempToPres.ToCondTemperature(condAbsPresDX, Selected_I_RefT);
                }
                catch (TempToPresException ex)
                {
                    OutParams outParams = new OutParams
                    {
                        ErrorStr = ex.Message,
                    };
                    //_messageBus.SendTo<PageErrorViewModel>(new OutMessage(outParams));
                    //_pageService.ChangePage(new PageError());
                }
            }
        }

        /// <summary>
        /// Переохлаждение
        /// </summary>
        public double I_TSubCDX { get; set; }
        /// <summary>
        /// температура жидкости
        /// </summary>
        private double liquidTempDX;
        public double LiquidTempDX
        {
            get
            {
                return liquidTempDX;
            }
            set
            {
                liquidTempDX = value;
                try
                {
                    I_TSubCDX = _convertTempToPres.ToSubColTemperature(I_TCondDX, liquidTempDX, Selected_I_RefT);
                }
                catch (TempToPresException ex)
                {
                    OutParams outParams = new OutParams
                    {
                        ErrorStr = ex.Message,
                    };
                    //_messageBus.SendTo<PageErrorViewModel>(new OutMessage(outParams));
                    //_pageService.ChangePage(new PageError());
                }
            }
        }
        /// <summary>
        /// Температура всасываемого газа
        /// </summary>
        public double I_TSucGasDX { get; set; }

        /// <summary>
        /// Мощность
        /// </summary>
        public double ValueCapacityDX { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
        public double DirectAirTempOutDX { get; set; } = 24;
        public double ValueAirFlowDX { get; set; }

        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInDX { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double ValueBaseHumDX { get; set; }

        #endregion

        #region МАКК
        /// <summary>
        /// запас по вентилятору - 0 %
        /// </summary>
        public double FanReserveAirFlow { get; set; } = 0;

        public int NumberOfFans { get; set; } = 1;
        #endregion

        #endregion

        #region Свойства открытые

        /// <summary>
        /// был ли расчёт воздухонагревателя
        /// если нет, устанавливаются первоначальные настройки для воздухонагревателя
        /// </summary>
        public bool CalcFluidHeater { get; set; }

        /// <summary>
        /// был ли расчёт воздухоохладителя
        /// если нет, устанавливаются первоначальные настройки для воздухоохладителя
        /// </summary>
        public bool CalcFluidCooler { get; set; }

        /// <summary>
        /// был ли расчёт парового нагревателя
        /// если нет, устанавливаются первоначальные настройки для парового нагревателя
        /// </summary>
        public bool CalcSteamHeater { get; set; }

        /// <summary>
        /// был ли расчёт испарителя
        /// если нет, устанавливаются первоначальные настройки для испарителя
        /// </summary>
        public bool CalcEvaporater { get; set; }

        /// <summary>
        /// был ли расчёт конденсатор
        /// если нет, устанавливаются первоначальные настройки для конденсатора
        /// </summary>
        public bool CalcCondensator { get; set; }

        #endregion

        #region Входные данные по умолчанию в Dll

        /// <summary>
        /// Влажность окружающего воздуха
        /// </summary>
        public string I_BaseHum { get; set; } = "15";
        /// <summary>
        /// Режим расчёта влажности
        /// </summary>
        public string I_BaseMode { get; set; } = "1";
        /// <summary>
        /// Высота над уровнем моря
        /// </summary>
        public string I_BaseSea { get; set; } = "0";
        /// <summary>
        /// Исходная температура
        /// </summary>
        public string I_BaseTemp { get; set; } = "20";
        /// <summary>
        /// Изоляция корпуса
        /// </summary>
        public string I_CasIso { get; set; } = "0";
        /// <summary>
        /// Катодное покрытие - окраска окунанием
        /// </summary>
        public string I_CatCoat { get; set; } = "0";
        /// <summary>
        /// Крышка отсека трубопровода коллектора и U-образного трубного колена
        /// </summary>
        public string I_CBox { get; set; } = "0";
        /// <summary>
        /// Сертифицированный
        /// </summary>
        public string I_CertMode { get; set; } = "0";
        /// <summary>
        /// Качество корпуса каплеуловителя
        /// </summary>
        public string I_ElimMat { get; set; } = "0";
        /// <summary>
        /// Тип каплеуловителя
        /// </summary>
        public string I_ElimTp { get; set; } = "0";

        /// <summary>
        /// Режим работоспособности
        /// </summary>
        public string I_EtaMax { get; set; } = "0";
        /// <summary>
        /// Звуконепроницаемое помещение для вентиляторов
        /// </summary>
        public string I_FanPlen { get; set; } = "0";
        /// <summary>
        /// Тип рамы
        /// </summary>
        public string I_FrameTp { get; set; } = "0";
        /// <summary>
        /// Использование полной высоты теплообменника
        /// </summary>
        public string I_FullH { get; set; } = "1";
        /// <summary>
        /// Геометрия
        /// </summary>
        public string I_Geometry { get; set; } = "0";
        /// <summary>
        /// Коррекция перепада давления воздуха
        /// </summary>
        public string I_KorrADP { get; set; } = "0";
        /// <summary>
        /// Коррекция перепада среднего давления
        /// </summary>
        public string I_KorrKDP { get; set; } = "0";
        /// <summary>
        /// Максимальный шаг оребрения
        /// </summary>
        public string I_LamAbsMax { get; set; } = "4";
        /// <summary>
        /// Качество рамы
        /// </summary>
        public string I_MatFrame { get; set; } = "7";
        /// <summary>
        /// Качество рядов
        /// </summary>
        public string I_Mode { get; set; } = "0";
        /// <summary>
        /// Качество дренажного поддона
        /// </summary>
        public string I_PanMat { get; set; } = "0";
        /// <summary>
        /// Тип дренажного поддона
        /// </summary>
        public string I_PanTp { get; set; } = "0";
        /// <summary>
        /// Переход на сечение круглой формы
        /// </summary>
        public string I_RCon { get; set; } = "0";
        /// <summary>
        /// Удельный вес (гликоль)
        /// </summary>
        public string I_SoleGew { get; set; } = "0";
        /// <summary>
        /// Теплопроводность гликоля
        /// </summary>
        public string I_SoleLeit { get; set; } = "0";
        /// <summary>
        /// Температура замерзания
        /// </summary>
        public string I_SoleTFrz { get; set; } = "0";
        /// <summary>
        /// Вязкость Гликоля
        /// </summary>
        public string I_SoleVisk { get; set; } = "0";
        /// <summary>
        /// Теплопередача гликоля
        /// </summary>
        public string I_SoleWaer { get; set; } = "0";
        /// <summary>
        /// Специальный припой
        /// </summary>
        public string I_SpcSolder { get; set; } = "0";
        /// <summary>
        /// Раздельные теплообменники
        /// </summary>
        public string I_SplitC { get; set; } = "0";
        /// <summary>
        /// Тип расчета
        /// </summary>
        public string I_Type { get; set; } = "1";
        /// <summary>
        /// Вентиляционное отверстие/канал
        /// </summary>
        public string I_VentD { get; set; } = "0";

        #endregion

        #region Внутренние поля и переменные
        /// <summary>
        /// Изэнтропийная эффективность
        /// </summary>
        public string I_IseEta { get; set; }
        protected static ILogs _logs { get; set; }
        protected static ISwitchLanguageService _switchLanguageService;
        protected static IConvertTempToPres _convertTempToPres;
        /// <summary>
        /// класс, отправляющий сообщения другим страницам
        /// </summary>
        protected static MessageBus _messageBus;

        private static InputDataCompressors _inputDataCompressors;

        /// <summary>
        /// количество знаков после запятой для NumOfPasses (число ходов)
        /// </summary>
        private const int numDecimalPlaces = 2;
        

        private static bool ExternSet = false;

        #endregion

        #region Конструктор
        public InputData(ILogs logs, ISwitchLanguageService switchLanguageService, IConvertTempToPres ConvertTempToPres, 
            MessageBus messageBus)
        {
            _logs = logs;
            _switchLanguageService = switchLanguageService;
            _convertTempToPres = ConvertTempToPres;
            _messageBus = messageBus;
        }
        
        #endregion

        #region Публичные методы
        
        /// <summary>
        /// присваивает ссылку на данные компрессора
        /// через DI будет циклическая ссылка
        /// </summary>
        /// <param name="inputDataCompressors"></param>
        public void SetInputDataCompressors(InputDataCompressors inputDataCompressors)
        {
            _inputDataCompressors = inputDataCompressors;
        }

        public void SetI_TCond(double value)
        {
            ExternSet = true;
            I_TCondCX = value;
            ExternSet = false;
        }

        public void SetI_TSubC(double value)
        {
            ExternSet = true;
            I_TSubCCX = value;
            ExternSet = false;
        }
        #endregion

        #region Методы, вызываются при обновлении свойств

        /// <summary>
        /// Установка параметров при изменении геометрии,
        /// вызывается при обновлении свойства SelectGeometry
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectGeometry(string value)
        {
            //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strGeometryСhanged + value); // "Геометрия изменена на -"
            if (value == Calculation.TO.Main.Properties.Resources._5012)
            {
                SetPipe5012();
            }
            if (value == Calculation.TO.Main.Properties.Resources._4816)
            {
                SetPipe4816();
            }
            if (value == Calculation.TO.Main.Properties.Resources._3512)
            {
                SetPipe3512();
            }
            if (value == Calculation.TO.Main.Properties.Resources._2510)
            {
                SetPipe2510();
            }
            if (value == Calculation.TO.Main.Properties.Resources._2507)
            {
                SetPipe2507();
            }

            // типоразмер 5012
            void SetPipe5012()
            {
                if (Pipe != null)
                {
                    Pipe.Clear();
                    Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu);
                    Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu);
                    //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0.35CuG);
                    SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
                }

                if (Fin != null)
                {
                    Fin.Clear();
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_12mmAl);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                    //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                    //Fin.Add(Calculation.TO.Main.Properties.Resources.0.25mmAl);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                    SelectedFin = Calculation.TO.Main.Properties.Resources._0_12mmAl;
                }
                if (StepFin != null)
                {
                    if (!StepFin.Contains(Calculation.TO.Main.Properties.Resources._7_0mm))
                        StepFin.Add(Calculation.TO.Main.Properties.Resources._7_0mm);
                    if (!StepFin.Contains(Calculation.TO.Main.Properties.Resources._12_0mm))
                        StepFin.Add(Calculation.TO.Main.Properties.Resources._12_0mm);
                    if (!StepFin.Contains(Calculation.TO.Main.Properties.Resources._1_8mm))
                        StepFin.Insert(0, Calculation.TO.Main.Properties.Resources._1_8mm);
                }
                TubesN = (int)(setValueHightFin / 50);
                setValueHight = true;
                ValueHightFin = TubesN * 50;
                setValueHight = false;
                CasingType.Clear();
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_1);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_2);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_3);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_4);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_5);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_6);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_7);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_9);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_10);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_11);
                SelectedCasingType = Calculation.TO.Main.Properties.Resources.CasingType_1;
            }

            void SetPipe4816()
            {
                Pipe.Clear();
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_0_40Cu);
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_0_50Cu);
                //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_0.70G);
                //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_1.00G);
                SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe16_0_40Cu;

                Fin.Clear();
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mm304);
               // Fin.Add(Calculation.TO.Main.Properties.Resources.0.25mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                SelectedFin = Calculation.TO.Main.Properties.Resources._0_15mmAl;
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._12_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._12_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._7_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._7_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._1_8mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._1_8mm);
                TubesN = (int)(setValueHightFin / 48);
                setValueHight = true;
                ValueHightFin = TubesN * 48;
                setValueHight = false;
                CasingType.Clear();
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_1_4835);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_2);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_4);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_6);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_7);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_9);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_10);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_11);
                SelectedCasingType = Calculation.TO.Main.Properties.Resources.CasingType_1_4835;
            }

            void SetPipe3512()
            {
                Pipe.Clear();
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu);
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu);
                //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0.35CuG);
                SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;

                Fin.Clear();
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_12mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.25mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                SelectedFin = Calculation.TO.Main.Properties.Resources._0_12mmAl;
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._12_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._12_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._7_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._7_0mm);
                if (!StepFin.Contains(Calculation.TO.Main.Properties.Resources._1_8mm))
                    StepFin.Insert(0, Calculation.TO.Main.Properties.Resources._1_8mm);
                TubesN = (int)(setValueHightFin / 35);
                setValueHight = true;
                ValueHightFin = TubesN * 35;
                setValueHight = false;
                CasingType.Clear();
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_1_4835);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_2);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_4);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_6);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_7);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_9);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_10);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_11);
                SelectedCasingType = Calculation.TO.Main.Properties.Resources.CasingType_1_4835;
            }

            void SetPipe2510()
            {
                Pipe.Clear();
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe9_0_30Cu);
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe9_0_50Cu);
                //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe9_0.30CuG);
                SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe9_0_30Cu;

                Fin.Clear();
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_10mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                SelectedFin = Calculation.TO.Main.Properties.Resources._0_10mmAl;
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._12_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._12_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._7_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._7_0mm);
                if (!StepFin.Contains(Calculation.TO.Main.Properties.Resources._1_8mm))
                    StepFin.Insert(0, Calculation.TO.Main.Properties.Resources._1_8mm);
                TubesN = (int)(setValueHightFin / 25);
                setValueHight = true;
                ValueHightFin = TubesN * 25;
                setValueHight = false;
                CasingType.Clear();
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_2);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_4);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_6);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_7);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_9);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_10);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_11);
                SelectedCasingType = Calculation.TO.Main.Properties.Resources.CasingType_2;

            }

            void SetPipe2507()
            {
                Pipe.Clear();
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe7_0_28Cu);
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe7_0_40Cu);
                //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe7_0.28CuG);
                SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe7_0_28Cu;

                Fin.Clear();
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_10mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.15mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmEpoxy);
                SelectedFin = Calculation.TO.Main.Properties.Resources._0_10mmAl;
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._12_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._12_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._7_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._7_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._5_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._5_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._4_5mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._4_5mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._4_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._4_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._3_5mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._3_5mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._3_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._3_0mm);
                if (!StepFin.Contains(Calculation.TO.Main.Properties.Resources._1_8mm))
                    StepFin.Insert(0, Calculation.TO.Main.Properties.Resources._1_8mm);
                TubesN = (int)(setValueHightFin / 25);
                setValueHight = true;
                ValueHightFin = TubesN * 25;
                setValueHight = false;
                CasingType.Clear();
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_2);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_4);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_6);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_7);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_9);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_10);
                CasingType.Add(Calculation.TO.Main.Properties.Resources.CasingType_11);
                SelectedCasingType = Calculation.TO.Main.Properties.Resources.CasingType_2;
            }
        }

        /// <summary>
        /// установка видимости ввода "концентрации гликоля",
        /// вызывается при обновлении свойства SelectedFluid
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectedFluid(string value)
        {
            //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strCoolantChanged + value); // Теплоноситель изменен на - 
            if (value == Calculation.TO.Main.Properties.Resources.FluidW100)
            {
                EnableTextBoxSC = false;
                I_SoleCnz = 0;
            }
            if (value == Calculation.TO.Main.Properties.Resources.FluidEthyleneGlycol)
            {
                EnableTextBoxSC = true;
            }
            if (value == Calculation.TO.Main.Properties.Resources.FluidPropyleneGlycol)
            {
                EnableTextBoxSC = true;
            }
        }

        /// <summary>
        /// Установка параметра "число труб",
        /// вызывается при обновлении свойства ValueHightFin
        /// </summary>
        private void SetTubesN()
        {
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._5012)
            {
                TubesN = (int)(ValueHightFin / 50);
            }
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._4816)
            {
                TubesN = (int)(ValueHightFin / 48);
            }
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._3512)
            {
                TubesN = (int)(ValueHightFin / 35);
            }
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._2510)
            {
                TubesN = (int)(ValueHightFin / 25);
            }
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._2507)
            {
                TubesN = (int)(ValueHightFin / 25);
            }
        }

        /// <summary>
        /// Переключение языка,
        /// вызывается при изменении свойства SelectedLanguage
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectedLanguage(string value)
        {
            //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strLangСhanged + value); // "Язык интерфейса изменен на -
            string culture = "en";
            //init = false;
            if (value == lang[1])
            {
                culture = "ru-RU";
            }
            _switchLanguageService.SwitchLanguage(culture);
        }

        /// <summary>
        /// Тип хладогента,
        /// вызывается при изменении свойства Selected_I_RefT
        /// </summary>
        /// <param name="value"></param>
        private void SetSelected_I_RefT(string value)
        {
            //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strСhanged_I_RefT + value);
            // Вызывается перерасчёт температуры кипениия или давление
            if (VisibilityEvapTemp == Visibility.Visible)  // если активно "температура кипения"
            {
                // Вызывается перерасчёт давления кипениия 
                EvapAbsPresDX = _convertTempToPres.ToPressure(I_TEvapDX, value);
            }
            else
            {
                // Вызывается перерасчёт температуры кипениия 
                I_TEvapDX = _convertTempToPres.ToTemperature(EvapAbsPresDX, value);
            }

            // Вызывается перерасчёт перегрева или температура всасываемого газа
            if (VisibilityOverH == Visibility.Visible) // если активно "перегрев"
            {
                // Вызывается перерасчёт перегрева на температуру всасываемого газа
                SuctGasReturnDX = I_TOvrHDX + I_TEvapDX;
            }
            else
            {
                // Вызывается перерасчёт температуры всасываемого газа на перегрев
                I_TOvrHDX = SuctGasReturnDX - I_TEvapDX;
            }

            // Вызывается перерасчёт температура конденсации или давление
            if (VisibilityCondTemp == Visibility.Visible) // если активно "температура конденсации"
            {
                // Вызывается перерасчёт давление конденсации 
                CondAbsPresDX = _convertTempToPres.ToCondPressure(I_TCondDX, Selected_I_RefT);
            }
            else
            {
                // Вызывается перерасчёт температуры конденсации
                I_TCondDX = _convertTempToPres.ToCondTemperature(CondAbsPresDX, Selected_I_RefT);
            }

            // Вызывается перерасчёт переохлаждения или температуры жидкости
            if (VisibilitySubCool == Visibility.Visible)  // если активно "переохлаждение"
            {
                // Вызывается перерасчёт температуры жидкости
                LiquidTempDX = _convertTempToPres.ToSubCol(I_TCondDX, I_TSubCDX, Selected_I_RefT);
            }
            else
            {
                // Вызывается перерасчёт переохлаждения
                I_TSubCDX = _convertTempToPres.ToSubColTemperature(I_TCondDX, LiquidTempDX, Selected_I_RefT);
            }
        }

        /// <summary>
        /// Коэффициент загрязнения трубы,
        /// вызывается при изменении свойства Selected_I_FoulingI
        /// </summary>
        /// <param name="value"></param>
        private void SetSelected_I_FoulingI(string value)
        {
            //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.strСhanged_I_FoulingI + value);
        }

        /// <summary>
        /// Вызывается при изменении значении свойства "I_HeightInt"
        /// корректирует это значение под кол-во труб
        /// </summary>
        /// <param name="value"></param>
        private void SetValueHeight(double value)
        {
            int k = GetGeometryHeight();
            if (k > 0)
            {
                int tubesN = (int)(setValueHightFin / k);
                value = tubesN * k;
                string old;
                // Меняет поле "I_HeightInt" через DevExpress
                //SetPropertyCore<string>("ValueHightFin", value.ToString(), out old);
                valueHightFin = value;
            }
        }

        /// <summary>
        /// Получение высоты из геометрии
        /// </summary>
        /// <returns></returns>
        private int GetGeometryHeight()
        {
            int k = 0;
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._5012)
            {
                k = 50;
            }
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._4816)
            {
                k = 48;
            }
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._3512)
            {
                k = 35;
            }
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._2510)
            {
                k = 25;
            }
            if (SelectGeometry == Calculation.TO.Main.Properties.Resources._2507)
            {
                k = 25;
            }
            return k;
        }

        /// <summary>
        /// Вызывается при переключении температура кипениия на давление
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectEvapTemp(string value)
        {
            //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.SetSelectEvapTemp + value);
            if (Selected_I_RefT == null) return;
            if (value == Calculation.TO.Main.Properties.Resources.EvaporationTemperature)
            {
                VisibilityEvapTemp = Visibility.Visible;
                VisibilityEvapPres = Visibility.Hidden;
                I_TEvapDX = _convertTempToPres.ToTemperature(EvapAbsPresDX, Selected_I_RefT);
            }
            if (value == Calculation.TO.Main.Properties.Resources.EvaporationAbsPres)
            {
                VisibilityEvapTemp = Visibility.Hidden;
                VisibilityEvapPres = Visibility.Visible;
                EvapAbsPresDX = _convertTempToPres.ToPressure(I_TEvapDX, Selected_I_RefT);
            }
        }

        /// <summary>
        /// Вызывается при переключении перегрева на температуру всасываемого газа
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectSuctOvrheat(string value)
        {
            //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.SetSelectSuctOvrheat + value);
            if (Selected_I_RefT == null) return;
            if (value == Calculation.TO.Main.Properties.Resources.Overheating)
            {
                VisibilityOverH = Visibility.Visible;
                VisibilitySuctGas = Visibility.Hidden;
                I_TOvrHDX = SuctGasReturnDX - I_TEvapDX;
            }
            if (value == Calculation.TO.Main.Properties.Resources.SuctGasReturn)
            {
                VisibilityOverH = Visibility.Hidden;
                VisibilitySuctGas = Visibility.Visible;
                SuctGasReturnDX = I_TOvrHDX + I_TEvapDX;
            }
        }

        /// <summary>
        /// Вызывается при переключении температура конденсации на давление
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectCondTemp(string value)
        {
            //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.SetSelectCondTemp + value);
            if (Selected_I_RefT == null) return;
            if (value == Calculation.TO.Main.Properties.Resources.CondensingTemperature)
            {
                VisibilityCondTemp = Visibility.Visible;
                VisibilityCondPres = Visibility.Hidden;
                I_TCondDX = _convertTempToPres.ToCondTemperature(CondAbsPresDX, Selected_I_RefT);
            }
            if (value == Calculation.TO.Main.Properties.Resources.AbsCondensingPressure)
            {
                VisibilityCondTemp = Visibility.Hidden;
                VisibilityCondPres = Visibility.Visible;
                CondAbsPresDX = _convertTempToPres.ToCondPressure(I_TCondDX, Selected_I_RefT);
            }
        }

        /// <summary>
        /// Вызывается при переключении переохлаждения на температуру жидкости
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectSubCooling(string value)
        {
            //_logs.Logger.Info(Calculation.TO.Main.Properties.Resources.SetSelectSubCooling + value);
            if (Selected_I_RefT == null) return;
            if (value == Calculation.TO.Main.Properties.Resources.SubCooling)
            {
                VisibilitySubCool = Visibility.Visible;
                VisibilityLiquidTemp = Visibility.Hidden;
                I_TSubCDX = _convertTempToPres.ToSubColTemperature(I_TCondDX, LiquidTempDX, Selected_I_RefT);
            }
            if (value == Calculation.TO.Main.Properties.Resources.LiquidTemp)
            {
                VisibilitySubCool = Visibility.Hidden;
                VisibilityLiquidTemp = Visibility.Visible;
                LiquidTempDX = _convertTempToPres.ToSubCol(I_TCondDX, I_TSubCDX, Selected_I_RefT);
            }
        }

        /// <summary>
        /// вызывается при смене размерности "кг/ч" или "г/с" при прямом расчёте по массовому расхлду
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectDirectMassFlowUnits(string value)
        {
            if (value == Calculation.TO.Main.Properties.Resources.kgh)
            {
                VisibilityDirectMassFlowKgH = Visibility.Visible;
                VisibilityDirectMassFlowGS = Visibility.Hidden;
            }
            if (value == Calculation.TO.Main.Properties.Resources.gs)
            {
                VisibilityDirectMassFlowKgH = Visibility.Hidden;
                VisibilityDirectMassFlowGS = Visibility.Visible;
            }
        }
        #endregion
    }
}
