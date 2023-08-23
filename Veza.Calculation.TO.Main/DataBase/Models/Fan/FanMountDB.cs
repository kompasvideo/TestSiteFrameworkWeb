using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Veza.HeatExchanger.DataBase.Models.FanAddEdit
{
    /// <summary>
    /// Геометрия монтажа вентилятора (стлбец Mount в таблице) 
    /// 2 - квадрат
    /// 1 - круг 
    /// </summary>
    sealed public class FanMountDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
