namespace Veza.HeatExchanger.Models.Main
{
    public class InputDataFluidHeaterCoolerDTO
    {
        /// <summary>
        /// Тип расчёта - direct, reverse
        /// </summary>
        public string SelectedMode { get; set; }
        /// <summary>
        /// Расчёт через мощность или температура воздуха на выходе
        /// </summary>
        public string SelectedDirectMode { get; set; }
        /// <summary>
        /// Мощность
        /// </summary>
        public string ValueCapacity { get; set; }
        /// <summary>
        /// Прямой расчёт по температуре воздуха на выходе
        /// </summary>
        //public string DirectAirTempOutHW { get; set; }
        /// <summary>
        /// Геометрия
        /// </summary>
        public string SelectGeometry { get; set; }
        /// <summary>
        /// Длина оребрения
        /// </summary>
        public string ValueWidthFin { get; set; }
        /// <summary>
        /// Высота оребрения
        /// </summary>
        public string ValueHightFin { get; set; }
        /// <summary>
        /// Число труб
        /// </summary>
        public string TubesN { get; set; }
        /// <summary>
        /// Трубка
        /// </summary>
        public string SelectedPipe { get; set; }
        /// <summary>
        /// Толщина оребрения
        /// </summary>
        public string SelectedFin { get; set; }
        /// <summary>
        /// Шаг оребрения
        /// </summary>
        public string SelectedStepFin { get; set; }
        /// <summary>
        /// Число рядов
        /// </summary>
        public string ValuePipe { get; set; }
        /// <summary>
        /// Количество отводов
        /// </summary>
        public string ValueCircuits { get; set; }
        /// <summary>
        /// Число ходов
        /// </summary>
        public string NumOfPasses { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public string ValueAirFlow { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public string I_AirTempIn { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public string ValueBaseHum { get; set; }
        /// <summary>
        /// Теплоноситель
        /// </summary>
        public string SelectedFluid { get; set; }
        /// <summary>
        /// Концентрация гликоля
        /// </summary>
        public string I_SoleCnz { get; set; }
        /// <summary>
        /// Температура теплоносителя на входе
        /// </summary>
        public string ValueMedTempIn { get; set; }
        /// <summary>
        /// Вариант расчёта - температура теплоносителя на выходе
        /// или расход теплоносителя
        /// </summary>
        public string SelectedVariantRasch { get; set; }
        /// <summary>
        /// значение списка SelectedVariantRasch
        /// </summary>
        public string ValueVariantRasch { get; set; }
        /// <summary>
        /// Максимальное падение давления
        /// </summary>
        public string ValueMedKPa { get; set; }


        // характеристики корпуса и коллектора
        /// <summary>
        /// Количество коллекторов
        /// </summary>
        public string I_HdrQta { get; set; }
        /// <summary>
        /// Материал коллектора
        /// </summary>
        public string Selected_I_MatHdr { get; set; }
        /// <summary>
        /// Диаметр коллектора на входе
        /// </summary>
        public string Selected_I_ConIn { get; set; }
        /// <summary>
        /// Диаметр коллектора на выходе
        /// </summary>
        public string Selected_I_ConOut { get; set; }
        /// <summary>
        /// Тип патрубков
        /// </summary>
        public string Selected_I_ConType { get; set; }
        /// <summary>
        /// Припуск на обработку по дине коллектора
        /// </summary>
        public string I_CSheetL { get; set; }
        /// <summary>
        /// Исполнение корпуса
        /// </summary>
        public string SelectedCasingType { get; set; }
        /// <summary>
        /// Материал корпуса
        /// </summary>
        public string Selected_I_CasMat { get; set; }
        /// <summary>
        /// Ориентация теплообменника
        /// </summary>
        public string SelectedEsapo { get; set; }
        /// <summary>
        /// Ориентация патрубков
        /// </summary>
        public string Select_I_CDir { get; set; }
        /// <summary>
        /// Направление потока воздуха
        /// </summary>
        public string SelectedAirFlowDirection { get; set; }
        /// <summary>
        /// Подключение теплоносителя
        /// </summary>
        public string SelectedConnectingCoolant { get; set; }

        // Дополнительные параметры
        /// <summary>
        /// Давление окружающего воздуха
        /// </summary>
        public string BaseBar { get; set; }
        /// <summary>
        /// Давление окружающего воздуха - единица измерения
        /// </summary>
        public string SelectGasWorkPressUnit { get; set; }
        /// <summary>
        /// Плотность окружающего воздуха
        /// </summary>
        public string I_BaseDens { get; set; }
        /// <summary>
        /// Запас поверхности
        /// </summary>
        public string I_ARes { get; set; }
        /// <summary>
        /// Резерв мощности
        /// </summary>
        public string I_LRes { get; set; }
        /// <summary>
        /// коэффициент загрязения трубы
        /// </summary>
        public string Select_I_FoulingI { get; set; }
        /// <summary>
        /// коэффициент загрязения воздуха
        /// </summary>
        public string Select_I_FoulingE { get; set; }
        /// <summary>
        /// автоматический шаг рёбер
        /// </summary>
        public string SelectAFPV { get; set; }
    }
}
