using System;
using System.Collections.Generic;
using Veza.HeatExchanger.DataBase.Interafces;
using Veza.HeatExchanger.Interfaces;
using System.Globalization;
using Veza.HeatExchanger.DataBase.Models;

namespace Veza.HeatExchanger.Services
{
    /// <summary>
    /// Сервис для поиска подходящего вентилятора
    /// </summary>
    sealed public class FanSelection : IFanSelection
    {
        #region Внутренние поля 
        private IFan _fanDb;
        #endregion

        #region Конструкторы
        public FanSelection(IFan fanDb)
        {
            _fanDb = fanDb;
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Поиск подходящих вентиляторов
        /// </summary>
        /// <param name="I_AirFlow"></param>
        /// <param name="I_AirFlowMax"></param>
        /// <param name="PresDropDry"></param>
        /// <returns></returns>
        public IList<FanDTO> FanSearch(double l_i_AirFlow, double l_i_AirFlowMax, string l_presDropDry)
        {
            double presDropDry = 0;
            try
            {
                presDropDry = Convert.ToDouble(l_presDropDry, CultureInfo.InvariantCulture);
            }
            catch 
            {
                return null;
            }
            return _fanDb.FanSearch(l_i_AirFlow, l_i_AirFlowMax, presDropDry);
        }

        /// <summary>
        /// расхожд воздуха для выбранного вентилятора при перепаде давления
        /// </summary>
        /// <param name="fanName"></param>
        /// <returns></returns>
        public string GetFanAirFlowAtPresDrop(string fanName)
        {
            return _fanDb.GetFanAirFlowAtPresDrop(fanName);
        }
        #endregion
    }
}
