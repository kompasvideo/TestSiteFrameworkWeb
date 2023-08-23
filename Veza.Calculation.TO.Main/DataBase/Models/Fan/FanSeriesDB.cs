using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Veza.HeatExchanger.DataBase.Models
{
    sealed public class FanSeriesDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BuilderId { get; set; }
        public FanBuilderDB Builder { get; set; }
    }
}
