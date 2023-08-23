using Veza.HeatExchanger.DataBase.Interafces;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.BusinessLogic.Fan.Models;
using Veza.HeatExchanger.BusinessLogic.TO.Models;
using Veza.HeatExchanger.DataBase.Models.FanAddEdit;
using Veza.HeatExchanger.Services;
using System.Collections.Generic;
using System.Linq;

namespace Veza.HeatExchanger.BusinessLogic.Fan
{
    public class FanService : IExtFanService
    {
        #region Внутренние поля и переменные  

        /// <summary>
        /// Служит для доступа к ресурсам строк приложения Resources.resx 
        /// в зависимости от установленного сейчас языка
        /// </summary>
        private IFan fanDb;
        private IList<string> Builder;
        private string selectedBuilder;
        private IList<string> Series;
        private string selectedSeries;
        private IList<FanOutDTO> FansP;

        /// <summary>
        /// количество строк таблицы с заполненными данными
        /// </summary>
        private int countPoints;
        /// <summary>
        /// Таблица скоростей
        /// </summary>
        private List<FanPointsDB> points;

        #endregion

        #region Конструктор
        public FanService(IFan fanDb)
        {
            this.fanDb = fanDb;
            Builder = new List<string>();
            Series = new List<string>();
            FansP = new List<FanOutDTO>();
        }
        #endregion

        #region Публичные методы       

        /// <summary>
        /// Возвраящяет производителей вентиляторов
        /// </summary>
        /// <returns></returns>
        public Fans GetListTypesFan()
        {
            List<FanTypes> fanTypes;
            if (GS.IsCultureRU())
            {
                fanTypes = new List<FanTypes>()
                {
                    new FanTypes(true, Calculation.TO.Main.Properties.Resources.TipologyAxialMonophaseRu),
                    new FanTypes(true, Calculation.TO.Main.Properties.Resources.TipologyAxialTriphaseRu),
                    new FanTypes(true, Calculation.TO.Main.Properties.Resources.TipologyDirectlyCoupledRu),
                    new FanTypes(true, Calculation.TO.Main.Properties.Resources.TipologyCentriphugalRu),
                };
            }
            else
            {
                fanTypes = new List<FanTypes>()
                {
                    new FanTypes(true, Calculation.TO.Main.Properties.Resources.TipologyAxialMonophase),
                    new FanTypes(true, Calculation.TO.Main.Properties.Resources.TipologyAxialTriphase),
                    new FanTypes(true, Calculation.TO.Main.Properties.Resources.TipologyDirectlyCoupled),
                    new FanTypes(true, Calculation.TO.Main.Properties.Resources.TipologyCentriphugal),
                };
            }
            fanDb.InitFan(Builder, ref selectedBuilder, Series, ref selectedSeries, FansP);
            return new Fans(fanTypes, Builder.ToList(), selectedBuilder, Series.ToList(), selectedSeries, FansP.ToList());
        }

        /// <summary>
        /// Возвраящяет вентилятор для редактирования
        /// </summary>
        /// <returns></returns>
        public FanT GetFanToEdit(string name)
        {
            return fanDb.GetEditFan(name);
        }

        /// <summary>
        /// Редактировать вентилятор 
        /// </summary>
        /// <param name="fan"></param>
        /// <returns></returns>
        public ErrorEdit SetFanToEdit(FanT fan)
        {            
            return fanDb.EditFan(fan);
        }

        /// <summary>
        /// Получить таблицу скоростей для выбранного вентилятора
        /// </summary>
        /// <param name="name"></param>
        public List<FanPointsDB> GetPoints(string name)
        {
            return fanDb.GetPoints(name);
        }

        /// <summary>
        /// Сохранить отредактированную таблицу скоростей для выбранного вентилятора
        /// </summary>
        /// <param name="fanPoints"></param>
        public void SetPoints(List<FanPointsDB> fanPoints)
        {
            points = fanPoints;
            CheckPoints();
            fanDb.SetPoints(fanPoints);
            //CheckSteps();
        }


        /// <summary>
        /// Получить таблицу "расход воздуха-мощность" для выбранного вентилятора
        /// </summary>
        /// <param name="name"></param>
        public List<FanStepEffPowerDB> GetSteps(string name)
        {
            return fanDb.GetSteps(name);
        }

        /// <summary>
        /// Сохранить таблицу "расход воздуха-мощность" для выбранного вентилятора
        /// </summary>
        /// <param name="fanSteps"></param>
        public void SetSteps(List<FanStepEffPowerDB> fanSteps)
        {
            fanDb.SetSteps(fanSteps);
        }

        #endregion

        #region Приватные методы

        /// <summary>
        /// Присваиваивание не заполненым строкам значение null
        /// </summary>
        private void CheckPoints()
        {
            countPoints = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i] != null)
                {
                    if (points[i].Voltage == 0 && points[i].Speed == 0 && points[i].Power == 0 && points[i].Current == 0
                        && points[i].Airflow == 0 && points[i].StatPressure == 0)
                    {
                        points[i] = null;
                    }
                    else countPoints++;
                }
            }
        }


        #endregion
    }
}
