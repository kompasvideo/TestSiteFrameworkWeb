namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// класс с свойствами, которые должны сохраняться при сохранении расчёта
    /// </summary>
    sealed public class SaveCodes
    {
        /// <summary>
        /// Тип расчёта(воздухонагреватель, воздухоохладитель и т.д.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Производственный код
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Производственный код короткий (для клиентов)
        /// </summary>
        public string ShortCode { get; set; }
        /// <summary>
        /// Высота оребрения
        /// </summary>
        public double ValueHightFin { get; set; }
        /// <summary>
        /// Требуемая мощность
        /// </summary>
        public double CapacityDX { get; set; }
        /// <summary>
        /// Конструкция/Подключение змеевика
        /// </summary>
        public string Esapo { get; set; }        
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double ValueAirFlowDX { get; set; }
        /// <summary>
        /// Расход воздуха - единица измерения
        /// </summary>
        public string SelectedAirFlow { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInDX { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double ValueBaseHumDX { get; set; }
        /// <summary>
        /// Теплоноситель
        /// </summary>
        public string SelectedFluid { get; set; }
        /// <summary>
        /// Температура теплоносителя на входе
        /// </summary>
        public double ValueMedTempIn { get; set; }
        /// <summary>
        /// Максимальное падение давления
        /// </summary>
        public double ValueMedKPa { get; set; }
        /// <summary>
        /// Концентрация гликоля
        /// </summary>
        public double I_SoleCnz { get; set; }
        /// <summary>
        /// Вариант расчёта - текст
        /// </summary>
        public string SelectedVariantRasch { get; set; }
        /// <summary>
        /// Вариант расчёта 
        /// </summary>
        public double ValueVariantRasch { get; set; }         
        /// <summary>
        /// Колическтво отводов
        /// </summary>
        public int ValueCircuits { get; set; }        
        /// <summary>
        /// Скорость воздуха
        /// </summary>
        public string AirVelocity { get; set; }        
        /// <summary>
        /// Перепад давления сухой воздуха 
        /// </summary>
        public string PresDropDry { get; set; }        
        /// <summary>
        /// ReverseLoad
        /// </summary>
        public string ReverseLoad { get; set; }         
        /// <summary>
        /// скорость жидкости
        /// </summary>
        public string MedVelo { get; set; }
        /// <summary>
        /// Температура воздуха на выходе
        /// </summary>
        public string AirTempOut { get; set; }
        /// <summary>
        /// Значение - мощность или температура воздуха на выходе
        /// </summary>
        public int SelectedDirect { get; set; }


        #region Воздухонагреватель
        /// <summary>
        /// Требуемая мощность Воздухонагреватель
        /// </summary>
        public double CapacityHW { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double ValueAirFlowHW { get; set; }
        /// <summary>
        /// Расход воздуха - единица измерения
        /// </summary>
        public string SelectedAirFlowHW { get; set; }
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
        /// Максимальное падение давления
        /// </summary>
        public double ValueMedKPaHW { get; set; }
        /// <summary>
        /// Вариант расчёта 
        /// </summary>
        public double ValueVariantRaschHW { get; set; }
        #endregion


        #region Воздухоохладитель
        /// <summary>
        /// Требуемая мощность Воздухоохладитель
        /// </summary>
        public double CapacityCW { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double ValueAirFlowCW { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInCW { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double ValueBaseHumCW { get; set; }
        /// <summary>
        /// Температура теплоносителя на входе
        /// </summary>
        public double ValueMedTempInCW { get; set; }
        /// <summary>
        /// Максимальное падение давления
        /// </summary>
        public double ValueMedKPaCW { get; set; }
        /// <summary>
        /// Вариант расчёта 
        /// </summary>
        public double ValueVariantRaschCW { get; set; }
        #endregion


        #region Паровой нагреватель
        /// <summary>
        /// Давление пара
        /// </summary>
        public double I_StmBar { get; set; }
        /// <summary>
        /// Температура пара
        /// </summary>
        public double I_StmTemp { get; set; }
        /// <summary>
        /// Требуемая мощность Паровой нагреватель
        /// </summary>
        public double CapacityST { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double ValueAirFlowST { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInST { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double ValueBaseHumST { get; set; }
        #endregion


        #region Конденсатор
        /// <summary>
        /// Тип хладогента
        /// </summary>
        public string Selected_I_RefT { get; set; }
        /// <summary>
        /// Коэффициент загрязнения трубы
        /// </summary>
        public string Selected_I_FoulingI { get; set; }
        /// <summary>
        /// Максимальное падение давление хладогента
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
        /// Температура конденсации
        /// </summary>
        public double I_TCondCX { get; set; }
        /// <summary>
        /// Температура кипения 
        /// </summary>
        public double I_TEvapCX { get; set; }
        /// <summary>
        /// Переохлаждение
        /// </summary>
        public double I_TSubCCX { get; set; }
        /// <summary>
        /// Перегрев
        /// </summary>
        public double I_TOvrHCX { get; set; }

        /// <summary>
        /// Требуемая мощность Конденсатор
        /// </summary>
        public double CapacityCX { get; set; }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double ValueAirFlowCX { get; set; }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempInCX { get; set; }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double ValueBaseHumCX { get; set; }
        #endregion


        #region Испаритель
        /// <summary>
        /// Количество контуров хладагента
        /// </summary>
        public int I_RCircDX { get; set; }
        /// <summary>
        /// Пропускная способность байпаса
        /// </summary>
        public double I_DxBypDX { get; set; }

        /// <summary>
        /// Максимальное падение давление хладогента
        /// </summary>
        public double I_TMaxPDX { get; set; }
        /// <summary>
        /// Температура горячего газа
        /// </summary>
        public double I_THotGasDX { get; set; }
        /// <summary>
        /// Температура всасываемого газа
        /// </summary>
        public double I_TSucGas { get; set; }
        /// <summary>
        /// Температура конденсации
        /// </summary>
        public double I_TCond { get; set; }
        /// <summary>
        /// Температура кипения 
        /// </summary>
        public double I_TEvapDX { get; set; }
        /// <summary>
        /// Переохлаждение
        /// </summary>
        public double I_TSubC { get; set; }
        /// <summary>
        /// Перегрев
        /// </summary>
        public double I_TOvrHDX { get; set; }
        #endregion
    }
}
