using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Veza.HeatExchanger.DataBase.Models.FanAddEdit
{
    sealed public class FanStepEffPowerDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ushort Step { get; set; }
        public short StatPressure { get; set; }
        public short TotalPressure { get; set; }
        public float EffFacto1 { get; set; }
        public float EffFacto2 { get; set; }
        public float PowerInput { get; set; }
    }
}
