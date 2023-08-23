﻿using Veza.HeatExchanger.Models.Main;

namespace Veza.HeatExchanger.Interfaces.Mappers
{
    public interface IEvaporaterMapper
    {
        /// <summary>
        /// Установка входных параметров
        /// SelectedMode - тип расчёта : direct - прямой, reverse - обратный
        /// DirectMode - список (мощность, температура воздуха на выходе)
        /// ValueCapacityHW - значение DirectMode
        /// SelectGeometry - геометрия
        /// ValueWidthFin - длина оребрения
        /// ValueHightFin - высота оребрения
        /// TubesN - число труб
        /// SelectedPipe - трубка
        /// SelectedFin - толщина оребрения
        /// SelectedStepFin - шаг оребрения
        /// ValueAirFlowHW - расход воздуха
        /// I_AirTempInHW - температура воздуха на входе
        /// ValueBaseHumHW - влажность воздуха на входе
        /// Selected_I_RefT - Тип хладогента
        /// Selected_I_FoulingI  - Коэффициент загрязнения трубы
        /// I_THotGasCX - Температура горячего газа
        /// SelectEvapTemp - Список температура кипения или давление кипения абс.
        /// I_TEvapDX - Температура кипения
        /// EvapAbsPresDX - давление кипения абс.
        /// SelectSuctOvrheat - Список перегрев всас. газа или температура всас. газа
        /// I_TOvrHDX - температура всас. газа
        /// SuctGasReturnDX - перегрев всас. газа
        /// SelectCondTemp - список - температура конденсации или давление конденсации абс.
        /// I_TCondCX - температура конденсации - значение
        /// CondAbsPresCX - давление конденсации абс. - значение 
        /// SelectSubCool - список переохлаждение или температура жидкости
        /// I_TSubCCX - переохлаждение 
        /// LiquidTempCX - температура жидкости
        /// ValuePipe - число рядов
        /// ValueCircuits - число отводов
        /// NumOfPasses - число ходов
        /// 
        /// --------   характеристики корпуса и коллектора   ---------
        /// I_MatHdr - количество коллекторов
        /// Selected_I_MatHdr - материал коллектора
        /// Selected_I_ConIn - диаметр коллектора на входе
        /// Selected_I_ConOut - диаметр коллектора на выходе
        /// Selected_I_ConType - тип патрубков
        /// I_CSheetL - припуск на обработку по длине коллектора
        /// SelectedCasingType - исполнение корпуса
        /// Selected_I_CasMat - материал корпуса
        /// SelectedEsapo = ориентация ткплообменника
        /// Select_I_CDir - ориентация патрубков
        /// SelectedAirFlowDirection - направление потока воздуха
        /// SelectedConnectingCoolant - подключение теплоносителя
        /// 
        /// -----------  Дополнительные параметры ------------
        /// BaseBar - давление окружающего воздуха
        /// SelectGasWorkPressUnit - ед. низмерения давление окружающего воздуха
        /// I_BaseDens - плотность окружающего овздуха
        /// I_ARes - запас поверхности
        /// I_LRes - резерв мощности
        /// Select_I_FoulingI - коэффициент загрязнения трубы
        /// Select_I_FoulingE - коэффициент загрязнения воздуха
        /// SelectAFPV - автоматический шаг оребрения
        /// </summary>
        /// <returns></returns>
        void InputToInputDataClass(InputDataEvaporaterDTO inputParams);
        /// <summary>
        /// Установка входных параметров
        /// SelectedMode - тип расчёта : direct - прямой, reverse - обратный
        /// DirectMode - список (мощность, температура воздуха на выходе)
        /// ValueCapacityDX - значение DirectMode
        /// SelectGeometry - геометрия
        /// ValueWidthFin - длина оребрения
        /// ValueHightFin - высота оребрения
        /// TubesN - число труб
        /// SelectedPipe - трубка
        /// SelectedFin - толщина оребрения
        /// SelectedStepFin - шаг оребрения
        /// ValueAirFlowDX - расход воздуха
        /// I_AirTempInDX - температура воздуха на входе
        /// ValueBaseHumDX - влажность воздуха на входе
        /// Selected_I_RefT - Тип хладогента
        /// Selected_I_FoulingI  - Коэффициент загрязнения трубы
        /// I_THotGasDX - Температура горячего газа
        /// SelectEvapTemp - Список температура кипения или давление кипения абс.
        /// I_TEvapDX - Температура кипения
        /// EvapAbsPresDX - давление кипения абс.
        /// SelectSuctOvrheat - Список перегрев всас. газа или температура всас. газа
        /// I_TOvrHDX - температура всас. газа
        /// SuctGasReturnDX - перегрев всас. газа
        /// SelectCondTemp - список - температура конденсации или давление конденсации абс.
        /// I_TCondDX - температура конденсации - значение
        /// CondAbsPresDX - давление конденсации абс. - значение 
        /// SelectSubCool - список переохлаждение или температура жидкости
        /// I_TSubCDX - переохлаждение 
        /// LiquidTempDX - температура жидкости
        /// ValuePipe - число рядов
        /// ValueCircuits - число отводов
        /// NumOfPasses - число ходов
        /// 
        /// --------   характеристики корпуса и коллектора   ---------
        /// I_MatHdr - количество коллекторов
        /// Selected_I_MatHdr - материал коллектора
        /// Selected_I_ConIn - диаметр коллектора на входе
        /// Selected_I_ConOut - диаметр коллектора на выходе
        /// Selected_I_ConType - тип патрубков
        /// I_CSheetL - припуск на обработку по длине коллектора
        /// SelectedCasingType - исполнение корпуса
        /// Selected_I_CasMat - материал корпуса
        /// SelectedEsapo = ориентация ткплообменника
        /// Select_I_CDir - ориентация патрубков
        /// SelectedAirFlowDirection - направление потока воздуха
        /// SelectedConnectingCoolant - подключение теплоносителя
        /// 
        /// -----------  Дополнительные параметры ------------
        /// BaseBar - давление окружающего воздуха
        /// SelectGasWorkPressUnit - ед. низмерения давление окружающего воздуха
        /// I_BaseDens - плотность окружающего овздуха
        /// I_ARes - запас поверхности
        /// I_LRes - резерв мощности
        /// Select_I_FoulingI - коэффициент загрязнения трубы
        /// Select_I_FoulingE - коэффициент загрязнения воздуха
        /// SelectAFPV - автоматический шаг оребрения
        /// </summary>
        /// <returns></returns>
        GetDataEvaporaterDTO InputDataClassToInputParams();
    }
}
