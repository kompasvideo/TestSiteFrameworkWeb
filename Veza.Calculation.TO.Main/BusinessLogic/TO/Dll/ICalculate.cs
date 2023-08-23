using System.Collections.Generic;
using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.Interfaces
{
    public interface ICalculate
    {
        //Need for licensing
        //string GetSecurityKey();
        //Path for DLL's and files
        //string GetDataPath();
        ParamsCalculationLibrary GetParams(string mode);
        void Init();

        List<DBLine> GetListInput();
        List<DBLineOut> GetListOutput();
        bool GetReturn();
        string GetTxtErr();
        //string GetTxtKeys();
        //void SaveToFile(string sFileName);
        //void SaveToExcel(string fileName, bool bOrderinColumns);
        //void LoadFromFile(string sFileName, string sData);
        //void LoadExcelToInput(string sFileName);
        void Calc();
        void End();
        //void ExcelLoopCalc(string fileName);
        //void SaveToFileResult(string sFileName);
        //List<string> GetFieldsSC();
        //List<string> GetFieldsSCDetail();
    }
}
