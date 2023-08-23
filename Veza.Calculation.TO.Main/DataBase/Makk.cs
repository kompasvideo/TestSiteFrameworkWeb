using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Veza.HeatExchanger.DataBase.Interafces;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.DataBase.Models.DTO;
using Veza.HeatExchanger.DataBase.Models.EquipmentMAKK;
using Veza.HeatExchanger.Interfaces;

namespace Veza.HeatExchanger.DataBase
{
    public class Makk : IMakk
    {
        #region Внутренние поля 
        private ILogs logs;
        private bool init = false;  // true - пересоздаёт базу
        private IFan fanDb;
        #endregion

        #region Конструкторы
        public Makk(ILogs logs, IFan fanDb)
        {
            this.logs = logs;
            this.fanDb = fanDb;
            if (init)
            {
                SeedDB();
                init = false;
            }
        }
        #endregion

        #region Публичные методы       

        /// <summary>
        /// Обновление в БД модели оборудования МАКК
        /// </summary>
        /// <param name="equipmentMAKKDTO"></param>
        public bool UpdateEquipmentMAKK(EquipmentMAKKDTO equipmentMAKKDTO)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.EquipmentMAKKs.Load();
                foreach (var makk in db.EquipmentMAKKs.ToList())
                {
                    if (makk.Id == equipmentMAKKDTO.Id)
                    {
                        makk.Name = equipmentMAKKDTO.Model;
                        makk.Seria = equipmentMAKKDTO.Seria;
                        makk.HeatExchangerCount = equipmentMAKKDTO.HeatExchangerCount;
                        makk.FanModelsCount = equipmentMAKKDTO.FanCount;
                        makk.CompressorCount = equipmentMAKKDTO.CompressorCount;
                        makk.NoOfCircuits = equipmentMAKKDTO.NoOfCircuits;
                        makk.HeatExchangerId = GetHeatExchanger(db, equipmentMAKKDTO.HeatExchanger);
                        makk.FanModelsId = fanDb.GetFan(db, equipmentMAKKDTO.Fan);
                        makk.CompressorId = GetCompressor(db, equipmentMAKKDTO.Compressor);

                        makk.TotVolumeRefr = equipmentMAKKDTO.TotalVolumeReceivers;
                        makk.TotalAbsorbedPower = equipmentMAKKDTO.TotalAbsorbedPower;
                        makk.Current = equipmentMAKKDTO.TotalOperatingCurrent;
                        makk.MaxCurrent = equipmentMAKKDTO.MaximumOperatingCurrent;
                        makk.LRA = equipmentMAKKDTO.LRA;
                        makk.LiquidTubeDiam = equipmentMAKKDTO.LiquidTubeDiameter;
                        makk.GasTubeDiam = equipmentMAKKDTO.GasTubeDiameter;
                        makk.Length = equipmentMAKKDTO.Length;
                        makk.Width = equipmentMAKKDTO.Width;
                        makk.Height = equipmentMAKKDTO.Height;
                        makk.ShipWeight = equipmentMAKKDTO.ShippingWeight;
                        makk.OperWeight = equipmentMAKKDTO.OperatingWeight;
                        makk.SoundPressure = equipmentMAKKDTO.SoundPressure;
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Добавление в БД модели оборудования МАКК
        /// </summary>
        /// <param name="equipmentMAKKDTO"></param>
        public void AddEquipmentMAKK(EquipmentMAKKDTO equipmentMAKKDTO)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.MAKKOptionDBs.Load();
                db.MAKKAccessoriesDBs.Load();
                var makk = new EquipmentMAKKDB();
                makk.HeatExchangerCount = equipmentMAKKDTO.HeatExchangerCount;
                makk.FanModelsCount = equipmentMAKKDTO.FanCount;
                makk.CompressorCount = equipmentMAKKDTO.CompressorCount;
                makk.NoOfCircuits = equipmentMAKKDTO.NoOfCircuits;
                makk.Name = equipmentMAKKDTO.Model;
                makk.Seria = equipmentMAKKDTO.Seria;
                makk.HeatExchangerId = AddHeatExchanger(db, equipmentMAKKDTO);
                makk.FanModelsId = fanDb.GetFan(db, equipmentMAKKDTO.Fan);
                makk.CompressorId = AddCompressor(db, equipmentMAKKDTO);
                int count = 0;
                foreach(var item in db.MAKKOptionDBs.ToList())
                {
                    switch (count)
                    {
                        case 0:
                            makk.MAKKOption_1 = item;
                            count++;
                            break;
                        case 1:
                            makk.MAKKOption_2 = item;
                            count++;
                            break;
                        case 2:
                            makk.MAKKOption_3 = item;
                            count++;
                            break;
                        case 3:
                            makk.MAKKOption_4 = item;
                            count++;
                            break;
                        case 4:
                            makk.MAKKOption_5 = item;
                            break;
                    }
                }
                count = 0;
                foreach (var item in db.MAKKAccessoriesDBs.ToList())
                {
                    switch (count)
                    {
                        case 0:
                            makk.MAKKAccessories_1 = item;
                            count++;
                            break;
                        case 1:
                            makk.MAKKAccessories_2 = item;
                            count++;
                            break;
                        case 2:
                            makk.MAKKAccessories_3 = item;
                            count++;
                            break;
                        case 3:
                            makk.MAKKAccessories_4 = item;
                            count++;
                            break;
                        case 4:
                            makk.MAKKAccessories_5 = item;
                            break;
                    }
                }
                makk.TotVolumeRefr = equipmentMAKKDTO.TotalVolumeReceivers;
                makk.TotalAbsorbedPower = equipmentMAKKDTO.TotalAbsorbedPower;
                makk.Current = equipmentMAKKDTO.TotalOperatingCurrent;
                makk.MaxCurrent = equipmentMAKKDTO.MaximumOperatingCurrent;
                makk.LRA = equipmentMAKKDTO.LRA;
                makk.LiquidTubeDiam = equipmentMAKKDTO.LiquidTubeDiameter;
                makk.GasTubeDiam = equipmentMAKKDTO.GasTubeDiameter;
                makk.Length = equipmentMAKKDTO.Length;
                makk.Width = equipmentMAKKDTO.Width;
                makk.Height = equipmentMAKKDTO.Height;
                makk.ShipWeight = equipmentMAKKDTO.ShippingWeight;
                makk.OperWeight = equipmentMAKKDTO.OperatingWeight;
                makk.SoundPressure = equipmentMAKKDTO.SoundPressure;
                db.EquipmentMAKKs.Add(makk);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Получений списка опций модели МАКК
        /// </summary>
        /// <param name="nameMAKK"></param>
        public List<MAKKOptionDB> GetOptions(string nameMAKK)
        {
            List<MAKKOptionDB> options = new List<MAKKOptionDB>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Compressors.Load();
                db.EquipmentMAKKs.Load();
                db.HeatExchangerDBs.Load();
                db.FanModelss.Load();
                db.MAKKOptionDBs.Load();
                db.MAKKAccessoriesDBs.Load();
                EquipmentMAKKDB equipmentMAKKDB = null;
                foreach (var item in db.EquipmentMAKKs.ToList())
                {
                    if (item.Name == nameMAKK)
                    {
                        equipmentMAKKDB = item;
                        break;
                    }
                }
                if (equipmentMAKKDB != null)
                {
                    if (equipmentMAKKDB.MAKKOption_1 != null)
                    {
                        options.Add(equipmentMAKKDB.MAKKOption_1);
                    }
                    if (equipmentMAKKDB.MAKKOption_2 != null)
                    {
                        options.Add(equipmentMAKKDB.MAKKOption_2);
                    }
                    if (equipmentMAKKDB.MAKKOption_3 != null)
                    {
                        options.Add(equipmentMAKKDB.MAKKOption_3);
                    }
                    if (equipmentMAKKDB.MAKKOption_4 != null)
                    {
                        options.Add(equipmentMAKKDB.MAKKOption_4);
                    }
                    if (equipmentMAKKDB.MAKKOption_5 != null)
                    {
                        options.Add(equipmentMAKKDB.MAKKOption_5);
                    }
                }
            }
            return options;
        }

        /// <summary>
        /// Получений списка "Доп. оборудование" модели МАКК
        /// </summary>
        /// <param name="nameMAKK"></param>
        public List<MAKKAccessoriesDB> GetAccessories(string nameMAKK)
        {
            List<MAKKAccessoriesDB> accessories = new List<MAKKAccessoriesDB>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Compressors.Load();
                db.EquipmentMAKKs.Load();
                db.HeatExchangerDBs.Load();
                db.FanModelss.Load();
                db.MAKKOptionDBs.Load();
                db.MAKKAccessoriesDBs.Load();
                EquipmentMAKKDB equipmentMAKKDB = null;
                foreach (var item in db.EquipmentMAKKs.ToList())
                {
                    if (item.Name == nameMAKK)
                    {
                        equipmentMAKKDB = item;
                        break;
                    }
                }
                if (equipmentMAKKDB != null)
                {
                    if (equipmentMAKKDB.MAKKAccessories_1 != null)
                    {
                        accessories.Add(equipmentMAKKDB.MAKKAccessories_1);
                    }
                    if (equipmentMAKKDB.MAKKAccessories_2 != null)
                    {
                        accessories.Add(equipmentMAKKDB.MAKKAccessories_2);
                    }
                    if (equipmentMAKKDB.MAKKAccessories_3 != null)
                    {
                        accessories.Add(equipmentMAKKDB.MAKKAccessories_3);
                    }
                    if (equipmentMAKKDB.MAKKAccessories_4 != null)
                    {
                        accessories.Add(equipmentMAKKDB.MAKKAccessories_4);
                    }
                    if (equipmentMAKKDB.MAKKAccessories_5 != null)
                    {
                        accessories.Add(equipmentMAKKDB.MAKKAccessories_5);
                    }
                }
            }
            return accessories;
        }

        /// <summary>
        /// Создаёт список оборудования МАКК
        /// </summary>
        /// <param name="MAKK"></param>
        public void InitEqupmentMAKK(IList<EquipmentMAKKDTO> MAKK)
        {
            //logs.Logger.Info("FanDb - InitFan");
            using (ApplicationContext db = new ApplicationContext())
            {
                db.HeatExchangerDBs.Load();
                db.FanModelss.Load();
                db.Compressors.Load();
                db.EquipmentMAKKs.Load();
                MAKK.Clear();
                EquipmentMAKKDTO equipmentMAKKDTO;
                foreach (var makk in db.EquipmentMAKKs.ToList())
                {
                    equipmentMAKKDTO = new EquipmentMAKKDTO()
                    {
                        Id = makk.Id,
                        Model = makk.Name,
                        Seria = makk.Seria,
                        HeatExchanger = makk.HeatExchangerId.Name,
                        HeatExchangerCount = makk.HeatExchangerCount,
                        Fan = makk.FanModelsId.Model,
                        FanCount = makk.FanModelsCount,
                        Compressor = makk.CompressorId.Name,
                        CompressorCount = makk.CompressorCount,
                        NoOfCircuits = makk.NoOfCircuits,

                        TotalVolumeReceivers =makk.TotVolumeRefr,
                        TotalAbsorbedPower = makk.TotalAbsorbedPower,
                        TotalOperatingCurrent = makk.Current,
                        MaximumOperatingCurrent = makk.MaxCurrent,
                        LRA = makk.LRA,
                        LiquidTubeDiameter = makk.LiquidTubeDiam,
                        GasTubeDiameter = makk.GasTubeDiam,
                        Length = makk.Length,
                        Width = makk.Width,
                        Height = makk.Height,
                        ShippingWeight = makk.ShipWeight,
                        OperatingWeight = makk.OperWeight,
                        SoundPressure = makk.SoundPressure,
                };
                    MAKK.Add(equipmentMAKKDTO);
                }
            }
        }

        /// <summary>
        /// Получить список МАКК с параметрами
        /// </summary>
        /// <returns></returns>
        public List<EquipmentMAKKDB> LoadMAKK()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanModelss.Load();
                db.Compressors.Load();
                db.HeatExchangerDBs.Load();
                db.EquipmentMAKKs.Load();
                List<EquipmentMAKKDB> equipmentMAKKs = new List<EquipmentMAKKDB>();
                foreach (var equip in db.EquipmentMAKKs.ToList())
                {
                    equipmentMAKKs.Add(equip);
                }
                EquipmentMAKKDTO equipmentMAKKDTO = new EquipmentMAKKDTO
                {
                    Id = 0,
                    Model = equipmentMAKKs[0].Name,
                    Seria = equipmentMAKKs[0].Seria,
                    HeatExchanger = equipmentMAKKs[0].HeatExchangerId.Name,
                    HeatExchangerCount = equipmentMAKKs[0].HeatExchangerCount,
                    Fan = equipmentMAKKs[0].FanModelsId.Model,
                    FanCount = equipmentMAKKs[0].FanModelsCount,
                    Compressor = equipmentMAKKs[0].CompressorId.Name,
                    CompressorCount = equipmentMAKKs[0].CompressorCount,
                    NoOfCircuits = equipmentMAKKs[0].NoOfCircuits,
                    Name = equipmentMAKKs[0].CompressorId.Name,

                    TotalVolumeReceivers = equipmentMAKKs[0].TotVolumeRefr,
                    TotalAbsorbedPower = equipmentMAKKs[0].TotalAbsorbedPower,
                    TotalOperatingCurrent = equipmentMAKKs[0].Current,
                    MaximumOperatingCurrent = equipmentMAKKs[0].MaxCurrent,
                    LRA = equipmentMAKKs[0].LRA,
                    LiquidTubeDiameter = equipmentMAKKs[0].LiquidTubeDiam,
                    GasTubeDiameter = equipmentMAKKs[0].GasTubeDiam,
                    Length = equipmentMAKKs[0].Length,
                    Width = equipmentMAKKs[0].Width,
                    Height = equipmentMAKKs[0].Height,
                    ShippingWeight = equipmentMAKKs[0].ShipWeight,
                    OperatingWeight = equipmentMAKKs[0].OperWeight,
                    SoundPressure = equipmentMAKKs[0].SoundPressure,
                };
                return equipmentMAKKs;
            }
        }

        /// <summary>
        /// получить список Хладогентов
        /// </summary>
        /// <returns></returns>
        public IList<string> GetHeatExchangers()
        {
            IList<string> heatExchangers = new List<string>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.HeatExchangerDBs.Load();
                //heatExchangers = (IList<string>)(from item in db.HeatExchangerDBs.ToList() select item.Name);                   
                foreach (var item in db.HeatExchangerDBs.ToList())
                {
                    heatExchangers.Add(item.Name);
                }
            }
            return heatExchangers;
        }       

        #endregion

        #region Приватные методы
        private void SeedDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                MAKKOptionDB mAKKOptionDB_1 = new MAKKOptionDB
                {
                    NameRu = "МК - Встроенный монтажный комплект",
                    NameEn = "MK - Built-in mounting kit",
                    NameShortRu = "МК",
                    NameShortEn = "MK",
                };
                db.MAKKOptionDBs.Add(mAKKOptionDB_1);
                MAKKOptionDB mAKKOptionDB_2 = new MAKKOptionDB
                {
                    NameRu = "РП - Регулятор производительности",
                    NameEn = "RP - Built-in mounting kit",
                    NameShortRu = "РП",
                    NameShortEn = "RP",
                };
                db.MAKKOptionDBs.Add(mAKKOptionDB_2);
                MAKKOptionDB mAKKOptionDB_3 = new MAKKOptionDB
                {
                    NameRu = "ЗК - Зимний комплект до -40°С",
                    NameEn = "ZK - Low temperature kit up to -40°С",
                    NameShortRu = "ЗК",
                    NameShortEn = "ZK",
                };
                db.MAKKOptionDBs.Add(mAKKOptionDB_3);
                MAKKOptionDB mAKKOptionDB_4 = new MAKKOptionDB
                {
                    NameRu = "АМ - Малошумное исполнение",
                    NameEn = "AM - Acoustic version with low noise",
                    NameShortRu = "АМ",
                    NameShortEn = "AM",
                };
                db.MAKKOptionDBs.Add(mAKKOptionDB_4);

                MAKKAccessoriesDB mAKKAccessoriesDB_1 = new MAKKAccessoriesDB
                {
                    NameRu = "КИВ-41 - Комплект виброизоляторов",
                    NameEn = "KIV-41 - Anti-vibration dampers kit",
                };
                db.MAKKAccessoriesDBs.Add(mAKKAccessoriesDB_1);
                MAKKAccessoriesDB mAKKAccessoriesDB_2 = new MAKKAccessoriesDB
                {
                    NameRu = "МОК-МАКК - Монтажный отдельный комплект для МАКК",
                    NameEn = "MOK-MAKK - Separate mounting kit for MAKK",
                };
                db.MAKKAccessoriesDBs.Add(mAKKAccessoriesDB_2);
                MAKKAccessoriesDB mAKKAccessoriesDB_3 = new MAKKAccessoriesDB
                {
                    NameRu = "РЕС-МАКК - Ресивер для МАКК",
                    NameEn = "RES-MAKK - Reciever kit for MAKK",
                };
                db.MAKKAccessoriesDBs.Add(mAKKAccessoriesDB_3);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// возвращяет объект теплообменника по имени
        /// </summary>
        /// <param name="db"></param>
        /// <param name="heatExchangerName"></param>
        /// <returns></returns>
        private HeatExchangerDB GetHeatExchanger(ApplicationContext db, string heatExchangerName)
        {
            foreach (var eMAKK in db.HeatExchangerDBs.ToList())
            {
                if (eMAKK.Name == heatExchangerName)
                {
                    return eMAKK;
                }
            }
            return null;
        }

        /// <summary>
        /// добавляет объект теплообменника по имени
        /// </summary>
        /// <param name="db"></param>
        /// <param name="heatExchangerName"></param>
        /// <returns></returns>
        private HeatExchangerDB AddHeatExchanger(ApplicationContext db, EquipmentMAKKDTO equipmentMAKKDTO)
        {
            HeatExchangerDB heatExchangerDB = new HeatExchangerDB
            {
                Name = equipmentMAKKDTO.HeatExchanger,
                I_Geometry = equipmentMAKKDTO.I_Geometry,
                I_WidthInt = equipmentMAKKDTO.I_WidthInt,
                I_HeightInt = equipmentMAKKDTO.I_HeightInt,
                I_Rows = equipmentMAKKDTO.I_Rows,
                I_Circuits = equipmentMAKKDTO.I_Circuits,
                I_Airflow = equipmentMAKKDTO.I_Airflow,
                I_AirHumIn = equipmentMAKKDTO.I_AirHumIn,
                I_AirTempIn = equipmentMAKKDTO.I_AirTempIn,
                I_ARes = equipmentMAKKDTO.I_ARes,
                I_BaseBar = equipmentMAKKDTO.I_BaseBar,
                I_BaseDens = equipmentMAKKDTO.I_BaseDens,
                I_BaseHum = equipmentMAKKDTO.I_BaseHum,
                I_BaseMode = equipmentMAKKDTO.I_BaseMode,
                I_BaseSea = equipmentMAKKDTO.I_BaseSea,
                I_BaseTemp = equipmentMAKKDTO.I_BaseTemp,
                I_CasIso = equipmentMAKKDTO.I_CasIso,
                I_CasMat = equipmentMAKKDTO.I_CasMat,
                I_CatCoat = equipmentMAKKDTO.I_CatCoat,
                I_CBox = equipmentMAKKDTO.I_CBox,
                I_CDir = equipmentMAKKDTO.I_CDir,
                I_CertMode = equipmentMAKKDTO.I_CertMode,
                I_ConIn = equipmentMAKKDTO.I_ConIn,
                I_ConOut = equipmentMAKKDTO.I_ConOut,
                I_ConType = equipmentMAKKDTO.I_ConType,
                I_CSheet = equipmentMAKKDTO.I_CSheet,
                I_CSheetL = equipmentMAKKDTO.I_CSheetL,
                I_ElimMat = equipmentMAKKDTO.I_ElimMat,
                I_ElimTp = equipmentMAKKDTO.I_ElimTp,
                I_Esapo = equipmentMAKKDTO.I_Esapo,
                I_FanPlen = equipmentMAKKDTO.I_FanPlen,
                I_FinThk = equipmentMAKKDTO.I_FinThk,
                I_FoulingE = equipmentMAKKDTO.I_FoulingE,
                I_FoulingI = equipmentMAKKDTO.I_FoulingI,
                I_FrameTp = equipmentMAKKDTO.I_FrameTp,
                I_FullH = equipmentMAKKDTO.I_FullH,
                I_HdrQta = equipmentMAKKDTO.I_HdrQta,
                I_IseEta = equipmentMAKKDTO.I_IseEta,
                I_KorrADP = equipmentMAKKDTO.I_KorrADP,
                I_KorrFA = equipmentMAKKDTO.I_KorrFA,
                I_KorrFI = equipmentMAKKDTO.I_KorrFI,
                I_KorrKDP = equipmentMAKKDTO.I_KorrKDP,
                I_LamAbsFix = equipmentMAKKDTO.I_LamAbsFix,
                I_LamAbsMax = equipmentMAKKDTO.I_LamAbsMax,
                I_LRes = equipmentMAKKDTO.I_LRes,
                I_MatFins = equipmentMAKKDTO.I_MatFins,
                I_MatFrame = equipmentMAKKDTO.I_MatFrame,
                I_MatHdr = equipmentMAKKDTO.I_MatHdr,
                I_MatRows = equipmentMAKKDTO.I_MatRows,
                I_Mode = equipmentMAKKDTO.I_Mode,
                I_PanMat = equipmentMAKKDTO.I_PanMat,
                I_PanTp = equipmentMAKKDTO.I_PanTp,
                I_PipeThk = equipmentMAKKDTO.I_PipeThk,
                I_RCon = equipmentMAKKDTO.I_RCon,
                I_RefT = equipmentMAKKDTO.I_RefT,
                I_SpcSolder = equipmentMAKKDTO.I_SpcSolder,
                I_TCond = equipmentMAKKDTO.I_TCond,
                I_TEvap = equipmentMAKKDTO.I_TEvap,
                I_THotGas = equipmentMAKKDTO.I_THotGas,
                I_TMaxP = equipmentMAKKDTO.I_TMaxP,
                I_TOvrH = equipmentMAKKDTO.I_TOvrH,
                I_TSubC = equipmentMAKKDTO.I_TSubC,
                I_TSucGas = equipmentMAKKDTO.I_TSucGas,
                I_Type = equipmentMAKKDTO.I_Type,
                I_Uneven = equipmentMAKKDTO.I_Uneven,
            };
            db.HeatExchangerDBs.Add(heatExchangerDB);
            //db.SaveChanges();
            return heatExchangerDB;
        }

        /// <summary>
        /// возвращяет объект компрессора по имени
        /// </summary>
        /// <param name="db"></param>
        /// <param name="compressorName"></param>
        /// <returns></returns>
        private CompressorDB GetCompressor(ApplicationContext db, string compressorName)
        {
            foreach (var v_compr in db.Compressors.ToList())
            {
                if (v_compr.Name == compressorName)
                {
                    return v_compr;
                }
            }
            return null;
        }

        /// <summary>
        /// добавляет объект компрессора по имени
        /// </summary>
        /// <param name="db"></param>
        /// <param name="compressorName"></param>
        /// <returns></returns>
        private CompressorDB AddCompressor(ApplicationContext db, EquipmentMAKKDTO equipmentMAKKDTO)
        {
            CompressorDB compressor = new CompressorDB
            {
                Name = equipmentMAKKDTO.Compressor,
                I_TEvap = equipmentMAKKDTO.I_TEvapC,
                I_TSucGas = equipmentMAKKDTO.I_TSucGasC,
                I_TOvrH = equipmentMAKKDTO.I_TOvrHC,
                I_TCond = equipmentMAKKDTO.I_TCondC,
                I_TSubC = equipmentMAKKDTO.I_TSubCC,
                LiquidTemp = equipmentMAKKDTO.LiquidTemp,
                Manufacturer = equipmentMAKKDTO.Manufacturer,
                RefrigerationCapacity = equipmentMAKKDTO.RefrigerationCapacity,
                MassFlow = equipmentMAKKDTO.MassFlow,
                Voltage = equipmentMAKKDTO.Voltage,
                PowerInput = equipmentMAKKDTO.PowerInput,
                Current = equipmentMAKKDTO.Current,
                COP = equipmentMAKKDTO.COP,
                HeatRejection = equipmentMAKKDTO.HeatRejection,
            };
            db.Compressors.Add(compressor);
            //db.SaveChanges();
            return compressor;
        }
        #endregion
    }
}
