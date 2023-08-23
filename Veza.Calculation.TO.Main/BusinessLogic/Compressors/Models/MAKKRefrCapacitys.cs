namespace Veza.HeatExchanger.BusinessLogic.Compressors.Models
{
    public class MAKKRefrCapacitys
    {        
        /// <summary>
        /// Холодопроизводительность конденсатора
        /// </summary>
        public double O_TotCap { get; set; }

        /// <summary>
        /// Холодопроизводительность компрессора
        /// </summary>
        public double RefrigerationCapacity { get; set; }

        /// <summary>
        /// разность кондесатора и компрессора
        /// </summary>
        public double Delta { get; set; }
    }
}
