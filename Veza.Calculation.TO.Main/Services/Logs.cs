using Veza.HeatExchanger.Interfaces;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.Services
{
    // плагин NLog
    sealed public class Logs : ILogs
    {
        #region Внутренние поля и переменные
        public NLog.Logger Logger { get; set; }
        private LoggingConfiguration config;
        private FileTarget logfile;
        /// <summary>
        /// Имя файла лога
        /// </summary>
        private string FileNameLoging { get; set; }
        #endregion

        #region Конструктор
        public Logs()
        {
            try
            {
                //string mydocu = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //string path = Path.Combine(mydocu, Calculation.TO.Main.Properties.Resources.PathVeza);
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //path = Path.Combine(path, Calculation.TO.Main.Properties.Resources.PathDll);
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //FileNameLoging = Path.Combine(path, Calculation.TO.Main.Properties.Resources.PathLogs);

                //config = new NLog.Config.LoggingConfiguration();

                //logfile = new NLog.Targets.FileTarget("logfile") { FileName = FileNameLoging };

                //// Rules for mapping loggers to targets            
                //config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
                //config.AddRule(LogLevel.Error, LogLevel.Fatal, logfile);

                //// Apply config           
                //NLog.LogManager.Configuration = config;
                //Logger = NLog.LogManager.GetCurrentClassLogger();

                //Logger.Info(" ");
                //Logger.Info(" ");
                //Logger.Info(" ");
                //Logger.Info("----------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(Calculation.TO.Main.Properties.Resources.strError", StaticData.ci), Calculation.TO.Main.Properties.Resources.strErrorLog", StaticData.ci) + ex.Message);
            }
        }
        #endregion
    }
}
