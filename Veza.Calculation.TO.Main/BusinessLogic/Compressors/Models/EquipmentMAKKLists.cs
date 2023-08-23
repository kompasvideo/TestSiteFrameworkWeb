using System.Collections.ObjectModel;
using Veza.Calculation.TO.Main.Interfaces;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Models.MAKK;
using Veza.HeatExchanger.ViewModels.Controls;

namespace Veza.HeatExchanger.BusinessLogic.Compressors.Models
{
    sealed public class EquipmentMAKKLists
    {
        /// <summary>
        /// Страница возврата
        /// </summary>
        public string ReturnPage { get; set; }

        /// <summary>
        /// Список вентиляторов
        /// </summary>
        public ObservableCollection<FanDTO> Fans { get; set; }

        /// <summary>
        /// Теплообменник
        /// </summary>
        public IInputData InputDataTO { get; set; }

        /// <summary>
        /// Список компрессоров
        /// </summary>
        public ObservableCollection<SelectCompressors> Compressors { get; set; }

        /// <summary>
        /// дополнительные параметры теплообменника
        /// </summary>
        public AdvanceParamsViewModel AdvanceParamsViewModelP { get; set; }

        /// <summary>
        /// Входные параметры компрессора
        /// </summary>
        public InputDataCompressors InputDataCompressorsP { get; set; }
    }
}
