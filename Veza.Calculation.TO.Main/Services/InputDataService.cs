using System.Collections.ObjectModel;
using System.Globalization;
using Veza.Calculation.TO.Main.Interfaces;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Models;
using System.Collections.Generic;
using System.Linq;

namespace Veza.HeatExchanger.Services
{
    /// <summary>
    /// работает с входными данными класса InputData
    /// </summary>
    sealed public class InputDataService : IInputDataService
    {
        private readonly IInputData inputData;
        private NumberStyles style = NumberStyles.AllowDecimalPoint;
        public InputDataService(IInputData inputData)
        {
            this.inputData = inputData;
        }

        // Возвращяют значения для длинного производственного кода
        /// <summary>
        /// Возвращяет код материала корпуса
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_I_CasMat()
        {
            if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_OSBP)
            {
                return "ОЦ";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_AL)
            {
                return "АМГ";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_OSSPP)
            {
                return "ОЦП";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_NSSPP)
            {
                return "НЖП";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_NSNO316)
            {
                return "НЖ2";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_NSBP)
            {
                return "НЖ";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_CU)
            {
                return "М";
            }
            return "_"; // ошибка
        }
        /// <summary>
        /// Возвращяет MATHDR - материал коллектора
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_I_MatHdr()
        {
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SSPC)
            {
                return "НЖП";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_316)
            {
                return "НЖ2";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Cu)
            {
                return "М";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CuNi)
            {
                return "МН";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Steel)
            {
                return "НЖ";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CuBl)
            {
                return "МБГ";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CUPC)
            {
                return "МП";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SP)
            {
                return "СГР";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SPC)
            {
                return "СП";
            }
            return "_"; // ошибка
        }
        /// <summary>
        /// ориентация патрубков - расположение в пространстве
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_I_Esapo(CalcMode calcMode)
        {
            if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoBD)
            {
                return "0"; // 0
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoBU)
            {
                if (calcMode == CalcMode.Evaporater)
                    return "2";
                return "1"; // 1
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoBUD)
            {
                return "2"; // 2
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoFD)
            {
                return "3"; // 3
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoFU)
            {
                if (calcMode == CalcMode.Evaporater)
                    return "5";
                return "4"; // 4 
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoFUD)
            {
                return "5"; // 5
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoLBI)
            {
                return "6"; // 6
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoLBO)
            {
                return "7"; // 7
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoVFB)
            {
                return "8"; // 8  уточнить для прямотока
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoVFT)
            {
                return "9"; // 9 уточнить для прямотока
            }
            return "_";// ошибка
        }
        /// <summary>
        /// Направление потока воздуха
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_AirFlowDirection()
        {
            if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }
        /// <summary>
        /// Подключение теплоносителя
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_ConnectingCoolant()
        {
            if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne)
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }
        /// <summary>
        /// Ориентация патрубков
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_I_CDir()
        {
            if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_0)
            {
                return "0"; // 0
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_1)
            {
                return "1"; // 1
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_2)
            {
                return "2"; // 2
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_3)
            {
                return "3"; // 3
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_4)
            {
                return "4"; // 4 
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_5)
            {
                return "5"; // 5
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_6)
            {
                return "6"; // 6
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_7)
            {
                return "7"; // 7
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_8)
            {
                return "8"; // 8  уточнить для прямотока
            }
            return "_";// ошибка
        }
        /// <summary>
        /// Возвращяет букву которая кодирует диаметр капиляра для режима Испаритель
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_Capilar()
        {
            switch (inputData.ProjectsOut.O_Capilar)
            {
                case "5":
                    return "А";
                case "6":
                    return "Б";
                case "8":
                    return "Б";  // пока не утверждено,в Dll - есть такое значение, в эксель - нет
                case "10":
                    return "Б";  // пока не утверждено,в Dll - есть такое значение, в эксель - нет
            }
            return "_"; // Ошибка
        }
        /// <summary>
        /// Возвращяет длину капиляра
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_CapLen()
        {
            return inputData.ProjectsOut.O_CapLen;
        }
        /// <summary>
        /// Размер рамы снизу
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameB()
        {
            return "." + inputData.ProjectsOut.O_FrameB;
        }
        /// <summary>
        /// Размер рамы сверху
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameT()
        {
            return "." + inputData.ProjectsOut.O_FrameT;
        }
        /// <summary>
        /// Толщина рамы
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameThk()
        {
            double d = GS.StringToDouble(inputData.ProjectsOut.O_FrameThk);
            if (d != double.NaN)
            {
                double d10 = d * 10;
                return "х" + ((int)d10).ToString();
            }
            return "_"; // Ошибка
        }
        /// <summary>
        /// Размер рамы на стороне коллектора
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameL()
        {
            return "-" + inputData.ProjectsOut.O_FrameL;
        }
        /// <summary>
        /// Размер рамы на стороне "кривой"
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_O_FrameR()
        {
            return "." + inputData.ProjectsOut.O_FrameR;
        }
        /// <summary>
        /// исполнение корпуса
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_CasingType()
        {
            if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_1)
            {
                return "01";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_2)
            {
                return "02";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_3)
            {
                return "03";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_4)
            {
                return "04";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_5)
            {
                return "05";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_6)
            {
                return "06";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_7)
            {
                return "07";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_9)
            {
                return "09";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_10)
            {
                return "10";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_11)
            {
                return "11";
            }
            return "_"; // ошибка
        }
        /// <summary>
        /// Тип патрубков
        /// </summary>
        /// <returns></returns>
        public string GetProdCode_I_ConType()
        {
            if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_1)
            {
                return "1";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_2)
            {
                return "2";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_3)
            {
                return "3";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_4)
            {
                return "4";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_5)
            {
                return "5";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_6)
            {
                return "6";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_7)
            {
                return "7";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_8)
            {
                return "8";
            }
            return "_"; // ошибка
        }

        // Возвращяют значения для короткого клиентского кода теплообменника 
        /// <summary>
        /// Возвращяет код материала корпуса
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_I_CasMat()
        {
            if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_OSBP)
            {
                return "1";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_AL)
            {
                return "3";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_OSSPP)
            {
                return "6";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_NSSPP)
            {
                return "7";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_NSNO316)
            {
                return "9";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_NSBP)
            {
                return "2";
            }
            else if (inputData.Selected_I_CasMat == Calculation.TO.Main.Properties.Resources.CASMAT_CU)
            {
                return "4";
            }
            return "_"; // ошибка
        }
        public void SetI_CasMat(string str)
        {
            switch (str)
            {
                case "1":
                    inputData.Selected_I_CasMat = Calculation.TO.Main.Properties.Resources.CASMAT_OSBP;
                    break;
                case "3":
                    inputData.Selected_I_CasMat = Calculation.TO.Main.Properties.Resources.CASMAT_AL;
                    break;
                case "6":
                    inputData.Selected_I_CasMat = Calculation.TO.Main.Properties.Resources.CASMAT_OSSPP;
                    break;
                case "7":
                    inputData.Selected_I_CasMat = Calculation.TO.Main.Properties.Resources.CASMAT_NSSPP;
                    break;
                case "9":
                    inputData.Selected_I_CasMat = Calculation.TO.Main.Properties.Resources.CASMAT_NSNO316;
                    break;
                case "2":
                    inputData.Selected_I_CasMat = Calculation.TO.Main.Properties.Resources.CASMAT_NSBP;
                    break;
                case "4":
                    inputData.Selected_I_CasMat = Calculation.TO.Main.Properties.Resources.CASMAT_CU;
                    break;
            }
        }

        /// <summary>
        /// Покрытие коллектора
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_I_CSheet()
        {
            if (inputData.Selected_I_CSheet == Calculation.TO.Main.Properties.Resources.CSHEET_SingleAirIn)
            {
                return "1";
            }
            else if (inputData.Selected_I_CSheet == Calculation.TO.Main.Properties.Resources.CSHEET_SingleAirOut)
            {
                return "2";
            }
            else if (inputData.Selected_I_CSheet == Calculation.TO.Main.Properties.Resources.CSHEET_Without)
            {
                return "0";
            }
            return "_"; // ошибка
        }
        public void SetI_CSheet(string str)
        {
            switch (str)
            {
                case "1":
                    inputData.Selected_I_CSheet = Calculation.TO.Main.Properties.Resources.CSHEET_SingleAirIn;
                    break;
                case "2":
                    inputData.Selected_I_CSheet = Calculation.TO.Main.Properties.Resources.CSHEET_SingleAirOut;
                    break;
                case "3":
                    inputData.Selected_I_CSheet = Calculation.TO.Main.Properties.Resources.CSHEET_Without;
                    break;
            }
        }
        /// <summary>
        /// Припуск на обработку по длине покрытия трубопровода коллектора ( в мм)
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_I_CSheetL()
        {
            return inputData.I_CSheetL.ToString();
        }
        public void SetI_CSheetL(string str)
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            int i = 0;
            if (int.TryParse(str, style, ci, out i))
            {
                inputData.I_CSheetL = i;
            }
        }
        public string GetI_CSheetL()
        {
            return inputData.I_CSheetL.ToString();
        }
        /// <summary>
        /// Тип патрубков
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_I_ConType()
        {
            if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_1)
            {
                return "2";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_2)
            {
                return "1";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_3)
            {
                return "2";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_4)
            {
                return "1";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_5)
            {
                return "2";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_6)
            {
                return "2";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_7)
            {
                return "2";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_8)
            {
                return "3";
            }
            return "_"; // ошибка
        }
        /// <summary>
        /// Возвращяет MATHDR - материал коллектора
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_I_MatHdr()
        {
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SSPC)
            {
                return "7";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_316)
            {
                return "9";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Cu)
            {
                return "4";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CuNi)
            {
                return "8";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Steel)
            {
                return "2";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CuBl)
            {
                return "3";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CUPC)
            {
                return "5";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SP)
            {
                return "1";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SPC)
            {
                return "6";
            }
            return "_"; // ошибка
        }

        /// <summary>
        /// исполнение корпуса
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_CasingType()
        {
            if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_1)
            {
                return "1";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_1_4835)
            {
                return "1";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_2)
            {
                return "2";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_3)
            {
                return "3";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_4)
            {
                return "4";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_5)
            {
                return "5";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_6)
            {
                return "6";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_7)
            {
                return "7";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_9)
            {
                return "9";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_10)
            {
                return "10";
            }
            else if (inputData.SelectedCasingType == Calculation.TO.Main.Properties.Resources.CasingType_11)
            {
                return "11";
            }
            return "_"; // ошибка
        }
        /// <summary>
        /// Возвращяет код-строку ориентации патрубка
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_I_CDir()
        {
            if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_0)
            {
                return "0";
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_1)
            {
                return "1";
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_2)
            {
                return "2";
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_3)
            {
                return "3";
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_4)
            {
                return "4";
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_5)
            {
                return "5";
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_6)
            {
                return "6";
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_6)
            {
                return "7";
            }
            else if (inputData.Select_I_CDir == Calculation.TO.Main.Properties.Resources.ConnectionDirection_6)
            {
                return "8";
            }

            return "_";// ошибка
        }
        public void SetI_CDir(string str)
        {
            switch (str)
            {
                case "0":
                    inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_0;
                    break;
                case "1":
                    inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_1;
                    break;
                case "2":
                    inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_2;
                    break;
                case "3":
                    inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_3;
                    break;
                case "4":
                    inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_4;
                    break;
                case "5":
                    inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_5;
                    break;
                case "6":
                    inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_6;
                    break;
                case "7":
                    inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_6;
                    break;
                case "8":
                    inputData.Select_I_CDir = Calculation.TO.Main.Properties.Resources.ConnectionDirection_6;
                    break;
            }
        }
        /// <summary>
        /// Диаметр коллектора вход
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_O_ConIN(bool overload = false)
        {
            if (inputData.ProjectsOut.O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN10)
            {
                return "010";
            }
            else if (inputData.ProjectsOut.O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN15)
            {
                return "015";
            }
            else if (inputData.ProjectsOut.O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN20)
            {
                return "020";
            }
            else if (inputData.ProjectsOut.O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN25)
            {
                return "025";
            }
            else if (inputData.ProjectsOut.O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN32)
            {
                return "032";
            }
            else if (inputData.ProjectsOut.O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN40)
            {
                return "040";
            }
            else if (inputData.ProjectsOut.O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN50)
            {
                return "050";
            }
            else if (inputData.ProjectsOut.O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN65)
            {
                return "065";
            }
            else if (inputData.ProjectsOut.O_ConIN == Calculation.TO.Main.Properties.Resources.CONINF_DN80)
            {
                return "080";
            }
            if (overload) return "_";
            return GetShortCode_O_ConOut(true);
        }
        /// <summary>
        /// Диаметр коллектора выход
        /// </summary>
        /// <returns></returns>
        public string GetShortCode_O_ConOut(bool overload = false)
        {
            if (inputData.ProjectsOut.O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN10)
            {
                return "010";
            }
            else if (inputData.ProjectsOut.O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN15)
            {
                return "015";
            }
            else if (inputData.ProjectsOut.O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN20)
            {
                return "020";
            }
            else if (inputData.ProjectsOut.O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN25)
            {
                return "025";
            }
            else if (inputData.ProjectsOut.O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN32)
            {
                return "032";
            }
            else if (inputData.ProjectsOut.O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN40)
            {
                return "040";
            }
            else if (inputData.ProjectsOut.O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN50)
            {
                return "050";
            }
            else if (inputData.ProjectsOut.O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN65)
            {
                return "065";
            }
            else if (inputData.ProjectsOut.O_ConOut == Calculation.TO.Main.Properties.Resources.CONINF_DN80)
            {
                return "080";
            }
            if (overload) return "_";
            return GetShortCode_O_ConIN(true);
        }

        // возвращяют значения используемые при расчёте внешней библиотекой
        /// <summary>
        /// Возвращяет код CONINF - Присоединительный размер коллектора на входе в дюймах
        /// </summary>
        /// <returns></returns>
        public string GetCalc_I_ConIn()
        {
            if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_0)
            {
                return "0";
            }
            else if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_DN10)
            {
                if (Get_I_MatHdrIsCu())
                    return "101";
                else return "1";
            }
            else if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_DN15)
            {
                if (Get_I_MatHdrIsCu())
                    return "102";
                else return "2";
            }
            else if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_DN20)
            {
                if (Get_I_MatHdrIsCu())
                    return "103";
                else return "3";
            }
            else if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_DN25)
            {
                if (Get_I_MatHdrIsCu())
                    return "104";
                else return "4";
            }
            else if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_DN32)
            {
                if (Get_I_MatHdrIsCu())
                    return "105";
                else return "5";
            }
            else if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_DN40)
            {
                if (Get_I_MatHdrIsCu())
                    return "106";
                else return "6";
            }
            else if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_DN50)
            {
                if (Get_I_MatHdrIsCu())
                    return "107";
                else return "7";
            }
            else if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_DN65)
            {
                if (Get_I_MatHdrIsCu())
                    return "108";
                else return "8";
            }
            else if (inputData.Selected_I_ConIn == Calculation.TO.Main.Properties.Resources.CONINF_DN80)
            {
                if (Get_I_MatHdrIsCu())
                    return "109";
                else return "9";
            }
            return "_"; // ошибка
        }
        public void SetI_ConIn(string str)
        {
            switch (str)
            {
                case "0":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_0;
                    break;
                case "1":
                case "101":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_DN10;
                    break;
                case "2":
                case "102":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_DN15;
                    break;
                case "3":
                case "103":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_DN20;
                    break;
                case "4":
                case "104":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_DN25;
                    break;
                case "5":
                case "105":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_DN32;
                    break;
                case "6":
                case "106":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_DN40;
                    break;
                case "7":
                case "107":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_DN50;
                    break;
                case "8":
                case "108":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_DN65;
                    break;
                case "9":
                case "109":
                    inputData.Selected_I_ConIn = Calculation.TO.Main.Properties.Resources.CONINF_DN80;
                    break;
            }
        }
        /// <summary>
        /// ориентация патрубков - расположение в пространстве
        /// для расчёта
        /// </summary>
        /// <returns></returns>
        public string GetCalc_I_Esapo()
        {
            if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoBD)
            {
                if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "H2";
                    }
                    else
                    {
                        return "H1";
                    }
                }
                else if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantTwo)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "H4";
                    }
                    else
                    {
                        return "H3";
                    }
                }
                return "H1"; // 0
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoBU)
            {
                if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                {
                    return "V7"; // 1
                }
                else
                {
                    return "V8";
                }
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoBUD)
            {
                if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                {
                    return "V7"; // 2
                }
                else
                {
                    return "V8";
                }
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoFD)
            {
                if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "V2";
                    }
                    else
                    {
                        return "V1";
                    }
                }
                else if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantTwo)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "V4";
                    }
                    else
                    {
                        return "V3";
                    }
                }
                return "V1"; // 3
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoFU)
            {
                if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "H2";
                    }
                    else
                    {
                        return "H1";
                    }
                }
                else if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantTwo)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "H4";
                    }
                    else
                    {
                        return "H3";
                    }
                }
                return "H1"; // 4 
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoFUD)
            {
                if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "H2";
                    }
                    else
                    {
                        return "H1";
                    }
                }
                else if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantTwo)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "H4";
                    }
                    else
                    {
                        return "H3";
                    }
                }
                return "H1"; // 5
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoLBI)
            {
                if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "H2";
                    }
                    else
                    {
                        return "H1";
                    }
                }
                else if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantTwo)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "H4";
                    }
                    else
                    {
                        return "H3";
                    }
                }
                return "H1"; // 6
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoLBO)
            {
                if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "V2";
                    }
                    else
                    {
                        return "V1";
                    }
                }
                else if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantTwo)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "V4";
                    }
                    else
                    {
                        return "V3";
                    }
                }
                return "V1"; // 7
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoVFB)
            {
                if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "V2";
                    }
                    else
                    {
                        return "V1";
                    }
                }
                else if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantTwo)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "V4";
                    }
                    else
                    {
                        return "V3";
                    }
                }
                return "V1"; // 8  уточнить для прямотока
            }
            else if (inputData.SelectedEsapo == Calculation.TO.Main.Properties.Resources.EsapoVFT)
            {
                if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantOne)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "V2";
                    }
                    else
                    {
                        return "V1";
                    }
                }
                else if (inputData.SelectedConnectingCoolant == Calculation.TO.Main.Properties.Resources.ConnectingTheCoolantTwo)
                {
                    if (inputData.SelectedAirFlowDirection == Calculation.TO.Main.Properties.Resources.AirFlowDirectionLeft)
                    {
                        return "V4";
                    }
                    else
                    {
                        return "V3";
                    }
                }
                return "V1"; // 9 уточнить для прямотока
            }
            return "_";// ошибка
        }
        /// <summary>
        /// Тип патрубков
        /// </summary>
        /// <returns></returns>
        public string GetCalc_I_ConType()
        {
            if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_1)
            {
                return "0";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_2)
            {
                return "0";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_3)
            {
                return "1";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_4)
            {
                return "1";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_5)
            {
                return "1";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_6)
            {
                return "0";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_7)
            {
                return "0";
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_8)
            {
                return "2";
            }
            return "_"; // ошибка
        }

        public int GetCalcI_I_ConType()
        {
            if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_1)
            {
                return 1;
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_2)
            {
                return 2;
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_3)
            {
                return 3;
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_4)
            {
                return 4;
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_5)
            {
                return 5;
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_6)
            {
                return 6;
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_7)
            {
                return 7;
            }
            else if (inputData.Selected_I_ConType == Calculation.TO.Main.Properties.Resources.CONTPA_8)
            {
                return 8;
            }
            return 9; // ошибка
        }

        public void SetI_ConType(int i)
        {
            switch (i)
            {
                case 1:
                    inputData.Selected_I_ConType = Calculation.TO.Main.Properties.Resources.CONTPA_1;
                    break;
                case 2:
                    inputData.Selected_I_ConType = Calculation.TO.Main.Properties.Resources.CONTPA_2;
                    break;
                case 3:
                    inputData.Selected_I_ConType = Calculation.TO.Main.Properties.Resources.CONTPA_3;
                    break;
                case 4:
                    inputData.Selected_I_ConType = Calculation.TO.Main.Properties.Resources.CONTPA_4;
                    break;
                case 5:
                    inputData.Selected_I_ConType = Calculation.TO.Main.Properties.Resources.CONTPA_5;
                    break;
                case 6:
                    inputData.Selected_I_ConType = Calculation.TO.Main.Properties.Resources.CONTPA_6;
                    break;
                case 7:
                    inputData.Selected_I_ConType = Calculation.TO.Main.Properties.Resources.CONTPA_7;
                    break;
                case 8:
                    inputData.Selected_I_ConType = Calculation.TO.Main.Properties.Resources.CONTPA_8;
                    break;
            }
        }
        /// <summary>
        /// Возвращяет MATHDR - материал коллектора
        /// </summary>
        /// <returns></returns>
        public string GetCalc_I_MatHdr()
        {
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SSPC)
            {
                return "6";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_316)
            {
                return "5";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Cu)
            {
                return "1";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CuNi)
            {
                return "11";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Steel)
            {
                return "6";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CuBl)
            {
                return "1";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CUPC)
            {
                return "1";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SP)
            {
                return "6";
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_SPC)
            {
                return "6";
            }
            return "_"; // ошибка
        }
        public void SetI_MatHdr(string str)
        {
            switch (str)
            {
                //case "6":
                //inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_SP;
                //break;
                case "6":
                    inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_Steel;
                    break;
                //case "1":
                //    inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_CuBl;
                //    break;
                case "1":
                    inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_Cu;
                    break;
                //case "1":
                //    inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_CUPC;
                //    break;
                //case "6":
                //    inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_SPC;
                //    break;
                //case "6":
                //    inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_SSPC;
                //    break;
                case "11":
                    inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_CuNi;
                    break;
                case "5":
                    inputData.Selected_I_MatHdr = Calculation.TO.Main.Properties.Resources.MATHDR_316;
                    break;
            }
        }
        public string GetI_MatHdr()
        {
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Steel)
                return "6";
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Cu)
                return "1";
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CuNi)
                return "11";
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_316)
                return "5";
            return "";
        }

        /// <summary>
        /// Припуск на обработку по длине покрытия трубопровода коллектора ( в мм)
        /// </summary>
        /// <returns></returns>
        public string GetString_I_CSheetL()
        {
            return inputData.I_CSheetL.ToString();
        }
        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        public string GetString_ValueAirFlowDX()
        {
            return inputData.ValueAirFlowDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        public string GetString_I_AirTempInDX()
        {
            return inputData.I_AirTempInDX.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Возвращяет размер коллектора в дюймах
        /// </summary>
        /// <returns></returns>
        public string GetOut_I_ConIn(string code)
        {
            switch (code)
            {
                case "1":
                case "101":
                    return Calculation.TO.Main.Properties.Resources.CONINF_DN10;
                case "2":
                case "102":
                    return Calculation.TO.Main.Properties.Resources.CONINF_DN15;
                case "3":
                case "103":
                    return Calculation.TO.Main.Properties.Resources.CONINF_DN20;
                case "4":
                case "104":
                    return Calculation.TO.Main.Properties.Resources.CONINF_DN25;
                case "5":
                case "105":
                    return Calculation.TO.Main.Properties.Resources.CONINF_DN32;
                case "6":
                case "106":
                    return Calculation.TO.Main.Properties.Resources.CONINF_DN40;
                case "7":
                case "107":
                    return Calculation.TO.Main.Properties.Resources.CONINF_DN50;
                case "8":
                case "108":
                    return Calculation.TO.Main.Properties.Resources.CONINF_DN65;
                case "9":
                case "109":
                    return Calculation.TO.Main.Properties.Resources.CONINF_DN80;
                default:
                    return "0";
            }
        }

        /// <summary>
        /// Возвраящет является ли матриал коллектора - медь
        /// </summary>
        /// <returns></returns>
        bool Get_I_MatHdrIsCu()
        {
            if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_Cu)
            {
                return true;
            }
            else if (inputData.Selected_I_MatHdr == Calculation.TO.Main.Properties.Resources.MATHDR_CuNi)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Установка списка ориентации теплообменника для Воздухонагревателя
        /// </summary>
        public void SetEsapoHeater()
        {
            if (inputData.Esapo == null)
            {
                inputData.Esapo = new ObservableCollection<string>();
            }
            inputData.Esapo.Clear();
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoBU);
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoFU);
            //inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoLBI);
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoLBO);
            //inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoVFT);
            //inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoVFB);
            inputData.SelectedEsapo = Calculation.TO.Main.Properties.Resources.EsapoLBO;
        }
        /// <summary>
        /// Установка списка ориентации теплообменника для Воздухоохлдителя
        /// </summary>
        public void SetEsapoCooler()
        {
            if (inputData.Esapo == null)
            {
                inputData.Esapo = new ObservableCollection<string>();
            }
            inputData.Esapo.Clear();
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoBU);
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoFU);
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoLBO);
            inputData.SelectedEsapo = Calculation.TO.Main.Properties.Resources.EsapoLBO;
        }
        /// <summary>
        /// Установка списка ориентации теплообменника для Паравого нагревателя
        /// </summary>
        public void SetEsapoSteamHeater()
        {
            inputData.Esapo.Clear();
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoBD);
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoFD);
            inputData.SelectedEsapo = Calculation.TO.Main.Properties.Resources.EsapoBD;
        }
        /// <summary>
        /// Установка списка ориентации теплообменника для Конденсатора
        /// </summary>
        public void SetEsapoCondensator()
        {
            inputData.Esapo.Clear();
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoBU);
            inputData.SelectedEsapo = Calculation.TO.Main.Properties.Resources.EsapoBU;
        }
        /// <summary>
        /// Установка списка ориентации теплообменника для Испарителя
        /// </summary>
        public void SetEsapoEvaporater()
        {
            inputData.Esapo.Clear();
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoBUD);
            inputData.Esapo.Add(Calculation.TO.Main.Properties.Resources.EsapoFUD);
            inputData.SelectedEsapo = Calculation.TO.Main.Properties.Resources.EsapoBUD;
        }

        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        public string GetString_ValueAirFlowHW()
        {
            return inputData.ValueAirFlowHW.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        public string GetString_I_AirTempInHW()
        {
            return inputData.I_AirTempInHW.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        public string GetString_ValueAirFlowCW()
        {
            return inputData.ValueAirFlowCW.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        public string GetString_I_AirTempInCW()
        {
            return inputData.I_AirTempInCW.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        public string GetString_ValueAirFlowST()
        {
            return inputData.ValueAirFlowST.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        public string GetString_I_AirTempInST()
        {
            return inputData.I_AirTempInST.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// расход воздуха в виде string
        /// </summary>
        /// <returns></returns>
        public string GetString_ValueAirFlowCX()
        {
            return inputData.ValueAirFlowCX.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// температура воздуха на входе
        /// </summary>
        /// <returns></returns>
        public string GetString_I_AirTempInCX()
        {
            return inputData.I_AirTempInCX.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Установка мощности или температуры воздуха на выходе
        /// </summary>
        /// <param name="param"></param>
        public void SetValueCapacityHW(string param)
        {
            if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.Capacity)
            {
                inputData.ValueCapacityHW = GS.StringToDouble(param);
            }
            else //if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.I_AirTempOut)
            {
                inputData.DirectAirTempOutHW = GS.StringToDouble(param);
            }
        }

        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        public string GetValueCapacityHW()
        {
            if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.Capacity)
                return inputData.ValueCapacityHW.ToString("G", CultureInfo.InvariantCulture);
            else
                return inputData.DirectAirTempOutHW.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Установить длину оребрения
        /// </summary>
        /// <param name="param"></param>
        public void SetValueWidthFin(string param)
        {
            inputData.ValueWidthFin = GS.StringToDouble(param);
        }
        /// <summary>
        /// Получить длину оребрения типа string
        /// </summary>
        public string GetValueWidthFin()
        {
            return inputData.ValueWidthFin.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Установить высоту оребрения
        /// </summary>
        /// <param name="param"></param>
        public void SetValueHightFin(string param)
        {
            inputData.ValueHightFin = GS.StringToDouble(param);
        }
        /// <summary>
        /// Получить высоту оребрения типа string
        /// </summary>
        public string GetValueHightFin()
        {
            return inputData.ValueHightFin.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Установить число труб
        /// </summary>
        /// <param name="param"></param>
        public void SetTubesN(string param)
        {
            int d;
            if (int.TryParse(param, out d))
            {
                inputData.TubesN = d;
            }
        }
        /// <summary>
        /// Получить число труб типа string
        /// </summary>
        /// <param name="param"></param>
        public string GetTubesN()
        {
            return inputData.TubesN.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка расхода воздуха
        /// </summary>
        /// <param name="param"></param>
        public void SetValueAirFlowHW(string param)
        {
            inputData.ValueAirFlowHW = GS.StringToDouble(param);
        }
        /// <summary>
        /// расход воздуха
        /// </summary>
        public string GetValueAirFlowHW()
        {
            return inputData.ValueAirFlowHW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка расхода воздуха
        /// </summary>
        /// <param name="param"></param>
        public void SetValueAirFlowST(string param)
        {
            inputData.ValueAirFlowST = GS.StringToDouble(param);
        }
        /// <summary>
        /// расход воздуха
        /// </summary>
        public string GetValueAirFlowST()
        {
            return inputData.ValueAirFlowST.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка расхода воздуха
        /// </summary>
        /// <param name="param"></param>
        public void SetValueAirFlowCW(string param)
        {
            inputData.ValueAirFlowCW = GS.StringToDouble(param);
        }
        /// <summary>
        /// расход воздуха
        /// </summary>
        public string GetValueAirFlowCW()
        {
            return inputData.ValueAirFlowCW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка температуры воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetI_AirTempInHW(string param)
        {
            inputData.I_AirTempInHW = GS.StringToDouble(param);
        }
        /// <summary>
        /// температуры воздуха на входе
        /// </summary>
        public string GetI_AirTempInHW()
        {
            return inputData.I_AirTempInHW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка температуры воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetI_AirTempInST(string param)
        {
            inputData.I_AirTempInST = GS.StringToDouble(param);
        }
        /// <summary>
        /// температуры воздуха на входе
        /// </summary>
        public string GetI_AirTempInST()
        {
            return inputData.I_AirTempInST.ToString("G", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Установка температуры воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetI_AirTempInCW(string param)
        {
            inputData.I_AirTempInCW = GS.StringToDouble(param);
        }
        /// <summary>
        /// температуры воздуха на входе
        /// </summary>
        public string GetI_AirTempInCW()
        {
            return inputData.I_AirTempInCW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка влажности воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetValueBaseHumHW(string param)
        {
            inputData.ValueBaseHumHW = GS.StringToDouble(param);
        }
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public string GetValueBaseHumHW()
        {
            return inputData.ValueBaseHumHW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка влажности воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetValueBaseHumST(string param)
        {
            inputData.ValueBaseHumST = GS.StringToDouble(param);
        }
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public string GetValueBaseHumST()
        {
            return inputData.ValueBaseHumST.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка влажности воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetValueBaseHumCW(string param)
        {
            inputData.ValueBaseHumCW = GS.StringToDouble(param);
        }
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public string GetValueBaseHumCW()
        {
            return inputData.ValueBaseHumCW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка концентрации гликоля
        /// </summary>
        /// <param name="param"></param>
        public void SetI_SoleCnz(string param)
        {
            inputData.I_SoleCnz = GS.StringToDouble(param);
        }
        /// <summary>
        /// концентрация гликоля
        /// </summary>
        /// <param name="param"></param>
        public string GetI_SoleCnz()
        {
            return inputData.I_SoleCnz.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка температуры теплоносителя на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetValueMedTempInHW(string param)
        {
            inputData.ValueMedTempInHW = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура теплоносителя на входе
        /// </summary>
        public string GetValueMedTempInHW()
        {
            return inputData.ValueMedTempInHW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка температуры теплоносителя на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetValueMedTempInCW(string param)
        {
            inputData.ValueMedTempInCW = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура теплоносителя на входе
        /// </summary>
        public string GetValueMedTempInCW()
        {
            return inputData.ValueMedTempInCW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка температуры теплоносителя на выходе 
        /// или росход теплоносителя
        /// </summary>
        /// <param name="param"></param>
        public void SetValueVariantRaschHW(string param)
        {
            double d;
            if (inputData.SelectedVariantRasch == Calculation.TO.Main.Properties.Resources.I_TempFluidOut)
            {
                inputData.ValueVariantRaschHW = GS.StringToDouble(param);
            }
            else // Calculation.TO.Main.Properties.Resources.I_MedFlow
            {
                inputData.I_MedFlowHW = GS.StringToDouble(param);
            }
        }
        /// <summary>
        /// температура теплоносителя на выходе 
        /// или росход теплоносителя
        /// </summary>
        public string GetValueVariantRaschHW()
        {
            if (inputData.SelectedVariantRasch == Calculation.TO.Main.Properties.Resources.I_TempFluidOut)
            {
                return inputData.ValueVariantRaschHW.ToString("G", CultureInfo.InvariantCulture);
            }
            else // Calculation.TO.Main.Properties.Resources.I_MedFlow
            {
                return inputData.I_MedFlowHW.ToString("G", CultureInfo.InvariantCulture);
            }
        }
        /// <summary>
        /// Установка максимального падения давления
        /// </summary>
        /// <param name="param"></param>
        public void SetValueMedKPaHW(string param)
        {
            inputData.ValueMedKPaHW = GS.StringToDouble(param);
        }
        /// <summary>
        /// Получение максимального падения давления
        /// </summary>
        public string GetValueMedKPaHW()
        {
            return inputData.ValueMedKPaHW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Устанавливаем число рядов
        /// </summary>
        /// <param name="param"></param>
        public void SetValuePipe(string param)
        {
            int d;
            if (int.TryParse(param, out d))
            {
                inputData.ValuePipe = d;
            }
        }
        /// <summary>
        /// Получение число рядов
        /// </summary>
        public string GetValuePipe()
        {
            return inputData.ValuePipe.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установка количества отводов
        /// </summary>
        /// <param name="param"></param>
        public void SetValueCircuits(string param)
        {
            int d;
            if (int.TryParse(param, out d))
            {
                inputData.ValueCircuits = d;
            }
        }
        /// <summary>
        /// Получение количества отводов
        /// </summary>
        public string GetValueCircuits()
        {
            return inputData.ValueCircuits.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Установить число ходов
        /// </summary>
        /// <param name="param"></param>
        public void SetNumOfPasses(string param)
        {
            inputData.NumOfPasses = GS.StringToDouble(param);
        }
        /// <summary>
        /// Получить число ходов
        /// </summary>
        public string GetNumOfPasses()
        {
            return inputData.NumOfPasses.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// переохлаждение, конденсатор
        /// </summary>
        /// <param name="param"></param>
        public void SetI_TSubCCX(string param)
        {
            inputData.I_TSubCCX = GS.StringToDouble(param);
        }
        /// <summary>
        /// переохлаждение, конденсатор
        /// </summary>
        public string GetI_TSubCCX()
        {
            return inputData.I_TSubCCX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// переохлаждение, Испаритель
        /// </summary>
        /// <param name="param"></param>
        public void SetI_TSubCDX(string param)
        {
            inputData.I_TSubCDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// переохлаждение, конденсатор
        /// </summary>
        public string GetI_TSubCDX()
        {
            return inputData.I_TSubCDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// температура жидкости, конденсатор
        /// </summary>
        /// <param name="param"></param>
        public void SetLiquidTempCX(string param)
        {
            inputData.LiquidTempCX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура жидкости, конденсатор
        /// </summary>
        public string GetLiquidTempCX()
        {
            return inputData.LiquidTempCX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// температура жидкости, Испаритель
        /// </summary>
        /// <param name="param"></param>
        public void SetLiquidTempDX(string param)
        {
            inputData.LiquidTempDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура жидкости, конденсатор
        /// </summary>
        public string GetLiquidTempDX()
        {
            return inputData.LiquidTempDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// давление конденсации абс.
        /// </summary>
        /// <param name="param"></param>
        public void SetCondAbsPresCX(string param)
        {
            inputData.CondAbsPresCX = GS.StringToDouble(param);
        }
        /// <summary>
        /// давление конденсации абс.
        /// </summary>
        public string GetCondAbsPresCX()
        {
            return inputData.CondAbsPresCX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// давление конденсации абс.
        /// </summary>
        /// <param name="param"></param>
        public void SetCondAbsPresDX(string param)
        {
            inputData.CondAbsPresDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// давление конденсации абс.
        /// </summary>
        public string GetCondAbsPresDX()
        {
            return inputData.CondAbsPresDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// температура конденсации
        /// </summary>
        /// <param name="param"></param>
        public void SetI_TCondCX(string param)
        {
            inputData.I_TCondCX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура конденсации
        /// </summary>
        public string GetI_TCondCX()
        {
            return inputData.I_TCondCX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// температура конденсации
        /// </summary>
        /// <param name="param"></param>
        public void SetI_TCondDX(string param)
        {
            inputData.I_TCondDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура конденсации
        /// </summary>
        public string GetI_TCondDX()
        {
            return inputData.I_TCondDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// температура горячего газа
        /// </summary>
        /// <param name="param"></param>
        public void SetI_THotGasCX(string param)
        {
            inputData.I_THotGasCX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура горячего газа
        /// </summary>
        /// <param name="param"></param>
        public string GetI_THotGasCX()
        {
            return inputData.I_THotGasCX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// температура горячего газа
        /// </summary>
        /// <param name="param"></param>
        public void SetI_THotGasDX(string param)
        {
            inputData.I_THotGasDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура горячего газа
        /// </summary>
        /// <param name="param"></param>
        public string GetI_THotGasDX()
        {
            return inputData.I_THotGasDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetValueBaseHumCX(string param)
        {
            inputData.ValueBaseHumCX = GS.StringToDouble(param);
        }
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        public string GetValueBaseHumCX()
        {
            return inputData.ValueBaseHumCX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Влажность воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetValueBaseHumDX(string param)
        {
            inputData.ValueBaseHumDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// влажность воздуха на входе
        /// </summary>
        public string GetValueBaseHumDX()
        {
            return inputData.ValueBaseHumDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetI_AirTempInCX(string param)
        {
            inputData.I_AirTempInCX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температуры воздуха на входе
        /// </summary>
        public string GetI_AirTempInCX()
        {
            return inputData.I_AirTempInCX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Температура воздуха на входе
        /// </summary>
        /// <param name="param"></param>
        public void SetI_AirTempInDX(string param)
        {
            inputData.I_AirTempInDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температуры воздуха на входе
        /// </summary>
        public string GetI_AirTempInDX()
        {
            return inputData.I_AirTempInDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        /// <param name="param"></param>
        public void SetValueAirFlowCX(string param)
        {
            inputData.ValueAirFlowCX = GS.StringToDouble(param);
        }
        /// <summary>
        /// расход воздуха
        /// </summary>
        public string GetValueAirFlowCX()
        {
            return inputData.ValueAirFlowCX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Расход воздуха
        /// </summary>
        /// <param name="param"></param>
        public void SetValueAirFlowDX(string param)
        {
            inputData.ValueAirFlowDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// расход воздуха
        /// </summary>
        public string GetValueAirFlowDX()
        {
            return inputData.ValueAirFlowDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Мощность
        /// </summary>
        /// <param name="param"></param>
        public void SetValueCapacityCX(string param)
        {
            inputData.ValueCapacityCX = GS.StringToDouble(param);
        }
        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        public string GetValueCapacityCX()
        {
            if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.Capacity)
                return inputData.ValueCapacityCX.ToString("G", CultureInfo.InvariantCulture);
            else
                return inputData.DirectAirTempOutCX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Мощность
        /// </summary>
        /// <param name="param"></param>
        public void SetValueCapacityDX(string param)
        {
            inputData.ValueCapacityDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        public string GetValueCapacityDX()
        {
            if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.Capacity)
                return inputData.ValueCapacityDX.ToString("G", CultureInfo.InvariantCulture);
            else
                return inputData.DirectAirTempOutDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Мощность
        /// </summary>
        /// <param name="param"></param>
        public void SetValueCapacityCW(string param)
        {
            inputData.ValueCapacityCW = GS.StringToDouble(param);
        }
        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        public string GetValueCapacityCW()
        {
            if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.Capacity)
                return inputData.ValueCapacityCW.ToString("G", CultureInfo.InvariantCulture);
            else
                return inputData.DirectAirTempOutCW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Мощность
        /// </summary>
        /// <param name="param"></param>
        public void SetValueCapacityST(string param)
        {
            inputData.ValueCapacityST = GS.StringToDouble(param);
        }
        /// <summary>
        /// Получение мощности или температуры воздуха на выходе
        /// </summary>
        /// <returns></returns>
        public string GetValueCapacityST()
        {
            if (inputData.SelectedDirect == Calculation.TO.Main.Properties.Resources.Capacity)
                return inputData.ValueCapacityST.ToString("G", CultureInfo.InvariantCulture);
            else
                return inputData.DirectAirTempOutST.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Давление пара
        /// </summary>
        /// <param name="param"></param>
        public void SetI_StmBar(string param)
        {
            inputData.I_StmBar = GS.StringToDouble(param);
        }
        /// <summary>
        /// Давление пара
        /// </summary>
        public string GetI_StmBar()
        {
            return inputData.I_StmBar.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Температура пара
        /// </summary>
        /// <param name="param"></param>
        public void SetI_StmTemp(string param)
        {
            inputData.I_StmTemp = GS.StringToDouble(param);
        }
        /// <summary>
        /// Температура пара
        /// </summary>
        public string GetI_StmTemp()
        {
            return inputData.I_StmTemp.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Значение списка SelectedVariantRasch
        /// </summary>
        /// <param name="param"></param>
        public void SetValueVariantRaschCW(string param)
        {
            inputData.ValueVariantRaschCW = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура теплоносителя на выходе 
        /// или росход теплоносителя
        /// </summary>
        public string GetValueVariantRaschCW()
        {
            if (inputData.SelectedVariantRasch == Calculation.TO.Main.Properties.Resources.I_TempFluidOut)
            {
                return inputData.ValueVariantRaschCW.ToString("G", CultureInfo.InvariantCulture);
            }
            else // Calculation.TO.Main.Properties.Resources.I_MedFlow
            {
                return inputData.I_MedFlowCW.ToString("G", CultureInfo.InvariantCulture);
            }
        }
        /// <summary>
        /// Максимальное падение давления
        /// </summary>
        /// <param name="param"></param>
        public void SetValueMedKPaCW(string param)
        {
            inputData.ValueMedKPaCW = GS.StringToDouble(param);
        }
        /// <summary>
        /// Получение максимального падения давления
        /// </summary>
        public string GetValueMedKPaCW()
        {
            return inputData.ValueMedKPaCW.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Количество контуров хладогента
        /// </summary>
        /// <param name="param"></param>
        public void SetI_RCircDX(string param)
        {
            int d;
            if (int.TryParse(param, out d))
            {
                inputData.I_RCircDX = d;
            }
        }
        /// <summary>
        /// Количество контуров хладогента
        /// </summary>
        public string GetI_RCircDX()
        {
            return inputData.I_RCircDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Пропускная способность байпаса
        /// </summary>
        /// <param name="param"></param>
        public void SetI_DxBypDX(string param)
        {
            inputData.I_DxBypDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// Пропускная способность байпаса
        /// </summary>
        public string GetI_DxBypDX()
        {
            return inputData.I_DxBypDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// температура кипения
        /// </summary>
        /// <param name="param"></param>
        public void SetI_TEvapDX(string param)
        {
            inputData.I_TEvapDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура кипения
        /// </summary>
        public string GetI_TEvapDX()
        {
            return inputData.I_TEvapDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// давление кипения абс.
        /// </summary>
        /// <param name="param"></param>
        public void SetEvapAbsPresDX(string param)
        {
            inputData.EvapAbsPresDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// давление кипения абс.
        /// </summary>
        public string GetEvapAbsPresDX()
        {
            return inputData.EvapAbsPresDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// температура всас. газа
        /// </summary>
        /// <param name="param"></param>
        public void SetI_TOvrHDX(string param)
        {
            inputData.I_TOvrHDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// температура всас. газа
        /// </summary>
        public string GetI_TOvrHDX()
        {
            return inputData.I_TOvrHDX.ToString("G", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// перегрев всас. газа
        /// </summary>
        /// <param name="param"></param>
        public void SetSuctGasReturnDX(string param)
        {
            inputData.SuctGasReturnDX = GS.StringToDouble(param);
        }
        /// <summary>
        /// перегрев всас. газа
        /// </summary>
        public string GetSuctGasReturnDX()
        {
            return inputData.SuctGasReturnDX.ToString("G", CultureInfo.InvariantCulture);
        }





        /// <summary>
        /// Получить список типов расчёта
        /// </summary>
        /// <returns></returns>
        public List<string> GetListMode()
        {
            return new List<string> { "direct", "reverse" };
        }
        /// <summary>
        /// Получить список Вариант расчёта - Мощность, Температура воздуха на выходе
        /// </summary>
        public List<string> GetListDirectMode()
        {
            return inputData.VariantRasch.ToList();
        }
        /// <summary>
        /// список геометрий
        /// </summary>
        public List<string> GetListGeometries()
        {
            return inputData.Geometry.ToList();
        }
        /// <summary>
        /// список трубок
        /// </summary>
        public List<string> GetListPipes()
        {
            return inputData.Pipe.ToList();
        }
        /// <summary>
        /// Список толщин оребрения
        /// </summary>
        /// <returns></returns>
        public List<string> GetListFins()
        {
            return inputData.Fin.ToList();
        }
        /// <summary>
        /// Список шагов оребрения
        /// </summary>
        /// <returns></returns>
        public List<string> GetListStepsFin()
        {
            return inputData.StepFin.ToList();
        }
        /// <summary>
        /// Список единиц измерения расхода воздуха
        /// </summary>
        /// <returns></returns>
        public List<string> GetListUnitsAirFlow()
        {
            return inputData.AirFlow.ToList();
        }
        /// <summary>
        /// Список теплоносителей
        /// </summary>
        /// <returns></returns>
        public List<string> GetListFluids()
        {
            return inputData.Fluid.ToList();
        }
        /// <summary>
        /// список вариантов расчёта по телоносителю - Температура теплоносителя на выходе, Расход теплоносителя
        /// </summary>
        /// <returns></returns>
        public List<string> GetListVariantRasch()
        {
            return inputData.VariantRasch.ToList();
        }
        /// <summary>
        /// список материалов корпуса
        /// </summary>
        public List<string> GetListI_MatHdr()
        {
            return inputData.I_MatHdr.ToList();
        }
        /// <summary>
        /// Список диаметров коллектора на входе
        /// </summary>
        /// <returns></returns>
        public List<string> GetListI_ConIn()
        {
            return inputData.I_ConIn.ToList();
        }
        /// <summary>
        /// Список диаметров коллектора на выходе
        /// </summary>
        /// <returns></returns>
        public List<string> GetListI_ConOut()
        {
            return inputData.I_ConOut.ToList();
        }
        /// <summary>
        /// список типов патрубков
        /// </summary>
        /// <returns></returns>
        public List<string> GetListI_ConType()
        {
            return inputData.I_ConType.ToList();
        }
        /// <summary>
        /// Список исполения корпуса
        /// </summary>
        /// <returns></returns>
        public List<string> GetListCasingType()
        {
            return inputData.CasingType.ToList();
        }
        /// <summary>
        /// Список материалов корпуса
        /// </summary>
        /// <returns></returns>
        public List<string> GetListI_CasMat()
        {
            return inputData.I_CasMat.ToList();
        }
        /// <summary>
        /// список ориентаций теплообменника
        /// </summary>
        /// <returns></returns>
        public List<string> GetListEsapo()
        {
            return inputData.Esapo.ToList();
        }
        /// <summary>
        /// список ориентаций патрубков
        /// </summary>
        public List<string> GetListI_CDir()
        {
            return inputData.I_CDir.ToList();
        }
        /// <summary>
        /// список направлений потока воздуха
        /// </summary>
        public List<string> GetListAirFlowDirection()
        {
            return inputData.AirFlowDirection.ToList();
        }
        /// <summary>
        /// список подключений теплоносителя
        /// </summary>
        public List<string> GetListConnectingCoolant()
        {
            return inputData.ConnectingCoolant.ToList();
        }


        /// <summary>
        /// Список хладогентов
        /// </summary>
        public List<string> GetListI_RefT()
        {
            return inputData.I_RefT.ToList();
        }
        /// <summary>
        /// Список коэффициентов загрязнения трубы
        /// </summary>
        public List<string> GetListI_FoulingI()
        {
            return inputData.I_FoulingI.ToList();
        }
        /// <summary>
        /// список - температк конденсации, давление конденсации абс
        /// </summary>
        public List<string> GetListCondensingTemperature()
        {
            return inputData.CondensingTemperature.ToList();
        }
        /// <summary>
        /// список переохлаждение или температура жидкости
        /// </summary>
        public List<string> GetListSubCooling()
        {
            return inputData.SubCooling.ToList();
        }
        /// <summary>
        /// список - температура кипения или давление кипения абс.
        /// </summary>
        public List<string> GetListEvaporatingTemperature()
        {
            return inputData.EvaporatingTemperature.ToList();
        }
        /// <summary>
        /// Список перегрев всас. газа или температура всас. газа
        /// </summary>
        public List<string> GetListSuctOvrheat()
        {
            return inputData.SuctOvrheat.ToList();
        }
    }
}
