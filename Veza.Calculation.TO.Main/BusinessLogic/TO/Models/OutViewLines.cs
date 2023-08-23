namespace Veza.HeatExchanger.Models
{
    /// <summary>
    /// Хранит свойства столбца выходных данных программы
    /// </summary>
    sealed public class OutViewLines
    {
        /// <summary>
        /// Код производственный
        /// </summary>
        public string O_Code { get; set; }
        /// <summary>
        /// Число рядов O_Rows
        /// </summary>
        public string O_Rows { get; set; }

        /// <summary>
        /// Кол-во отводов O_Circuits
        /// </summary>
        public string O_Circuits { get; set; }
        /// <summary>
        /// Шаг оребрения O_LamAbs
        /// </summary>
        public string O_LamAbs { get; set; }
        /// <summary>
        /// скорость воздуха O_AirVelo
        /// </summary>
        public string O_AirVelo { get; set; }
        /// <summary>
        /// Перепад давления (сухое состояние) O_AirPaT
        /// </summary>
        public string O_AirPaT { get; set; }
        /// <summary>
        /// Резервная нагрузка O_LRes
        /// </summary>
        public string O_LRes { get; set; }
        /// <summary>
        /// Скорость жидкости O_MedVelo 
        /// </summary>
        public string O_MedVelo { get; set; }
        /// <summary>
        /// Перепад давления O_MedKPa
        /// </summary>
        public string O_MedKPa { get; set; }
        /// <summary>
        /// Температура воздуха на выходе O_AirTempOut
        /// </summary>
        public string O_AirTempOut { get; set; }
        /// <summary>
        /// Общая производительность
        /// </summary>
        public string O_TotCap { get; set; }
        /// <summary>
        /// Абсолютная влажность воздуха 
        /// </summary>
        public string O_AirHumInAbs { get; set; }
        /// <summary>
        /// Влажность воздуха на выходе
        /// </summary>
        public string O_AirHumOut { get; set; }
        /// <summary>
        /// Массовый расход
        /// </summary>
        public string O_Airkgs { get; set; }
        /// <summary>
        /// Температура жидкости на выходе
        /// </summary>
        public string O_MedTempOut { get; set; }
        /// <summary>
        /// Расход жидкости
        /// </summary>
        public string O_MedFlow { get; set; }
        /// <summary>
        /// Объём жидкости
        /// </summary>
        public string O_Volume { get; set; }
        /// <summary>
        /// длина оребрения
        /// </summary>
        public string O_WidthInt { get; set; }
        /// <summary>
        /// высота оребрения
        /// </summary>
        public string O_HeightInt { get; set;}        

        // Паровый нагреватель
        /// <summary>
        /// Давление пара
        /// </summary>
        public string O_StmBar { get; set; }
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

        // Конденсатор
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
        /// Абсолютная влажность воздуха на выходе O_AirTempOutAbs
        /// </summary>
        public string O_AirHumOutAbs { get; set; }
        /// <summary>
        /// Пропускная способность O_KValue
        /// </summary>
        public string O_KValue { get; set; }
        /// <summary>
        /// Логарифмический перепад температур O_LogTDif
        /// </summary>
        public string O_LogTDif { get; set; }
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
        /// Явная производительность/мощность
        /// </summary>
        public string O_SenCap { get; set; }




        // Испаритель
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
        /// Перепад давления (влажное состояние)
        /// </summary>
        public string O_AirPaF { get; set; }
        /// <summary>
        /// Отношение явной теплоты к общей
        /// </summary>
        public string O_SHR { get; set; }



        // корпус и коллектор
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
        /// Общая ширина
        /// </summary>
        public string O_WidthExt { get; set; }
        /// <summary>
        /// Рядов в высоту
        /// </summary>
        public string O_RowsH { get; set; }
        /// <summary>
        /// Коль-во  коллекторов
        /// </summary>
        public string O_HdrQta { get; set; }


        public override string ToString()
        {
            return $"O_Code={O_Code}; O_Rows={O_Rows};O_Circuits={O_Circuits};O_LamAbs={O_LamAbs};O_AirVelo={O_AirVelo};O_AirPaT={O_AirPaT}"+
                $"O_LRes={O_LRes};O_MedVelo={O_MedVelo};O_MedKPa={O_MedKPa};O_AirTempOut={O_AirTempOut};O_TotCap={O_TotCap}"+
                $"O_AirHumInAbs={O_AirHumInAbs};O_MedTempOut={O_MedTempOut};O_MedFlow={O_MedFlow};O_Volume={O_Volume}";
        }
    }
}
