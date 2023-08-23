using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Reflection;
using Isc.Eng.B0003;
using Veza.HeatExchanger.Interfaces;

namespace Veza.HeatExchanger.Models
{
    sealed public class Calculate : ICalculate
    {
        EngineB0003.COILTYP mMode { get; set; }
        //to have the interface module global
        static EngineB0003 MyEngine;
        //Need for licensing
        string SecurityKey { get; set; }
        string SupplierKey { get; set; }
        //Path for DLL's and files
        string DataPath { get; set; }
        string sMode;
        string MyMode
        {
            set
            {
                switch (value)
                {
                    case "HW":
                        mMode = EngineB0003.COILTYP.HeatingWater;  // 1
                        break;
                    case "CW":
                        mMode = (EngineB0003.COILTYP)4;
                        break;
                    case "ST":
                        mMode = (EngineB0003.COILTYP)3;
                        break;
                    case "CX":
                        mMode = (EngineB0003.COILTYP)2;
                        break;
                    case "DX":
                        mMode = (EngineB0003.COILTYP)5;
                        break;
                    case "TW":
                        mMode = (EngineB0003.COILTYP)6;
                        break;
                    default:
                        break;
                }
                sMode = value;
            }
        }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        List<string> txtErr { get; set; }
        bool returnError { get; set; }
        /// <summary>
        /// Ключи
        /// </summary>
        string txtKeys { get; set; }
        /// <summary>
        /// таблица Input
        /// </summary>
        List<DBLine> lines { get; set; }
        /// <summary>
        /// таблица Output
        /// </summary>
        List<DBLineOut> linesOut { get; set; }

        bool init { get; set; }
        private ILogs logs;

        public Calculate(ILogs logs)
        {
        }

        public void InitParam()
        { 
            // вызвать код dll BBox запущенный в linux из под Wine


            this.logs = logs;
            SupplierKey = Calculation.TO.Main.Properties.Resources.VEZ;
            string sX = Calculation.TO.Main.Properties.Resources.DataPath;
            sX = StaticData.Path;
            if (sX == "?")
            {
                //sX = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Calculation.TO.Main.Properties.Resources.BBox);               
                sX = StaticData.Path;
            }

            DataPath = sX;
            SecurityKey = Calculation.TO.Main.Properties.Resources.SecKey;


            //Create the class
            MyEngine = new EngineB0003();
            if (MyEngine == null)
            {
                //MessageBox.Show(Calculation.TO.Main.Properties.Resources.ErrorLoadingClass);
            }
        }

        //Need for licensing
        public string GetSecurityKey()
        {
            return SecurityKey;
        }
        //Path for DLL's and files
        public string GetDataPath()
        {
            return DataPath;
        }

        public ParamsCalculationLibrary GetParams(string mode)
        {
            MyMode = mode;
            return new ParamsCalculationLibrary
            {
                DataPath = DataPath,
                SecurityKey = SecurityKey,
                SupplierKey = SupplierKey,
                MyMode = mode,
                MyEngine = MyEngine
            };
        }

        /// <summary>
        /// Инициализация расчёта
        /// </summary>
        public void Init()
        {
            init = true;
            InitParam();
            txtKeys = "";
            txtErr = new List<string>();
            lines = new List<DBLine>();
            linesOut = new List<DBLineOut>();
            if (mMode == EngineB0003.COILTYP.EnergyRecovery)
            {
                PrepareTW();
            }
            else
            {
                PrepareSC();
            }

            string path = SupplierKey + Calculation.TO.Main.Properties.Resources.Default + sMode + Calculation.TO.Main.Properties.Resources.txt;
            string sDefaultFile = Path.Combine(DataPath, path);
            if (File.Exists(sDefaultFile))
            {
                LoadFromFile(sDefaultFile, "");
            }
        }

        public void End()
        {
            MyEngine.ServiceEnd();
        }

        public List<DBLine> GetListInput()
        {
            return lines;
        }

        public List<DBLineOut> GetListOutput()
        {
            return linesOut;
        }

        public bool GetReturn()
        {
            //_//logs.Logger.Info("Результат расчёта - " + returnError);
            return returnError;
        }

        public string GetTxtErr()
        {
            string str = "";
            foreach (string line in txtErr)
            {
                str += line + "\r";
            }
            //logs.Logger.Info($"{Calculation.TO.Main.Properties.Resources.TxtErr} - {str}");
            return str;
        }

        public string GetTxtKeys()
        {
            //logs.Logger.Info($"{Calculation.TO.Main.Properties.Resources.TxtKeys} - {txtKeys}");
            return txtKeys;
        }

        public void SaveToFile(string sFileName)
        {
            try
            {
                if (File.Exists(sFileName))
                {
                    File.Delete(sFileName);
                }
                StringBuilder sData = new StringBuilder("", 1_500);
                foreach (DBLine line in lines)
                {
                    sData.AppendLine($"{line.Field}={line.Content}");
                }
                File.WriteAllText(sFileName, sData.ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public void SaveToExcel(string fileName, bool bOrderinColumns)
        {
            if (MyEngine.SaveToExcel(fileName, bOrderinColumns) != 0)
            {
                //MessageBox.Show(Calculation.TO.Main.Properties.Resources.ErrorWritingFile);
            }
            else
            {
                //MessageBox.Show(Calculation.TO.Main.Properties.Resources.Done);
            }
        }

        public void LoadFromFile(string sFileName, string sData)
        {
            //    gridOut.DataSource = Nothing
            try
            {
                if (sFileName != "")
                {
                    if (File.Exists(sFileName))
                    {
                        sData = File.ReadAllText(sFileName);
                    }
                }
                string[] asX;
                if (sData.Contains(";") != false)
                    asX = sData.Split(';');
                else
                    asX = sData.Split('\n');

                foreach (string sX in asX)
                {
                    if (sX.Trim() != "")
                    {
                        string[] asY = sX.Split('=');
                        asY[1] = asY[1].Replace("\r", "");
                        try
                        {
                            if (lines.Count > 0)
                            {
                                DBLine sLine = new DBLine();
                                //foreach (DBLine sLine in lines)
                                for (int i = 0; i < lines.Count; i++)
                                {
                                    if (lines[i].Field.Contains(asY[0]))
                                    {
                                        if (asY[1].ToLower() != Calculation.TO.Main.Properties.Resources.null1)
                                            lines[i].Content = asY[1];
                                        else
                                            lines[i].Content = "";
                                        sLine.Field = lines[i].Field;
                                        sLine.Content = lines[i].Content;
                                        sLine.Note = lines[i].Note;
                                        lines.RemoveAt(i);
                                        lines.Insert(i, sLine);
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(Calculation.TO.Main.Properties.Resources.FieldMissing + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public void LoadExcelToInput(string sFileName)
        {
            if (File.Exists(sFileName))
            {
                string sData = "";
                if (MyEngine.GetInputFromExcel(sFileName, ref sData) != 0)
                {
                    //MessageBox.Show(Calculation.TO.Main.Properties.Resources.ErrorOnReadingFile);
                }
                LoadFromFile("", sData);
            }
        }

        public void Calc()
        {
            bool b = init;
            //txtErr.Add("Calculate...");
            txtErr.Clear();
            txtKeys = "";
            StringBuilder sData = new StringBuilder();
            if (mMode != EngineB0003.COILTYP.EnergyRecovery)
            {
                foreach (DBLine line in lines)
                {
                    if (!string.IsNullOrEmpty(line.Content))
                    {
                        sData.Append($"{line.Field}={line.Content};");
                    }
                }

                string sInpData = sData.ToString();
                //sInpData = "I_Airflow=21700;I_AirHumIn=85;I_AirTempIn=-2;I_ARes=0;I_BaseBar=1013.25;I_BaseDens=1.2;I_BaseHum=15;I_BaseMode=1;I_BaseSea=0;I_BaseTemp=20;I_Capacity=230;I_CasIso=0;I_CasMat=1;I_CatCoat=0;I_CBox=0;I_CDir=0;I_CertMode=0;I_ConIn=0;I_ConOut=0;I_ConType=0;I_CSheet=0;I_CSheetL=0;I_ElimMat=0;I_ElimTp=0;I_Esapo=V1;I_EtaMax=0;I_FanPlen=0;I_FinThk=0.12;I_FoulingE=0;I_FoulingI=0;I_FrameTp=0;I_FullH=1;I_Geometry=2;I_HdrQta=0;I_HeightInt=1200;I_KorrADP=0;I_KorrFA=0;I_KorrFI=0;I_KorrKDP=0;I_LamAbsFix=2.5;I_LamAbsMax=12;I_LRes=0;I_MatFins=2;I_MatFrame=7;I_MatHdr=6;I_MatRows=1;I_MedKPa=50;I_MedTempIn=90;I_MedTempOut=70;I_MedTyp=0;I_Mode=0;I_PanMat=0;I_PanTp=0;I_PipeThk=0.32;I_RCon=0;I_SoleGew=0;I_SoleLeit=0;I_SoleTFrz=0;I_SoleVisk=0;I_SoleWaer=0;I_SpcSolder=0;I_SplitC=0;I_Type=1;I_Uneven=0;I_VentD=0;I_WidthInt=1400;";
                //sInpData = "I_Airflow=21700;I_AirHumIn=85;I_AirTempIn=-2;I_ARes=0;I_BaseBar=1013.25;I_BaseDens=1.2;I_BaseHum=15;I_BaseMode=1;I_BaseSea=0;I_BaseTemp=20;I_Capacity=230;I_CasIso=0;I_CasMat=1;I_CatCoat=0;I_CBox=0;I_CDir=0;I_CertMode=0;I_ConIn=0;I_ConOut=0;I_ConType=0;I_CSheet=0;I_CSheetL=0;I_ElimMat=0;I_ElimTp=0;I_Esapo=V1;I_EtaMax=0;I_FanPlen=0;I_FinThk=0.12;I_FoulingE=0;I_FoulingI=0;I_FrameTp=0;I_FullH=1;I_Geometry=2;I_HdrQta=0;I_HeightInt=1200;I_KorrADP=0;I_KorrFA=0;I_KorrFI=0;I_KorrKDP=0;I_LamAbsFix=2.5;I_LamAbsMax=12;I_LRes=0;I_MatFins=2;I_MatFrame=7;I_MatHdr=6;I_MatRows=1;I_MedKPa=50;I_MedTempIn=90;I_MedTempOut=70;I_MedTyp=0;I_Mode=0;I_PanMat=0;I_PanTp=0;I_PipeThk=0.32;I_RCon=0;I_SoleGew=0;I_SoleLeit=0;I_SoleTFrz=0;I_SoleVisk=0;I_SoleWaer=0;I_SpcSolder=0;I_SplitC=0;I_Type=1;I_Uneven=0;I_VentD=0;I_WidthInt=1400;";    
                //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.InputData + sInpData);
                //Startup the calculation
                //Dim iRet As Integer = MyEngine.CalculateSC(sInpData)
                int iRet = MyEngine.CalculateSC(sInpData);
                //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.CalculationResultCode + iRet);

                //    'Return = 0 all ok, 2=coils with errors, 3=coils with warnings, other values: see error list
                if (iRet == 1 || iRet > 3)
                {
                    // 9999 = very heavy system error - it should not be happen
                    if (iRet == 9999 && MyEngine.SystemMessage.Length > 0)
                    {
                        txtErr.Add($"{iRet}: {MyEngine.SystemMessage}");
                    }
                    else
                    {
                        //  other errors
                        txtErr.Add($"{iRet}: {MyEngine.GetMessageText(iRet)}");
                    }
                    returnError = true;
                    return;
                }
            }
            else
            {
                //    for energy recovery
                StringBuilder sDataMain = new StringBuilder();
                StringBuilder sDataHeater = new StringBuilder();
                StringBuilder sDataCooler = new StringBuilder();
                string sFld;
                foreach (DBLine line in lines)
                {
                    if (!string.IsNullOrEmpty(line.Content))
                    {
                        sFld = line.Field.Substring(0, 2);
                        switch (sFld)
                        {
                            case "M:":
                                sDataMain.AppendLine($"{sFld}={line.Content}");
                                break;
                            case "H:":
                                sDataHeater.AppendLine($"{sFld}={line.Content}");
                                break;
                            case "C:":
                                sDataCooler.AppendLine($"{sFld}={line.Content}");
                                break;

                        }
                    }
                }

                int iRet = MyEngine.CalculateTW(sDataMain.ToString(), sDataHeater.ToString(), sDataCooler.ToString());

                if (iRet != 2 && iRet != 3 && iRet != 0)
                {
                    if (iRet == 9999 && MyEngine.SystemMessage.Length > 0)
                    {
                        txtErr.Add($"{iRet}: {MyEngine.SystemMessage}");
                    }
                    else
                    {
                        txtErr.Add($"{iRet}: {MyEngine.GetMessageText(iRet)}");
                    }
                    returnError = true;
                    //logs.Logger.Info(Calculation.TO.Main.Properties.Resources.ErrorTextFromDll + txtErr);
                    return;
                }
            }
            //txtErr.Add("Load Results");
            ShowResult();
        }

        public void ExcelLoopCalc(string fileName)
        {
            if (File.Exists(fileName))
            {
                if (MyEngine.ExcelLoopCalc(fileName) != 0)
                {
                    //MessageBox.Show(Calculation.TO.Main.Properties.Resources.ErrorOnCreatingFile + MyEngine.SystemMessage);
                }
                else
                {
                    //MessageBox.Show(Calculation.TO.Main.Properties.Resources.Done);
                }
            }
        }

        public void SaveToFileResult(string sFileName)
        {
            StringBuilder oBld = new StringBuilder();

            try
            {
                if (File.Exists(sFileName))
                {
                    File.Delete(sFileName);
                }

                oBld.AppendLine(Calculation.TO.Main.Properties.Resources.INPUT);
                foreach (DBLine line in lines)
                {
                    oBld.AppendLine($"{line.Field}={line.Content}");
                }

                oBld.AppendLine(Calculation.TO.Main.Properties.Resources.OUTPUT);
                oBld.Append(Calculation.TO.Main.Properties.Resources.Field1);
                oBld.Append(Calculation.TO.Main.Properties.Resources.Field2);
                oBld.Append(Calculation.TO.Main.Properties.Resources.Field3);
                oBld.AppendLine(Calculation.TO.Main.Properties.Resources.Field4);

                foreach (DBLineOut lOut in linesOut)
                {
                    oBld.Append($"{lOut.Field}|{lOut.Content1}|{lOut.Content2}|{lOut.Content3}|{lOut.Content4}|{lOut.Content5}|");
                    oBld.Append($"{lOut.Content6}|{lOut.Content7}|{lOut.Content8}|{lOut.Content9}|{lOut.Content10}|");
                    oBld.Append($"{lOut.Content11}|{lOut.Content12}|{lOut.Content13}|{lOut.Content14}|{lOut.Content15}|");
                    oBld.Append($"{lOut.Content16}|{lOut.Content17}|{lOut.Content18}|{lOut.Content19}|{lOut.Content20}|");
                    oBld.Append($"{lOut.Content21}|{lOut.Content22}|{lOut.Content23}|{lOut.Content24}|{lOut.Content25}|");
                    oBld.Append($"{lOut.Content26}|{lOut.Content27}|{lOut.Content28}|{lOut.Content29}|{lOut.Content30}|");
                    oBld.AppendLine($"{lOut.Content31}|{lOut.Content32}|{lOut.Note}");
                }
                File.WriteAllText(sFileName, oBld.ToString());

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public List<string> GetFieldsSC()
        {
            MyMode = "HW";
            List<string> Fields = new List<string>();
            //MyEngine = CreateClass(SupplierKey, DataPath);
            //Startup the interface
            int iRet = MyEngine.ServiceStart(DataPath, SecurityKey, Calculation.TO.Main.Properties.Resources.VEZ, EngineB0003.COILTYP.HeatingWater); // 1

            //if (iRet != 0)
            //    //'in case of error
            //    MessageBox.Show($"{iRet}: {MyEngine.GetMessageText(iRet)}");
            //else
            {
                // retrieve the input/output fields  for a cooler
                string sX = "";
                iRet = MyEngine.GetFieldsSC(ref sX);
                //if (iRet != 0)
                //    //        'in case of error
                //    MessageBox.Show($"{iRet}: {MyEngine.GetMessageText(iRet)}");
                //else
                {
                    // show the fields filed by field
                    string[] asX = sX.Split(';');
                    for (int i = 0; i < asX.Length; i++)
                    {
                        //Debug.Print(asX(i))
                        Fields.Add(asX[i]);
                    }
                    //        MsgBox("Fields")
                    //MessageBox.Show(sX); //show all fields
                }
            }

            //close the interface
            MyEngine.ServiceEnd();
            return Fields;
        }

        public List<string> GetFieldsSCDetail()
        {
            MyMode = "HW";
            List<string> Fields = new List<string>();
            //Startup the interface
            int iRet = MyEngine.ServiceStart(DataPath, SecurityKey, Calculation.TO.Main.Properties.Resources.VEZ, EngineB0003.COILTYP.HeatingWater); // 1

            //if (iRet != 0)
            //    //'in case of error
            //    MessageBox.Show($"{iRet}: {MyEngine.GetMessageText(iRet)}");
            //else
            {
                // retrieve the input/output fields  for a cooler
                string sX = "";
                iRet = MyEngine.GetFieldsSCDetail(ref sX);
                //if (iRet != 0)
                //    //        'in case of error
                //    MessageBox.Show($"{iRet}: {MyEngine.GetMessageText(iRet)}");
                //else
                {
                    // show the fields filed by field
                    string[] asX = sX.Split('\n');
                    for (int i = 0; i < asX.Length; i++)
                    {
                        asX[i] = asX[i].Replace("\r", "");
                        //Debug.Print(asX(i))
                        Fields.Add(asX[i]);
                    }
                    //        MsgBox("Fields")
                    //MessageBox.Show(sX); //show all fields                    
                }
            }

            //close the interface
            MyEngine.ServiceEnd();
            return Fields;
        }


        //Single coils input field
        void PrepareSC()
        {
            //Create the interface and startup it - it must be do before you use any other function
            //MyEngine = CreateClass(SupplierKey, DataPath);

            //'Start it up
            int iRet =0;
            try
            {
                iRet = MyEngine.ServiceStart(DataPath, SecurityKey, Calculation.TO.Main.Properties.Resources.VEZ, mMode);
            }
            catch (Exception e) 
            {
                ;
            }

            //In case of error
            if (iRet != 0)
            {
                txtErr.Add(string.Format($"{iRet}: {MyEngine.GetMessageText(iRet)}"));
                return;
            }

            //Read the Possible fields for this coil type
            string sX = "";
            iRet = MyEngine.GetFieldsSCDetail(ref sX);
            if (iRet != 0)
            {
                txtErr.Add(string.Format($"{iRet}: {MyEngine.GetMessageText(iRet)}"));
                return;
            }

            //Use only input fields and add each field as a row on the grid table
            string[] asL = sX.Split('\n');
            foreach (string sXL in asL)
            {
                string[] asX = sXL.Replace("\r", "").Split('|');
                DBLine sLine = new DBLine();
                if (asX[0].StartsWith(Calculation.TO.Main.Properties.Resources.i, StringComparison.InvariantCultureIgnoreCase)) // "i_"
                {
                    //Helper GetNote to have the description of each field
                    sLine.Field = asX[0];
                    sLine.Content = "";// asX[1];
                    switch (asX[2])
                    {
                        case "#":
                            sLine.Note = $"[{asX[3]}] {asX[1]}";
                            break;
                        case "%":
                            sLine.Note = $"{asX[1]}";
                            break;
                        case "$":
                            sLine.Note = $"{asX[1]}";
                            break;
                    }
                    lines.Add(sLine);
                }
            }

        }

        // Twin Coil - energy recovery 
        void PrepareTW()
        {
            //Input Table

            //MyEngine = CreateClass(SupplierKey, DataPath);

            int iRet = MyEngine.ServiceStart(DataPath, SecurityKey, Calculation.TO.Main.Properties.Resources.VEZ, (EngineB0003.COILTYP)6);

            if (iRet != 0)
            {
                txtErr.Add(string.Format($"{iRet}: {MyEngine.GetMessageText(iRet)}"));
                return;
            }

            string sXM = "";
            string sXC = "";
            string sXH = "";
            iRet = MyEngine.GetFieldsTWDetail(ref sXM, ref sXH, ref sXC);
            if (iRet != 0)
            {
                txtErr.Add(string.Format($"{iRet}: {MyEngine.GetMessageText(iRet)}"));
                return;
            }

            string[] asL = sXM.Split('\n');
            foreach (string sXL in asL)
            {
                string[] asX = sXL.Replace("\r", "").Split('|');
                DBLine sLine = new DBLine();
                if (asX[0].StartsWith("i_", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Helper GetNote to have the description of each field
                    sLine.Field = asX[0];
                    sLine.Content = asX[1];
                    sLine.Note = asX[2];
                    lines.Add(sLine);
                }
            }
        }


        //show results after calculation
        void ShowResult()
        {
            linesOut.Clear();
            //Get number of results
            int iResults = MyEngine.NumberOfResults();
            if (iResults == 0)
            {
                returnError = true;
                return;
            }


            if (mMode != (EngineB0003.COILTYP)6) // EnergyRecovery
            {
                //    for single coils
                //    Read the Possible fields for this coil type
                string sX = "";
                int iRet = MyEngine.GetFieldsSCDetail(ref sX);
                if (iRet != 0)
                {
                    txtErr.Add($"{iRet}: {MyEngine.GetMessageText(iRet)}");
                    return;
                }

                // Use only thw input fields and add each field as a row on the grid table
                string[] asL = sX.Split('\n');
                foreach (string sX1 in asL)
                {
                    string[] asX = sX1.Replace("\r", "").Split('|');
                    if (asX[0].StartsWith(Calculation.TO.Main.Properties.Resources.o, StringComparison.InvariantCultureIgnoreCase)) // "o_"
                    {
                        DBLineOut dBLineOut = new DBLineOut();
                        dBLineOut.Field = asX[0];
                        dBLineOut.Note = GetNote(asX[1], asX[3]);
                        linesOut.Add(dBLineOut);
                    }
                }

                //    go in loop for each result set
                for (int i = 1; i <= iResults; i++)
                {
                    string sData = "";

                    //  Retrieve the resultset for requested result index
                    iRet = MyEngine.GetResultSC(i, ref sData);

                    //  in case of error
                    if (iRet != 0)
                    {
                        txtErr.Add($"{iRet}: {MyEngine.GetMessageText(iRet)}");
                        return;
                    }

                    string[] asX = sData.Split(';');
                    for (int j = 0; j < asX.Length; j++)
                    {
                        string[] asY = asX[j].Split('=');
                        // Now add all result / output fields with name and description to my grid table as rows
                        SetData(asY[0], asY[1], i);
                        if (asY[0] == Calculation.TO.Main.Properties.Resources.OCode) // "O_Code"
                        {
                            if (string.IsNullOrEmpty(txtKeys))
                            {
                                txtKeys = $"({i}) {asY[1]}";
                            }
                            else
                            {
                                txtKeys = txtKeys + $"\r\n({i}) {asY[1]}";
                            }
                        }
                    }
                }
            }
            else
            {
                // Energy recovery mode
                txtKeys = "";

                string sDataMain = "";
                string sDataHeater = "";
                string sDataCooler = "";

                string sXM = "";
                string sXC = "";
                string sXH = "";

                int iRet = MyEngine.GetFieldsTWDetail(ref sXM, ref sXH, ref sXC);
                if (iRet != 0)
                {
                    txtErr.Add($"{iRet}: {MyEngine.GetMessageText(iRet)}");
                    return;
                }

                //    Add the main data to the table as rows
                string[] asL = sXM.Split('\n');
                foreach (string sX in asL)
                {
                    string[] asX = sX.Replace("\r", "").Split('|');
                    if (asX[0].StartsWith(Calculation.TO.Main.Properties.Resources.o, StringComparison.InvariantCultureIgnoreCase)) // "o_"
                    {
                        DBLineOut dBLineOut = new DBLineOut();
                        dBLineOut.Field = "M:" + asX[0];
                        dBLineOut.Note = GetNote(asX[1], asX[3], "Main");
                        linesOut.Add(dBLineOut);
                    }
                }

                //  add the heater fields to the table as rows
                asL = sXH.Split('\n');
                foreach (string sX in asL)
                {
                    string[] asX = sX.Replace("\r", "").Split('|');
                    if (asX[0].StartsWith(Calculation.TO.Main.Properties.Resources.o, StringComparison.InvariantCultureIgnoreCase))
                    {
                        //            Dim oRow As DataRow = MyDataTable.Rows.Add("H:" & asX(0), "", GetNote(asX(1), asX(3), "Heater"))
                        DBLineOut dBLineOut = new DBLineOut();
                        dBLineOut.Field = "H:" + asX[0];
                        dBLineOut.Note = GetNote(asX[1], asX[3], "Heater");
                        linesOut.Add(dBLineOut);
                    }
                }

                asL = sXC.Split('\n');
                foreach (string sX in asL)
                {
                    string[] asX = sX.Replace("\r", "").Split('|');
                    if (asX[0].StartsWith(Calculation.TO.Main.Properties.Resources.o, StringComparison.InvariantCultureIgnoreCase))
                    {
                        DBLineOut dBLineOut = new DBLineOut();
                        dBLineOut.Field = "C:" + asX[0];
                        dBLineOut.Note = GetNote(asX[1], asX[3], "Cooler");
                        linesOut.Add(dBLineOut);
                    }
                }

                for (int i = 1; i <= iResults; i++)
                {
                    iRet = MyEngine.GetResultTW(i, ref sDataMain, ref sDataHeater, ref sDataCooler);
                    if (iRet != 0)
                    {
                        txtErr.Add($"{iRet}: {MyEngine.GetMessageText(iRet)}");
                        return;
                    }

                    for (int k = 1; i <= 3; i++)
                    {

                        string sX;
                        string sZ;
                        if (k == 1)
                        {
                            sX = sDataMain;
                            sZ = "M:";
                        }
                        else if (k == 2)
                        {
                            sX = sDataHeater;
                            sZ = "H:";
                        }
                        else
                        {
                            sX = sDataCooler;
                            sZ = "C:";
                        }

                        string[] asX = sX.Split(';');

                        for (int j = 0; j < asX.Length; j++)
                        {
                            string[] asY = asX[j].Split('=');
                            SetData(sZ + asY[0], asY[1], i);
                            if (asY[0] == Calculation.TO.Main.Properties.Resources.OCode)
                            {
                                if (txtKeys.Length == 0)
                                    txtKeys = $"({i}) {asY[1]}";
                                else
                                    txtKeys = txtKeys + $"\r\n({i}) {asY[1]}";
                            }
                        }
                    }
                }
            }
            returnError = false;
        }

        //helper to have the description for each field name
        string GetNote(string sLabel, string sUnit, string sPrefix = "")
        {
            string sRet = "";
            if (sUnit.Length == 0)
            {
                sRet = sLabel;
            }
            else
            {
                sRet = "[" + sUnit + "] " + sLabel;
            }
            if (sPrefix.Length > 0)
                sRet = sPrefix + ", " + sRet;
            return sRet;
        }

        //Update Row with Data
        void SetData(string sField, string oData, int iIndex = 1)
        {
            DBLineOut lineMod = null;
            foreach (DBLineOut line in linesOut)
            {
                if (line.Field == sField)
                {
                    lineMod = line;
                    break;
                }
            }
            if (lineMod == null)
            {
                return;
            }
            else
            {
                switch (iIndex)
                {
                    case 1:
                        lineMod.Content1 = oData;
                        break;
                    case 2:
                        lineMod.Content2 = oData;
                        break;
                    case 3:
                        lineMod.Content3 = oData;
                        break;
                    case 4:
                        lineMod.Content4 = oData;
                        break;
                    case 5:
                        lineMod.Content5 = oData;
                        break;
                    case 6:
                        lineMod.Content6 = oData;
                        break;
                    case 7:
                        lineMod.Content7 = oData;
                        break;
                    case 8:
                        lineMod.Content8 = oData;
                        break;
                    case 9:
                        lineMod.Content9 = oData;
                        break;
                    case 10:
                        lineMod.Content10 = oData;
                        break;
                    case 11:
                        lineMod.Content11 = oData;
                        break;
                    case 12:
                        lineMod.Content12 = oData;
                        break;
                    case 13:
                        lineMod.Content13 = oData;
                        break;
                    case 14:
                        lineMod.Content14 = oData;
                        break;
                    case 15:
                        lineMod.Content15 = oData;
                        break;
                    case 16:
                        lineMod.Content16 = oData;
                        break;
                    case 17:
                        lineMod.Content17 = oData;
                        break;
                    case 18:
                        lineMod.Content18 = oData;
                        break;
                    case 19:
                        lineMod.Content19 = oData;
                        break;
                    case 20:
                        lineMod.Content20 = oData;
                        break;
                    case 21:
                        lineMod.Content21 = oData;
                        break;
                    case 22:
                        lineMod.Content22 = oData;
                        break;
                    case 23:
                        lineMod.Content23 = oData;
                        break;
                    case 24:
                        lineMod.Content24 = oData;
                        break;
                    case 25:
                        lineMod.Content25 = oData;
                        break;
                    case 26:
                        lineMod.Content26 = oData;
                        break;
                    case 27:
                        lineMod.Content27 = oData;
                        break;
                    case 28:
                        lineMod.Content28 = oData;
                        break;
                    case 29:
                        lineMod.Content29 = oData;
                        break;
                    case 30:
                        lineMod.Content30 = oData;
                        break;
                    case 31:
                        lineMod.Content31 = oData;
                        break;
                    case 32:
                        lineMod.Content32 = oData;
                        break;
                    case 33:
                        lineMod.Content33 = oData;
                        break;
                    case 34:
                        lineMod.Content34 = oData;
                        break;
                    case 35:
                        lineMod.Content35 = oData;
                        break;
                    case 36:
                        lineMod.Content36 = oData;
                        break;
                    case 37:
                        lineMod.Content37 = oData;
                        break;
                    case 38:
                        lineMod.Content38 = oData;
                        break;
                    case 39:
                        lineMod.Content39 = oData;
                        break;
                    case 40:
                        lineMod.Content40 = oData;
                        break;
                }
            }
        }

    }
}
