using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Veza.HeatExchanger.Models;

namespace My.Interfaces
{
    public interface ITableDataService
    {
        void SetSaveVisibility(Visibility visibility);
        void InitTable(double koef);
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
        void GetProjectOutT();

        void SetIdToCalc(int i);
        void SetPrintVisibility(Visibility visibility);
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
    }
}
