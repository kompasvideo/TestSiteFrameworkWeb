using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Veza.HeatExchanger.Models.MAKK;
using Veza.HeatExchanger.Services;

namespace Veza.HeatExchanger.BusinessLogic.Compressors.Select_8
{
    internal class Select8 : ISelect8
    {
        #region Внутренние поля 
        /// <summary>
        /// флаг загружена ли dll
        /// </summary>
        private static bool fRun = false;

        /// <summary>
        /// Класс в библиотеке
        /// </summary>
        private Type t;

        /// <summary>
        /// Созданный экземпляр класса
        /// </summary>
        private object instanceClass;

        private NumberStyles style = NumberStyles.AllowDecimalPoint;
        #endregion

        #region Публичные методы

        /// <summary>
        /// Загрузка dll
        /// </summary>
        public void LoadDll()
        {


            // вызвать код dll Select8 запущенный в linux из под Wine



            //if (fRun) return;
            //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string pathDll = path + Calculation.TO.Main.Properties.Resources.Select8PathDll;
            //string dllSelect8 = path + Calculation.TO.Main.Properties.Resources.Select8Path;
            //Directory.SetCurrentDirectory(dllSelect8);

            //// загрузка dll    
            //Assembly asm = Assembly.LoadFrom(pathDll);
            //t = asm.GetType("Select8.Select8Main", true, true);
            //// создаем экземпляр класса Program
            //instanceClass = Activator.CreateInstance(t);

            //Directory.SetCurrentDirectory(path);
            //fRun = true;
        }

        /// <summary>
        /// загружена ли dll
        /// </summary>
        /// <returns></returns>
        public bool IsLoadDll()
        {
            if (instanceClass != null) return true;
            else return false;
        }

        /// <summary>
        /// Подбор компрессоров - получение данных копрессоров для входных величин
        /// </summary>
        /// <param name="i_TEvap"></param>
        /// <param name="i_TOvrH"></param>
        /// <param name="i_TCond"></param>
        /// <param name="i_TSubC"></param>
        /// <param name="totCap">необходимая мощность</param>
        /// <param name="dxCxMas">необходимый массовый расход</param>
        /// <returns></returns>
        public (SelectCompressors, List<SelectCompressors>) SelectCompessors(double i_TEvap, double i_TOvrH,
            double i_TCond, double i_TSubC, double totCap, double dxCxMas)
        {
            Object[] objects = new Object[4];
            objects[0] = i_TEvap;
            objects[1] = i_TOvrH;
            objects[2] = i_TCond;
            objects[3] = i_TSubC;
            List<string> compr_ZP385KCE_TWD = (List<string>)RunMetod("GetResultCompr_ZP385KCE_TWD", objects);
            List<string> compr_ZP485KCE_TWD = (List<string>)RunMetod("GetResultCompr_ZP485KCE_TWD", objects);

            List<SelectCompressors> compressors = new List<SelectCompressors>();
            SelectCompressors selectCompessors = new SelectCompressors();
            double compr_ZP385KCE_TWD_coolCap;
            double compr_ZP385KCE_TWD_massFlow = AddCompessor(compr_ZP385KCE_TWD, compressors, out compr_ZP385KCE_TWD_coolCap);
            double compr_ZP485KCE_TWD_coolCap;
            double compr_ZP485KCE_TWD_massFlow = AddCompessor(compr_ZP485KCE_TWD, compressors, out compr_ZP485KCE_TWD_coolCap);

            // процент на подбор компрессора
            double procent = 0.2;
            if (dxCxMas >= compr_ZP485KCE_TWD_massFlow)
            {
                selectCompessors = compressors[1];
                compressors.RemoveAt(1);
            }
            else if (dxCxMas >= compr_ZP385KCE_TWD_massFlow)
            {
                selectCompessors = compressors[0];
                compressors.RemoveAt(0);
            }
            else
            {
                if ((dxCxMas * (1 - procent)) >= compr_ZP385KCE_TWD_massFlow || dxCxMas >= compr_ZP385KCE_TWD_massFlow)
                {
                    selectCompessors = compressors[0];
                    compressors.RemoveAt(0);
                }
            }
            return (selectCompessors, compressors);
        }

        #endregion

        #region Приватные методы

        /// <summary>
        /// Вызов метода
        /// </summary>
        /// <param name="name"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        private object RunMetod(string name, object[] objects)
        {
            // получаем метод 
            MethodInfo method = t.GetMethod(name);

            // вызываем метод, передаем ему значения для параметров и получаем результат
            return method.Invoke(instanceClass, objects);
        }

        /// <summary>
        /// Добавить компрессор в список "compressors"
        /// </summary>
        /// <param name="compr"></param>
        /// <param name="compressors"></param>
        /// <param name="coolCap"></param>
        /// <returns></returns>
        private double AddCompessor(List<string> compr, List<SelectCompressors> compressors, out double coolCap)
        {
            double d = 0;
            string massFlow = "";
            coolCap = 0;
            if (compr.Count > 7)
            {
                d = GS.StringToDouble(compr[5]);
                d *= 3.6;
                massFlow = d.ToString("F2", CultureInfo.InvariantCulture);

                compressors.Add(new SelectCompressors()
                {
                    Manufacturer = Calculation.TO.Main.Properties.Resources.Select8,
                    Compessor = compr[0],
                    RefrigerationCapacity = compr[1],
                    PowerInput = compr[2],
                    COP = compr[3],
                    Current = compr[4],
                    MassFlow = massFlow,
                    HeatRejection = compr[6],
                    Voltage = Calculation.TO.Main.Properties.Resources.Vol380V50H3,
                });
            }
            return d;
        }

        #endregion
    }
}
