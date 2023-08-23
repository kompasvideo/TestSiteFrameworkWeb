using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Veza.HeatExchanger.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Veza.HeatExchanger.ViewModels.Controls
{
    /// <summary>
    /// ViewModel для страницы Frames\AdvanceParams (содержит привязанные свойства)
    /// </summary>
    sealed public class AdvanceParamsViewModel 
    {
        #region Свойства привязанные к Xaml
        /// <summary>
        /// Давление окружающего воздуха - еденица измерения
        /// </summary>
        public ObservableCollection<String> GasWorkPressUnit { get; set; }
        public string SelectGasWorkPressUnit { get; set; }
        /// <summary>
        /// Давление окружающего воздуха
        /// </summary>
        public double I_BaseBar 
        { 
            get
            {
                if (SelectGasWorkPressUnit == Calculation.TO.Main.Properties.Resources.GasWorkPressUnitVBar)
                {
                    return BaseBar * 1000;
                }
                if (SelectGasWorkPressUnit == Calculation.TO.Main.Properties.Resources.GasWorkPressUnitVPa)
                {
                    return BaseBar * 100;
                }
                if (SelectGasWorkPressUnit == Calculation.TO.Main.Properties.Resources.GasWorkPressUnitVKPa)
                {
                    return BaseBar * 100_000;
                }
                return BaseBar;
            }
        }
        public double BaseBar { get; set; } = 1013.25;
        /// <summary>
        /// Плотность окружающего воздуха, кг/м³
        /// </summary>
        public double I_BaseDens { get; set; } = 1.2;
        /// <summary>
        /// Запас поверхности
        /// </summary>
        public double I_ARes { get; set; } = 0;
        /// <summary>
        /// Резерв мощности
        /// </summary>
        public double I_LRes { get; set; } = 0;
        /// <summary>
        /// Коэффициент загрязнения трубы
        /// </summary>
        public double I_FoulingI { get; set; } = 0;
        public ObservableCollection<String> I_FoulingI_List { get; set; }
        public string Select_I_FoulingI { set; get; }
        /// <summary>
        /// коэффициент загрязнения воздуха
        /// </summary>
        public double I_FoulingE { get; set; } = 0;
        public ObservableCollection<String> I_FoulingE_List { get; set; }
        public string Select_I_FoulingE { set; get; }
        /// <summary>
        ///  Автоматический шаг рёбер
        /// </summary>
        public ObservableCollection<String> AFPV { get; set; }
        public string SelectAFPV { get; set; }       

        #endregion

        #region Конструктор
        public AdvanceParamsViewModel()
        {
            SetPropertiesChangeLang();
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Установка свойств при смене языка
        /// </summary>
        public void SetPropertiesChangeLang()
        {
            // Давление окружающего воздуха - еденица измерения
            GasWorkPressUnit = new ObservableCollection<string>();
            GasWorkPressUnit.Add(Calculation.TO.Main.Properties.Resources.GasWorkPressUnitV);
            GasWorkPressUnit.Add(Calculation.TO.Main.Properties.Resources.GasWorkPressUnitVBar);
            GasWorkPressUnit.Add(Calculation.TO.Main.Properties.Resources.GasWorkPressUnitVPa);
            GasWorkPressUnit.Add(Calculation.TO.Main.Properties.Resources.GasWorkPressUnitVKPa);
            SelectGasWorkPressUnit = Calculation.TO.Main.Properties.Resources.GasWorkPressUnitV;

            // автоматический шаг ребер
            AFPV = new ObservableCollection<string>();
            AFPV.Add(Calculation.TO.Main.Properties.Resources.False);
            AFPV.Add(Calculation.TO.Main.Properties.Resources.True);
            SelectAFPV = Calculation.TO.Main.Properties.Resources.False;

            // коэффициент загрязнения воздуха
            I_FoulingE_List = new ObservableCollection<String>();
            I_FoulingE_List.Add(Calculation.TO.Main.Properties.Resources.FoulAirClean);
            I_FoulingE_List.Add(Calculation.TO.Main.Properties.Resources.FoulAirF5);
            I_FoulingE_List.Add(Calculation.TO.Main.Properties.Resources.FoulAirG3);
            I_FoulingE_List.Add(Calculation.TO.Main.Properties.Resources.FoulAirNo);
            I_FoulingE_List.Add(Calculation.TO.Main.Properties.Resources.FoulAirIndustry);
            Select_I_FoulingE = Calculation.TO.Main.Properties.Resources.FoulAirClean;

            // Коэффициент загрязнения трубы
            I_FoulingI_List = new ObservableCollection<String>();
            I_FoulingI_List.Add(Calculation.TO.Main.Properties.Resources.FoulH2ODW);
            I_FoulingI_List.Add(Calculation.TO.Main.Properties.Resources.FoulH2OHSW);
            I_FoulingI_List.Add(Calculation.TO.Main.Properties.Resources.FoulH2OCDW);
            I_FoulingI_List.Add(Calculation.TO.Main.Properties.Resources.FoulH2OGW);
            I_FoulingI_List.Add(Calculation.TO.Main.Properties.Resources.FoulH2OSO);
            Select_I_FoulingI = Calculation.TO.Main.Properties.Resources.FoulH2ODW;
        }

        /// <summary>
        /// I_FoulingE коэффициент загрязнения воздуха
        /// </summary>
        /// <returns></returns>
        public int Get_I_FoulingE()
        {
            if (Select_I_FoulingE == Calculation.TO.Main.Properties.Resources.FoulAirF5)
                return 1;
            if (Select_I_FoulingE == Calculation.TO.Main.Properties.Resources.FoulAirG3)
                return 2;
            if (Select_I_FoulingE == Calculation.TO.Main.Properties.Resources.FoulAirNo)
                return 3;
            if (Select_I_FoulingE == Calculation.TO.Main.Properties.Resources.FoulAirIndustry)
                return 4;
            /// Calculation.TO.Main.Properties.Resources.FoulAirClean
            return 0;
        }

        public void Set_I_FoulingE(int i)
        {
            switch (i)
            {
                case 0:
                    Select_I_FoulingE = Calculation.TO.Main.Properties.Resources.FoulAirClean;
                    break;
                case 1:
                    Select_I_FoulingE = Calculation.TO.Main.Properties.Resources.FoulAirF5;
                    break;
                case 2:
                    Select_I_FoulingE = Calculation.TO.Main.Properties.Resources.FoulAirG3;
                    break;
                case 3:
                    Select_I_FoulingE = Calculation.TO.Main.Properties.Resources.FoulAirNo;
                    break;
                case 4:
                    Select_I_FoulingE = Calculation.TO.Main.Properties.Resources.FoulAirIndustry;
                    break;
            }
        }

        /// <summary>
        /// I_FoulingI Коэффициент загрязнения трубы
        /// </summary>
        /// <returns></returns>
        public int Get_I_FoulingI()
        {
            if (Select_I_FoulingI == Calculation.TO.Main.Properties.Resources.FoulH2OHSW)
                return 1;
            if (Select_I_FoulingI == Calculation.TO.Main.Properties.Resources.FoulH2OCDW)
                return 2;
            if (Select_I_FoulingI == Calculation.TO.Main.Properties.Resources.FoulH2OGW)
                return 3;
            if (Select_I_FoulingI == Calculation.TO.Main.Properties.Resources.FoulH2OSO)
                return 4;
            /// Calculation.TO.Main.Properties.Resources.FoulH2OHSW
            return 0;
        }

        public void Set_I_FoulingI(int i)
        {
            switch (i)
            {
                case 0:
                    Select_I_FoulingI = Calculation.TO.Main.Properties.Resources.FoulH2ODW;
                    break;
                case 1:
                    Select_I_FoulingI = Calculation.TO.Main.Properties.Resources.FoulH2OHSW;
                    break;
                case 2:
                    Select_I_FoulingI = Calculation.TO.Main.Properties.Resources.FoulH2OCDW;
                    break;
                case 3:
                    Select_I_FoulingI = Calculation.TO.Main.Properties.Resources.FoulH2OGW;
                    break;
                case 4:
                    Select_I_FoulingI = Calculation.TO.Main.Properties.Resources.FoulH2OSO;
                    break;
            }
        }
        /// <summary>
        /// Установить давление окружающего воздуха
        /// </summary>
        /// <param name="param"></param>
        public void SetBaseBar(string param)
        {
            BaseBar = GS.StringToDouble(param);
        }

        public string GetBaseBar()
        {
            return BaseBar.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установить плотность окружающего воздуха
        /// </summary>
        /// <param name="param"></param>
        public void SetI_BaseDens(string param)
        {            
           I_BaseDens = GS.StringToDouble(param);            
        }
        public string GetI_BaseDens()
        {
            return I_BaseDens.ToString("G", CultureInfo.InvariantCulture); 
        }
        /// <summary>
        /// Запас поверхности
        /// </summary>
        /// <param name="param"></param>
        public void SetI_ARes(string param)
        {            
            I_ARes = GS.StringToDouble(param);            
        }
        public string GetI_ARes()
        {
            return I_ARes.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Резерв мощности
        /// </summary>
        /// <param name="param"></param>
        public void SetI_LRes(string param)
        {
             I_LRes = GS.StringToDouble(param);            
        }
        public string GetI_LRes()
        {
            return I_LRes.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// список единиц измерения давления окр. воздуха
        /// </summary>
        public List<string> GetListGasWorkPressUnit()
        {
            return GasWorkPressUnit.ToList();
        }
        /// <summary>
        /// Список коэффициентов загрязнения трубы
        /// </summary>
        /// <returns></returns>
        public List<string> GetListI_FoulingI()
        {
            return I_FoulingI_List.ToList();
        }
        /// <summary>
        /// Список коэффициентов загрязнения воздуха
        /// </summary>
        /// <returns></returns>
        public List<string> GetListI_FoulingE()
        {
            return I_FoulingE_List.ToList();
        }
        /// <summary>
        /// Список автоматический шаг рёбер
        /// </summary>
        /// <returns></returns>
        public List<string> GetListAFPV()
        {
            return AFPV.ToList();
        }
        #endregion
    }
}
