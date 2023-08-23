using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Veza.HeatExchanger.DataBase.Models
{
    sealed public class HeatExchangerDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        #region Параметры теплообменника

        /// <summary>
        /// Геометрия 
        /// 5012
        /// 4816
        /// </summary>
        public string I_Geometry { get; set; }

        /// <summary>
        /// Длина оребрения
        /// </summary>
        public double I_WidthInt { get; set; }

        /// <summary>
        /// Высота оребрения
        /// </summary>
        public double I_HeightInt { get; set; }

        /// <summary>
        /// Число рядов
        /// </summary>
        public int I_Rows { get; set; }

        /// <summary>
        /// Ко-во отводов
        /// </summary>
        public int I_Circuits { get; set; }

        /// <summary>
        /// Расход воздуха
        /// </summary>
        public double I_Airflow { get; set; }

        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        public double I_AirHumIn { get; set; }

        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        public double I_AirTempIn { get; set; }

        /// <summary>
        /// запас поверхности
        /// </summary>
        public double I_ARes { get; set; }

        /// <summary>
        /// Давление окружающего воздуха
        /// </summary>
        public double I_BaseBar { get; set; }

        /// <summary>
        /// Плотность окружающего воздуха
        /// </summary>
        public double I_BaseDens { get; set; }

        /// <summary>
        /// Влажность окружающего воздуха
        /// </summary>
        public string I_BaseHum { get; set; }

        /// <summary>
        /// Режим расчёта влажности
        /// </summary>
        public string I_BaseMode { get; set; }

        /// <summary>
        /// Высота над уровнем моря
        /// </summary>
        public string I_BaseSea { get; set; }

        /// <summary>
        /// Исходная температура
        /// </summary>
        public string I_BaseTemp { get; set; }

        /// <summary>
        /// Изоляция корпуса
        /// </summary>
        public string I_CasIso { get; set; }

        /// <summary>
        /// Материал корпуса
        /// </summary>
        public string I_CasMat { get; set; }

        /// <summary>
        /// Катодное покрытие - окраска окунанием
        /// </summary>
        public string I_CatCoat { get; set; }

        /// <summary>
        ///  Крышка отсека трубопровода коллектора и U-образного трубного колена
        /// </summary>
        public string I_CBox { get; set; }

        /// <summary>
        /// Направление соединения
        /// </summary>
        public string I_CDir { get; set; }

        /// <summary>
        /// Сертифицированный
        /// </summary>
        public string I_CertMode { get; set; }

        /// <summary>
        /// Присоединительный размер на входе в дюймах
        /// </summary>
        public string I_ConIn { get; set; }

        /// <summary>
        /// Присоединительный размер на выходе в дюймах
        /// </summary>
        public string I_ConOut { get; set; }

        /// <summary>
        /// Наличие резьбы на патрубке
        /// </summary>
        public int I_ConType { get; set; }

        /// <summary>
        /// Покрытие коллектора
        /// </summary>
        public string I_CSheet { get; set; }

        /// <summary>
        /// Таблица обложки заголовка по длине
        /// </summary>
        public string I_CSheetL { get; set; }

        /// <summary>
        /// Качество корпуса каплеуловителя
        /// </summary>
        public string I_ElimMat { get; set; }

        /// <summary>
        /// Тип каплеуловителя
        /// </summary>
        public string I_ElimTp { get; set; }

        /// <summary>
        /// Конструкция/Подключение змеевика
        /// </summary>
        public string I_Esapo { get; set; }

        /// <summary>
        /// Звуконепроницаемое помещение для вентиляторов
        /// </summary>
        public string I_FanPlen { get; set; }

        /// <summary>
        /// толщина оребрения
        /// </summary>
        public string I_FinThk { get; set; }

        /// <summary>
        /// Коэффициент Загрязнения Воздуха
        /// </summary>
        public int I_FoulingE { get; set; }

        /// <summary>
        /// Коэффициент Загрязнения Труб
        /// </summary>
        public int I_FoulingI { get; set; }

        /// <summary>
        /// Тип рамы
        /// </summary>
        public string I_FrameTp { get; set; }

        /// <summary>
        ///  Использование полной высоты теплообменника
        /// </summary>
        public string I_FullH { get; set; }

        /// <summary>
        /// Кол-во коллекторов
        /// </summary>
        public string I_HdrQta { get; set; }

        /// <summary>
        /// Изэнтропийная эффективность
        /// </summary>
        public string I_IseEta { get; set; }

        /// <summary>
        /// Коррекция перепада давления воздуха
        /// </summary>
        public string I_KorrADP { get; set; }

        /// <summary>
        ///  Корректировка внешней поверхности
        /// </summary>
        public string I_KorrFA { get; set; }

        /// <summary>
        /// Коррекция внутренней поверхности
        /// </summary>
        public string I_KorrFI { get; set; }

        /// <summary>
        /// Коррекция перепада среднего давления
        /// </summary>
        public string I_KorrKDP { get; set; }

        /// <summary>
        /// шаг оребрения
        /// </summary>
        public string I_LamAbsFix { get; set; }

        /// <summary>
        /// шаг оребрения
        /// </summary>
        public string I_LamAbsMax { get; set; }

        /// <summary>
        /// Резерв мощности
        /// </summary>
        public double I_LRes { get; set; }

        /// <summary>
        /// Материал трубки
        /// </summary>
        public string I_MatFins { get; set; }

        /// <summary>
        /// Качество рамы
        /// </summary>
        public string I_MatFrame { get; set; }

        /// <summary>
        /// материал коллектора
        /// </summary>
        public string I_MatHdr { get; set; }

        /// <summary>
        /// Материал 
        /// </summary>
        public string I_MatRows { get; set; }

        /// <summary>
        ///  Качество рядов
        /// </summary>
        public string I_Mode { get; set; }

        /// <summary>
        /// Качество дренажного поддона
        /// </summary>
        public string I_PanMat { get; set; }

        /// <summary>
        /// Тип дренажного поддона
        /// </summary>
        public string I_PanTp { get; set; }

        /// <summary>
        /// Размеры и материал трубки
        /// </summary>
        public string I_PipeThk { get; set; }

        /// <summary>
        /// Переход на сечение круглой формы
        /// </summary>
        public string I_RCon { get; set; }

        /// <summary>
        /// Тип хладагента
        /// </summary>
        public string I_RefT { get; set; }

        /// <summary>
        /// Специальный припой
        /// </summary>
        public string I_SpcSolder { get; set; }

        /// <summary>
        /// Температура конденсации
        /// </summary>
        public double I_TCond { get; set; }

        /// <summary>
        /// Температура кипениия
        /// </summary>
        public double I_TEvap { get; set; }

        /// <summary>
        /// Температура горячего газа
        /// </summary>
        public double I_THotGas { get; set; }

        /// <summary>
        /// Максимальное падение давления хладагента
        /// </summary>
        public double I_TMaxP { get; set; }

        /// <summary>
        /// Перегрев
        /// </summary>
        public double I_TOvrH { get; set; }

        /// <summary>
        /// Переохлаждение
        /// </summary>
        public double I_TSubC { get; set; }

        /// <summary>
        /// Температура всасываемого газа
        /// </summary>
        public double I_TSucGas { get; set; }

        /// <summary>
        /// Тип расчета
        /// </summary>
        public string I_Type { get; set; }

        /// <summary>
        ///  Неравное число ходов в контурах
        /// </summary>
        public string I_Uneven { get; set; }
        #endregion
    }
}
