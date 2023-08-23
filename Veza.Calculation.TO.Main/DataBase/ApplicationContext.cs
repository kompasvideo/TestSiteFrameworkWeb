using Microsoft.EntityFrameworkCore;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.DataBase.Models.EquipmentMAKK;
using Veza.HeatExchanger.DataBase.Models.FanAddEdit;

namespace Veza.HeatExchanger.DataBase
{
    sealed public class ApplicationContext : DbContext
    {
        public DbSet<FanPointsDB> FanPointss { get; set; }
        public DbSet<FanMountDB> FanMounts { get; set; }
        public DbSet<FanMaterialsDB> FanMaterialss { get; set; }
        public DbSet<FanDirectionDB> FanDirections { get; set; }
        public DbSet<FanTipologyDB> FanTipologys { get; set; }
        public DbSet<FanBuilderDB> FanBuilders { get; set; }
        public DbSet<FanSeriesDB> FanSeriess { get; set; }
        public DbSet<FanStepEffPowerDB> FanStepEffPowers { get; set; }
        public DbSet<FanModelsDB> FanModelss { get; set; }
        public DbSet<CompressorDB> Compressors { get; set; }
        public DbSet<EquipmentMAKKDB> EquipmentMAKKs { get; set; }
        public DbSet<HeatExchangerDB> HeatExchangerDBs { get; set; }
        public DbSet<MAKKOptionDB> MAKKOptionDBs { get; set; }
        public DbSet<MAKKAccessoriesDB> MAKKAccessoriesDBs { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Calculation.TO.Main.Properties.Resources.ConnectionStringDbFan);
        }
    }
}
