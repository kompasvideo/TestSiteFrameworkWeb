using System.Collections.ObjectModel;
using Veza.HeatExchanger.BusinessLogic.Fan.Models;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.DataBase.Models.FanAddEdit;
using System.Resources;
using System.Collections.Generic;

namespace Veza.HeatExchanger.DataBase.Interafces
{
    public interface IFan
    {

        /// <summary>
        /// Создаёт список производителей (Builder) и сериий вентиляторов (Series) в окне "Просмотр вентиляторов"
        /// </summary>
        /// <param name="Builder"></param>
        /// <param name="SelectedBuilder"></param>
        /// <param name="Series"></param>
        /// <param name="SelectedSeries"></param>
        void InitFan(IList<string> Builder, ref string SelectedBuilder, IList<string> Series,
            ref string SelectedSeries, IList<FanOutDTO> Fans);

        /// <summary>
        /// Добавляет новый вентилятор в БД
        /// </summary>
        /// <param name="fanDTO"></param>
        void AddFan(FanDTO fanDTO);

        /// <summary>
        /// Редактирует новый вентилятор в БД
        /// </summary>
        /// <param name="fanDTO"></param>
        ErrorEdit EditFan(FanT fan);

        /// <summary>
        /// Получить для редактирования вентилятор в БД 
        /// </summary>
        /// <param name="name"></param>
        FanT GetEditFan(string name);

        /// <summary>
        /// обновляется список вентиляторов при изменении типа вентиялтора
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Fans"></param>
        /// <param name="FanTip1"></param>
        /// <param name="FanTip2"></param>
        /// <param name="FanTip3"></param>
        /// <param name="FanTip4"></param>
        void Update(ObservableCollection<FanDTO> Fans, 
            bool FanTip1, bool FanTip2, bool FanTip3, bool FanTip4);

        /// <summary>
        /// Обновляется список вентиляторов при изменении производителя
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Fans"></param>
        /// <param name="FanTip1"></param>
        /// <param name="FanTip2"></param>
        /// <param name="FanTip3"></param>
        /// <param name="FanTip4"></param>
        /// <param name="selectedBuilder"></param>
        /// <param name="Series"></param>
        /// <returns></returns>
        ObservableCollection<string> Update(ObservableCollection<FanDTO> Fans,
            bool FanTip1, bool FanTip2, bool FanTip3, bool FanTip4, string selectedBuilder, ObservableCollection<string> Series);

        /// <summary>
        /// Обновляется список вентиляторов при изменении Серии
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Fans"></param>
        /// <param name="FanTip1"></param>
        /// <param name="FanTip2"></param>
        /// <param name="FanTip3"></param>
        /// <param name="FanTip4"></param>
        /// <param name="selectedBuilder"></param>
        /// <param name="Series"></param>
        /// <param name="selectedSeries"></param>
        /// <returns></returns>
        void Update(ObservableCollection<FanDTO> Fans,
            bool FanTip1, bool FanTip2, bool FanTip3, bool FanTip4,
            string selectedBuilder, string selectedSeries);

        /// <summary>
        /// Возвраящяет имя производителя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetBuilder(int id);

        /// <summary>
        /// Возвращяет имя серии по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetSeries(int id);

        /// <summary>
        /// Обновление списков Производители (Builder) и Серии (Series) для окна "Редактировать вентилятор"
        /// </summary>
        /// <param name="Builder"></param>
        /// <param name="SelectedBuilder"></param>
        /// <param name="Series"></param>
        /// <param name="SelectedSeries"></param>
        void UpdateBuilderAndSeries(ObservableCollection<string> Builder, ref string SelectedBuilder,
            ObservableCollection<string> Series, ref string SelectedSeries);

        /// <summary>
        /// Обновление списков Производители (Builder) и Серии (Series) для окна "Добавить вентилятор"
        /// </summary>
        /// <param name="Builder"></param>
        /// <param name="SelectedBuilder"></param>
        /// <param name="Series"></param>
        /// <param name="SelectedSeries"></param>
        void UpdateAddBuilderAndSeries(ObservableCollection<string> Builder, ref string SelectedBuilder,
            ObservableCollection<string> Series, ref string SelectedSeries);

        /// <summary>
        /// Поиск подходящего вентилятора по расходу воздуха и давлению
        /// </summary>
        /// <param name="airFlow">расход воздуха</param>
        /// <param name="presDropDry">давление</param>
        /// <returns></returns>
        IList<FanDTO> FanSearch(double airFlow, double airFlowMax, double presDropDry);

        /// <summary>
        /// расхожд воздуха для выбранного вентилятора при перепаде давления
        /// </summary>
        /// <param name="fanName"></param>
        /// <returns></returns>
        string GetFanAirFlowAtPresDrop(string fanName);

        /// <summary>
        /// получить список вентиляторов
        /// </summary>
        /// <returns></returns>
        IList<string> GetFans();      

        /// <summary>
        /// Получить все материалы
        /// </summary>
        /// <returns></returns>
        IList<string> GetMaterials();

        /// <summary>
        /// Получить IP защиты
        /// </summary>
        /// <returns></returns>
        IList<string> GetIP();

        string GetSelectedIP();

        IList<string> GetDirections();

        /// <summary>
        /// Получение всех точек переданного вентилятора
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<FanPointsDB> GetPoints(string name);

        /// <summary>
        /// Сохранить отредактированную таблицу скоростей для выбранного вентилятора
        /// </summary>
        /// <param name="fanPoints"></param>
        void SetPoints(List<FanPointsDB> fanPoints);

        /// <summary>
        /// Получение таблицы FanStepEffPower - мощность по шагам
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<FanStepEffPowerDB> GetSteps(string name);

        /// <summary>
        /// Сохранить таблицу "расход воздуха-мощность" для выбранного вентилятора
        /// </summary>
        /// <param name="fanSteps"></param>
        void SetSteps(List<FanStepEffPowerDB> fanSteps);

        void CalcPoint(float temp, float humidity, float density, float speed, float size, FanPointsDB fanPoints);

        void CalcStepEffPower(List<FanStepEffPowerDB> listFanStepEffPower, float max, float min, float AStatPres,
           float BStatPres, float CStatPres, float ATotalPres, float BTotalPres, float CTotalPres,
           float AEffFactor, float BEffFactor, float CEffFactor, float APowerInput, float BPowerInput, float CPowerInput);
        void CalcStepEffPower(FanStepEffPowerDB selectedStep, float max, float min, float AStatPres,
           float BStatPres, float CStatPres, float ATotalPres, float BTotalPres, float CTotalPres,
           float AEffFactor, float BEffFactor, float CEffFactor, float APowerInput, float BPowerInput, float CPowerInput);

        FanModelsDB GetFan(ApplicationContext db, string fanName);

        double GetCurrent(string fanName);
    }
}
