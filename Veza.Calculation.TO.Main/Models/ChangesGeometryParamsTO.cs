using System.Collections.Generic;

namespace Veza.HeatExchanger.Models
{
    public class ChangesGeometryParamsTO
    {
        /// <summary>
        /// Размер и материал трубки 
        /// </summary>
        public List<string> Pipe { get; set; }

        /// <summary>
        /// Выбранный размер и материал трубки
        /// </summary>
        public string SelectedPipe { get; set; }

        /// <summary>
        /// толщина оребрения и материал
        /// </summary>
        public List<string> Fin { get; set; }

        /// <summary>
        /// выбранная толщина оребрения и материал
        /// </summary>
        public string SelectedFin { get; set; }

        /// <summary>
        /// Шаг оребрения
        /// </summary>
        public List<string> StepFin { get; set; }

        /// <summary>
        /// Выбранный шаг оребрения
        /// </summary>
        public string SelectedStepFin { get; set; }
    }
}
