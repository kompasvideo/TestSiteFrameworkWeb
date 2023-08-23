using System.Collections.Generic;

namespace Veza.HeatExchanger.Models.Main
{
    public class GetDataCondensatorDTO
    {
        /// <summary>
        /// Тип расчёта - direct, reverse
        /// </summary>
        public string SelectedMode { get; set; }
        /// <summary>
        /// список Тип расчёта - direct, reverse
        /// </summary>
        public List<string> ListMode { get; set; }
        /// <summary>
        /// Расчёт через мощность или температура воздуха на выходе
        /// </summary>
        public string SelectedDirectMode { get; set; }
        /// <summary>
        /// список Вариант расчёта - Мощность, Температура воздуха на выходе
        /// </summary>
        public List<string> ListDirectMode { get; set; }
        /// <summary>
        /// Мощность
        /// </summary>
        public string ValueCapacity { get; set; }
        /// <summary>
        /// Мощность граничные условия
        /// </summary>
        public List<double> ValueCapacityMinMax { get; set; } = new List<double> { 0.5, 50_000 };
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
        public string DirectAirTempOutHW { get; set; }
        /// <summary>
        /// Геометрия
        /// </summary>
        public string SelectGeometry { get; set; }
        /// <summary>
        /// Список геометрий
        /// </summary>
        public List<string> ListGeometries { get; set; }
        /// <summary>
        /// Длина оребрения
        /// </summary>
        public string ValueWidthFin { get; set; }
        /// <summary>
        /// Длина оребрения граничные условия
        /// </summary>
        public List<int> ValueWidthFinMinMax { get; set; } = new List<int> { 100, 12_000 };
        /// <summary>
        /// Высота оребрения
        /// </summary>
        public string ValueHightFin { get; set; }
        /// <summary>
        /// Высота оребрения граничные условия
        /// </summary>
        public List<int> ValueHightFinMinMax { get; set; } = new List<int> { 100, 2_500 };
        /// <summary>
        /// Число труб
        /// </summary>
        public string TubesN { get; set; }
        /// <summary>
        /// Число труб граничные условия
        /// </summary>
        public List<int> TubesNMinMax { get; set; } = new List<int> { 1, 1_000 };
        /// <summary>
        /// Трубка
        /// </summary>
        public string SelectedPipe { get; set; }
        /// <summary>
        /// Список трубок
        /// </summary>
        public List<string> ListPipes { get; set; }
        /// <summary>
        /// Толщина оребрения
        /// </summary>
        public string SelectedFin { get; set; }
        /// <summary>
        /// Список толщин оребрения
        /// </summary>
        public List<string> ListFins { get; set; }
        /// <summary>
        /// Шаг оребрения
        /// </summary>
        public string SelectedStepFin { get; set; }
        /// <summary>
        /// Список шагов оребрения
        /// </summary>
        public List<string> ListStepsFin { get; set; }
        /// <summary>
        /// Число рядов
        /// </summary>
        public string ValuePipe { get; set; }
        /// <summary>
        /// Число рядов граничные условия
        /// </summary>
        public List<int> ValuePipeMinMax { get; set; } = new List<int> { 1, 1_000 };
        /// <summary>
        /// Количество отводов
        /// </summary>
        public string ValueCircuits { get; set; }
        /// <summary>
        /// Количество отводов граничные условия
        /// </summary>
        public List<int> ValueCircuitsMinMax { get; set; } = new List<int> { 1, 1_000 };
        /// <summary>
        /// Число ходов
        /// </summary>
        public string NumOfPasses { get; set; }
        /// <summary>
        /// Число ходов граничные условия
        /// </summary>
        public List<int> NumOfPassesMinMax { get; set; } = new List<int> { 1, 1_000 };
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public string ValueAirFlow { get; set; }
        /// <summary>
        /// Расход воздуха граничные условия
        /// </summary>
        public List<int> ValueAirFlowMinMax { get; set; } = new List<int> { 50, 1_000_000 };
        /// <summary>
        /// Список единиц измерения расхода воздуха
        /// </summary>
        public List<string> ListUnitsAirFlow { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public string I_AirTempIn { get; set; }
        /// <summary>
        /// Температура воздуха на входе граничные условия
        /// </summary>
        public List<int> I_AirTempInMinMax { get; set; } = new List<int> { -100, 400 };
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public string ValueBaseHum { get; set; }
        /// <summary>
        /// Влажность воздуха на входе граничные условия
        /// </summary>
        public List<int> ValueBaseHumMinMax { get; set; } = new List<int> { 0, 100 };
        /// <summary>
        /// Тип хладогента
        /// </summary>
        public string Selected_I_RefT { get; set; }
        /// <summary>
        /// Список хладогентов
        /// </summary>
        public List<string> ListI_RefT { get; set; }
        /// <summary>
        /// Коэффициент загрязнения трубы
        /// </summary>
        public string Selected_I_FoulingI { get; set; }
        /// <summary>
        /// Список коэффициентов загрязнения трубы
        /// </summary>
        public List<string> ListI_FoulingI { get; set; }
        /// <summary>
        /// Температура горячего газа
        /// </summary>
        public string I_THotGasCX { get; set; }
        /// <summary>
        /// Температура горячего газа граничные условия
        /// </summary>
        public List<int> I_THotGasCXMinMax { get; set; } = new List<int> { 0, 100 };
        /// <summary>
        /// список - температура конденсации или давление конденсации абс.
        /// </summary>
        public string SelectCondTemp { get; set; }
        /// <summary>
        /// список - температа конденсации, давление конденсации абс
        /// </summary>
        public List<string> ListCondensingTemperature { get; set; }
        /// <summary>
        /// температура конденсации - значение
        /// </summary>
        public string I_TCondCX { get; set; }
        /// <summary>
        /// Температура конденсации граничные условия
        /// </summary>
        public List<int> I_TCondCXMinMax { get; set; } = new List<int> { -100, 200 };
        /// <summary>
        /// давление конденсации абс. - значение 
        /// </summary>
        public string CondAbsPresCX { get; set; }
        /// <summary>
        /// Давление конденсации граничные условия
        /// </summary>
        public List<int> CondAbsPresCXMinMax { get; set; } = new List<int> { -100, 200 };
        /// <summary>
        /// список переохлаждение или температура жидкости
        /// </summary>
        public string SelectSubCool { get; set; }
        /// <summary>
        /// список переохлаждение или температура жидкости
        /// </summary>
        public List<string> ListSubCooling { get; set; }
        /// <summary>
        /// переохлаждение - значение
        /// </summary>
        public string I_TSubCCX { get; set; }
        /// <summary>
        /// Температура переохлаждения граничные условия
        /// </summary>
        public List<int> I_TSubCCXMinMax { get; set; } = new List<int> { -100, 200 };
        /// <summary>
        /// температура жидкости - значение
        /// </summary>
        public string LiquidTempCX { get; set; }
        /// <summary>
        /// Температура жидкости граничные условия
        /// </summary>
        public List<int> LiquidTempCXMinMax { get; set; } = new List<int> { -100, 200 };


        // характеристики корпуса и коллектора
        /// <summary>
        /// Количество коллекторов
        /// </summary>
        public string I_MatHdr { get; set; }
        /// <summary>
        /// количество коллекторов граничные условия
        /// </summary>
        public List<int> I_MatHdrMinMax { get; set; } = new List<int> { 0, 100 };
        /// <summary>
        /// Материал коллектора
        /// </summary>
        public string Selected_I_MatHdr { get; set; }
        /// <summary>
        /// список материалов корпуса
        /// </summary>
        public List<string> ListI_MatHdr { get; set; }
        /// <summary>
        /// Диаметр коллектора на входе
        /// </summary>
        public string Selected_I_ConIn { get; set; }
        /// <summary>
        /// Список диаметров коллектора на входе
        /// </summary>
        public List<string> ListI_ConIn { get; set; }
        /// <summary>
        /// Диаметр коллектора на выходе
        /// </summary>
        public string Selected_I_ConOut { get; set; }
        /// <summary>
        /// Список диаметров коллектора на выходе
        /// </summary>
        public List<string> ListI_ConOut { get; set; }
        /// <summary>
        /// Тип патрубков
        /// </summary>
        public string Selected_I_ConType { get; set; }
        /// <summary>
        /// список типов патрубков
        /// </summary>
        public List<string> ListI_ConType { get; set; }
        /// <summary>
        /// Припуск на обработку по дине коллектора
        /// </summary>
        public string I_CSheetL { get; set; }
        /// <summary>
        /// Исполнение корпуса
        /// </summary>
        public string SelectedCasingType { get; set; }
        /// <summary>
        /// Список исполения корпуса
        /// </summary>
        public List<string> ListCasingType { get; set; }
        /// <summary>
        /// Материал корпуса
        /// </summary>
        public string Selected_I_CasMat { get; set; }
        /// <summary>
        /// Список материалов корпуса
        /// </summary>
        public List<string> ListI_CasMat { get; set; }
        /// <summary>
        /// Ориентация теплообменника
        /// </summary>
        public string SelectedEsapo { get; set; }
        /// <summary>
        /// список ориентаций теплообменника
        /// </summary>
        public List<string> ListEsapo { get; set; }
        /// <summary>
        /// Ориентация патрубков
        /// </summary>
        public string Select_I_CDir { get; set; }
        /// <summary>
        /// список ориентаций патрубков
        /// </summary>
        public List<string> ListI_CDir { get; set; }
        /// <summary>
        /// Направление потока воздуха
        /// </summary>
        public string SelectedAirFlowDirection { get; set; }
        /// <summary>
        /// список направлений потока воздуха
        /// </summary>
        public List<string> ListAirFlowDirection { get; set; }
        /// <summary>
        /// Подключение теплоносителя
        /// </summary>
        public string SelectedConnectingCoolant { get; set; }
        /// <summary>
        /// список подключений теплоносителя
        /// </summary>
        public List<string> ListConnectingCoolant { get; set; }


        // Дополнительные параметры
        /// <summary>
        /// Давление окружающего воздуха
        /// </summary>
        public string BaseBar { get; set; }
        /// <summary>
        /// Давление окружающего воздуха граничные условия
        /// </summary>
        public List<double> BaseBarMinMax { get; set; } = new List<double> { 0, 100_000 };
        /// <summary>
        /// Давление окружающего воздуха - единица измерения
        /// </summary>
        public string SelectGasWorkPressUnit { get; set; }
        /// <summary>
        /// список единиц измерения давления окр. воздуха
        /// </summary>
        public List<string> ListGasWorkPressUnit { get; set; }
        /// <summary>
        /// Плотность окружающего воздуха
        /// </summary>
        public string I_BaseDens { get; set; }
        /// <summary>
        /// Плотность окружающего воздуха граничные условия
        /// </summary>
        public List<double> I_BaseDensMinMax { get; set; } = new List<double> { 0, 100 };
        /// <summary>
        /// Запас поверхности
        /// </summary>
        public string I_ARes { get; set; }
        /// <summary>
        /// Запас поверхности граничные условия
        /// </summary>
        public List<int> I_AResMinMax { get; set; } = new List<int> { 0, 100 };
        /// <summary>
        /// Резерв мощности
        /// </summary>
        public string I_LRes { get; set; }
        /// <summary>
        /// Резерв мощности граничные условия
        /// </summary>
        public List<int> I_LResMinMax { get; set; } = new List<int> { 0, 100 };
        /// <summary>
        /// коэффициент загрязения трубы
        /// </summary>
        //public string Select_I_FoulingI { get; set; }     //  для конднсатора он есть восновных параметрах
        /// <summary>
        /// Список коэффициентов загрязнения трубы
        /// </summary>
        /// <returns></returns>
        //public List<string> ListI_FoulingI { get; set; }
        /// <summary>
        /// коэффициент загрязения воздуха
        /// </summary>
        public string Select_I_FoulingE { get; set; }  
        /// <summary>
        /// Список коэффициентов загрязнения воздуха
        /// </summary>
        /// <returns></returns>
        public List<string> ListI_FoulingE { get; set; }
        /// <summary>
        /// автоматический шаг рёбер
        /// </summary>
        public string SelectAFPV { get; set; }
        /// <summary>
        /// Список автоматический шаг рёбер
        /// </summary>
        /// <returns></returns>
        public List<string> ListAFPV { get; set; }
        
    }
}
