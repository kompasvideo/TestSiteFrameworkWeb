using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Veza.HeatExchanger.Services;

namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// хранит свойства входных и выходных данных программы
    /// </summary>
    sealed public class OutView 
    {
        #region Свойства привязанные к Xaml
        /// <summary>
        /// Код производственный
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Код производственный короткий для клиентов
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// был ли расчёт
        /// </summary>
        public bool Calc { get; set; }

        /// <summary>
        /// ошибка при расчёте
        /// </summary>
        public bool CalcError { get; set; }
        /// <summary>
        /// тип расчёта - прямой или обратный
        /// </summary>
        public CalcEnum CalcType { get; set; }
        /// <summary>
        /// Номер строки при табличном расчёте
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Ширина оребрения
        /// </summary>
        public double I_WidthInt { get; set; }

        private double valueHightFin;
        private double setValueHightFin;
        private bool setValueHight;
        /// <summary>
        /// Высота оребрения
        /// </summary>
        public double I_HeightInt
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
                        SetValueHight(value);
                    }
                    else
                    {
                        string old;
                        // Меняет поле "I_HeightInt" через DevExpress
                        //SetPropertyCore<string>("I_HeightInt", value.ToString(), out old);
                    }
                }
            }
        }
        /// <summary>
        /// Число рядов O_Rows
        /// </summary>
        public string NoOfRows { get; set; }

        /// <summary>
        /// кол-во отводов I_Circuits
        /// </summary>
        public string Circuits { get; set; }
        /// <summary>
        /// Шаг оребрения O_LamAbs
        /// </summary>
        public double FinSpace { get; set; }
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
                if (value != null)
                {
                    selectedStepFin = value;
                    FinSpace = GS.StringToDouble(value.Split(' ')[0]);
                    SetSelectStepFin(value);
                }
            }
        }

        /// <summary>
        /// скорость воздуха O_AirVelo
        /// </summary>
        public string AirVelocity { get; set; }
        /// <summary>
        /// Перепад давления (сухое состояние) O_AirPaT
        /// </summary>
        public string PresDropDry { get; set; }
        /// <summary>
        /// Резервная нагрузка O_LRes
        /// </summary>
        public string ReverseLoad { get; set; }
        /// <summary>
        /// Скорость жидкости O_MedVelo 
        /// </summary>
        public string MedVelo { get; set; }
        /// <summary>
        /// Перепад давления O_MedKPa
        /// </summary>
        public string MedKPa { get; set; }
        /// <summary>
        /// Температура воздуха на выходе O_AirTempOut
        /// </summary>
        public string AirTempOut { get; set; }
        /// <summary>
        /// типоразмер
        /// </summary>
        public int I_Geo { get; set; }
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
        /// Толщина оребрения
        /// </summary>
        public string I_FinThk { get; set; }
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
                SetSelectFin(value);
            }
        }

        /// <summary>
        /// Размеры трубки - толщина
        /// </summary>
        public string I_PipeThk { get; set; }

        /// <summary>
        /// Диаметр трубки
        /// </summary>
        public string I_Pipe { get; set; }
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
                SetSelectPipe(value);
            }
        }

        /// <summary>
        /// материал трубки
        /// </summary>
        public string I_MatRows { get; set; }
        /// <summary>
        /// материал оребрения
        /// </summary>
        public string I_MatFins { get; set; }
        /// <summary>
        ///  Мощность
        /// </summary>
        public string O_TotCap { get; set; }
        /// <summary>
        /// Давление окружающего воздуха
        /// </summary>
        public string I_BaseBar { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public string O_AirTempOutStr { get; set; }
        /// <summary>
        /// Абсолютная влажность воздуха
        /// </summary>
        public string O_AirHumOutAbs { get; set; }
        /// <summary>
        /// Скорость воздушного потока
        /// </summary>
        public string O_AirVelo { get; set; }
        /// <summary>
        /// Перепад давления (сухое состояние)
        /// </summary>
        public string O_AirPaT { get; set; }
        /// <summary>
        /// производственный код
        /// </summary>
        public string O_Code { get; set; }
        /// <summary>
        /// Температура жидкости на выходе
        /// </summary>
        public string O_MedTempOutStr { get; set; }
        /// <summary>
        /// Расход жидкости
        /// </summary>
        public string O_MedFlow { get; set; }
        /// <summary>
        /// Объём жидкости
        /// </summary>
        public string O_Volume { get; set; }
        /// <summary>
        /// Скорость жидкости
        /// </summary>
        public string O_MedVelo { get; set; }
        /// <summary>
        /// Перепад давления
        /// </summary>
        public string O_MedKPa { get; set; }
        /// <summary>
        /// Ширина оребрения
        /// </summary>
        public string I_WidthIntStr { get; set; }
        /// <summary>
        /// Высота оребрения
        /// </summary>
        public string I_HeightIntStr { get; set; }
        /// <summary>
        /// Жидкость
        /// </summary>
        public string I_MedTyp { get; set; }
        /// <summary>
        /// Концентрация гликоля
        /// </summary>
        public string I_SoleCnz { get; set; }
        /// <summary>
        ///  Мощность
        /// </summary>
        public string O_TotCapPrint { get; set; }
        /// <summary>
        /// Влажность воздуха на выходе
        /// </summary>
        public string O_AirHumOut { get; set; }
        /// <summary>
        /// Массовый расход
        /// </summary>
        public string O_Airkgs { get; set; }
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
        /// Пропускная способность O_KValue
        /// </summary>
        public string O_KValue { get; set; }
        /// <summary>
        /// Логарифмический перепад температур O_LogTDif
        /// </summary>
        public string O_LogTDif { get; set; }



        #region Воздухонагреватель

        /// <summary>
        /// Требуемая мощность
        /// </summary>
        public string I_CapacityHW { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double I_AirflowHW { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInHW { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public string I_AirTempOutHW { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double I_AirHumInHW { get; set; }

        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public string I_AirTempInStrHW { get; set; }
        /// <summary>
        /// Температура жидкости на входе
        /// </summary>
        public double I_MedTempInHW { get; set; }
        /// <summary>
        /// Температура жидкости на выходе
        /// </summary>
        public double I_MedTempOutHW { get; set; }
        /// <summary>
        /// Температура жидкости на входе
        /// </summary>
        public string I_MedTempInStrHW { get; set; }
        #endregion


        #region Воздухоохладитель

        /// <summary>
        /// Требуемая мощность
        /// </summary>
        public string I_CapacityCW { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double I_AirflowCW { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInCW { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public string I_AirTempOutCW { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double I_AirHumInCW { get; set; }

        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public string I_AirTempInStrCW { get; set; }
        /// <summary>
        /// Температура жидкости на входе
        /// </summary>
        public double I_MedTempInCW { get; set; }
        /// <summary>
        /// Температура жидкости на выходе
        /// </summary>
        public double I_MedTempOutCW { get; set; }
        /// <summary>
        /// Температура жидкости на входе
        /// </summary>
        public string I_MedTempInStrCW { get; set; }
        #endregion


        #region  Паровой нагреватель

        /// <summary>
        /// Давление пара
        /// </summary>
        public double O_StmBar { get; set; }
        /// <summary>
        /// Объём пара
        /// </summary>
        public string O_StmFlow { get; set; }
        /// <summary>
        /// Скрытая теплота пара
        /// </summary>
        public string O_StmHeat { get; set; }
        /// <summary>
        /// Скорость движения пара
        /// </summary>
        public string O_StmVelo { get; set; }
        /// <summary>
        /// Давление пара (применяется в табличном расчёте)
        /// </summary>
        public string I_StmBar { get; set; }
        /// <summary>
        /// Температура пара (применяется в табличном расчёте)
        /// </summary>
        public string I_StmTemp { get; set; }

        /// <summary>
        /// Требуемая мощность
        /// </summary>
        public string I_CapacityST { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double I_AirflowST { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInST { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public string I_AirTempOutST { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double I_AirHumInST { get; set; }

        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public string I_AirTempInStrST { get; set; }
        #endregion


        #region Конденсатор
        /// <summary>
        /// Температура конденсации
        /// </summary>
        public string I_TCondCX { get; set; }
        /// <summary>
        /// Переохлаждение
        /// </summary>
        public string I_TSubCCX { get; set; }

        /// <summary>
        /// Температура горячего газа
        /// </summary>
        public string I_THotGasCX { get; set; }
        /// <summary>
        /// Падение давления хладагента
        /// </summary>
        public string O_DxCxBar { get; set; }
        /// <summary>
        /// Падение давления хладагента
        /// </summary>
        public string O_DxCxK { get; set; }
        /// <summary>
        /// Массовый расход хладагента
        /// </summary>
        public string O_DxCxMas { get; set; }
        /// <summary>
        /// Давление хладагента
        /// </summary>
        public string O_DxCxPres { get; set; }
        /// <summary>
        /// Объем хладагента
        /// </summary>
        public string O_DxCxVol { get; set; }
        /// <summary>
        /// Температура горячего газа
        /// </summary>
        public string O_THotGas { get; set; }
        /// <summary>
        /// Перегревание
        /// </summary>
        public string O_TOvrH { get; set; }
        /// <summary>
        /// Количество контуров хладагента
        /// </summary>
        public string O_RCirc { get; set; }
        /// <summary>
        /// Удельный вес (гликоль)
        /// </summary>
        public string O_SoleGew { get; set; }
        /// <summary>
        /// Телпопроводность гликоля
        /// </summary>
        public string O_SoleLeit { get; set; }
        /// <summary>
        /// Температура замерзания
        /// </summary>
        public string O_SoleTFrz { get; set; }
        /// <summary>
        /// Вязкость гликоля
        /// </summary>
        public string O_SoleVisk { get; set; }
        /// <summary>
        /// Теплопередача гликоля
        /// </summary>
        public string O_SoleWaer { get; set; }
        /// <summary>
        /// Требуемая мощность
        /// </summary>
        public string I_CapacityCX { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double I_AirflowCX { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInCX { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public string I_AirTempInStrCX { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public string I_AirTempOutCX { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double I_AirHumInCX { get; set; }

        #endregion


        #region Испаритель
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInDX { get; set; }

        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public string I_AirTempInStrDX { get; set; }

        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double I_AirHumInDX { get; set; }
        /// <summary>
        /// Требуемая мощность
        /// </summary>
        public string I_CapacityDX { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double I_AirflowDX { get; set; }

        /// <summary>
        /// Температура конденсации
        /// </summary>
        public string I_TCondDX { get; set; }
        /// <summary>
        /// Температура кипения
        /// </summary>
        public string I_TEvapDX { get; set; }

        /// <summary>
        /// Переохлаждение
        /// </summary>
        public string I_TSubCDX { get; set; }
        /// <summary>
        /// Перегрев всас. газа
        /// </summary>
        public string I_TOvrHDX { get; set; }
        /// <summary>
        /// Размер / диаметр капилляра
        /// </summary>
        public string O_Capilar { get; set; }
        /// <summary>
        /// Длина капилляра
        /// </summary>
        public string O_CapLen { get; set; }
        /// <summary>
        /// Количество конденсата
        /// </summary>
        public string O_CondQta { get; set; }
        /// <summary>
        /// Распределитель хладагента
        /// </summary>
        public string O_DxDist { get; set; }
        /// <summary>
        /// Явная производительность/мощность
        /// </summary>
        public string O_SenCap { get; set; }
        /// <summary>
        /// Перепад давления (влажное состояние)
        /// </summary>
        public string O_AirPaF { get; set; }
        /// <summary>
        /// Отношение явной теплоты к общей
        /// </summary>
        public string O_SHR { get; set; }

        #endregion


        #region корпус и коллектор
        /// <summary>
        /// Присоединительный размер на входе 
        /// </summary>
        public string O_ConIN { get; set; }
        /// <summary>
        /// Присоединительный размер на выходе
        /// </summary>
        public string O_ConOut { get; set; }
        /// <summary>
        /// Общая глубина
        /// </summary>
        public string O_DepthExt { get; set; }
        /// <summary>
        /// Глубина оребрения
        /// </summary>
        public string O_DepthInt { get; set; }
        /// <summary>
        /// Размер рамы снизу
        /// </summary>
        public string O_FrameB { get; set; }
        /// <summary>
        /// Размер ребра жёсткости рамы
        /// </summary>
        public string O_FrameC { get; set; }
        /// <summary>
        /// Размер рамы на стороне трубопровода коллектора
        /// </summary>
        public string O_FrameL { get; set; }
        /// <summary>
        /// Размер рамы на стороне кривой
        /// </summary>
        public string O_FrameR { get; set; }
        /// <summary>
        /// Размер рамы сверху
        /// </summary>
        public string O_FrameT { get; set; }
        /// <summary>
        /// Толщина обшивки рамы
        /// </summary>
        public string O_FrameThk { get; set; }
        /// <summary>
        /// Общая высота
        /// </summary>
        public string O_HeightExt { get; set; }
        /// <summary>
        /// высота оребрения
        /// </summary>
        public string O_HeightInt { get; set; }
        /// <summary>
        /// Общая ширина
        /// </summary>
        public string O_WidthExt { get; set; }
        /// <summary>
        /// ширина оребрения
        /// </summary>
        public string O_WidthInt { get; set; }
        /// <summary>
        /// Рядов в высоту
        /// </summary>
        public string O_RowsH { get; set; }
        /// <summary>
        /// Коль-во  коллекторов
        /// </summary>
        public string O_HdrQta { get; set; }
        /// <summary>
        /// Материал коллектора
        /// </summary>
        public string O_MatHdr { get; set; }
        /// <summary>
        /// Припуск на обработку по длине коллектора
        /// </summary>
        public string I_CSheetL { get; set; }
        /// <summary>
        /// Тип патрубков
        /// </summary>
        public string I_ConType { get; set; }
        /// <summary>
        /// Исполнение корпуса
        /// </summary>
        public string CasingType { get; set; }
        /// <summary>
        /// Материал корпуса
        /// </summary>
        public string I_CasMat { get; set; }
        /// <summary>
        /// Ориентация теплообменника
        /// </summary>
        public string I_Esapo { get; set; }
        /// <summary>
        /// Ориентация патрубков
        /// </summary>
        public string I_CDir { get; set; }
        /// <summary>
        /// Направление потока воздуха
        /// </summary>
        public string AirFlowDirection { get; set; }
        /// <summary>
        /// Подключение теплоносителя
        /// </summary>
        public string ConnectingTheCoolant { get; set; }

        #endregion

        #endregion

        #region Внутренние поля и переменные
        private static TableData _tableData;
        #endregion

        #region Конструкторы
        public OutView(TableData tableData)
        {
            _tableData = tableData;
            SetFluidList();
            SetPipeList();
            SetFinList();
            SetStepFinList();
            SetGeometry();
        }

        public OutView()
        {
        }
        #endregion

        #region Методы открытые

        /// <summary>
        /// Полное копирование экземпляра класса с увеличение Id на 1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OutView Copy(int id)
        {
            OutView outView = new OutView();
            outView.Id = id;
            outView.I_WidthInt = I_WidthInt;
            outView.I_HeightInt = I_HeightInt;
            outView.Name = Name;
            outView.NoOfRows = NoOfRows;
            outView.Circuits = Circuits;
            outView.FinSpace = FinSpace;
            outView.I_Geo = I_Geo;
            outView.I_FinThk = I_FinThk;
            outView.I_PipeThk = I_PipeThk;
            outView.CalcType = CalcType;
            outView.I_SoleCnz = I_SoleCnz;
            outView.I_StmBar = I_StmBar;
            outView.I_StmTemp = I_StmTemp;


            outView.I_AirTempInHW = I_AirTempInHW;
            outView.I_AirHumInHW = I_AirHumInHW;
            outView.I_AirflowHW = I_AirflowHW;
            outView.I_MedTempInHW = I_MedTempInHW;
            outView.I_MedTempInStrHW = I_MedTempInStrHW;
            outView.I_MedTempOutHW = I_MedTempOutHW;

            outView.I_AirTempInCW = I_AirTempInCW;
            outView.I_AirHumInCW = I_AirHumInCW;
            outView.I_AirflowCW = I_AirflowCW;
            outView.I_MedTempInCW = I_MedTempInCW;
            outView.I_MedTempInStrCW = I_MedTempInStrCW;
            outView.I_MedTempOutCW = I_MedTempOutCW;

            outView.I_AirTempInST = I_AirTempInST;
            outView.I_AirHumInST = I_AirHumInST;
            outView.I_AirflowST = I_AirflowST;

            outView.I_AirTempInCX = I_AirTempInCX;
            outView.I_AirHumInCX = I_AirHumInCX;
            outView.I_AirflowCX = I_AirflowCX;
            outView.I_TCondCX = I_TCondCX;
            outView.I_TSubCCX = I_TSubCCX;
            outView.I_THotGasCX = I_THotGasCX;

            outView.I_AirTempInDX = I_AirTempInDX;
            outView.I_AirHumInDX = I_AirHumInDX;
            outView.I_AirflowDX = I_AirflowDX;
            outView.I_TCondDX = I_TCondDX;
            outView.I_TEvapDX = I_TEvapDX;
            outView.I_TSubCDX = I_TSubCDX;
            outView.I_TOvrHDX = I_TOvrHDX;

            outView.Fluid = new ObservableCollection<string>();
            outView.Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidW100);
            outView.Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidEthyleneGlycol);
            outView.Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidPropyleneGlycol);
            outView.SelectedFluid = SelectedFluid;

            outView.Geometry = new ObservableCollection<string>();
            outView.Geometry.Add(Calculation.TO.Main.Properties.Resources._5012);
            outView.Geometry.Add(Calculation.TO.Main.Properties.Resources._4816);
            outView.Geometry.Add(Calculation.TO.Main.Properties.Resources._3512);
            outView.Geometry.Add(Calculation.TO.Main.Properties.Resources._2510);
            outView.Geometry.Add(Calculation.TO.Main.Properties.Resources._2507);
            outView.Pipe = new ObservableCollection<string>();
            outView.Fin = new ObservableCollection<string>();
            outView.StepFin = StepFin;
            outView.SelectGeometry = SelectGeometry;
            switch (SelectGeometry)
            {
                case "5012":
                    outView.Pipe.Clear();
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu);
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu);
                    //outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0.35CuG);
                    outView.SelectedPipe = SelectedPipe;
                    outView.Fin.Clear();
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_12mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                    //outView.Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                    //outView.Fin.Add(Calculation.TO.Main.Properties.Resources.0.25mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                    outView.SelectedFin = SelectedFin;
                    break;
                case "4816":
                    outView.Pipe.Clear();
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_0_40Cu);
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_0_50Cu);
                    //outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_0.70G);
                    //outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_1.00G);
                    outView.SelectedPipe = SelectedPipe;
                    outView.Fin.Clear();
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                    //outView.Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                    //outView.Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mm304);
                    //outView.Fin.Add(Calculation.TO.Main.Properties.Resources.0.25mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                    outView.SelectedFin = SelectedFin;
                    break;
                case "3512":
                    outView.Pipe.Clear();
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu);
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu);
                    //outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0.35CuG);
                    outView.SelectedPipe = SelectedPipe;
                    outView.Fin.Clear();
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_12mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                    //outView.Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                    //outView.Fin.Add(Calculation.TO.Main.Properties.Resources.0.25mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                    outView.SelectedFin = SelectedFin;
                    break;
                case "2510":
                    outView.Pipe.Clear();
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe9_0_30Cu);
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe9_0_50Cu);
                    //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe9_0_30CuG);
                    outView.SelectedPipe = SelectedPipe;
                    outView.Fin.Clear();
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_10mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                    //outView.Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                    outView.SelectedFin = SelectedFin;
                    break;
                case "2507":
                    outView.Pipe.Clear();
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe7_0_28Cu);
                    outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe7_0_40Cu);
                    //outView.Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe7_0_28CuG);
                    outView.SelectedPipe = SelectedPipe;
                    outView.Fin.Clear();
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_10mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmAl);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                    outView.Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                    outView.SelectedFin = SelectedFin;
                    break;
            }
            outView.SelectedStepFin = SelectedStepFin;

            return outView;
        }

        public override string ToString()
        {
            return $"Calc={Calc};CalcType={CalcType};Id={Id};I_WidthInt={I_WidthInt};I_HeightInt={I_HeightInt};I_AirTempIn={I_AirTempInDX};" +
                $"I_AirTempIn={I_AirTempInDX};I_AirHumIn={I_AirHumInDX};I_Capacity={I_CapacityDX};I_Airflow={I_AirflowDX};I_MedTempInHW={I_MedTempInHW};" +
                $"I_MedTempOutHW={I_MedTempOutHW};Name={Name};NoOfRows={NoOfRows};Circuits={Circuits};FinSpace={FinSpace};" +
                $"AirVelocity={AirVelocity};PresDropDry={PresDropDry};ReverseLoad={ReverseLoad};MedVelo={MedVelo};MedKPa={MedKPa};" +
                $"AirTempOut={AirTempOut};I_Geo={I_Geo};I_FinThk={I_FinThk};I_PipeThk={I_PipeThk};I_Pipe={I_Pipe};I_MatRows={I_MatRows};" +
                $"I_MatFins={I_MatFins};O_TotCap={O_TotCapPrint};I_BaseBar={I_BaseBar};I_AirTempInStr={I_AirTempInStrDX};" +
                $"O_AirTempOutStr={O_AirTempOutStr};O_AirHumInAbs={O_AirHumOutAbs};O_AirVelo={O_AirVelo};O_AirPaT={O_AirPaT}" +
                $"O_Code={O_Code};I_MedFlow={O_MedFlow};+" +
                $"O_Volume={O_Volume};O_MedVelo={O_MedVelo};O_MedKPa={O_MedKPa};I_WidthIntStr={I_WidthIntStr};" +
                $"I_HeightIntStr={I_HeightIntStr};I_MedTyp={I_MedTyp};I_SoleCnz={I_SoleCnz}";
        }

        #endregion

        #region Методы внутренние

        /// <summary>
        /// Установка параметров при изменении геометрии,
        /// вызывается при обновлении свойства SelectGeometry
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectGeometry(string value)
        {
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
                    if (_tableData != null)
                    {
                        if (_tableData.I_PipeThk == null)
                        {
                            SelectedPipe = _tableData.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
                        }
                        else
                        {
                            SelectedPipe = _tableData.I_PipeThk = GetPipe( _tableData.I_PipeThk);
                        }
                    }
                    else
                    {
                        SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
                    }
                }
                if (Fin != null)
                {
                    Fin.Clear();
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_12mmAl);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                    //Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmAl);
                    //Fin.Add(Calculation.TO.Main.Properties.Resources._0_25mmAl);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                    if (_tableData != null)
                    {
                        if (_tableData.I_FinThk == null)
                        {
                            SelectedFin = _tableData.I_FinThk = Calculation.TO.Main.Properties.Resources._0_12mmAl;
                        }
                        else
                        {
                            SelectedFin = _tableData.I_FinThk = GetFin(_tableData.I_FinThk);
                        }
                    }
                    else
                    {
                        SelectedFin = Calculation.TO.Main.Properties.Resources._0_12mmAl;
                    }
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
                int tubesN = (int)(setValueHightFin / 50);
                setValueHight = true;
                I_HeightInt = tubesN * 50;
                setValueHight = false;
                I_Geo = 5012;
                _tableData.I_Geo = "5012";
            }

            void SetPipe4816()
            {
                if (Pipe != null)
                {
                    Pipe.Clear();
                    Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_0_40Cu);
                    Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_0_50Cu);
                    //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_0.70G);
                    //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe16_1.00G);
                    if (_tableData != null)
                    {
                        SelectedPipe = _tableData.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe16_0_40Cu;
                    }
                    else
                    {
                        SelectedPipe = _tableData.I_PipeThk = GetPipe( _tableData.I_PipeThk);
                    }
                }
                if (Fin != null)
                {
                    Fin.Clear();
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                    //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                    //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mm304);
                    //Fin.Add(Calculation.TO.Main.Properties.Resources.0.25mmAl);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                    Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                    if (_tableData != null)
                    {
                        if (_tableData.I_FinThk == null)
                        {
                            SelectedFin = _tableData.I_FinThk = Calculation.TO.Main.Properties.Resources._0_15mmAl;
                        }
                        else
                        {
                            SelectedFin = _tableData.I_FinThk = GetFin(_tableData.I_FinThk);
                        }
                    }
                    else
                    {
                        SelectedFin = Calculation.TO.Main.Properties.Resources._0_15mmAl;
                    }
                }
                if (StepFin != null)
                {
                    if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._12_0mm))
                        StepFin.Remove(Calculation.TO.Main.Properties.Resources._12_0mm);
                    if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._7_0mm))
                        StepFin.Remove(Calculation.TO.Main.Properties.Resources._7_0mm);
                    if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._1_8mm))
                        StepFin.Remove(Calculation.TO.Main.Properties.Resources._1_8mm);
                }
                int tubesN = (int)(setValueHightFin / 48);
                setValueHight = true;
                I_HeightInt = tubesN * 48;
                setValueHight = false;
                I_Geo = 4816;
                _tableData.I_Geo = "4816";
            }

            void SetPipe3512()
            {
                Pipe.Clear();
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu);
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu);
                //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0.35CuG);
                if (_tableData != null)
                {
                    if (_tableData.I_PipeThk == null)
                    {
                        SelectedPipe = _tableData.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
                    }
                    else
                    {
                        SelectedPipe = _tableData.I_PipeThk = GetPipe( _tableData.I_PipeThk);
                    }
                }
                else
                {
                    SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
                }

                Fin.Clear();
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_12mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.25mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                if (_tableData != null)
                {
                    if (_tableData.I_FinThk == null)
                    {
                        SelectedFin = _tableData.I_FinThk = Calculation.TO.Main.Properties.Resources._0_12mmAl;
                    }
                    else
                    {
                        SelectedFin = _tableData.I_FinThk = GetFin(_tableData.I_FinThk);
                    }
                }
                else
                {
                    SelectedFin = Calculation.TO.Main.Properties.Resources._0_12mmAl;
                }
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._12_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._12_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._7_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._7_0mm);
                if (!StepFin.Contains(Calculation.TO.Main.Properties.Resources._1_8mm))
                    StepFin.Insert(0, Calculation.TO.Main.Properties.Resources._1_8mm);
                int tubesN = (int)(setValueHightFin / 35);
                setValueHight = true;
                I_HeightInt = tubesN * 35;
                setValueHight = false;
                I_Geo = 3512;
                _tableData.I_Geo = "3512";
            }

            void SetPipe2510()
            {
                Pipe.Clear();
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe9_0_30Cu);
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe9_0_50Cu);
                //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe9_0_30CuG);
                if (_tableData != null)
                {
                    if (_tableData.I_PipeThk == null)
                    {
                        SelectedPipe = _tableData.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe9_0_30Cu;
                    }
                    else
                    {
                        SelectedPipe = _tableData.I_PipeThk = GetPipe( _tableData.I_PipeThk);
                    }
                }
                else
                {
                    SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe9_0_30Cu;
                }

                Fin.Clear();
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_10mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                if (_tableData != null)
                {
                    if (_tableData.I_FinThk == null)
                    {
                        SelectedFin = _tableData.I_FinThk = Calculation.TO.Main.Properties.Resources._0_10mmAl;
                    }
                    else
                    {
                        SelectedFin = _tableData.I_FinThk = GetFin(_tableData.I_FinThk);
                    }
                }
                else
                {
                    SelectedFin = Calculation.TO.Main.Properties.Resources._0_10mmAl;
                }
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._12_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._12_0mm);
                if (StepFin.Contains(Calculation.TO.Main.Properties.Resources._7_0mm))
                    StepFin.Remove(Calculation.TO.Main.Properties.Resources._7_0mm);
                if (!StepFin.Contains(Calculation.TO.Main.Properties.Resources._1_8mm))
                    StepFin.Insert(0, Calculation.TO.Main.Properties.Resources._1_8mm);
                int tubesN = (int)(setValueHightFin / 25);
                setValueHight = true;
                I_HeightInt = tubesN * 25;
                setValueHight = false;
                I_Geo = 2510;
                _tableData.I_Geo = "2510";
            }

            void SetPipe2507()
            {
                Pipe.Clear();
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe7_0_28Cu);
                Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe7_0_40Cu);
                //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe7_0_28CuG);
                if (_tableData != null)
                {
                    if (_tableData.I_PipeThk == null)
                    {
                        SelectedPipe = _tableData.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe7_0_28Cu;
                    }
                    else
                    {
                        SelectedPipe = _tableData.I_PipeThk = GetPipe( _tableData.I_PipeThk);
                    }
                }
                else
                {
                    SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe7_0_28Cu;
                }

                Fin.Clear();
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_10mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
                //Fin.Add(Calculation.TO.Main.Properties.Resources.0.20mmAl);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
                Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
                //Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
                if (_tableData != null)
                {
                    if (_tableData.I_FinThk == null)
                    {
                        SelectedFin = _tableData.I_FinThk = Calculation.TO.Main.Properties.Resources._0_10mmAl;
                    }
                    else
                    {
                        SelectedFin = _tableData.I_FinThk = GetFin(_tableData.I_FinThk);
                    }
                }
                else
                {
                    SelectedFin = Calculation.TO.Main.Properties.Resources._0_10mmAl;
                }
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
                int tubesN = (int)(setValueHightFin / 25);
                setValueHight = true;
                I_HeightInt = tubesN * 25;
                setValueHight = false;
                I_Geo = 2507;
                _tableData.I_Geo = "2507";
            }

            string GetPipe(string I_PipeThk)
            {
                foreach (var pipe in Pipe)
                {
                    if (pipe == I_PipeThk)
                    {
                        return pipe;
                    }
                }
                return Pipe[0];
            }

            string GetFin( string I_FinThk)
            {
                foreach (var fin in Fin)
                {
                    if (fin == I_FinThk)
                    {
                        return fin;
                    }
                }
                return Fin[0];
            }
        }

        /// <summary>
        /// Установка толщины оребрения и материала
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectFin(string value)
        {
            if (value != null)
            {
                _tableData.I_FinThk = value;
            }
        }

        /// <summary>
        /// Установка размера и материала трубки
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectPipe(string value)
        {
            _tableData.I_PipeThk = value;
        }

        /// <summary>
        /// Вызывается при изменении значении свойства "I_HeightInt"
        /// корректирует это значение под кол-во труб
        /// </summary>
        /// <param name="value"></param>
        private void SetValueHight(double value)
        {
            string i_geo_str = I_Geo.ToString();
            int k = 0;
            if (i_geo_str == Calculation.TO.Main.Properties.Resources._5012)
            {
                k = 50;
            }
            if (i_geo_str == Calculation.TO.Main.Properties.Resources._4816)
            {
                k = 48;
            }
            if (i_geo_str == Calculation.TO.Main.Properties.Resources._3512)
            {
                k = 35;
            }
            if (i_geo_str == Calculation.TO.Main.Properties.Resources._2510)
            {
                k = 25;
            }
            if (i_geo_str == Calculation.TO.Main.Properties.Resources._2507)
            {
                k = 25;
            }
            if (k > 0)
            {
                int tubesN = (int)(setValueHightFin / k);
                value = tubesN * k;
                string old;
                // Меняет поле "I_HeightInt" через DevExpress
                //SetPropertyCore<string>("I_HeightInt", value.ToString(), out old);
                valueHightFin = value;
            }
        }

        /// <summary>
        /// установка видимости ввода "концентрации гликоля",
        /// вызывается при обновлении свойства SelectedFluid
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectedFluid(string value)
        {
            _tableData.I_MedTyp = value;
        }

        /// <summary>
        /// Устанавливает шаг оребрения
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectStepFin(string value)
        {
            _tableData.I_LamAbsFix = value;
        }

        /// <summary>
        /// Загрузка списка геометрии
        /// </summary>
        private void SetGeometry()
        {
            Geometry = new ObservableCollection<string>();
            Geometry.Add(Calculation.TO.Main.Properties.Resources._5012);
            Geometry.Add(Calculation.TO.Main.Properties.Resources._4816);
            Geometry.Add(Calculation.TO.Main.Properties.Resources._3512);
            Geometry.Add(Calculation.TO.Main.Properties.Resources._2510);
            Geometry.Add(Calculation.TO.Main.Properties.Resources._2507);
            if (_tableData != null)
            {
                if (_tableData.I_Geo == null)
                {
                    SelectGeometry = _tableData.I_Geo = Calculation.TO.Main.Properties.Resources._5012;
                }
                else
                {
                    SelectGeometry = _tableData.I_Geo;
                }
            }
            else
            {
                SelectGeometry = Calculation.TO.Main.Properties.Resources._5012;
            }
        }

        /// <summary>
        /// Загрузка списка теплоносителя
        /// </summary>
        /// <param name="outView"></param>
        private void SetFluidList()
        {
            Fluid = new ObservableCollection<string>();
            Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidW100);
            Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidEthyleneGlycol);
            Fluid.Add(Calculation.TO.Main.Properties.Resources.FluidPropyleneGlycol);
            if (_tableData != null)
            {
                if (_tableData.I_MedTyp == null)
                {
                    SelectedFluid = _tableData.I_MedTyp = Calculation.TO.Main.Properties.Resources.FluidW100;
                }
                else
                {
                    selectedFluid = _tableData.I_MedTyp;
                }
            }
            else
            {
                SelectedFluid = Calculation.TO.Main.Properties.Resources.FluidW100;
            }
        }

        /// <summary>
        /// Загрузка списка трубок
        /// </summary>
        /// <param name="outView"></param>
        private void SetPipeList()
        {
            Pipe = new ObservableCollection<string>();
            Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu);
            Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu);
            //Pipe.Add(Calculation.TO.Main.Properties.Resources.pipe12_0.35CuG);            
            if (_tableData != null)
            {
                if (_tableData.I_PipeThk == null)
                {
                    SelectedPipe = _tableData.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
                }
                else
                {
                    SelectedPipe = _tableData.I_PipeThk;
                }
            }
            else
            {
                SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
            }
        }

        /// <summary>
        /// Загрузка списка толщина оребрения
        /// </summary>
        /// <param name="outView"></param>
        private void SetFinList()
        {
            Fin = new ObservableCollection<string>();
            Fin.Add(Calculation.TO.Main.Properties.Resources._0_12mmAl);
            Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmAl);
            Fin.Add(Calculation.TO.Main.Properties.Resources._0_18mmAl);
            Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmAl);
            Fin.Add(Calculation.TO.Main.Properties.Resources._0_25mmAl);
            Fin.Add(Calculation.TO.Main.Properties.Resources._0_16mmCu);
            Fin.Add(Calculation.TO.Main.Properties.Resources._0_15mmEpoxy);
            Fin.Add(Calculation.TO.Main.Properties.Resources._0_20mmEpoxy);
            if (_tableData != null)
            {
                if (_tableData.I_FinThk == null)
                {
                    SelectedFin = _tableData.I_FinThk = Calculation.TO.Main.Properties.Resources._0_12mmAl;
                }
                else
                {
                    SelectedFin = _tableData.I_FinThk;
                }
            }
            else
            {
                SelectedFin = Calculation.TO.Main.Properties.Resources._0_12mmAl;
            }
        }

        /// <summary>
        /// Загрузка списка шаг оребрения
        /// </summary>
        /// <param name="outView"></param>
        private void SetStepFinList()
        {
            StepFin = new ObservableCollection<string>();
            StepFin.Add(Calculation.TO.Main.Properties.Resources._1_8mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._2_0mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._2_2mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._2_5mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._3_0mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._3_5mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._4_0mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._4_5mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._5_0mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._7_0mm);
            StepFin.Add(Calculation.TO.Main.Properties.Resources._12_0mm);
            if (_tableData != null)
            {
                if (_tableData.I_LamAbsFix == null)
                {
                    SelectedStepFin = _tableData.I_LamAbsFix = Calculation.TO.Main.Properties.Resources._2_5mm;
                }
                else
                {
                    SelectedStepFin = _tableData.I_LamAbsFix;
                }
            }
            else
            {
                SelectedStepFin = Calculation.TO.Main.Properties.Resources._2_5mm;
            }
        }

        #endregion
    }
}
