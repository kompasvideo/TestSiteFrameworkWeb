using InvoTechData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Veza.HeatExchanger.BusinessLogic.Models;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.MAKK;
using Veza.HeatExchanger.Services;
using Veza.Calculation.TO.Main;

namespace Veza.HeatExchanger.BusinessLogic.Compressors.InvoTech
{
    public class InvoTech : IInvoTech
    {
        #region Внутренние поля 
        private static bool fRun = false;

        /// <summary>
        /// Путь к этому приложению
        /// </summary>
        private static string programPath;

        private static string dllInvoTechPath;

        /// <summary>
        /// Класс в библиотеке
        /// </summary>
        private Type t;

        /// <summary>
        /// Созданный экземпляр класса
        /// </summary>
        private object instanceClass;

        /// <summary>
        /// Получить список частот
        /// </summary>
        private List<CodeInfoV> lstParam2;

        /// <summary>
        /// Список моделей компрессоров
        /// </summary>
        private List<CodeInfoV> CbeModel { get; set; }

        /// <summary>
        /// Содержит данные для таблицы Performance (InvoTechData.dll)
        /// </summary>
        private List<InvoCapabilityV> PerformanceTableV { get; set; }
        private string selectedModel;

        /// <summary>
        /// выбранная частота
        /// </summary>
        private string selectedPowerSupply;

        /// <summary>
        /// Выбранный элемент "напряжение/частота/кол-во полюсов"
        /// </summary>
        private string selectedList_U_Hz_P;

        /// <summary>
        /// Поле Evaporating Temperature - Температура кипения
        /// </summary>
        private decimal sEEvaporatingTemperature;

        /// <summary>
        /// Поле  Suction Gas Temperature(°C):
        /// </summary>
        private decimal sESuctionGasTemperature;

        /// <summary>
        /// Поле Suction Gas Superheat(K) - Перегрев всас. газа
        /// </summary>
        private decimal sESuctionGasSuperheat;

        /// <summary>
        ///  Поле Condensing Temperature(°C) - Температура конденсации
        /// </summary>
        private decimal sECondensingTemperature;

        /// <summary>
        /// Поле Liquid Subcooling(K) - Переохлаждение
        /// </summary>
        private decimal sELiquidSubcooling;

        /// <summary>
        /// поле Liquid Temperature(°C)：
        /// </summary>
        private decimal sELiquidTemperature;

        /// <summary>
        /// Список частот
        /// [0] - 50Hz  
        /// [1] - 60Hz
        /// </summary>
        private List<string> PowerSupply { get; set; }

        /// <summary>
        /// выбранная частота
        /// </summary>
        private string SelectedPowerSupply
        {
            get => selectedPowerSupply;
            set
            {
                selectedPowerSupply = value;
                SetSelectedPowerSupply(value);
            }
        }

        /// <summary>
        /// Поле Evaporating Temperature
        /// </summary>
        private decimal SEEvaporatingTemperature
        {
            get => sEEvaporatingTemperature;
            set
            {
                sEEvaporatingTemperature = value;
                SEEvaporatingTemperature_EditValueChanged(value);
            }
        }

        /// <summary>
        /// поле Evaporating Pressure (Gage)(barg):
        /// </summary>
        private double TEEvaporatingPressure { get; set; }

        /// <summary>
        /// Поле Suction Useful Superheat(100%):
        /// </summary>
        private decimal SESuctionUsefulSuperheat { get; set; }

        /// <summary>
        /// Поле  Suction Gas Temperature(°C):
        /// </summary>
        private decimal SESuctionGasTemperature
        {
            get => sESuctionGasTemperature;
            set
            {
                sESuctionGasTemperature = value;
                SESuctionGasTemperature_EditValueChanged(value);
            }
        }

        /// <summary>
        /// Поле Suction Gas Superheat(K):
        /// </summary>
        private decimal SESuctionGasSuperheat
        {
            get => sESuctionGasSuperheat;
            set
            {
                sESuctionGasSuperheat = value;
                SESuctionGasSuperheat_EditValueChanged(value);
            }
        }

        /// <summary>
        ///  Поле Condensing Temperature(°C)
        /// </summary>
        private decimal SECondensingTemperature
        {
            get => sECondensingTemperature;
            set
            {
                sECondensingTemperature = value;
                SECondensingTemperature_EditValueChanged(value);
            }
        }

        /// <summary>
        /// Поле Condensing Pressure (Gage)(barg):
        /// </summary>
        private double TECondensingPressureGageBarg { get; set; }

        /// <summary>
        /// Поле Liquid Subcooling(K):
        /// </summary>
        private decimal SELiquidSubcooling
        {
            get => sELiquidSubcooling;
            set
            {
                sELiquidSubcooling = value;
                SELiquidSubcooling_EditValueChanged(value);
            }
        }

        /// <summary>
        /// поле Liquid Temperature(°C)：
        /// </summary>
        private decimal SELiquidTemperature
        {
            get => sELiquidTemperature;
            set
            {
                sELiquidTemperature = value;
                SELiquidTemperature_EditValueChanged(value);
            }
        }

        /// <summary>
        /// Список "напряжение/частота/кол-во полюсов" 
        /// [0] - 220V/50Hz/1P
        /// [1] - 220V/50Hz/3P
        /// [2] - 380V/50Hz/3P
        /// [0] - 220V/60Hz/1P
        /// [1] - 220V/60Hz/3P
        /// [2] - 380V/60Hz/3P
        /// [3] - 460V/60Hz/3P
        /// [4] - 575V/60Hz/3P
        /// </summary>
        private List<string> List_U_Hz_P { get; set; }

        /// <summary>
        /// Выбранный элемент "напряжение/частота/кол-во полюсов"
        /// </summary>
        private string SelectedList_U_Hz_P
        {
            get => selectedList_U_Hz_P;
            set
            {
                selectedList_U_Hz_P = value;
                SetSelectedList_U_Hz_P(value);
            }
        }

        /// <summary>
        /// Список моделей компрессоров
        /// </summary>
        private List<string> Models { get; set; }

        /// <summary>
        /// выбранный компрессор
        /// </summary>
        private string SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                SetParamsPerformanceTableToProperties();
            }
        }

        /// <summary>
        /// Performance Operating Conditions
        /// </summary>
        private string OperatingConditions { get; set; }

        /// <summary>
        /// Performance Compessor
        /// </summary>
        private string Compessor { get; set; }

        /// <summary>
        /// Performance Refrigeration Capacity
        /// </summary>
        private string RefrigerationCapacity { get; set; }

        /// <summary>
        /// Performance Power Input
        /// </summary>
        private string PowerInput { get; set; }

        /// <summary>
        /// Performance COP
        /// </summary>
        private string COP { get; set; }

        /// <summary>
        /// Performance Current
        /// </summary>
        private string Current { get; set; }

        /// <summary>
        /// Performance Mass Flow
        /// </summary>
        private string MassFlow { get; set; }

        /// <summary>
        /// Performance Heat Rejection 
        /// </summary>
        private string HeatRejection { get; set; }

        private NumberStyles style = NumberStyles.AllowDecimalPoint;
        #endregion

        #region Публичные методы

        public void LoadDll()
        {


            // вызвать код dll InvoTech запущенный в linux из под Wine


            //if (fRun) return;
            //string path = programPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //string pathDll = path + Calculation.TO.Main.Properties.Resources.InvoTechPathDll;
            //dllInvoTechPath = path + Calculation.TO.Main.Properties.Resources.InvoTechPath;
            //Directory.SetCurrentDirectory(dllInvoTechPath);
            //LoadDllInvoTech(pathDll);
            //Directory.SetCurrentDirectory(programPath);
        }

        public void SetParams(double i_TEvap, double i_TOvrH, double i_TCond, double i_TSubC)
        {
            // Температура кипения С
            SEEvaporatingTemperature = (decimal)i_TEvap;

            // Перегрев всас. газа К
            SESuctionGasSuperheat = (decimal)i_TOvrH;

            // Температура конденсации С
            SECondensingTemperature = (decimal)i_TCond;

            // Переохлаждение К
            SELiquidSubcooling = (decimal)i_TSubC;
        }

        /// <summary>
        /// Подбор компрессора
        /// </summary>
        /// <param name="totCap"></param>
        /// <param name="dxCxMas"></param>
        public (SelectCompressors, List<SelectCompressors>) SelectCompessors(double totCap, double dxCxMas)
        {
            List<SelectCompressors> compressors = new List<SelectCompressors>();
            SelectedPowerSupply = PowerSupply[0];
            List_U_Hz_P = new List<string>(GetListHz());
            SelectedList_U_Hz_P = List_U_Hz_P[2];
            SelectCompressors selectCompessors = new SelectCompressors();
            SelectCompressors compessor;
            PerformanceTableV = GetPerformanceTables();
            if (PerformanceTableV == null) return (null, null);
            double total = Double.MaxValue;
            double refCap = 0;
            double masFlow = 0;
            double heatReject = 0;
            foreach (var perf in PerformanceTableV)
            {
                if (perf.Model.Contains("YS")) continue;
                if (perf.Model.Contains("YH95")) continue;
                if (perf.Model.Contains("YH119")) continue;
                if (perf.Model.Contains("YH183")) continue;
                if (perf.Model.Contains("YH230")) continue;
                if (perf.Model.Contains("YH325")) continue;
                if (perf.Model.Contains("YH610")) continue;
                compressors.Add( new SelectCompressors()
                {
                    Manufacturer = Calculation.TO.Main.Properties.Resources.InvoTech,
                    Compessor = perf.Model,
                    RefrigerationCapacity = perf.Capability3,
                    PowerInput = perf.Capability4,
                    COP = perf.Capability5,
                    Current = perf.Capability6,
                    MassFlow = perf.Capability7,
                    HeatRejection = perf.Capability8,
                    Voltage = Calculation.TO.Main.Properties.Resources.Vol380V50H3,
                });

                if (perf != null && !string.IsNullOrEmpty(perf.Capability3))
                {
                    // Capability3- Refrigeration capacity 
                    //if (Double.TryParse(perf.Capability3, style, StaticData.ci, out refCap) && totCap < refCap)
                    // Capability8 - Heat Rejection
                    heatReject = GS.StringToDouble(perf.Capability8);
                    if (totCap < heatReject)
                    {
                        // Capability7- MassFlow 
                        //if (Double.TryParse(perf.Capability7, style, StaticData.ci, out masFlow))
                        {
                            if (heatReject < total)
                            //if (refCap < total)
                            {
                                selectCompessors.Manufacturer = Calculation.TO.Main.Properties.Resources.InvoTech;
                                selectCompessors.Compessor = perf.Model;
                                selectCompessors.RefrigerationCapacity = perf.Capability3;
                                selectCompessors.PowerInput = perf.Capability4;
                                selectCompessors.COP = perf.Capability5;
                                selectCompessors.Current = perf.Capability6;
                                selectCompessors.MassFlow = perf.Capability7;
                                selectCompessors.HeatRejection = perf.Capability8;
                                selectCompessors.Voltage = Calculation.TO.Main.Properties.Resources.Vol380V50H3;
                                //total = refCap;
                                total = heatReject;
                            }
                        }
                    }
                }
            }            
            Remove(compressors, selectCompessors);
            compressors.Sort();
            return (selectCompessors, compressors);
        }
        #endregion

        #region Приватные методы

        /// <summary>
        /// Загрузка dll
        /// </summary>
        /// <param name="path"></param>
        private void LoadDllInvoTech(string path)
        {
            Assembly asm = Assembly.LoadFrom(path);
            t = asm.GetType("InvoTech.InvoTechMain", true, true);
            // создаем экземпляр класса Program
            instanceClass = Activator.CreateInstance(t);
            LoadParams();
        }

        /// <summary>
        /// Загрузка параметров из библиотеки InvoTech.dll
        /// </summary>
        private void LoadParams()
        {
            RunMetod("InitData", null);
            PowerSupply = new List<string>(GetPowerSupply());  // 50Hz  60Hz
            SelectedPowerSupply = PowerSupply[0];
            List_U_Hz_P = new List<string>(GetListHz());
            SelectedList_U_Hz_P = GetSelectedList_U_Hz_P(); // 220V/50Hz/1P  220V/50Hz/3P   380V/50Hz/3P
            CbeModel = GetComperssors();
            Models = new List<string>(GetCompressorModels());
            SelectedModel = Models[0];
            fRun = true;
        }

        /// <summary>
        /// Получить список частот
        /// </summary>
        /// <returns></returns>
        private List<string> GetPowerSupply()
        {
            Object obj = RunMetod("GetPowerSupply", null);
            lstParam2 = obj as List<CodeInfoV>;
            if (lstParam2 != null)
            {
                List<string> list = new List<string>();
                foreach (CodeInfoV c in lstParam2)
                {
                    list.Add(c.CodeName);
                }
                return list;
            }
            return null;
        }

        /// <summary>
        /// Изменение текущей частоты
        /// </summary>
        /// <param name="value"></param>
        private void SetPowerSupply(string value)
        {
            Object[] objects = new Object[1];
            objects[0] = value;
            RunMetod("SetPowerSupply", objects);
        }

        /// <summary>
        /// Вызов метода
        /// </summary>
        /// <param name="name"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        private object RunMetod(string name, object[] objects)
        {
            // получаем метод 
            MethodInfo method = t.GetMethod(name);

            // вызываем метод, передаем ему значения для параметров и получаем результат
            return method.Invoke(instanceClass, objects);
        }

        /// <summary>
        /// Поле Suction Gas Temperature(°C):
        /// </summary>
        /// <returns></returns>
        private decimal GetSESuctionGasTemperature()
        {
            Object obj = RunMetod("GetSESuctionGasTemperature", null);
            return (decimal)obj;
        }

        /// <summary>
        /// Поле Liquid Subcooling(K):
        /// </summary>
        /// <returns></returns>
        private decimal GetSELiquidSubcooling()
        {
            Object obj = RunMetod("GetSELiquidSubcooling", null);
            return (decimal)obj;
        }

        /// <summary>
        /// Поле Suction Gas Superheat(K):
        /// </summary>
        /// <returns></returns>
        private decimal GetSESuctionGasSuperheat()
        {
            Object obj = RunMetod("GetSESuctionGasSuperheat", null);
            return (decimal)obj;
        }

        /// <summary>
        /// поле Liquid Temperature(°C)：
        /// </summary>
        /// <returns></returns>
        private decimal GetSELiquidTemperature()
        {
            Object obj = RunMetod("GetSELiquidTemperature", null);
            return (decimal)obj;
        }

        /// <summary>
        /// получить поле Evaporating Pressure (Gage)(barg):
        /// </summary>
        /// <returns></returns>
        private double GetTEEvaporatingPressure()
        {
            Object obj = RunMetod("GetTEEvaporatingPressure", null);
            return (double)obj;
        }

        /// <summary>
        /// получить поле Condensing Pressure (Gage)(barg):
        /// </summary>
        /// <returns></returns>
        private double GetTECondensingPressureGageBarg()
        {
            Object obj = RunMetod("GetTECondensingPressureGageBarg", null);
            return (double)obj;
        }

        /// <summary>
        /// вернуть список "напряжение/частота/кол-во полюсов"
        /// пример "380V/50Hz/3P"
        /// </summary>
        /// <returns></returns>
        private List<string> GetListHz()
        {
            Object obj = RunMetod("GetListHz", null);
            return (List<string>)obj;
        }

        /// <summary>
        /// Вернуть выбранный элемент
        /// "напряжение/частота/кол-во полюсов"
        /// пример "380V/50Hz/3P"
        /// </summary>
        /// <returns></returns>
        private string GetSelectedList_U_Hz_P()
        {
            Object obj = RunMetod("GetSelectedList_U_Hz_P", null);
            return (string)obj;
        }

        /// <summary>
        /// Изменить выбранный элемент
        /// "напряжение/частота/кол-во полюсов"
        /// </summary>
        /// <param name="value"></param>
        private void SetSelected_U_Hz_P(string value)
        {
            Object[] objects = new Object[1];
            objects[0] = value;
            RunMetod("SetSelected_U_Hz_P", objects);
        }

        /// <summary>
        /// вернуть список копрессоров
        /// </summary>
        /// <returns></returns>
        private List<CodeInfoV> GetComperssors()
        {
            Object obj = RunMetod("GetComperssors", null);
            return (List<CodeInfoV>)obj;
        }

        /// <summary>
        /// Преобразовать список моделей компресоров в список string
        /// </summary>
        /// <returns></returns>
        private List<string> GetCompressorModels()
        {
            List<string> models = new List<string>();
            foreach (var model in CbeModel)
            {
                models.Add(model.CodeName);
            }
            return models;
        }

        /// <summary>
        /// Возвращяет список выходных параметров для комрессоров
        /// </summary>
        /// <returns></returns>
        private List<InvoCapabilityV> GetPerformanceTables()
        {
            Object obj = RunMetod("GetPerformanceTables", null);
            return (List<InvoCapabilityV>)obj;
        }

        /// <summary>
        /// установка выходных свойств компрессора
        /// </summary>
        private void SetParamsPerformanceTableToProperties()
        {
            if (PerformanceTableV == null) return;
            foreach (var perf in PerformanceTableV)
            {
                if (SelectedModel == perf.Model)
                {
                    OperatingConditions = perf.Capability1;
                    Compessor = perf.Model;
                    RefrigerationCapacity = perf.Capability3;
                    PowerInput = perf.Capability4;
                    COP = perf.Capability5;
                    Current = perf.Capability6;
                    MassFlow = perf.Capability7;
                    HeatRejection = perf.Capability8;
                    break;
                }
            }
        }

        /// <summary>
        /// Изменение частоты
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectedPowerSupply(string value)
        {
            if (fRun)
            {
                SetPowerSupply(value);

                List_U_Hz_P = new List<string>(GetListHz());
                SelectedList_U_Hz_P = GetSelectedList_U_Hz_P();
                CbeModel = GetComperssors();
                Models = new List<string>(GetCompressorModels());
                SelectedModel = Models[0];
                PerformanceTableV = GetPerformanceTables();
                SetParamsPerformanceTableToProperties();
            }
        }

        /// <summary>
        /// Изменение выбранного элемента "напряжение/частота/кол-во полюсов"
        /// </summary>
        /// <param name="value"></param>
        private void SetSelectedList_U_Hz_P(string value)
        {
            if (fRun)
            {
                if (value != null)
                {
                    SetSelected_U_Hz_P(value);

                    CbeModel = GetComperssors();
                    Models = new List<string>(GetCompressorModels());
                    SelectedModel = Models[0];
                    PerformanceTableV = GetPerformanceTables();
                    SetParamsPerformanceTableToProperties();
                }
            }
        }

        /// <summary>
        /// Изменение поля Evaporating Temperature
        /// </summary>
        /// <param name="value"></param>
        private void SEEvaporatingTemperature_EditValueChanged(decimal value)
        {
            Object[] objects = new Object[1];
            objects[0] = value;
            RunMetod("SEEvaporatingTemperature_EditValueChanged", objects);
            sESuctionGasSuperheat = GetSESuctionGasSuperheat();
            TEEvaporatingPressure = GetTEEvaporatingPressure();
            PerformanceTableV = GetPerformanceTables();

            sESuctionGasTemperature = GetSESuctionGasTemperature();
            SetParamsPerformanceTableToProperties();
        }

        /// <summary>
        /// Изменение поля Suction Gas Temperature(°C)
        /// </summary>
        /// <param name="value"></param>
        private void SESuctionGasTemperature_EditValueChanged(decimal value)
        {
            Object[] objects = new Object[1];
            objects[0] = value;
            RunMetod("SESuctionGasTemperature_EditValueChanged", objects);
            sESuctionGasSuperheat = GetSESuctionGasSuperheat();
            PerformanceTableV = GetPerformanceTables();
            SetParamsPerformanceTableToProperties();
        }

        /// <summary>
        /// Изменение поля Suction Gas Superheat(K):
        /// </summary>
        /// <param name="value"></param>
        private void SESuctionGasSuperheat_EditValueChanged(decimal value)
        {
            Object[] objects = new Object[1];
            objects[0] = value;
            RunMetod("SESuctionGasSuperheat_EditValueChanged", objects);
            sESuctionGasTemperature = GetSESuctionGasTemperature();
            PerformanceTableV = GetPerformanceTables();
            SetParamsPerformanceTableToProperties();
        }

        /// <summary>
        /// Изменение поле Condensing Temperature(°C)
        /// </summary>
        private void SECondensingTemperature_EditValueChanged(decimal value)
        {
            Object[] objects = new Object[1];
            objects[0] = value;
            RunMetod("SECondensingTemperature_EditValueChanged", objects);
            sELiquidTemperature = GetSELiquidTemperature();
            TECondensingPressureGageBarg = GetTECondensingPressureGageBarg();
            PerformanceTableV = GetPerformanceTables();

            sELiquidSubcooling = GetSELiquidSubcooling();
            SetParamsPerformanceTableToProperties();
        }

        /// <summary>
        /// Изменение поля Liquid Subcooling(K):
        /// </summary>
        private void SELiquidSubcooling_EditValueChanged(decimal value)
        {
            Object[] objects = new Object[1];
            objects[0] = value;
            RunMetod("SELiquidSubcooling_EditValueChanged", objects);
            sELiquidTemperature = GetSELiquidTemperature();
            PerformanceTableV = GetPerformanceTables();
            SetParamsPerformanceTableToProperties();
        }

        /// <summary>
        /// Изменение поля Liquid Temperature(°C)：
        /// </summary>
        private void SELiquidTemperature_EditValueChanged(decimal value)
        {
            Object[] objects = new Object[1];
            objects[0] = value;
            RunMetod("SELiquidTemperature_EditValueChanged", objects);
            sELiquidSubcooling = GetSELiquidSubcooling();
            PerformanceTableV = GetPerformanceTables();
            SetParamsPerformanceTableToProperties();
        }

        /// <summary>
        /// Удалить элемент из списка
        /// </summary>
        /// <param name="compressors">Список </param>
        /// <param name="selectCompessors">Удаляемый элемент</param>
        private void Remove(List<SelectCompressors> compressors, SelectCompressors selectCompessors)
        {
            int delIndex = -1;
            for(int i = 0; i < compressors.Count; i++)
            {
                if (compressors[i].Compessor == selectCompessors.Compessor)
                {
                    delIndex = i;
                    break;
                }
            }
            if (delIndex > -1) compressors.RemoveAt(delIndex);
        }
        
        #endregion
    }
}
