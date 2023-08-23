using Veza.HeatExchanger.Models;
using System.Collections.Generic;

namespace Veza.HeatExchanger.Interfaces
{
    public interface IInputDataService
    {
        /// <summary>
        /// Возвращяет код материала корпуса
        /// </summary>
        /// <returns></returns>
        string GetProdCode_I_CasMat();
        /// <summary>
        /// Возвращяет MATHDR - материал коллектора
        /// </summary>
        /// <returns></returns>
        string GetProdCode_I_MatHdr();
        /// <summary>
        /// Размер рамы снизу
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameB();
        /// <summary>
        /// Размер рамы сверху
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameT();
        /// <summary>
        /// Толщина рамы
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameThk();
        /// <summary>
        /// Размер рамы на стороне коллектора
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameL();
        /// <summary>
        /// Размер рамы на стороне "кривой"
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameR();
        string GetProdCode_I_Esapo(CalcMode calcMode);
        string GetProdCode_AirFlowDirection();
        string GetProdCode_ConnectingCoolant();
        string GetProdCode_I_CDir();
        /// <summary>
        /// Возвращяет букву которая кодирует диаметр капиляра для режима Испаритель
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_Capilar();
        /// <summary>
        /// Возвращяет длину капиляра
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_CapLen();
        /// <summary>
        /// исполнение корпуса
        /// </summary>
        /// <returns></returns>
        string GetProdCode_CasingType();
        /// <summary>
        /// Тип патрубков
        /// </summary>
        /// <returns></returns>
        string GetProdCode_I_ConType();

        /// <summary>
        /// Возвращяет код материала корпуса
        /// </summary>
        /// <returns></returns>
        string GetShortCode_I_CasMat();
        void SetI_CasMat(string str);
        /// <summary>
        /// Диаметр коллектора вход
        /// </summary>
        /// <returns></returns>
        string GetShortCode_O_ConIN(bool overload = false);
        /// <summary>
        /// Диаметр коллектора выход
        /// </summary>
        /// <returns></returns>
        string GetShortCode_O_ConOut(bool overload = false);
        /// <summary>
        /// Покрытие коллектора
        /// </summary>
        /// <returns></returns>
        string GetShortCode_I_CSheet();
        void SetI_CSheet(string str);
        /// <summary>
        /// Припуск на обработку по длине покрытия трубопровода коллектора ( в мм)
        /// </summary>
        /// <returns></returns>
        string GetShortCode_I_CSheetL();
        void SetI_CSheetL(string str);
        string GetI_CSheetL();
        /// <summary>
        /// Наличие резьбы на патрубке
        /// </summary>
        /// <returns></returns>
        string GetShortCode_I_ConType();
        /// <summary>
        /// Возвращяет MATHDR - материал коллектора
        /// </summary>
        /// <returns></returns>
        string GetShortCode_I_MatHdr();
        /// <summary>
        /// Установка количества коллекторов
        /// </summary>
        /// <param name="str"></param>
        void SetI_MatHdr(string str);
        string GetI_MatHdr();
        /// <summary>
        /// исполнение корпуса
        /// </summary>
        /// <returns></returns>
        string GetShortCode_CasingType();
        string GetShortCode_I_CDir();
        void SetI_CDir(string str);

        /// <summary>
        /// Возвращяет код CONINF - Присоединительный размер коллектора на входе в дюймах
        /// </summary>
        /// <returns></returns>
        string GetCalc_I_ConIn();
        void SetI_ConIn(string str);
        /// <summary>
        /// Тип патрубков
        /// </summary>
        /// <returns></returns>
        string GetCalc_I_ConType();
        int GetCalcI_I_ConType();
        void SetI_ConType(int i);
        string GetCalc_I_Esapo();
        /// <summary>
        /// Возвращяет MATHDR - материал коллектора
        /// </summary>
        /// <returns></returns>
        string GetCalc_I_MatHdr();

        /// <summary>
        /// Припуск на обработку по длине покрытия трубопровода коллектора ( в мм)
        /// </summary>
        /// <returns></returns>
        string GetString_I_CSheetL();         
        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        string GetString_ValueAirFlowDX();

        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        string GetString_I_AirTempInDX();

        /// <summary>
        /// Тип патрубков
        /// </summary>
        /// <returns></returns>        
        string GetOut_I_ConIn(string code);      

        /// <summary>
        /// Установка списка ориентации теплообменника для Воздухонагревателя
        /// </summary>
        void SetEsapoHeater();
        /// <summary>
        /// Установка списка ориентации теплообменника для Воздухоохлдителя
        /// </summary>
        void SetEsapoCooler();
        /// <summary>
        /// Установка списка ориентации теплообменника для Паравого нагревателя
        /// </summary>
        void SetEsapoSteamHeater();
        /// <summary>
        /// Установка списка ориентации теплообменника для Конденсатора
        /// </summary>
        void SetEsapoCondensator();
        /// <summary>
        /// Установка списка ориентации теплообменника для Испарителя
        /// </summary>
        void SetEsapoEvaporater();
        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        string GetString_ValueAirFlowHW();
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        string GetString_I_AirTempInHW();
        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        string GetString_ValueAirFlowCW();
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        string GetString_I_AirTempInCW();
        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        string GetString_ValueAirFlowST();
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        string GetString_I_AirTempInST();
        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        string GetString_ValueAirFlowCX();
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        string GetString_I_AirTempInCX();
        /// <summary>
        /// Установка мощности или температуры воздуха на выходе
        /// </summary>
        /// <param name="param"></param>
        void SetValueCapacityHW(string param);
        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        string GetValueCapacityHW();
        /// <summary>
        /// Установка мощности или температуры воздуха на выходе
        /// </summary>
        /// <param name="param"></param>
        void SetValueCapacityCW(string param);
        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        string GetValueCapacityCW();
        /// <summary>
        /// Установить длину оребрения
        /// </summary>
        /// <param name="param"></param>
        void SetValueWidthFin(string param);
        /// <summary>
        /// Получить длину оребрения типа string
        /// </summary>
        string GetValueWidthFin();
        /// <summary>
        /// Установить высоту оребрения
        /// </summary>
        /// <param name="param"></param>
        void SetValueHightFin(string param);
        /// <summary>
        /// Получить высоту оребрения типа string
        /// </summary>
        string GetValueHightFin();
        /// <summary>
        /// Установить число труб
        /// </summary>
        /// <param name="param"></param>
        void SetTubesN(string param);
        /// <summary>
        /// Получить число труб типа string
        /// </summary>
        /// <param name="param"></param>
        string GetTubesN();
        /// <summary>
        /// Установка расхода воздуха
        /// </summary>
        /// <param name="param"></param>
        void SetValueAirFlowHW(string param);
        /// <summary>
        /// расход воздуха
        /// </summary>
        string GetValueAirFlowHW();
        /// <summary>
        /// Установка расхода воздуха
        /// </summary>
        /// <param name="param"></param>
        void SetValueAirFlowST(string param);
        /// <summary>
        /// расход воздуха
        /// </summary>
        string GetValueAirFlowST();
        /// <summary>
        /// Установка расхода воздуха
        /// </summary>
        /// <param name="param"></param>
        void SetValueAirFlowCW(string param);
        /// <summary>
        /// расход воздуха
        /// </summary>
        string GetValueAirFlowCW();
        /// <summary>
        /// Установка температуры воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetI_AirTempInHW(string param);
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        string GetI_AirTempInHW();
        /// <summary>
        /// Установка температуры воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetI_AirTempInST(string param);
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        string GetI_AirTempInST();
        /// <summary>
        /// Установка температуры воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetI_AirTempInCW(string param);
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        string GetI_AirTempInCW();
        /// <summary>
        /// Установка влажности воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetValueBaseHumHW(string param);
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        string GetValueBaseHumHW();
        /// <summary>
        /// Установка влажности воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetValueBaseHumST(string param);
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        string GetValueBaseHumST();
        /// <summary>
        /// Установка влажности воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetValueBaseHumCW(string param);
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        string GetValueBaseHumCW();
        /// <summary>
        /// Установка концентрации гликоля
        /// </summary>
        /// <param name="param"></param>
        void SetI_SoleCnz(string param);
        /// <summary>
        /// концентрация гликоля
        /// </summary>
        /// <param name="param"></param>
        string GetI_SoleCnz();
        /// <summary>
        /// Установка температуры теплоносителя на входе
        /// </summary>
        /// <param name="param"></param>
        void SetValueMedTempInHW(string param);
        /// <summary>
        /// температура теплоносителя на входе
        /// </summary>
        string GetValueMedTempInHW();
        /// <summary>
        /// Установка температуры теплоносителя на входе
        /// </summary>
        /// <param name="param"></param>
        void SetValueMedTempInCW(string param);
        /// <summary>
        /// температура теплоносителя на входе
        /// </summary>
        string GetValueMedTempInCW();
        /// <summary>
        /// Установка температуры теплоносителя на выходе 
        /// или росход теплоносителя
        /// </summary>
        /// <param name="param"></param>
        void SetValueVariantRaschHW(string param);
        /// <summary>
        /// температура теплоносителя на выходе 
        /// или росход теплоносителя
        /// </summary>
        string GetValueVariantRaschHW();
        /// <summary>
        /// Установка максимального падения давления
        /// </summary>
        /// <param name="param"></param>
        void SetValueMedKPaHW(string param);
        /// <summary>
        /// Получение максимального падения давления
        /// </summary>
        string GetValueMedKPaHW();
        /// <summary>
        /// Устанавливаем число рядов
        /// </summary>
        /// <param name="param"></param>
        void SetValuePipe(string param);
        /// <summary>
        /// Получение число рядов
        /// </summary>
        string GetValuePipe();
        /// <summary>
        /// Установка количества отводов
        /// </summary>
        /// <param name="param"></param>
        void SetValueCircuits(string param);
        /// <summary>
        /// Получение количества отводов
        /// </summary>
        string GetValueCircuits();
        /// <summary>
        /// Установить число ходов
        /// </summary>
        /// <param name="param"></param>
        void SetNumOfPasses(string param);
        /// <summary>
        /// Получить число ходов
        /// </summary>
        string GetNumOfPasses();
        /// <summary>
        /// переохлаждение, конденсатор
        /// <param name="param"></param>
        void SetI_TSubCCX(string param);
        /// <summary>
        /// переохлаждение, конденсатор
        /// </summary>
        string GetI_TSubCCX();
        /// <summary>
        /// переохлаждение, Испаритель
        /// <param name="param"></param>
        void SetI_TSubCDX(string param);
        /// <summary>
        /// переохлаждение, конденсатор
        /// </summary>
        string GetI_TSubCDX();
        /// <summary>
        /// температура жидкости, конденсатор
        /// </summary>
        /// <param name="param"></param>
        void SetLiquidTempCX(string param);
        /// <summary>
        /// температура жидкости, конденсатор
        /// </summary>
        string GetLiquidTempCX();
        /// <summary>
        /// температура жидкости, Испаритель
        /// </summary>
        /// <param name="param"></param>
        void SetLiquidTempDX(string param);
        /// <summary>
        /// температура жидкости, конденсатор
        /// </summary>
        string GetLiquidTempDX();
        /// <summary>
        /// давление конденсации абс.
        /// </summary>
        /// <param name="param"></param>
        void SetCondAbsPresCX(string param);
        /// <summary>
        /// давление конденсации абс.
        /// </summary>
        string GetCondAbsPresCX();
        /// <summary>
        /// давление конденсации абс.
        /// </summary>
        /// <param name="param"></param>
        void SetCondAbsPresDX(string param);
        /// <summary>
        /// давление конденсации абс.
        /// </summary>
        string GetCondAbsPresDX();
        /// <summary>
        /// температура конденсации
        /// </summary>
        /// <param name="param"></param>
        void SetI_TCondCX(string param);
        /// <summary>
        /// температура конденсации
        /// </summary>
        string GetI_TCondCX();
        /// <summary>
        /// температура конденсации
        /// </summary>
        /// <param name="param"></param>
        void SetI_TCondDX(string param);
        /// <summary>
        /// температура конденсации
        /// </summary>
        string GetI_TCondDX();
        /// <summary>
        /// температура горячего газа
        /// </summary>
        /// <param name="param"></param>
        void SetI_THotGasCX(string param);
        /// <summary>
        /// температура горячего газа
        /// </summary>
        string GetI_THotGasCX();
        /// <summary>
        /// температура горячего газа
        /// </summary>
        /// <param name="param"></param>
        void SetI_THotGasDX(string param);
        /// <summary>
        /// температура горячего газа
        /// </summary>
        string GetI_THotGasDX();
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetValueBaseHumCX(string param);
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        string GetValueBaseHumCX();
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetValueBaseHumDX(string param);
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        string GetValueBaseHumDX();
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetI_AirTempInCX(string param);
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        string GetI_AirTempInCX();
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        void SetI_AirTempInDX(string param);
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        string GetI_AirTempInDX();
        /// <summary>
        /// Расход воздуха
        /// </summary>
        /// <param name="param"></param>
        void SetValueAirFlowCX(string param);
        /// <summary>
        /// расход воздуха
        /// </summary>
        string GetValueAirFlowCX();
        /// <summary>
        /// Расход воздуха
        /// </summary>
        /// <param name="param"></param>
        void SetValueAirFlowDX(string param);
        /// <summary>
        /// расход воздуха
        /// </summary>
        string GetValueAirFlowDX();
        /// <summary>
        /// Мощность
        /// </summary>
        /// <param name="param"></param>
        void SetValueCapacityCX(string param);
        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        string GetValueCapacityCX();
        /// <summary>
        /// Мощность
        /// </summary>
        /// <param name="param"></param>
        void SetValueCapacityDX(string param);
        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        string GetValueCapacityDX();
        /// <summary>
        /// Мощность
        /// </summary>
        /// <param name="param"></param>
        void SetValueCapacityST(string param);
        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        string GetValueCapacityST();
        /// <summary>
        /// Давление пара
        /// </summary>
        /// <param name="param"></param>
        void SetI_StmBar(string param);
        /// <summary>
        /// Давление пара
        /// </summary>
        string GetI_StmBar();
        /// <summary>
        /// Температура пара
        /// </summary>
        /// <param name="param"></param>
        void SetI_StmTemp(string param);
        /// <summary>
        /// Температура пара
        /// </summary>
        string GetI_StmTemp();
        /// <summary>
        /// Значение списка SelectedVariantRasch
        /// </summary>
        /// <param name="param"></param>
        void SetValueVariantRaschCW(string param);
        /// <summary>
        /// температура теплоносителя на выходе 
        /// или росход теплоносителя
        /// </summary>
        string GetValueVariantRaschCW();
        /// <summary>
        /// Максимальное падение давления
        /// </summary>
        /// <param name="param"></param>
        void SetValueMedKPaCW(string param);
        /// <summary>
        /// Получение максимального падения давления
        /// </summary>
        string GetValueMedKPaCW();
        /// <summary>
        /// Количество контуров хладогента
        /// </summary>
        /// <param name="param"></param>
        void SetI_RCircDX(string param);
        /// <summary>
        /// Количество контуров хладогента
        /// </summary>
        string GetI_RCircDX();
        /// <summary>
        /// Пропускная способность байпаса
        /// </summary>
        /// <param name="param"></param>
        void SetI_DxBypDX(string param);
        /// <summary>
        /// Пропускная способность байпаса
        /// </summary>
        string GetI_DxBypDX();
        /// <summary>
        /// температура кипения
        /// </summary>
        /// <param name="param"></param>
        void SetI_TEvapDX(string param);
        /// <summary>
        /// температура кипения
        /// </summary>
        string GetI_TEvapDX();
        /// <summary>
        /// давление кипения абс.
        /// </summary>
        /// <param name="param"></param>
        void SetEvapAbsPresDX(string param);
        /// <summary>
        /// давление кипения абс.
        /// </summary>
        string GetEvapAbsPresDX();
        /// <summary>
        /// температура всас. газа
        /// </summary>
        /// <param name="param"></param>
        void SetI_TOvrHDX(string param);
        /// <summary>
        /// температура всас. газа
        /// </summary>
        string GetI_TOvrHDX();
        /// <summary>
        /// перегрев всас. газа
        /// </summary>
        /// <param name="param"></param>
        void SetSuctGasReturnDX(string param);
        /// <summary>
        /// перегрев всас. газа
        /// </summary>
        string GetSuctGasReturnDX();






        /// <summary>
        /// Получить список типов расчёта
        /// </summary>
        /// <returns></returns>
        List<string> GetListMode();

        /// <summary>
        /// список Вариант расчёта - Мощность, Температура воздуха на выходе
        /// </summary>
        List<string> GetListDirectMode();

        /// <summary>
        /// список геометрий
        /// </summary>
        List<string> GetListGeometries();

        /// <summary>
        /// список трубок
        /// </summary>
        List<string> GetListPipes();
        /// <summary>
        /// Список толщин оребрения
        /// </summary>
        /// <returns></returns>
        List<string> GetListFins();
        /// <summary>
        /// Список шагов оребрения
        /// </summary>
        /// <returns></returns>
        List<string> GetListStepsFin();
        /// <summary>
        /// Список единиц измерения расхода воздуха
        /// </summary>
        /// <returns></returns>
        List<string> GetListUnitsAirFlow();
        /// <summary>
        /// Список теплоносителей
        /// </summary>
        /// <returns></returns>
        List<string> GetListFluids();
        /// <summary>
        /// список вариантов расчёта по телоносителю - Температура теплоносителя на выходе, Расход теплоносителя
        /// </summary>
        /// <returns></returns>
        List<string> GetListVariantRasch();
        /// <summary>
        /// список материалов корпуса
        /// </summary>
        List<string> GetListI_MatHdr();
        /// <summary>
        /// Список диаметров коллектора на входе
        /// </summary>
        /// <returns></returns>
        List<string> GetListI_ConIn();
        /// <summary>
        /// Список диаметров коллектора на выходе
        /// </summary>
        /// <returns></returns>
        List<string> GetListI_ConOut();
        /// <summary>
        /// список типов патрубков
        /// </summary>
        /// <returns></returns>
        List<string> GetListI_ConType();
        /// <summary>
        /// Список исполения корпуса
        /// </summary>
        /// <returns></returns>
        List<string> GetListCasingType();
        /// <summary>
        /// Список материалов корпуса
        /// </summary>
        /// <returns></returns>
        List<string> GetListI_CasMat();
        /// <summary>
        /// список ориентаций теплообменника
        /// </summary>
        /// <returns></returns>
        List<string> GetListEsapo();
        /// <summary>
        /// список ориентаций патрубков
        /// </summary>
        List<string> GetListI_CDir();
        /// <summary>
        /// список направлений потока воздуха
        /// </summary>
        List<string> GetListAirFlowDirection();
        /// <summary>
        /// список подключений теплоносителя
        /// </summary>
        List<string> GetListConnectingCoolant();


        /// <summary>
        /// Список хладогентов
        /// </summary>
        List<string> GetListI_RefT();
        /// <summary>
        /// Список коэффициентов загрязнения трубы
        /// </summary>
        List<string> GetListI_FoulingI();
        /// <summary>
        /// список - температк конденсации, давление конденсации абс
        /// </summary>
        List<string> GetListCondensingTemperature();
        /// <summary>
        /// список переохлаждение или температура жидкости
        /// </summary>
        List<string> GetListSubCooling();
        /// <summary>
        /// список - температура кипения или давление кипения абс.
        /// </summary>
        List<string> GetListEvaporatingTemperature();
        /// <summary>
        /// Список перегрев всас. газа или температура всас. газа
        /// </summary>
        List<string> GetListSuctOvrheat();
    }
}
