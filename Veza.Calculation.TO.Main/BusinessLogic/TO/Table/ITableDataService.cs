using System.Windows;
using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.Interfaces
{
    public interface ITableDataService
    {
        void InitTable();
        void InitTableSwitchMode(double koef);
        /// <summary>
        /// восстановление параметров из сохранённого расчёта
        /// </summary>
        /// <param name="saveCodes"></param>
        /// <returns></returns>
        bool Decoder(SaveCodes saveCodes);

        /// <summary>
        /// Нажата кнопка - Добавить строку
        /// </summary>
        void AddRow();

        OutView GetOutView();

        /// <summary>
        /// Нажата кнопка - Сохранить
        /// </summary>       
        void Save(string type, int id);

        /// <summary>
        ///  Возвращяет свойство SelectProjectsOutTable
        /// </summary>
        /// <returns></returns>
        OutView GetSelect();

        /// <summary>
        /// Очистка выходных данных перед повторным расчётом
        /// </summary>
        void ClearOutputData();
        void ClearOutputData(int index);

        /// <summary>
        /// Считывает выходные результаты
        /// </summary>
        void GetProjectOut();

        void SetIdToCalc(int i);
        /// <summary>
        ///  Возвращяет значение поля FinSpase в TableData.ProjectsOutTable[]
        /// </summary>
        /// <param name="index">Индекс в коллекции</param>
        /// <returns>Значение поля FinSpase</returns>
        double GetFinSpace(int index);
        /// <summary>
        /// Обновление данных списка табличного расчёта
        /// </summary>
        void UpdateProjectsOutTable();
        /// <summary>
        /// Возвращяет индекс строки
        /// </summary>
        /// <returns></returns>
        int GetIdToCalc();
        /// <summary>
        /// Получение значения поля O_FrameB
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameB();

        /// <summary>
        /// Получение значения поля O_FrameT
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameT();

        /// <summary>
        /// Получение значения поля O_FrameThk
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameThk();

        /// <summary>
        /// Получение значения поля O_FrameL
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameL();

        /// <summary>
        /// Получение значения поля O_FrameR
        /// </summary>
        /// <returns></returns>
        string GetProdCode_O_FrameR();

        /// <summary>
        ///  Диаметр коллектора вход
        /// </summary>
        /// <returns></returns>
        string GetShortCode_O_ConIN(bool overload = false);

        /// <summary>
        ///  Диаметр коллектора выход
        /// </summary>
        /// <returns></returns>
        string GetShortCode_O_ConOut(bool overload = false);

        /// <summary>
        /// Получить геометрия
        /// </summary>
        /// <returns></returns>
        string Get_I_Geo();

        /// <summary>
        /// Установить геометрию
        /// </summary>
        /// <param name="geometry"></param>
        void Set_I_Geo(string geometry);
        /// <summary>
        /// Получить Размеры и материал трубки
        /// </summary>
        /// <returns></returns>
        string Get_I_PipeThk();

        /// <summary>
        /// Установить Размеры и материал трубки
        /// </summary>
        /// <param name="pipe"></param>
        void Set_I_PipeThk(string pipe);
        
        /// <summary>
        /// Получить толщину оребрения
        /// </summary>
        /// <returns></returns>
        string Get_I_FinThk();

        /// <summary>
        /// Установить толщину оребрения
        /// </summary>
        /// <param name="fin"></param>
        void Set_I_FinThk(string fin);

        /// <summary>
        /// Получить шаг оребрения
        /// </summary>
        /// <returns></returns>
        string Get_I_LamAbsFix();

        /// <summary>
        /// Установить шаг оребрения
        /// </summary>
        /// <param name="fin"></param>
        void Set_I_LamAbsFix(string fin);

        /// <summary>
        /// Получить теплоноситель
        /// </summary>
        /// <returns></returns>
        string Get_I_MedTyp();

        /// <summary>
        /// Установить теплоноситель
        /// </summary>
        /// <param name="fin"></param>
        void Set_I_MedTyp(string fin);
    }
}
