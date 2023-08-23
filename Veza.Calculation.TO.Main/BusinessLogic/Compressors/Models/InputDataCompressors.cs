using Veza.Calculation.TO.Main.Interfaces;
using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.BusinessLogic.Compressors.Models
{
    public sealed class InputDataCompressors
    {
        #region Привязанные к xaml свойства

        private static double i_TEvap;
        /// <summary>
        /// Температура кипения С
        /// </summary>        
        public double I_TEvap
        {
            get => i_TEvap;
            set
            {
                i_TEvap = value;
                if (!runF)
                {
                    runF = true;
                    I_TOvrH = I_TSucGas - value;
                    runF = false;
                }
            }
        }

        private static double i_TSucGas;
        /// <summary>
        /// Температура всасываемого газа
        /// </summary>
        public double I_TSucGas
        {
            get => i_TSucGas;
            set
            {
                i_TSucGas = value;
                if (!runF)
                {
                    runF = true;
                    I_TOvrH = value - i_TEvap;
                    runF = false;
                }
            }
        }

        private static double i_TOvrH;
        /// <summary>
        /// Перегрев всас. газа К
        /// </summary>
        public double I_TOvrH
        {
            get => i_TOvrH;
            set
            {
                i_TOvrH = value;
                if (_inputData != null)
                {
                    _inputData.I_TOvrHCX = value;
                }
                if (!runF)
                {
                    runF = true;
                    I_TSucGas = value + I_TEvap;
                    runF = false;
                }

            }
        }

        private static double i_TCond;
        /// <summary>
        /// Температура конденсации С = 45
        /// </summary>  
        public double I_TCond
        {
            get => i_TCond;
            set
            {
                i_TCond = value;
                if (!runF)
                {
                    runF = true;
                    I_TSubC = value - LiquidTemp;
                    runF = false;
                }
                if (!ExternSet)
                {
                    if (_inputData != null)
                    {
                        _inputData.SetI_TCond(value);
                    }
                }
            }
        }

        private static double i_TSubC;
        /// <summary>
        /// Переохлаждение  = 0
        /// </summary>
        public double I_TSubC
        {
            get => i_TSubC;
            set
            {
                i_TSubC = value;
                if (!ExternSet)
                {
                    if (_inputData != null)
                    {
                        _inputData.SetI_TSubC(value);
                    }
                }
                if (!runF)
                {
                    runF = true;
                    LiquidTemp = i_TCond - value;
                    runF = false;
                }
            }
        }

        private static double liquidTemp;
        /// <summary>
        /// температура жидкости   = 45
        /// </summary>
        public double LiquidTemp
        {
            get => liquidTemp;
            set
            {
                liquidTemp = value;
                if (!runF)
                {
                    if (!ExternSet)
                    {
                        if (_inputData != null)
                        {
                            _inputData.LiquidTempCX = value;
                        }
                    }
                    runF = true;
                    I_TSubC = i_TCond - value;
                    runF = false;
                }
            }
        }
        #endregion

        #region Внутренние поля 
        private static IInputData _inputData;
        private static bool runF;
        private static bool ExternSet = false;
        #endregion

        #region Конструкторы

        public InputDataCompressors(IInputData inputData) :this()
        {
            _inputData = inputData;           
        }

        public InputDataCompressors()
        {
            I_TEvap = 7;
            I_TSucGas = 12;
            I_TOvrH = 5;
            I_TCond = 45;
            I_TSubC = 3;
            LiquidTemp = 42;
        }

        #endregion

        #region Публичные методы

        public void SetI_TCond(double value)
        {
            ExternSet = true;
            I_TCond = value;
            ExternSet = false;
        }

        public void SetI_TSubC(double value)
        {
            ExternSet = true;
            I_TSubC = value;
            ExternSet = false;
        }
        #endregion
    }
}
