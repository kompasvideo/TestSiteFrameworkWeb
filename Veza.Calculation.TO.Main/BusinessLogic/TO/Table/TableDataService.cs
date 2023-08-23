using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Veza.Calculation.TO.Main.Interfaces;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.ViewModels;

namespace Veza.HeatExchanger.Services
{
    sealed public class TableDataService : ITableDataService
    {
        #region Внутренние поля и переменные

        /// <summary>
        /// index при доступе к ProjectOut
        /// </summary>
        private int IdToCalc = -1;
        /// <summary>
        /// Служит для доступа к ресурсам строк приложения Resources.resx 
        /// в зависимости от установленного сейчас языка
        /// </summary>
        private ResourceManager rm;

        private TableData tableData;
        private IInputData inputData;
        #endregion

        #region Конструкторы
        public TableDataService(TableData tableData, ILogs logs, IInputData inputData)
        {
            this.tableData = tableData;
            this.inputData = inputData;
            rm = new ResourceManager("Veza.HeatExchanger.Properties.Resources", Assembly.GetExecutingAssembly());
        }
        #endregion

        #region Методы открытые    

        /// <summary>
        /// Первоначальная настройка таблицы
        /// </summary>
        public void InitTable()
        {
            if (TableData.ProjectsOutTable.Count == 0)
            {
                OutView outView = GetProjectOut_Input();
                TableData.ProjectsOutTable.Add(outView);
                TableData.SelectProjectsOutTable = TableData.ProjectsOutTable[0];
            }
        }

        /// <summary>
        /// Обновление таблицы при изменении режима расчёта - воздухонагреватель, воздуоохладителт и т.д.
        /// </summary>
        public void InitTableSwitchMode(double koef)
        {
            TableData.WidthColumn_1 = (int)(50 * koef);
            TableData.ProjectsOutTable.Clear();
            OutView outView = GetProjectOut_Input();
            TableData.ProjectsOutTable.Add(outView);
            TableData.SelectProjectsOutTable = TableData.ProjectsOutTable[0];
        }

        /// <summary>
        /// Считывает выходные результаты
        /// </summary>
        public void GetProjectOut()
        {
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_Pipe))
            {
                TableData.ProjectsOutTable[IdToCalc].I_Pipe = inputData.ProjectsOut.I_Pipe;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_MatRows))
            {
                TableData.ProjectsOutTable[IdToCalc].I_MatRows = inputData.ProjectsOut.I_MatRows;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_MatFins))
            {
                TableData.ProjectsOutTable[IdToCalc].I_MatFins = inputData.ProjectsOut.I_MatFins;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_BaseBar))
            {
                TableData.ProjectsOutTable[IdToCalc].I_BaseBar = inputData.ProjectsOut.I_BaseBar;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrDX))
            {
                TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrDX = inputData.ProjectsOut.I_AirTempInStrDX;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrCX))
            {
                TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrCX = inputData.ProjectsOut.I_AirTempInStrCX;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrHW))
            {
                TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrHW = inputData.ProjectsOut.I_AirTempInStrHW;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrCW))
            {
                TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrCW = inputData.ProjectsOut.I_AirTempInStrCW;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrST))
            {
                TableData.ProjectsOutTable[IdToCalc].I_AirTempInStrST = inputData.ProjectsOut.I_AirTempInStrST;
            }

            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_MedTempInStrHW))
            {
                TableData.ProjectsOutTable[IdToCalc].I_MedTempInStrHW = inputData.ProjectsOut.I_MedTempInStrHW;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_MedTempInStrCW))
            {
                TableData.ProjectsOutTable[IdToCalc].I_MedTempInStrCW = inputData.ProjectsOut.I_MedTempInStrCW;
            }

            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_StmFlow))
            {
                TableData.ProjectsOutTable[IdToCalc].O_StmFlow = inputData.ProjectsOut.O_StmFlow;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_StmHeat))
            {
                TableData.ProjectsOutTable[IdToCalc].O_StmHeat = inputData.ProjectsOut.O_StmHeat;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_StmVelo))
            {
                TableData.ProjectsOutTable[IdToCalc].O_StmVelo = inputData.ProjectsOut.O_StmVelo;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_TCondDX))
            {
                TableData.ProjectsOutTable[IdToCalc].I_TCondDX = inputData.I_TCondDX.ToString("G", CultureInfo.InvariantCulture);
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_TEvapDX))
            {
                TableData.ProjectsOutTable[IdToCalc].I_TEvapDX = inputData.I_TEvapDX.ToString("G", CultureInfo.InvariantCulture);
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_TSubCDX))
            {
                TableData.ProjectsOutTable[IdToCalc].I_TSubCDX = inputData.I_TSubCDX.ToString("G", CultureInfo.InvariantCulture);
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_TOvrHDX))
            {
                TableData.ProjectsOutTable[IdToCalc].I_TOvrHDX = inputData.I_TOvrHDX.ToString("G", CultureInfo.InvariantCulture);
            }

            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_TCondCX))
            {
                TableData.ProjectsOutTable[IdToCalc].I_TCondCX = inputData.I_TCondCX.ToString("G", CultureInfo.InvariantCulture);
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_TSubCCX))
            {
                TableData.ProjectsOutTable[IdToCalc].I_TSubCCX = inputData.I_TSubCCX.ToString("G", CultureInfo.InvariantCulture);
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].I_THotGasCX))
            {
                TableData.ProjectsOutTable[IdToCalc].I_THotGasCX = inputData.I_THotGasCX.ToString("G", CultureInfo.InvariantCulture);
            }

            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_DxCxBar))
            {
                TableData.ProjectsOutTable[IdToCalc].O_DxCxBar = inputData.ProjectsOut.O_DxCxBar;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_DxCxK))
            {
                TableData.ProjectsOutTable[IdToCalc].O_DxCxK = inputData.ProjectsOut.O_DxCxK;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_DxCxMas))
            {
                TableData.ProjectsOutTable[IdToCalc].O_DxCxMas = inputData.ProjectsOut.O_DxCxMas;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_DxCxPres))
            {
                TableData.ProjectsOutTable[IdToCalc].O_DxCxPres = inputData.ProjectsOut.O_DxCxPres;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_DxCxVol))
            {
                TableData.ProjectsOutTable[IdToCalc].O_DxCxVol = inputData.ProjectsOut.O_DxCxVol;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_THotGas))
            {
                TableData.ProjectsOutTable[IdToCalc].O_THotGas = inputData.ProjectsOut.O_THotGas;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_TOvrH))
            {
                TableData.ProjectsOutTable[IdToCalc].O_TOvrH = inputData.ProjectsOut.O_TOvrH;
            }
            if (string.IsNullOrEmpty(TableData.ProjectsOutTable[IdToCalc].O_RCirc))
            {
                TableData.ProjectsOutTable[IdToCalc].O_RCirc = inputData.ProjectsOut.O_RCirc;
            }
        }

        /// <summary>
        /// восстановление параметров из сохранённого расчёта
        /// </summary>
        /// <param name="saveCodes"></param>
        /// <returns></returns>
        public bool Decoder(SaveCodes saveCodes)
        {
            OutView outView = null;
            foreach (var item in TableData.ProjectsOutTable)
            {
                if (item.Id == TableData.SelectProjectsOutTable.Id)
                {
                    outView = item;
                    break;
                }
            }

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
                    outView.I_Geo = int.Parse(I_Geo);
                    break;
                default:
                    return false;
            }

            // Длина оребрения
            string str = codes[4].Split('-')[1];
            outView.I_WidthInt = double.Parse(str, CultureInfo.InvariantCulture);

            // Высота оребрения
            outView.I_HeightInt = saveCodes.ValueHightFin;
            str = codes[2].Substring(0, 2);

            // Температура воздуха на входе
            outView.I_AirTempInDX = saveCodes.I_AirTempInDX;
            outView.I_AirTempInCX = saveCodes.I_AirTempInCX;
            outView.I_AirTempInST = saveCodes.I_AirTempInST;
            outView.I_AirTempInHW = saveCodes.I_AirTempInHW;
            outView.I_AirTempInCW = saveCodes.I_AirTempInCW;

            // Влажность воздуха на входе
            outView.I_AirHumInDX = saveCodes.ValueBaseHumDX;
            outView.I_AirHumInCX = saveCodes.ValueBaseHumCX;
            outView.I_AirHumInST = saveCodes.ValueBaseHumST;
            outView.I_AirHumInHW = saveCodes.ValueBaseHumHW;
            outView.I_AirHumInCW = saveCodes.ValueBaseHumCW;

            // Мощность
            outView.I_CapacityDX = saveCodes.CapacityDX.ToString("G", CultureInfo.InvariantCulture);
            outView.I_CapacityCX = saveCodes.CapacityCX.ToString("G", CultureInfo.InvariantCulture);
            outView.I_CapacityST = saveCodes.CapacityST.ToString("G", CultureInfo.InvariantCulture);
            outView.I_CapacityHW = saveCodes.CapacityHW.ToString("G", CultureInfo.InvariantCulture);
            outView.I_CapacityCW = saveCodes.CapacityCW.ToString("G", CultureInfo.InvariantCulture);

            // Расход воздуха
            outView.I_AirflowDX = saveCodes.ValueAirFlowDX;
            outView.I_AirflowCX = saveCodes.ValueAirFlowCX;
            outView.I_AirflowST = saveCodes.ValueAirFlowST;
            outView.I_AirflowHW = saveCodes.ValueAirFlowHW;
            outView.I_AirflowCW = saveCodes.ValueAirFlowCW;

            // Температура теплоносителя на входе
            outView.I_MedTempInHW = saveCodes.ValueMedTempInHW;
            outView.I_MedTempInCW = saveCodes.ValueMedTempInCW;

            // Температура теплоносителя на выходе
            if (saveCodes.SelectedVariantRasch == Calculation.TO.Main.Properties.Resources.I_TempFluidOut)
            {
                outView.I_MedTempOutHW = saveCodes.ValueVariantRaschHW;
                outView.I_MedTempOutCW = saveCodes.ValueVariantRaschCW;
            }

            // Число рядов
            string[] mas = codes[4].Split('x', 'х');
            str = mas[0].Substring(1, mas[0].Length - 1);
            outView.NoOfRows = str;

            // Колическтво отводов
            outView.Circuits = saveCodes.ValueCircuits.ToString();

            // Трубка
            String str2 = codes[2].Substring(3, 2);
            String str3 = codes[2].Substring(5, codes[2].Length - 5);
            switch (str)
            {
                case "12":
                    switch (str2)
                    {
                        case "32":
                            outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe12_0_32Cu;
                            break;
                        case "50":
                            outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe12_0_50Cu;
                            break;
                        case "35":
                            outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe12_0_35CuG;
                            break;
                    }
                    break;
                case "16":
                    switch (str2)
                    {
                        case "40":
                            outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe16_0_40Cu;
                            break;
                        case "50":
                            outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe16_0_50Cu;
                            break;
                        case "70":
                            outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe16_0_70G;
                            break;
                        case "100":
                            outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe16_1_00G;
                            break;
                    }
                    break;
                case "9.52":
                    switch (str2)
                    {
                        case "30":
                            if (str3 == "М")
                                outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe9_0_30Cu;
                            else
                                outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe9_0_30CuG;
                            break;
                        case "50":
                            outView.I_PipeThk = Calculation.TO.Main.Properties.Resources.pipe9_0_50Cu;
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
                            //inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe7_0.28CuG", Calculation.TO.Main.Properties.Resources.Culture);
                            break;
                        case "40":
                            inputData.SelectedPipe = Calculation.TO.Main.Properties.Resources.pipe7_0_40Cu;
                            break;
                    }
                    break;
            }

            // Толщина оребрения
            str = codes[3].Substring(1, 1);
            mas = codes[3].Split('x', 'х');
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
                            outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_12mmAl;
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
                            outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_15mmAl;
                            break;
                        case "АЛЭ":
                            outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_15mmEpoxy;
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
                    outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_16mmCu;
                    break;
                case "18":
                    switch (str3)
                    {
                        case "АЛ":
                            outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_18mmAl;
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
                            outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_20mmAl;
                            break;
                        case "АЛЭ":
                            outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_20mmEpoxy;
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
                            outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_20mm304;
                            break;
                        case "НЖ3":
                            outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_20mm304;
                            break;
                    }
                    break;
                case "25":
                    switch (str3)
                    {
                        case "АЛ":
                            outView.I_FinThk = Calculation.TO.Main.Properties.Resources._0_25mmAl;
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
                    outView.FinSpace = 1.8;
                    break;
                case "20":
                    outView.FinSpace = 2.0;
                    break;
                case "22":
                    outView.FinSpace = 2.2;
                    break;
                case "25":
                    outView.FinSpace = 2.5;
                    break;
                case "30":
                    outView.FinSpace = 3.0;
                    break;
                case "35":
                    outView.FinSpace = 3.5;
                    break;
                case "40":
                    outView.FinSpace = 4.0;
                    break;
                case "45":
                    outView.FinSpace = 4.5;
                    break;
                case "50":
                    outView.FinSpace = 5.0;
                    break;
                case "70":
                    outView.FinSpace = 7.0;
                    break;
                case "120":
                    outView.FinSpace = 12.0;
                    break;
            }

            // Скорость воздуха
            outView.AirVelocity = saveCodes.AirVelocity;
            // Перепад давления сухой воздуха
            outView.PresDropDry = saveCodes.PresDropDry;
            // Резервная нагрузка
            outView.ReverseLoad = saveCodes.ReverseLoad;
            // скорость жидкости             
            outView.MedVelo = saveCodes.MedVelo;
            // Максимальное падение давления
            outView.MedKPa = saveCodes.ValueMedKPa.ToString("G", CultureInfo.InvariantCulture);
            // Температура воздуха на выходе
            outView.AirTempOut = saveCodes.AirTempOut;


            // Расход воздуха
            outView.I_AirflowHW = saveCodes.ValueAirFlowHW;
            // Температура воздуха на входе
            outView.I_AirTempInHW = saveCodes.I_AirTempInHW;
            // Влажность воздуха на входе
            outView.I_AirHumInHW = saveCodes.ValueBaseHumHW;
            // Мощность
            outView.I_CapacityHW = saveCodes.CapacityHW.ToString("G", CultureInfo.InvariantCulture);

            // Расход воздуха
            outView.I_AirflowCW = saveCodes.ValueAirFlowCW;
            // Температура воздуха на входе
            outView.I_AirTempInCW = saveCodes.I_AirTempInCW;
            // Влажность воздуха на входе
            outView.I_AirHumInCW = saveCodes.ValueBaseHumCW;
            // Мощность
            outView.I_CapacityCW = saveCodes.CapacityCW.ToString("G", CultureInfo.InvariantCulture);

            // Расход воздуха
            outView.I_AirflowST = saveCodes.ValueAirFlowST;
            // Температура воздуха на входе
            outView.I_AirTempInST = saveCodes.I_AirTempInST;
            // Влажность воздуха на входе
            outView.I_AirHumInST = saveCodes.ValueBaseHumST;
            // Мощность
            outView.I_CapacityST = saveCodes.CapacityST.ToString("G", CultureInfo.InvariantCulture);

            // Расход воздуха
            outView.I_AirflowCX = saveCodes.ValueAirFlowCX;
            // Температура воздуха на входе
            outView.I_AirTempInCX = saveCodes.I_AirTempInCX;
            // Влажность воздуха на входе
            outView.I_AirHumInCX = saveCodes.ValueBaseHumCX;
            // Мощность
            outView.I_CapacityCX = saveCodes.CapacityCX.ToString("G", CultureInfo.InvariantCulture);

            // Расход воздуха
            outView.I_AirflowDX = saveCodes.ValueAirFlowDX;
            // Температура воздуха на входе
            outView.I_AirTempInDX = saveCodes.I_AirTempInDX;
            // Влажность воздуха на входе
            outView.I_AirHumInDX = saveCodes.ValueBaseHumDX;
            // Мощность
            outView.I_CapacityDX = saveCodes.CapacityDX.ToString("G", CultureInfo.InvariantCulture);

            return true;
        }

        /// <summary>
        /// Очистка выходных данных перед повторным расчётом
        /// </summary>
        public void ClearOutputData()
        {
            foreach (var item in TableData.ProjectsOutTable)
            {
                // был ли расчёт
                item.Calc = false;
                // Перепад давления сухой воздуха
                item.PresDropDry = "";
                // Скорость воздуха
                item.AirVelocity = "";
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
                // Массовый расход хладагента
                item.O_DxCxMas = "";
                // Общая производительность
                item.O_TotCap = "";
                item.MedVelo = "";
                item.MedKPa = "";
                item.O_MedFlow = "";
                item.AirTempOut = "";
            }
        }
        public void ClearOutputData(int index)
        {
            int i = 0;
            foreach (var item in TableData.ProjectsOutTable)
            {
                if (i == index)
                {
                    // был ли расчёт
                    item.Calc = false;
                    // Перепад давления сухой воздуха
                    item.PresDropDry = "";
                    // Скорость воздуха
                    item.AirVelocity = "";
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
                    // Массовый расход хладагента
                    item.O_DxCxMas = "";
                    // Общая производительность
                    item.O_TotCap = "";
                    item.MedVelo = "";
                    item.MedKPa = "";
                    item.O_MedFlow = "";
                    item.AirTempOut = "";
                }
                i++;
            }
        }

        /// <summary>
        /// Нажата кнопка - Добавить строку
        /// </summary>
        public void AddRow()
        {
            int count = TableData.ProjectsOutTable.Count;
            int maxId = 0;
            foreach (var item in TableData.ProjectsOutTable)
            {
                if (item.Id > maxId)
                    maxId = item.Id;
            }
            OutView outView = TableData.ProjectsOutTable[count - 1].Copy(maxId + 1);
            TableData.ProjectsOutTable.Add(outView);
        }

        /// <summary>
        /// Возвращяет выделенную "строку"
        /// </summary>
        /// <returns></returns>
        public OutView GetOutView()
        {
            return TableData.ProjectsOutTable[IdToCalc];
        }

        /// <summary>
        /// Нажата кнопка - Сохранить
        /// </summary>
        public void Save(string type, int id)
        {
            int index = 0;
            for (int i = 0; i < TableData.ProjectsOutTable.Count; i++)
            {
                if (TableData.ProjectsOutTable[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
        }

        /// <summary>
        ///  Возвращяет свойство SelectProjectsOutTable
        /// </summary>
        /// <returns></returns>
        public OutView GetSelect()
        {
            return TableData.SelectProjectsOutTable;
        }

        /// <summary>
        /// Записывает индекс выделенной "строки"
        /// </summary>
        /// <param name="i"></param>
        public void SetIdToCalc(int i)
        {
            IdToCalc = i;
        }

        /// <summary>
        /// Возвращяет индекс строки
        /// </summary>
        /// <returns></returns>
        public int GetIdToCalc()
        {
            return IdToCalc;
        }

        /// <summary>
        ///  Возвращяет значение поля FinSpase в TableData.ProjectsOutTable[]
        /// </summary>
        /// <param name="index">Индекс в коллекции</param>
        /// <returns>Значение поля FinSpase</returns>
        public double GetFinSpace(int index)
        {
            if (TableData.ProjectsOutTable.Count > 0)
            {
                return TableData.ProjectsOutTable[index].FinSpace;
            }
            string[] stepFin = inputData.SelectedStepFin.Split(' ');
            return double.Parse(stepFin[0], CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Обновление данных списка табличного расчёта
        /// </summary>
        public void UpdateProjectsOutTable()
        {
            foreach (var outView in TableData.ProjectsOutTable)
            {
                if (!outView.Calc)
                {
                    if (!outView.CalcError)
                    {
                        if (TableData.ProjectsOutTable.Count == 1)
                        {
                            outView.I_WidthInt = inputData.ValueWidthFin;
                            outView.I_HeightInt = inputData.ValueHightFin;
                            outView.NoOfRows = inputData.ValuePipe.ToString("G", CultureInfo.InvariantCulture);
                            //outView.NoOfRows = inputData.ProjectsOut.NoOfRows;
                            outView.Circuits = inputData.ValueCircuits.ToString();
                            //outView.Circuits = inputData.ProjectsOut.Circuits;
                            outView.FinSpace = double.Parse(inputData.SelectedStepFin.Split(' ')[0], CultureInfo.InvariantCulture);
                            outView.I_AirTempInDX = inputData.I_AirTempInDX;
                            outView.I_AirTempInStrDX = inputData.ProjectsOut.I_AirTempInStrDX;
                            outView.I_AirHumInDX = inputData.ValueBaseHumDX;
                            outView.I_AirflowDX = inputData.ValueAirFlowDX;
                            outView.I_MedTempInHW = inputData.ValueMedTempIn;
                            outView.I_MedTempInCW = inputData.ValueMedTempIn;
                            outView.I_MedTempOutHW = inputData.ValueVariantRaschHW;
                            outView.I_MedTempOutCW = inputData.ValueVariantRaschCW;
                            outView.I_SoleCnz = inputData.I_SoleCnz.ToString("G", CultureInfo.InvariantCulture);
                            outView.I_StmBar = inputData.I_StmBar.ToString("G", CultureInfo.InvariantCulture);
                            //outView.I_StmBar = inputData.ProjectsOut.I_StmBar;
                            outView.I_StmTemp = inputData.I_StmTemp.ToString("G", CultureInfo.InvariantCulture);
                            outView.I_TCondDX = inputData.I_TCondDX.ToString("G", CultureInfo.InvariantCulture);
                            outView.I_TEvapDX = inputData.I_TEvapDX.ToString("G", CultureInfo.InvariantCulture);
                            outView.I_TSubCDX = inputData.I_TSubCDX.ToString("G", CultureInfo.InvariantCulture);
                            outView.I_TOvrHDX = inputData.I_TOvrHDX.ToString("G", CultureInfo.InvariantCulture);

                            outView.I_TCondCX = inputData.I_TCondCX.ToString("G", CultureInfo.InvariantCulture);
                            outView.I_TSubCCX = inputData.I_TSubCCX.ToString("G", CultureInfo.InvariantCulture);
                            outView.I_THotGasCX = inputData.I_THotGasCX.ToString("G", CultureInfo.InvariantCulture);

                            outView.I_MedTempInStrHW = inputData.ProjectsOut.I_MedTempInStrHW;
                            outView.I_MedTempInStrCW = inputData.ProjectsOut.I_MedTempInStrCW;
                            //outView.I_HeightIntStr = inputData.ProjectsOut.I_HeightIntStr;
                            outView.I_HeightIntStr = outView.I_HeightInt + " " + Calculation.TO.Main.Properties.Resources.mm;
                            outView.I_WidthIntStr = outView.I_WidthInt + " " + Calculation.TO.Main.Properties.Resources.mm;

                            outView.I_AirTempInHW = inputData.I_AirTempInHW;
                            outView.I_AirTempInStrHW = inputData.ProjectsOut.I_AirTempInStrHW;
                            outView.I_AirHumInHW = inputData.ValueBaseHumHW;
                            outView.I_AirflowHW = inputData.ValueAirFlowHW;
                            outView.I_MedTempInHW = inputData.ValueMedTempInHW;
                            outView.I_MedTempOutHW = inputData.ValueVariantRaschHW;

                            outView.I_AirTempInCW = inputData.I_AirTempInCW;
                            outView.I_AirTempInStrCW = inputData.ProjectsOut.I_AirTempInStrCW;
                            outView.I_AirHumInCW = inputData.ValueBaseHumCW;
                            outView.I_AirflowCW = inputData.ValueAirFlowCW;
                            outView.I_MedTempInCW = inputData.ValueMedTempInCW;
                            outView.I_MedTempOutCW = inputData.ValueVariantRaschCW;

                            outView.I_AirTempInST = inputData.I_AirTempInST;
                            outView.I_AirTempInStrST = inputData.ProjectsOut.I_AirTempInStrST;
                            outView.I_AirHumInST = inputData.ValueBaseHumST;
                            outView.I_AirflowST = inputData.ValueAirFlowST;

                            outView.I_AirTempInCX = inputData.I_AirTempInCX;
                            outView.I_AirTempInStrCX = inputData.ProjectsOut.I_AirTempInStrCX;
                            outView.I_AirHumInCX = inputData.ValueBaseHumCX;
                            outView.I_AirflowCX = inputData.ValueAirFlowCX;

                            outView.I_AirTempInDX = inputData.I_AirTempInDX;
                            outView.I_AirTempInStrDX = inputData.ProjectsOut.I_AirTempInStrDX;
                            outView.I_AirHumInDX = inputData.ValueBaseHumDX;
                            outView.I_AirflowDX = inputData.ValueAirFlowDX;
                        }
                        outView.I_FinThk = inputData.SelectedFin.Split(' ')[0];
                        outView.I_PipeThk = inputData.SelectedPipe.Split(' ')[2];
                        outView.I_Geo = int.Parse(inputData.SelectGeometry);
                        outView.SelectGeometry = outView.I_Geo.ToString();
                        outView.CalcType = CalcEnum.Reverse;
                        outView.I_Pipe = inputData.ProjectsOut.I_Pipe;
                        outView.I_MatRows = inputData.ProjectsOut.I_MatRows;
                        outView.I_MatFins = inputData.ProjectsOut.I_MatFins;
                        outView.I_BaseBar = inputData.ProjectsOut.I_BaseBar;
                        outView.I_MedTyp = inputData.SelectedFluid;
                        outView.I_FinThk += " " + Calculation.TO.Main.Properties.Resources.mm;
                        outView.I_PipeThk += " " + Calculation.TO.Main.Properties.Resources.mm;
                        outView.Calc = inputData.ProjectsOut.Calc;
                    }
                }
            }
        }

        /// <summary>
        /// Получение значения поля O_FrameB
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameB()
        {
            return "." + TableData.ProjectsOutTable[IdToCalc].O_FrameB;
        }

        /// <summary>
        /// Получение значения поля O_FrameT
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameT()
        {
            return "." + TableData.ProjectsOutTable[IdToCalc].O_FrameT;
        }

        /// <summary>
        /// Получение значения поля O_FrameThk
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameThk()
        {
            string str = TableData.ProjectsOutTable[IdToCalc].O_FrameThk;
            double d = GS.StringToDouble(str);
            if (d != double.NaN)
            {
                d = d * 10;
                str = d.ToString("G", CultureInfo.InvariantCulture);
                return "x" + str;
            }
            return "_";
        }

        /// <summary>
        /// Получение значения поля O_FrameL
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameL()
        {
            return "-" + TableData.ProjectsOutTable[IdToCalc].O_FrameL;
        }

        /// <summary>
        /// Получение значения поля O_FrameR
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameR()
        {
            return "." + TableData.ProjectsOutTable[IdToCalc].O_FrameR;
        }

        /// <summary>
        ///  Диаметр коллектора вход
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_O_ConIN(bool overload = false)
        {
            if (TableData.ProjectsOutTable[IdToCalc].O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN10)
            {
                return "010";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN15)
            {
                return "015";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN20)
            {
                return "020";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN25)
            {
                return "025";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN32)
            {
                return "032";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN40)
            {
                return "040";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN50)
            {
                return "050";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN65)
            {
                return "065";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN80)
            {
                return "080";
            }
            if (overload) return "_";
            return GetShortCode_O_ConOut(true);
        }

        /// <summary>
        ///  Диаметр коллектора выход
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_O_ConOut(bool overload = false)
        {
            if (TableData.ProjectsOutTable[IdToCalc].O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN10)
            {
                return "010";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN15)
            {
                return "015";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN20)
            {
                return "020";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN25)
            {
                return "025";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN32)
            {
                return "032";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN40)
            {
                return "040";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN50)
            {
                return "050";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN65)
            {
                return "065";
            }
            else if (TableData.ProjectsOutTable[IdToCalc].O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN80)
            {
                return "080";
            }
            if (overload) return "_";
            return GetShortCode_O_ConIN(true);
        }

        /// <summary>
        /// Получить геометрия
        /// </summary>
        /// <returns></returns>
        public string Get_I_Geo()
        {
            return TableData.ProjectsOutTable[IdToCalc].SelectGeometry;
        }

        /// <summary>
        /// Установить геометрию
        /// </summary>
        /// <param name="geometry"></param>
        public void Set_I_Geo(string geometry)
        {
            TableData.ProjectsOutTable[IdToCalc].SelectGeometry = geometry;
        }

        /// <summary>
        /// Получить Размеры и материал трубки
        /// </summary>
        /// <returns></returns>
        public string Get_I_PipeThk()
        {
            return TableData.ProjectsOutTable[IdToCalc].SelectedPipe;
        }

        /// <summary>
        /// Установить толщину оребрения
        /// </summary>
        /// <param name="fin"></param>
        public void Set_I_PipeThk(string pipe)
        {
            TableData.ProjectsOutTable[IdToCalc].SelectedPipe = pipe;
        }

        /// <summary>
        /// Получить толщину оребрения
        /// </summary>
        /// <returns></returns>
        public string Get_I_FinThk()
        {
            return TableData.ProjectsOutTable[IdToCalc].SelectedFin;
        }

        /// <summary>
        /// Установить толщину оребрения
        /// </summary>
        /// <param name="fin"></param>
        public void Set_I_FinThk(string fin)
        {
            TableData.ProjectsOutTable[IdToCalc].SelectedFin = fin;
        }

        /// <summary>
        /// Получить шаг оребрения
        /// </summary>
        /// <returns></returns>
        public string Get_I_LamAbsFix()
        {
            return TableData.ProjectsOutTable[IdToCalc].SelectedStepFin;
        }

        /// <summary>
        /// Установить шаг оребрения
        /// </summary>
        /// <param name="fin"></param>
        public void Set_I_LamAbsFix(string fin)
        {
            TableData.ProjectsOutTable[IdToCalc].SelectedStepFin = fin;
        }

        /// <summary>
        /// Получить теплоноситель
        /// </summary>
        /// <returns></returns>
        public string Get_I_MedTyp()
        {
            return TableData.ProjectsOutTable[IdToCalc].SelectedFluid;
        }

        /// <summary>
        /// Установить теплоноситель
        /// </summary>
        /// <param name="fluid"></param>
        public void Set_I_MedTyp(string fluid)
        {
            TableData.ProjectsOutTable[IdToCalc].SelectedFluid = fluid;
        }
        #endregion

        #region Методы внутренние
        /// <summary>
        /// Считывает входные данные
        /// </summary>
        private OutView GetProjectOut_Input()
        {
            OutView outView = new OutView(tableData);
            if (inputData.ProjectsOut == null)
            {
                throw new Exception(Calculation.TO.Main.Properties.Resources.strException1); // "InputData, GetProjectOut(), ProjectsOut = null"
            }
            else
            {
                outView.I_WidthInt = inputData.ValueWidthFin;
                outView.I_HeightInt = inputData.ValueHightFin;
                outView.FinSpace = double.Parse(inputData.SelectedStepFin.Split(' ')[0], CultureInfo.InvariantCulture);
                outView.I_FinThk = inputData.SelectedFin.Split(' ')[0];
                outView.I_PipeThk = inputData.SelectedPipe.Split(' ')[2];
                outView.NoOfRows = inputData.ValuePipe.ToString("G", CultureInfo.InvariantCulture);
                outView.I_Geo = int.Parse(inputData.SelectGeometry);
                outView.CalcType = CalcEnum.Reverse;
                outView.I_SoleCnz = inputData.I_SoleCnz.ToString("G", CultureInfo.InvariantCulture);

                outView.I_StmBar = inputData.I_StmBar.ToString("G", CultureInfo.InvariantCulture);
                outView.I_StmTemp = inputData.I_StmTemp.ToString("G", CultureInfo.InvariantCulture);
                outView.Circuits = inputData.ValueCircuits.ToString();

                outView.I_AirTempInHW = inputData.I_AirTempInHW;
                outView.I_AirHumInHW = inputData.ValueBaseHumHW;
                outView.I_AirflowHW = inputData.ValueAirFlowHW;
                outView.I_MedTempInHW = inputData.ValueMedTempInHW;
                outView.I_MedTempOutHW = inputData.ValueVariantRaschHW;

                outView.I_AirTempInCW = inputData.I_AirTempInCW;
                outView.I_AirHumInCW = inputData.ValueBaseHumCW;
                outView.I_AirflowCW = inputData.ValueAirFlowCW;
                outView.I_MedTempInCW = inputData.ValueMedTempInCW;
                outView.I_MedTempOutCW = inputData.ValueVariantRaschCW;

                outView.I_AirTempInST = inputData.I_AirTempInST;
                outView.I_AirHumInST = inputData.ValueBaseHumST;
                outView.I_AirflowST = inputData.ValueAirFlowST;

                outView.I_AirTempInCX = inputData.I_AirTempInCX;
                outView.I_AirHumInCX = inputData.ValueBaseHumCX;
                outView.I_AirflowCX = inputData.ValueAirFlowCX;
                outView.I_TCondCX = inputData.I_TCondCX.ToString("G", CultureInfo.InvariantCulture);
                outView.I_TSubCCX = inputData.I_TSubCCX.ToString("G", CultureInfo.InvariantCulture);
                outView.I_THotGasCX = inputData.I_THotGasCX.ToString("G", CultureInfo.InvariantCulture);

                outView.I_AirTempInDX = inputData.I_AirTempInDX;
                outView.I_AirHumInDX = inputData.ValueBaseHumDX;
                outView.I_AirflowDX = inputData.ValueAirFlowDX;
                outView.I_TCondDX = inputData.I_TCondDX.ToString("G", CultureInfo.InvariantCulture);
                outView.I_TEvapDX = inputData.I_TEvapDX.ToString("G", CultureInfo.InvariantCulture);
                outView.I_TSubCDX = inputData.I_TMaxPDX.ToString("G", CultureInfo.InvariantCulture);
            }
            return outView;
        }
        #endregion
    }
}
