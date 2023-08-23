using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veza.HeatExchanger.BusinessLogic.Compressors.Models;
using Veza.HeatExchanger.Exceptions;
using Veza.HeatExchanger.Interfaces.Refrigerants;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Services;

namespace Veza.Calculation.TO.Main.Interfaces
{
    public interface IInputData
    {
        #region Свойства привязанные к Xaml

        /// <summary>
        /// Геометрия O_Code, пример 5012
        /// </summary>
        ObservableCollection<string> Geometry { get; set; }
        string SelectGeometry { get; set; }        

        /// <summary>
        /// Размер и материал трубки 
        /// </summary>
          ObservableCollection<string> Pipe { get; set; }
          string SelectedPipe { get; set; }

        /// <summary>
        /// Шаг оребрения
        /// </summary>
          ObservableCollection<string> StepFin { get; set; }
          string SelectedStepFin { get; set; }

        /// <summary>
        /// толщина оребрения и материал
        /// </summary>
          ObservableCollection<string> Fin { get; set; }
          string SelectedFin { get; set; }

        /// <summary>
        /// длина оребрения
        /// </summary>
          double ValueWidthFin { get; set; }

        /// <summary>
        /// высота оребрения
        /// </summary>
          double ValueHightFin { get; set; }

        /// <summary>
        /// Кол-во отводов
        /// </summary>
          int ValueCircuits { get; set; }
        /// <summary>
        /// Число ходов
        /// </summary>
          double NumOfPasses { get; set; }
        /// <summary>
        /// Число рядов
        /// </summary>
          int ValuePipe { get; set; }

        /// <summary>
        /// Расход воздуха
        /// </summary>
          ObservableCollection<string> AirFlow { get; set; }
          string SelectedAirFlow { get; set; }

        /// <summary>
        /// Теплоноситель
        /// </summary>
          ObservableCollection<string> Fluid { get; set; }
          string SelectedFluid { get; set; }
        /// <summary>
        /// Температура теплоносителя на входе
        /// </summary>
          double ValueMedTempIn { get; set; }
        /// <summary>
        /// Максимальное падение давления жидкости
        /// </summary>
          double ValueMedKPa { get; set; }

        /// <summary>
        /// Концентрация гликоля
        /// </summary>
          double I_SoleCnz { get; set; }

        /// <summary>
        /// Количество труб (вычисляемое поле)
        /// </summary>
          int TubesN { get; set; }

        /// <summary>
        /// Доступность для ввода значения свойства "Концентрация гликоля"
        /// </summary>
          bool EnableTextBoxSC { get; set; } 

        /// <summary>
        /// Выбор языка интерфейса программы
        /// </summary>
          List<string> lang { get; set; }
          ObservableCollection<string> bLanguge { get; set; } //"Русский";
          string SelectedLanguage { get; set; }

        /// <summary>
        /// Вариант расчёта - температура теплоносителя на выходе или расход теплоносителя
        /// </summary>
          ObservableCollection<string> VariantRasch { get; set; }

        /// <summary>
        /// Температура теплоносителя на выходе
        /// </summary>
          double ValueVariantRasch { get; set; }
          string SelectedVariantRasch { get; set; }

        /// <summary>
        /// Номер строки при табличном расчёте
        /// </summary>
          int Id { get; set; }

        /// <summary>
        /// ComboBox - мощность или температура воздуха на выходе (для прямого расчёта)
        /// </summary>
          ObservableCollection<string> Direct { get; set; }
          string SelectedDirect { get; set; }
        // Рама, коллектор
        /// <summary>
        /// тип коллектора - CSHEET
        /// - Без
        /// - Одиночный вход воздуха
        /// - Одиночный выход воздуха
        /// </summary>
          ObservableCollection<string> I_CSheet { get; set; }
          string Selected_I_CSheet { get; set; }
        /// <summary>
        /// Коль-во коллекторов
        /// </summary>
          string I_HdrQta { get; set; } 
        /// <summary>
        /// материал коллектора
        /// </summary>
          ObservableCollection<string> I_MatHdr { get; set; }
          string Selected_I_MatHdr { get; set; }
        /// <summary>
        /// Припуск на обработку по длине покрытия трубопровода коллектора ( в мм)
        /// </summary>
          int I_CSheetL { get; set; }
        /// <summary>
        /// Присоединительный размер на входе в дюймах
        /// </summary>
          ObservableCollection<string> I_ConIn { get; set; }
          string Selected_I_ConIn { get; set; }
        /// <summary>
        /// Присоединительный размер на выходе в дюймах
        /// </summary>
          ObservableCollection<string> I_ConOut { get; set; }
          string Selected_I_ConOut { get; set; }
        /// <summary>
        /// Тип патрубков
        /// </summary>
          ObservableCollection<string> I_ConType { get; set; }
          string Selected_I_ConType { get; set; }
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
          ObservableCollection<string> CasingType { get; set; }
          string SelectedCasingType { get; set; }
        /// <summary>
        /// Материал корпуса
        /// </summary>
          ObservableCollection<string> I_CasMat { get; set; }
          string Selected_I_CasMat { get; set; }
        /// <summary>
        /// Ориентация теплообменника 
        /// </summary>
          ObservableCollection<string> AirFlowDirection { get; set; }
          string SelectedAirFlowDirection { get; set; }
        /// <summary>
        /// подключение теплоносителя 
        /// </summary>
          ObservableCollection<string> ConnectingCoolant { get; set; }
          string SelectedConnectingCoolant { get; set; }
        /// <summary>
        /// ориентация патрубков
        /// </summary>
          ObservableCollection<string> I_CDir { get; set; }
          string Select_I_CDir { get; set; }
        /// <summary>
        /// Конструкция/Подключение змеевика I_Esapo
        /// </summary>
          ObservableCollection<string> Esapo { get; set; }
          string SelectedEsapo { get; set; }
        /// <summary>
        /// Конструкция/Подключение змеевика
        /// </summary>
          string I_Esapo { get; set; } 
        /// <summary>
        /// Таблица вывода результатов расчёта
        /// </summary>
          OutView ProjectsOut { get; set; }
        /// <summary>
        /// ProjectsOutView нужен для вывода в Page -> ListView данных из ProjectsOut
        /// </summary>
          ObservableCollection<OutView> ProjectsOutView { get; set; }
        /// <summary>
        /// Значение мощности для прямого расчёта по массовому расходу
        /// </summary>
          double CapacityMassFlow { get; set; }

        /// <summary>
        /// Прямой расчёт по массову расходу ?
        /// true - да
        /// </summary>
          bool IsMassFlowCalc { get; set; }

          string I_MatFins { get; set; }

          string I_MatRows { get; set; }
          string I_Uneven { get; set; }

          bool Equipment { get; set; } 



        #region Воздухонагреватель    HW
          bool InitHW { get; set; }
        /// <summary>
        /// Мощность Воздухонагреватель
        /// </summary>
          double ValueCapacityHW { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
          double DirectAirTempOutHW { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
          double ValueAirFlowHW { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
          double I_AirTempInHW { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
          double ValueBaseHumHW { get; set; }

        /// <summary>
        /// Температура теплоносителя на входе
        /// </summary>
          double ValueMedTempInHW { get; set; }
        /// <summary>
        /// Расход теплоносителя
        /// </summary>
          double I_MedFlowHW { get; set; } 
        /// <summary>
        /// Температура теплоносителя на выходе
        /// </summary>
          double ValueVariantRaschHW { get; set; }
        /// <summary>
        /// Максимальное падение давления жидкости
        /// </summary>
          double ValueMedKPaHW { get; set; }

        #endregion


        #region Воздухоохладитель     CW

          bool InitCW { get; set; }
        /// <summary>
        /// Мощность Воздухоохладитель
        /// </summary>
          double ValueCapacityCW { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
          double DirectAirTempOutCW { get; set; } 
          double ValueAirFlowCW { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
          double I_AirTempInCW { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
          double I_AirTempOutCW { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
          double ValueBaseHumCW { get; set; }
        /// <summary>
        /// Температура теплоносителя на входе
        /// </summary>
          double ValueMedTempInCW { get; set; }
        /// <summary>
        /// Расход теплоносителя
        /// </summary>
          double I_MedFlowCW { get; set; } 
        /// <summary>
        /// Температура теплоносителя на выходе
        /// </summary>
          double ValueVariantRaschCW { get; set; }
        /// <summary>
        /// Максимальное падение давления жидкости
        /// </summary>
          double ValueMedKPaCW { get; set; }
        #endregion


        #region Паровой испаритель    ST
          bool InitST { get; set; }
        /// <summary>
        /// Давление пара
        /// </summary>
          double I_StmBar { get; set; }

        /// <summary>
        /// Температура пара
        /// </summary>
          double I_StmTemp { get; set; }
        /// <summary>
        /// Мощность Паровой испаритель
        /// </summary>
          double ValueCapacityST { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
          double ValueAirFlowST { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
          double DirectAirTempOutST { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
          double I_AirTempInST { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
          double I_AirTempOutST { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
          double ValueBaseHumST { get; set; }
        #endregion

        #region Конденсатор           CX    
        /// <summary>
        /// Тип хладагента
        /// </summary>
          ObservableCollection<string> I_RefT { get; set; }
          string Selected_I_RefT { get; set; }

        /// <summary>
        /// Коэффициент загрязнения трубы
        /// </summary>
          ObservableCollection<string> I_FoulingI { get; set; }
          string Selected_I_FoulingI { get; set; }

          string I_FoulingIP { get; set; }


          bool InitCX { get; set; }
        /// <summary>
        /// Мощность Конденсатор
        /// </summary>
          double ValueCapacityCX { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
          double DirectAirTempOutCX { get; set; } 
          double ValueAirFlowCX { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
          double I_AirTempInCX { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
          double I_AirTempOutCX { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
          double ValueBaseHumCX { get; set; }

        #region температкра кипения
        /// <summary>
        /// Температура кипениия
        /// </summary>
          double I_TEvapCX { get; set; }
          ObservableCollection<string> EvaporatingTemperature { get; set; }
          string SelectEvapTemp { get; set; }
        /// <summary>
        /// давление кипения
        /// </summary>
          double EvapAbsPresCX { get; set; }
        /// <summary>
        /// Видимость слова "°С" для температуры кипения
        /// </summary>
          Visibility? VisibilityEvapTemp { get; set; } 
        /// <summary>
        /// Видимость слова "бар" для давления кипения
        /// </summary>
          Visibility? VisibilityEvapPres { get; set; } 
        #endregion

        #region перегрев
        /// <summary>
        /// Перегрев
        /// </summary>
          double I_TOvrHCX { get; set; }
          ObservableCollection<string> SuctOvrheat { get; set; }
          string SelectSuctOvrheat { get; set; }
          double SuctGasReturnCX { get; set; }
        /// <summary>
        /// Видимость слова "К" для перегрева всас. газа
        /// </summary>
          Visibility? VisibilityOverH { get; set; } 
        /// <summary>
        /// Видимость слова "°С" для температуры всас газа
        /// </summary>
          Visibility? VisibilitySuctGas { get; set; } 
        #endregion

        #region температура конденсации
        /// <summary>
        /// Температура конденсации
        /// </summary>
          double I_TCondCX { get; set; }
          ObservableCollection<string> CondensingTemperature { get; set; }
        /// <summary>
        /// Давление конденсации
        /// </summary>
          string SelectCondTemp { get; set; }
        /// <summary>
        /// давление конденскации
        /// </summary>
          double CondAbsPresCX { get; set; }
        /// <summary>
        /// Видимость слова "°С" для температуры конденсации
        /// </summary>
          Visibility? VisibilityCondTemp { get; set; }
        /// <summary>
        /// Видимость слова "бар" для давления конденсации
        /// </summary>
          Visibility? VisibilityCondPres { get; set; } 
        #endregion

        #region Переохлаждение


        /// <summary>
        /// Переохлаждение
        /// </summary>
          double I_TSubCCX { get; set; }
          ObservableCollection<string> SubCooling { get; set; }
          string SelectSubCool { get; set; }
        /// <summary>
        /// температура жидкости
        /// </summary>
          double LiquidTempCX { get; set; }
        /// <summary>
        /// Видимость слова "K" для переохлаждения
        /// </summary>
          Visibility? VisibilitySubCool { get; set; } 
        /// <summary>
        /// Видимость слова "°С" для температуры жидкости
        /// </summary>
          Visibility? VisibilityLiquidTemp { get; set; } 
        #endregion

        /// <summary>
        /// Максимальное падение давления хладагента
        /// </summary>
          double I_TMaxPCX { get; set; }

        /// <summary>
        /// Температура горячего газа
        /// </summary>
          double I_THotGasCX { get; set; }

        /// <summary>
        /// Температура всасываемого газа
        /// </summary>
          double I_TSucGasCX { get; set; }

        /// <summary>
        /// Выбор размерности "кг/ч" или "г/с" при прямом расчёте по массовому расхлду
        /// </summary>
          ObservableCollection<string> DirectMassFlowUnits { get; set; }
          string SelectDirectMassFlowUnits { get; set; }
        /// <summary>
        /// вариант "кг/ч"
        /// </summary>
          Visibility? VisibilityDirectMassFlowKgH { get; set; } 
        /// <summary>
        /// вариант "г/с"
        /// </summary>
          Visibility? VisibilityDirectMassFlowGS { get; set; } 

        /// <summary>
        /// Прямой расчёт по массовому расходу
        /// </summary>
          double DirectMassFlowCX { get; set; } 

        /// <summary>
        /// Значение мощности для прямого расчёта по массовому расходу
        /// </summary>
          double CapacityMassFlowCX { get; set; }
        #endregion

        #region  Испаритель            DX  
          bool InitDX { get; set; }
        /// <summary>
        /// Количество контуров хладагента
        /// </summary>
          int I_RCircDX { get; set; }
        /// <summary>
        /// Пропускная способность байпаса
        /// </summary>
          double I_DxBypDX { get; set; }

        /// <summary>
        /// Прямой расчёт по массовому расходу
        /// </summary>
          double DirectMassFlowDX { get; set; } 

        /// <summary>
        /// Максимальное падение давления хладагента
        /// </summary>
          double I_TMaxPDX { get; set; }

        /// <summary>
        /// Температура горячего газа
        /// </summary>
          double I_THotGasDX { get; set; }

        /// <summary>
        /// Температура кипениия
        /// </summary>
          double I_TEvapDX { get; set; }

        /// <summary>
        /// давление кипения
        /// </summary>
          double EvapAbsPresDX { get; set; }

        /// <summary>
        /// Перегрев
        /// </summary>
          double I_TOvrHDX { get; set; }

          double SuctGasReturnDX { get; set; }

        /// <summary>
        /// Температура конденсации
        /// </summary>
          double I_TCondDX { get; set; }

        /// <summary>
        /// давление конденскации
        /// </summary>
          double CondAbsPresDX { get; set; }

        /// <summary>
        /// Переохлаждение
        /// </summary>
          double I_TSubCDX { get; set; }
        /// <summary>
        /// температура жидкости
        /// </summary>
          double LiquidTempDX { get; set; }
        /// <summary>
        /// Температура всасываемого газа
        /// </summary>
          double I_TSucGasDX { get; set; }

        /// <summary>
        /// Мощность
        /// </summary>
          double ValueCapacityDX { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
          double DirectAirTempOutDX { get; set; } 
          double ValueAirFlowDX { get; set; }

        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
          double I_AirTempInDX { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
          double ValueBaseHumDX { get; set; }

        #endregion

        #region МАКК
        /// <summary>
        /// запас по вентилятору - 0 %
        /// </summary>
          double FanReserveAirFlow { get; set; } 

          int NumberOfFans { get; set; } 
        #endregion

        #endregion

        #region Свойства открытые

        /// <summary>
        /// был ли расчёт воздухонагревателя
        /// если нет, устанавливаются первоначальные настройки для воздухонагревателя
        /// </summary>
          bool CalcFluidHeater { get; set; }

        /// <summary>
        /// был ли расчёт воздухоохладителя
        /// если нет, устанавливаются первоначальные настройки для воздухоохладителя
        /// </summary>
          bool CalcFluidCooler { get; set; }

        /// <summary>
        /// был ли расчёт парового нагревателя
        /// если нет, устанавливаются первоначальные настройки для парового нагревателя
        /// </summary>
          bool CalcSteamHeater { get; set; }

        /// <summary>
        /// был ли расчёт испарителя
        /// если нет, устанавливаются первоначальные настройки для испарителя
        /// </summary>
          bool CalcEvaporater { get; set; }

        /// <summary>
        /// был ли расчёт конденсатор
        /// если нет, устанавливаются первоначальные настройки для конденсатора
        /// </summary>
          bool CalcCondensator { get; set; }

        #endregion

        #region Входные данные по умолчанию в Dll

        /// <summary>
        /// Влажность окружающего воздуха
        /// </summary>
          string I_BaseHum { get; set; } 
        /// <summary>
        /// Режим расчёта влажности
        /// </summary>
          string I_BaseMode { get; set; } 
        /// <summary>
        /// Высота над уровнем моря
        /// </summary>
          string I_BaseSea { get; set; } 
        /// <summary>
        /// Исходная температура
        /// </summary>
          string I_BaseTemp { get; set; } 
        /// <summary>
        /// Изоляция корпуса
        /// </summary>
          string I_CasIso { get; set; } 
        /// <summary>
        /// Катодное покрытие - окраска окунанием
        /// </summary>
          string I_CatCoat { get; set; } 
        /// <summary>
        /// Крышка отсека трубопровода коллектора и U-образного трубного колена
        /// </summary>
          string I_CBox { get; set; }
        /// <summary>
        /// Сертифицированный
        /// </summary>
          string I_CertMode { get; set; } 
        /// <summary>
        /// Качество корпуса каплеуловителя
        /// </summary>
          string I_ElimMat { get; set; } 
        /// <summary>
        /// Тип каплеуловителя
        /// </summary>
          string I_ElimTp { get; set; } 

        /// <summary>
        /// Режим работоспособности
        /// </summary>
          string I_EtaMax { get; set; } 
        /// <summary>
        /// Звуконепроницаемое помещение для вентиляторов
        /// </summary>
          string I_FanPlen { get; set; } 
        /// <summary>
        /// Тип рамы
        /// </summary>
          string I_FrameTp { get; set; } 
        /// <summary>
        /// Использование полной высоты теплообменника
        /// </summary>
          string I_FullH { get; set; }
        /// <summary>
        /// Геометрия
        /// </summary>
          string I_Geometry { get; set; } 
        /// <summary>
        /// Коррекция перепада давления воздуха
        /// </summary>
          string I_KorrADP { get; set; } 
        /// <summary>
        /// Коррекция перепада среднего давления
        /// </summary>
          string I_KorrKDP { get; set; } 
        /// <summary>
        /// Максимальный шаг оребрения
        /// </summary>
          string I_LamAbsMax { get; set; } 
        /// <summary>
        /// Качество рамы
        /// </summary>
          string I_MatFrame { get; set; } 
        /// <summary>
        /// Качество рядов
        /// </summary>
          string I_Mode { get; set; } 
        /// <summary>
        /// Качество дренажного поддона
        /// </summary>
          string I_PanMat { get; set; } 
        /// <summary>
        /// Тип дренажного поддона
        /// </summary>
          string I_PanTp { get; set; } 
        /// <summary>
        /// Переход на сечение круглой формы
        /// </summary>
          string I_RCon { get; set; } 
        /// <summary>
        /// Удельный вес (гликоль)
        /// </summary>
          string I_SoleGew { get; set; } 
        /// <summary>
        /// Теплопроводность гликоля
        /// </summary>
          string I_SoleLeit { get; set; } 
        /// <summary>
        /// Температура замерзания
        /// </summary>
          string I_SoleTFrz { get; set; } 
        /// <summary>
        /// Вязкость Гликоля
        /// </summary>
          string I_SoleVisk { get; set; } 
        /// <summary>
        /// Теплопередача гликоля
        /// </summary>
          string I_SoleWaer { get; set; } 
        /// <summary>
        /// Специальный припой
        /// </summary>
          string I_SpcSolder { get; set; }
        /// <summary>
        /// Раздельные теплообменники
        /// </summary>
          string I_SplitC { get; set; } 
        /// <summary>
        /// Тип расчета
        /// </summary>
          string I_Type { get; set; } 
        /// <summary>
        /// Вентиляционное отверстие/канал
        /// </summary>
          string I_VentD { get; set; }
        /// <summary>
        /// Изэнтропийная эффективность
        /// </summary>
        string I_IseEta { get; set; }
        #endregion

        #region Публичные методы

        /// <summary>
        /// присваивает ссылку на данные компрессора
        /// через DI будет циклическая ссылка
        /// </summary>
        /// <param name="inputDataCompressors"></param>
        void SetInputDataCompressors(InputDataCompressors inputDataCompressors);

        void SetI_TCond(double value);

        void SetI_TSubC(double value);
        #endregion
    }
}
