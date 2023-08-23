using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Veza.HeatExchanger.DataBase.Models.EquipmentMAKK
{
    public sealed class MAKKAccessoriesDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NameRu { get; set; }
        public string NameEn { get; set; }
    }
}
