using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veza.HeatExchanger.Models.Main
{
    public class InputDataEvaporaterDTO
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
        public string DirectAirTempOutHW { get; set; }
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
        /// Тип хладогента
        /// </summary>
        public string Selected_I_RefT { get; set; }
        /// <summary>
        /// Коэффициент загрязнения трубы
        /// </summary>
        public string Selected_I_FoulingI { get; set; }
        /// <summary>
        /// список - температура кипения или давление кипения абс.
        /// </summary>
        public string SelectEvapTemp { get; set; }
        /// <summary>
        /// Температура кипения
        /// </summary>
        public string I_TEvapDX { get; set; }
        /// <summary>
        /// давление кипения абс.
        /// </summary>
        public string EvapAbsPresDX { get; set; }
        /// <summary>
        /// Список перегрев всас. газа или температура всас. газа
        /// </summary>
        public string SelectSuctOvrheat { get; set; }
        /// <summary>
        /// температура всас. газа
        /// </summary>
        public string I_TOvrHDX { get; set; }
        /// <summary>
        /// перегрев всас. газа
        /// </summary>
        public string SuctGasReturnDX { get; set; }
        /// <summary>
        /// список - температура конденсации или давление конденсации абс.
        /// </summary>
        public string SelectCondTemp { get; set; }
        /// <summary>
        /// температура конденсации - значение
        /// </summary>
        public string I_TCondDX { get; set; }
        /// <summary>
        /// давление конденсации абс. - значение 
        /// </summary>
        public string CondAbsPresDX { get; set; }
        /// <summary>
        /// список переохлаждение или температура жидкости
        /// </summary>
        public string SelectSubCool { get; set; }
        /// <summary>
        /// переохлаждение - значение
        /// </summary>
        public string I_TSubCDX { get; set; }
        /// <summary>
        /// температура жидкости - значение
        /// </summary>
        public string LiquidTempDX { get; set; }
        /// <summary>
        /// Количество контуров хладогента
        /// </summary>
        public string I_RCircDX { get; set; }
        /// <summary>
        /// Пропускная способность байпаса
        /// </summary>
        public string I_DxBypDX { get; set; }


        // характеристики корпуса и коллектора
        /// <summary>
        /// Количество коллекторов
        /// </summary>
        public string I_MatHdr { get; set; }
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
        /// коэффициент загрязения воздуха
        /// </summary>
        public string Select_I_FoulingE { get; set; }
        /// <summary>
        /// автоматический шаг рёбер
        /// </summary>
        public string SelectAFPV { get; set; }
    }
}
