using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Veza.HeatExchanger.BusinessLogic.Fan.Models;
using Veza.HeatExchanger.BusinessLogic.TO.Models;
using Veza.HeatExchanger.DataBase.Interafces;
using Veza.HeatExchanger.DataBase.Models;
using Veza.HeatExchanger.DataBase.Models.FanAddEdit;
using Veza.HeatExchanger.DataBase.Models.Mappers;
using Veza.HeatExchanger.Interfaces;
using Veza.HeatExchanger.Models;
using Veza.HeatExchanger.Services;

namespace Veza.HeatExchanger.DataBase
{
    sealed public class Fan : IFan
    {
        #region Внутренние поля 
        private ILogs _logs;
        LinkedList<SelectedFans> fans = new LinkedList<SelectedFans>();
        private bool init = false;  // true - пересоздаёт базу
        private ErrorEdit errorEdit;
        #endregion

        #region Конструкторы
        public Fan(ILogs logs)
        {
            _logs = logs;
            if (init)
            {
                SeedDB();
                init = false;
            }
        }
        #endregion

        #region Публичные методы

        /// <summary>
        /// Создаёт список производителей (Builder) и сериий вентиляторов (Series) в окне "Просмотр вентиляторов"
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="selectedBuilder"></param>
        /// <param name="series"></param>
        /// <param name="selectedSeries"></param>
        public void InitFan(IList<string> builder, ref string selectedBuilder, IList<string> series,
            ref string selectedSeries, IList<FanOutDTO> Fans)
        {
            //_logs.Logger.Info("FanDb - InitFan");
            using (ApplicationContext db = new ApplicationContext())
            {
                LoadFanAllDB(db);
                List<string> tipologys = new List<string>();
                foreach (var tipology in db.FanTipologys.ToList())
                {
                    if (GS.IsCultureRU())
                    {
                        tipologys.Add(tipology.NameRus);
                    }
                    else
                    {
                        tipologys.Add(tipology.Name);
                    }
                }
                builder.Clear();
                builder.Add(Calculation.TO.Main.Properties.Resources.FanAll);
                selectedBuilder = Calculation.TO.Main.Properties.Resources.FanAll;
                foreach (var b in db.FanBuilders.ToList())
                {
                    builder.Add(b.Name);
                }
                series.Clear();
                series.Add(Calculation.TO.Main.Properties.Resources.FanAll);
                selectedSeries = Calculation.TO.Main.Properties.Resources.FanAll;
                foreach (var s in db.FanSeriess.ToList())
                {
                    series.Add(s.Name);
                }
                Fans.Clear();
                foreach (var fan in db.FanModelss.ToList())
                {
                    Fans.Add(MapperFanModelsToFanOutDTO.FanModelsToFanOutDTO(fan, builder, series, tipologys));
                }
            }
        }

        /// <summary>
        /// Добавляет новый вентилятор в БД
        /// </summary>
        /// <param name="fanDTO"></param>
        public void AddFan(FanDTO fanDTO)
        {
            //_logs.Logger.Info("FanDb - AddFan");
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanTipologys.Load();
                db.FanPointss.Load();
                db.FanModelss.Load();
                FanTipologyDB fanTipology1 = new FanTipologyDB
                {
                    Name = Calculation.TO.Main.Properties.Resources.TipologyAxialMonophase,
                    Id = 1,
                    NameRus = Calculation.TO.Main.Properties.Resources.TipologyAxialMonophaseRu
                };
                FanTipologyDB fanTipology2 = new FanTipologyDB
                {
                    Name = Calculation.TO.Main.Properties.Resources.TipologyAxialTriphase,
                    Id = 2,
                    NameRus = Calculation.TO.Main.Properties.Resources.TipologyAxialTriphaseRu
                };
                FanTipologyDB fanTipology3 = new FanTipologyDB
                {
                    Name = Calculation.TO.Main.Properties.Resources.TipologyCentriphugal,
                    Id = 3,
                    NameRus = Calculation.TO.Main.Properties.Resources.TipologyCentriphugalRu
                };
                FanTipologyDB fanTipology4 = new FanTipologyDB
                {
                    Name = Calculation.TO.Main.Properties.Resources.TipologyDirectlyCoupled,
                    Id = 4,
                    NameRus = Calculation.TO.Main.Properties.Resources.TipologyDirectlyCoupledRu
                };
                FanTipologyDB fanTipology = null;

                switch (fanDTO.Tipology)
                {
                    case Tipology.AxialMonophase:
                        fanTipology = fanTipology1;
                        if (!db.FanTipologys.Contains(fanTipology1))
                        {
                            db.FanTipologys.Add(fanTipology1);
                        }
                        break;
                    case Tipology.AxialTriphase:
                        fanTipology = fanTipology2;
                        if (!db.FanTipologys.Contains(fanTipology2))
                        {
                            db.FanTipologys.Add(fanTipology2);
                        }
                        break;
                    case Tipology.DirectlyCoupled:
                        fanTipology = fanTipology3;
                        if (!db.FanTipologys.Contains(fanTipology3))
                        {
                            db.FanTipologys.Add(fanTipology3);
                        }
                        break;
                    case Tipology.Centriphugal:
                        fanTipology = fanTipology4;
                        if (!db.FanTipologys.Contains(fanTipology4))
                        {
                            db.FanTipologys.Add(fanTipology4);
                        }
                        break;
                }
                FanBuilderDB fanBuilder = null;
                if (!string.IsNullOrEmpty(fanDTO.SelectedBuilder))
                {
                    // есть такой Builder (Производитель)
                    foreach (var builder in db.FanBuilders.ToList())
                    {
                        if (builder.Name == fanDTO.SelectedBuilder)
                        {
                            fanBuilder = builder;
                            break;
                        }
                    }
                }
                if (fanBuilder == null)
                {
                    // добавить Builder (Производителя)
                    fanBuilder = new FanBuilderDB { Name = fanDTO.TextBuilder };
                    db.FanBuilders.Add(fanBuilder);
                }

                FanSeriesDB fanSeries = null;
                if (!string.IsNullOrEmpty(fanDTO.SelectedSeries))
                {
                    // есть такой Series (Серия)
                    foreach (var series in db.FanSeriess.ToList())
                    {
                        if (series.Name == fanDTO.SelectedSeries)
                        {
                            fanSeries = series;
                            break;
                        }
                    }
                }
                if (fanSeries == null)
                {
                    // добавить Series (Серия)
                    fanSeries = new FanSeriesDB { Name = fanDTO.TextSeries, Builder = fanBuilder };
                    db.FanSeriess.Add(fanSeries);
                }

                if (fanDTO.Points01 != null) db.FanPointss.Add(fanDTO.Points01);
                if (fanDTO.Points02 != null) db.FanPointss.Add(fanDTO.Points02);
                if (fanDTO.Points03 != null) db.FanPointss.Add(fanDTO.Points03);
                if (fanDTO.Points04 != null) db.FanPointss.Add(fanDTO.Points04);
                if (fanDTO.Points05 != null) db.FanPointss.Add(fanDTO.Points05);
                if (fanDTO.Points06 != null) db.FanPointss.Add(fanDTO.Points06);
                if (fanDTO.Points07 != null) db.FanPointss.Add(fanDTO.Points07);
                if (fanDTO.Points08 != null) db.FanPointss.Add(fanDTO.Points08);
                if (fanDTO.Points09 != null) db.FanPointss.Add(fanDTO.Points09);
                if (fanDTO.Points10 != null) db.FanPointss.Add(fanDTO.Points10);
                if (fanDTO.Points11 != null) db.FanPointss.Add(fanDTO.Points11);
                if (fanDTO.Points12 != null) db.FanPointss.Add(fanDTO.Points12);
                if (fanDTO.Points13 != null) db.FanPointss.Add(fanDTO.Points13);
                if (fanDTO.Points14 != null) db.FanPointss.Add(fanDTO.Points14);
                if (fanDTO.Points15 != null) db.FanPointss.Add(fanDTO.Points15);
                if (fanDTO.Points16 != null) db.FanPointss.Add(fanDTO.Points16);
                if (fanDTO.Points17 != null) db.FanPointss.Add(fanDTO.Points17);
                if (fanDTO.Points18 != null) db.FanPointss.Add(fanDTO.Points18);
                if (fanDTO.Points19 != null) db.FanPointss.Add(fanDTO.Points19);
                if (fanDTO.Points20 != null) db.FanPointss.Add(fanDTO.Points20);

                if (fanDTO.FanStep01 != null) db.FanStepEffPowers.Add(fanDTO.FanStep01);
                if (fanDTO.FanStep02 != null) db.FanStepEffPowers.Add(fanDTO.FanStep02);
                if (fanDTO.FanStep03 != null) db.FanStepEffPowers.Add(fanDTO.FanStep03);
                if (fanDTO.FanStep04 != null) db.FanStepEffPowers.Add(fanDTO.FanStep04);
                if (fanDTO.FanStep05 != null) db.FanStepEffPowers.Add(fanDTO.FanStep05);
                if (fanDTO.FanStep06 != null) db.FanStepEffPowers.Add(fanDTO.FanStep06);
                if (fanDTO.FanStep07 != null) db.FanStepEffPowers.Add(fanDTO.FanStep07);
                if (fanDTO.FanStep08 != null) db.FanStepEffPowers.Add(fanDTO.FanStep08);
                if (fanDTO.FanStep09 != null) db.FanStepEffPowers.Add(fanDTO.FanStep09);
                if (fanDTO.FanStep10 != null) db.FanStepEffPowers.Add(fanDTO.FanStep10);
                if (fanDTO.FanStep11 != null) db.FanStepEffPowers.Add(fanDTO.FanStep11);
                if (fanDTO.FanStep12 != null) db.FanStepEffPowers.Add(fanDTO.FanStep12);
                if (fanDTO.FanStep13 != null) db.FanStepEffPowers.Add(fanDTO.FanStep13);
                if (fanDTO.FanStep14 != null) db.FanStepEffPowers.Add(fanDTO.FanStep14);
                if (fanDTO.FanStep15 != null) db.FanStepEffPowers.Add(fanDTO.FanStep15);
                if (fanDTO.FanStep16 != null) db.FanStepEffPowers.Add(fanDTO.FanStep16);
                if (fanDTO.FanStep17 != null) db.FanStepEffPowers.Add(fanDTO.FanStep17);
                if (fanDTO.FanStep18 != null) db.FanStepEffPowers.Add(fanDTO.FanStep18);
                if (fanDTO.FanStep19 != null) db.FanStepEffPowers.Add(fanDTO.FanStep19);
                if (fanDTO.FanStep20 != null) db.FanStepEffPowers.Add(fanDTO.FanStep20);
                if (fanDTO.FanStep21 != null) db.FanStepEffPowers.Add(fanDTO.FanStep21);

                FanDirectionDB fanDirection = null;
                if (!string.IsNullOrEmpty(fanDTO.Direction))
                {
                    // есть такой Direction (направление)
                    foreach (var direction in db.FanDirections.ToList())
                    {
                        if (direction.Name == fanDTO.Direction)
                        {
                            fanDirection = direction;
                            break;
                        }
                    }
                }
                if (fanDirection == null)
                {
                    fanDirection = new FanDirectionDB { Name = fanDTO.Direction };
                    db.FanDirections.Add(fanDirection);
                }

                FanMaterialsDB fanMaterials = null;
                if (!string.IsNullOrEmpty(fanDTO.Materials))
                {
                    // есть такой Material 
                    foreach (var material in db.FanMaterialss.ToList())
                    {
                        if (material.Name == fanDTO.Materials)
                        {
                            fanMaterials = material;
                            break;
                        }
                    }
                }
                if (fanMaterials == null)
                {
                    fanMaterials = new FanMaterialsDB { Name = fanDTO.Materials };
                    db.FanMaterialss.Add(fanMaterials);
                }

                FanMountDB fanMount = null;
                foreach (var mount in db.FanMounts.ToList())
                {
                    if (fanDTO.MountId == mount.Id)
                    {
                        fanMount = mount;
                        break;
                    }
                }

                FanModelsDB fanModel = new FanModelsDB
                {
                    Model = fanDTO.Model,
                    Voltage = fanDTO.Voltage,
                    ConnectionOfMotor = fanDTO.ConnectionOfMotor,
                    Size1 = fanDTO.Size1,
                    MotorPoles = fanDTO.MotorPoles,
                    Direction = fanDirection,
                    Speed = fanDTO.Speed,
                    Power = fanDTO.Power,
                    Current = fanDTO.Current,
                    NoiseLp = fanDTO.NoiseLp,
                    AirFlow_Min = fanDTO.AirFlowMin,
                    AirFlow_Max = fanDTO.AirFlowMax,
                    Materials = fanMaterials,
                    NBlades = fanDTO.NBlades,
                    IP = fanDTO.IP,
                    Mount = fanMount,
                    MountSize = fanDTO.MountSize,
                    Holes = fanDTO.Holes,
                    Weight = fanDTO.Weight,
                    AStatPres = fanDTO.AStatPres,
                    BStatPres = fanDTO.BStatPres,
                    CStatPres = fanDTO.CStatPres,
                    ATotalPres = fanDTO.ATotalPres,
                    BTotalPres = fanDTO.BTotalPres,
                    CTotalPres = fanDTO.CTotalPres,
                    AEffFactor = fanDTO.AEffFactor,
                    BEffFactor = fanDTO.BEffFactor,
                    CEffFactor = fanDTO.CEffFactor,
                    APowerInput = fanDTO.APowerInput,
                    BPowerInput = fanDTO.BPowerInput,
                    CPowerInput = fanDTO.CPowerInput,
                    Tipology = fanTipology,
                    Builder = fanBuilder,
                    Series = fanSeries,
                    Points01 = fanDTO.Points01,
                    Points02 = fanDTO.Points02,
                    Points03 = fanDTO.Points03,
                    Points04 = fanDTO.Points04,
                    Points05 = fanDTO.Points05,
                    Points06 = fanDTO.Points06,
                    Points07 = fanDTO.Points07,
                    Points08 = fanDTO.Points08,
                    Points09 = fanDTO.Points09,
                    Points10 = fanDTO.Points10,
                    Points11 = fanDTO.Points11,
                    Points12 = fanDTO.Points12,
                    Points13 = fanDTO.Points13,
                    Points14 = fanDTO.Points14,
                    Points15 = fanDTO.Points15,
                    Points16 = fanDTO.Points16,
                    Points17 = fanDTO.Points17,
                    Points18 = fanDTO.Points18,
                    Points19 = fanDTO.Points19,
                    Points20 = fanDTO.Points20,
                    FanStep01 = fanDTO.FanStep01,
                    FanStep02 = fanDTO.FanStep02,
                    FanStep03 = fanDTO.FanStep03,
                    FanStep04 = fanDTO.FanStep04,
                    FanStep05 = fanDTO.FanStep05,
                    FanStep06 = fanDTO.FanStep06,
                    FanStep07 = fanDTO.FanStep07,
                    FanStep08 = fanDTO.FanStep08,
                    FanStep09 = fanDTO.FanStep09,
                    FanStep10 = fanDTO.FanStep10,
                    FanStep11 = fanDTO.FanStep11,
                    FanStep12 = fanDTO.FanStep12,
                    FanStep13 = fanDTO.FanStep13,
                    FanStep14 = fanDTO.FanStep14,
                    FanStep15 = fanDTO.FanStep15,
                    FanStep16 = fanDTO.FanStep16,
                    FanStep17 = fanDTO.FanStep17,
                    FanStep18 = fanDTO.FanStep18,
                    FanStep19 = fanDTO.FanStep19,
                    FanStep20 = fanDTO.FanStep20,
                    FanStep21 = fanDTO.FanStep21,
                };
                db.FanModelss.Add(fanModel);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Редактирует новый вентилятор в БД
        /// </summary>
        /// <param name="fanT"></param>
        public ErrorEdit EditFan(FanT fanT)
        {
            errorEdit = ErrorEdit.ErrorNo;
            //_logs.Logger.Info("FanDb - EditFan");
            using (ApplicationContext db = new ApplicationContext())
            {
                LoadFanAllDB(db);
                string tipologyName;
                try
                {
                    tipologyName = fanT.FanTypesP.Where(f => f.IsSelect == true).First().Name;
                    errorEdit = ErrorEdit.ErrorTipologySelect;
                    FanTipologyDB fanTipology = db.FanTipologys.ToList().Where(t => (t.Name.Equals(tipologyName)
                        || t.NameRus.Equals(tipologyName))).First();
                    errorEdit = ErrorEdit.ErrorTipologyName;
                    FanBuilderDB fanBuilder = db.FanBuilders.ToList().Where(b => b.Name.Equals(fanT.SelectedBuilder)).FirstOrDefault();
                    if (fanBuilder is null)
                    {
                        fanBuilder = new FanBuilderDB { Name = fanT.SelectedBuilder };
                        db.FanBuilders.Add(fanBuilder);
                    }
                    errorEdit = ErrorEdit.ErrorBuilder;
                    FanSeriesDB fanSeries = db.FanSeriess.ToList().Where(s => s.Name.Equals(fanT.SelectedSeries)).FirstOrDefault();
                    if (fanSeries is null)
                    {
                        fanSeries = new FanSeriesDB { Name = fanT.SelectedSeries, Builder = fanBuilder };
                        db.FanSeriess.Add(fanSeries);
                    }
                    errorEdit = ErrorEdit.ErrorBuilder;
                    FanDirectionDB fanDirection = db.FanDirections.ToList().Where(d =>
                        d.Name.Equals(fanT.SelectDirection)).FirstOrDefault();
                    if (fanDirection is null)
                    {
                        fanDirection = new FanDirectionDB { Name = fanT.SelectDirection };
                        db.FanDirections.Add(fanDirection);
                    }
                    errorEdit = ErrorEdit.ErrorDirection;
                    FanMaterialsDB fanMaterials = db.FanMaterialss.ToList().Where(m =>
                        m.Name.Equals(fanT.SelectMaterial)).FirstOrDefault();
                    if (fanMaterials is null)
                    {
                        fanMaterials = new FanMaterialsDB { Name = fanT.SelectMaterial };
                        db.FanMaterialss.Add(fanMaterials);
                    }
                    errorEdit = ErrorEdit.ErrorMaterial;
                    FanMountDB fanMount = db.FanMounts.ToList().Where(m => m.Name.Equals(fanT.SelectedMount)).FirstOrDefault();
                    if (fanMount is null)
                    {
                        fanMount = new FanMountDB() { Name = fanT.SelectedMount };
                        db.FanMounts.Add(fanMount);
                    }
                    errorEdit = ErrorEdit.ErrorMount;
                    foreach (var fan in db.FanModelss.ToList())
                    {
                        if (fan.Id == fanT.Id)
                        {
                            fan.Model = fanT.Model;
                            fan.Voltage = fanT.Voltage;
                            if (fanT.ConnectionOfMotor == Calculation.TO.Main.Properties.Resources.ConnectionOfMotorStar)
                            {
                                fan.ConnectionOfMotor = 1;
                            }
                            else if (fanT.ConnectionOfMotor == Calculation.TO.Main.Properties.Resources.ConnectionOfMotorTriangle)
                            {
                                fan.ConnectionOfMotor = 2;
                            }
                            else fan.ConnectionOfMotor = 0;
                            fan.Size1 = fanT.Size1;
                            fan.MotorPoles = fanT.MotorPoles;
                            fan.Direction = fanDirection;
                            fan.Speed = fanT.Speed;
                            fan.Power = fanT.Power;
                            fan.Current = fanT.Current;
                            fan.NoiseLp = fanT.NoiseLp;
                            fan.AirFlow_Min = fanT.AirFlowMin;
                            fan.AirFlow_Max = fanT.AirFlowMax;
                            fan.Materials = fanMaterials;
                            fan.NBlades = fanT.NBlades;
                            fan.IP = fanT.SelectedIP;
                            fan.Mount = fanMount;
                            fan.MountSize = fanT.MountSize;
                            fan.Holes = fanT.Holes;
                            fan.Weight = fanT.Weight;
                            fan.AStatPres = fanT.AStatPres;
                            fan.BStatPres = fanT.BStatPres;
                            fan.CStatPres = fanT.CStatPres;
                            fan.ATotalPres = fanT.ATotalPres;
                            fan.BTotalPres = fanT.BTotalPres;
                            fan.CTotalPres = fanT.CTotalPres;
                            fan.AEffFactor = fanT.AEffFactor;
                            fan.BEffFactor = fanT.BEffFactor;
                            fan.CEffFactor = fanT.CEffFactor;
                            fan.APowerInput = fanT.APowerInput;
                            fan.BPowerInput = fanT.BPowerInput;
                            fan.CPowerInput = fanT.CPowerInput;
                            fan.Tipology = fanTipology;
                            fan.Builder = fanBuilder;
                            fan.Series = fanSeries;
                            break;
                        }
                    }
                    errorEdit = ErrorEdit.ErrorModels;
                    db.SaveChanges();
                    errorEdit = ErrorEdit.ErrorNo;
                }
                catch (SystemException e)
                {
                    return errorEdit;
                }
            }
            return errorEdit;
        }

        /// <summary>
        /// Получить для редактирования вентилятор в БД 
        /// </summary>
        /// <param name="name"></param>
        public FanT GetEditFan(string name)
        {
            //_logs.Logger.Info("FanDb - GetEditFan");
            FanT fan = new FanT();
            using (ApplicationContext db = new ApplicationContext())
            {
                LoadFanAllDB(db);
                foreach (var f in db.FanModelss.ToList())
                {
                    if (f.Model == name)
                    {
                        fan.Id = f.Id;
                        #region fan.FanTypesP
                        List<FanTypes> fanTypesP = new List<FanTypes>();
                        foreach (var ft in db.FanTipologys.ToList())
                        {
                            if (f.Tipology == ft)
                                fanTypesP.Add(new FanTypes(true, ft.Name));
                            else
                                fanTypesP.Add(new FanTypes(false, ft.Name));
                        }
                        fan.FanTypesP = fanTypesP;
                        #endregion
                        #region fan.Builder
                        List<string> builder = new List<string>();
                        foreach (var fb in db.FanBuilders.ToList())
                        {
                            if (f.Builder == fb)
                                fan.SelectedBuilder = fb.Name;
                            builder.Add(fb.Name);
                        }
                        if (fan.SelectedBuilder == null && builder.Count > 0)
                            fan.SelectedBuilder = builder[0];
                        fan.Builder = builder;
                        #endregion
                        #region fan.Series
                        List<string> series = new List<string>();
                        foreach (var fs in db.FanSeriess.ToList())
                        {
                            if (f.Series == fs)
                                fan.SelectedSeries = fs.Name;
                            series.Add(fs.Name);
                        }
                        if (fan.SelectedSeries == null && series.Count > 0)
                            fan.SelectedSeries = series[0];
                        fan.Series = series;
                        #endregion
                        fan.Model = f.Model;
                        #region fan.Materials
                        List<string> materials = new List<string>();
                        foreach (var m in db.FanMaterialss.ToList())
                        {
                            materials.Add(m.Name);
                        }
                        fan.Materials = materials;
                        #endregion
                        fan.SelectMaterial = f.Materials.Name;
                        fan.Voltage = f.Voltage;
                        fan.Size1 = f.Size1;
                        fan.MotorPoles = f.MotorPoles;
                        fan.Speed = f.Speed;
                        fan.Power = f.Power;
                        fan.Current = f.Current;
                        fan.NoiseLp = f.NoiseLp;
                        fan.AirFlowMin = f.AirFlow_Min;
                        fan.AirFlowMax = f.AirFlow_Max;
                        fan.NBlades = f.NBlades;
                        #region fan.Mounts
                        List<string> mounts = new List<string>();
                        foreach (var m in db.FanMounts.ToList())
                        {
                            mounts.Add(m.Name);
                        }
                        fan.Mounts = mounts;
                        #endregion
                        fan.SelectedMount = f.Mount.Name;
                        fan.MountSize = f.MountSize;
                        fan.Holes = f.Holes;

                        fan.IP = GetIP().ToList();
                        fan.SelectedIP = f.IP;
                        fan.Weight = f.Weight;
                        fan.AStatPres = f.AStatPres;
                        fan.BStatPres = f.BStatPres;
                        fan.CStatPres = f.CStatPres;
                        fan.ATotalPres = f.ATotalPres;
                        fan.BTotalPres = f.BTotalPres;
                        fan.CTotalPres = f.CTotalPres;
                        fan.AEffFactor = f.AEffFactor;
                        fan.BEffFactor = f.BEffFactor;
                        fan.CEffFactor = f.CEffFactor;
                        fan.APowerInput = f.APowerInput;
                        fan.BPowerInput = f.BPowerInput;
                        fan.CPowerInput = f.CPowerInput;
                        #region fan.Directions
                        List<string> directions = new List<string>();
                        foreach (var d in db.FanDirections.ToList())
                        {
                            directions.Add(d.Name);
                        }
                        fan.Directions = directions;
                        #endregion
                        fan.SelectDirection = f.Direction.Name;
                        if (f.ConnectionOfMotor == 1)
                        {
                            fan.ConnectionOfMotor = Calculation.TO.Main.Properties.Resources.ConnectionOfMotorStar;
                        }
                        else if (f.ConnectionOfMotor == 2)
                        {
                            fan.ConnectionOfMotor = Calculation.TO.Main.Properties.Resources.ConnectionOfMotorTriangle;
                        }
                        else fan.ConnectionOfMotor = "";
                        return fan;
                    }
                }
            }
            return fan;
        }

        /// <summary>
        /// обновляется список вентиляторов при изменении типа вентиялтора
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Fans"></param>
        /// <param name="FanTip1"></param>
        /// <param name="FanTip2"></param>
        /// <param name="FanTip3"></param>
        /// <param name="FanTip4"></param>
        public void Update(ObservableCollection<FanDTO> Fans,
            bool FanTip1, bool FanTip2, bool FanTip3, bool FanTip4)
        {
            //_logs.Logger.Info("FanDb - Update");
            using (ApplicationContext db = new ApplicationContext())
            {
                LoadFanAllDB(db);
                Fans.Clear();
                foreach (var fan in db.FanModelss.ToList())
                {
                    bool fAdd = false;
                    fAdd = IsAdd(FanTip1, FanTip2, FanTip3, FanTip4, fan, fAdd);
                    if (fAdd)
                    {
                        Fans.Add(MapperFanModelsToFanDTO.FanModelsToFanDTO(fan));
                    }
                }
            }
        }

        /// <summary>
        /// Обновляется список вентиляторов при изменении производителя
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Fans"></param>
        /// <param name="FanTip1"></param>
        /// <param name="FanTip2"></param>
        /// <param name="FanTip3"></param>
        /// <param name="FanTip4"></param>
        /// <param name="selectedBuilder"></param>
        /// <param name="Series"></param>
        /// <returns></returns>
        public ObservableCollection<string> Update(ObservableCollection<FanDTO> Fans,
            bool FanTip1, bool FanTip2, bool FanTip3, bool FanTip4,
            string selectedBuilder, ObservableCollection<string> Series)
        {
            //_logs.Logger.Info("FanDb - ObservableCollection<string> Update ");
            ObservableCollection<string> LSeries = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                LoadFanAllDB(db);
                Fans.Clear();
                foreach (var fan in db.FanModelss.ToList())
                {
                    bool fAdd = false;
                    fAdd = IsAdd(FanTip1, FanTip2, FanTip3, FanTip4, fan, fAdd);
                    int builderId = -1;
                    fAdd = IsAddBuilder(selectedBuilder, db, fan, fAdd, ref builderId);
                    LSeries = Modify(db, builderId, ref Series);
                    if (fAdd)
                    {
                        Fans.Add(MapperFanModelsToFanDTO.FanModelsToFanDTO(fan));
                    }
                }
            }
            return LSeries;
        }

        /// <summary>
        /// Обновляется список вентиляторов при изменении Серии
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Fans"></param>
        /// <param name="FanTip1"></param>
        /// <param name="FanTip2"></param>
        /// <param name="FanTip3"></param>
        /// <param name="FanTip4"></param>
        /// <param name="selectedBuilder"></param>
        /// <param name="Series"></param>
        /// <param name="selectedSeries"></param>
        /// <returns></returns>
        public void Update(ObservableCollection<FanDTO> Fans,
            bool FanTip1, bool FanTip2, bool FanTip3, bool FanTip4,
            string selectedBuilder, string selectedSeries)
        {
            //_logs.Logger.Info("FanDb - Update selectedBuilder selectedSeries ");
            using (ApplicationContext db = new ApplicationContext())
            {
                LoadFanAllDB(db);
                Fans.Clear();
                foreach (var fan in db.FanModelss.ToList())
                {
                    bool fAdd = false;
                    fAdd = IsAdd(FanTip1, FanTip2, FanTip3, FanTip4, fan, fAdd);
                    int builderId = -1;
                    fAdd = IsAddBuilder(selectedBuilder, db, fan, fAdd, ref builderId);

                    if (selectedSeries != Calculation.TO.Main.Properties.Resources.FanAll)
                    {
                        foreach (var series in db.FanSeriess.ToList())
                        {
                            if (series.Name == selectedSeries)
                            {
                                if (fan.Series.Id != series.Id)
                                    fAdd = false;
                            }

                        }
                    }
                    if (fAdd)
                    {
                        Fans.Add(MapperFanModelsToFanDTO.FanModelsToFanDTO(fan));
                    }
                }
            }
        }

        /// <summary>
        /// Возвраящяет имя производителя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetBuilder(int id)
        {
            //_logs.Logger.Info("FanDb - GetBuilder");
            string retName = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var builder in db.FanBuilders.ToList())
                {
                    if (builder.Id == id)
                    {
                        retName = builder.Name;
                        break;
                    }
                }
            }
            return retName;
        }

        /// <summary>
        /// Возвращяет имя серии по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSeries(int id)
        {
            //_logs.Logger.Info("FanDb - GetSeries");
            string retName = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var series in db.FanSeriess.ToList())
                {
                    if (series.Id == id)
                    {
                        retName = series.Name;
                        break;
                    }
                }
            }
            return retName;
        }

        /// <summary>
        /// Обновление списков Производители (Builder) и Серии (Series) для окна "Редактировать вентилятор"
        /// </summary>
        /// <param name="Builder"></param>
        /// <param name="SelectedBuilder"></param>
        /// <param name="Series"></param>
        /// <param name="SelectedSeries"></param>
        public void UpdateBuilderAndSeries(ObservableCollection<string> Builder, ref string SelectedBuilder,
            ObservableCollection<string> Series, ref string SelectedSeries)
        {
            //_//logs.Logger.Info("FanDb - UpdateBuilderAndSeries");
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanBuilders.Load();
                db.FanSeriess.Load();
                Builder.Clear();
                Builder.Add(Calculation.TO.Main.Properties.Resources.FanAll);
                SelectedBuilder = Calculation.TO.Main.Properties.Resources.FanAll;
                foreach (var builder in db.FanBuilders.ToList())
                {
                    Builder.Add(builder.Name);
                }
                Series.Clear();
                Series.Add(Calculation.TO.Main.Properties.Resources.FanAll);
                SelectedSeries = Calculation.TO.Main.Properties.Resources.FanAll;
                foreach (var series in db.FanSeriess.ToList())
                {
                    Series.Add(series.Name);
                }
            }
        }

        /// <summary>
        /// Обновление списков Производители (Builder) и Серии (Series) для окна "Добавить вентилятор"
        /// </summary>
        /// <param name="Builder"></param>
        /// <param name="SelectedBuilder"></param>
        /// <param name="Series"></param>
        /// <param name="SelectedSeries"></param>
        public void UpdateAddBuilderAndSeries(ObservableCollection<string> Builder, ref string SelectedBuilder,
            ObservableCollection<string> Series, ref string SelectedSeries)
        {
            //_logs.Logger.Info("FanDb - UpdateAddBuilderAndSeries");
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanBuilders.Load();
                db.FanSeriess.Load();
                Builder.Clear();
                SelectedBuilder = Calculation.TO.Main.Properties.Resources.FanAll;
                foreach (var builder in db.FanBuilders.ToList())
                {
                    Builder.Add(builder.Name);
                }
                Series.Clear();
                SelectedSeries = Calculation.TO.Main.Properties.Resources.FanAll;
                foreach (var series in db.FanSeriess.ToList())
                {
                    Series.Add(series.Name);
                }
            }
        }

        /// <summary>
        /// Поиск подходящего вентилятора по расходу воздуха и давлению
        /// </summary>
        /// <param name="airFlow">расход воздуха</param>
        /// <param name="presDropDry">давление</param>
        /// <returns></returns>
        public IList<FanDTO> FanSearch(double airFlow, double airFlowMax, double presDropDry)
        {
            fans.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                LoadFanAllDB(db);
                AirFlowAbove(airFlowMax, db);
                if (fans.Count > 0)
                {
                    CheckMaxPressure(presDropDry, airFlowMax, db);
                    CheckPressure(presDropDry, airFlowMax, db);
                    SelectBest(presDropDry, airFlowMax, db);
                }
            }
            if (fans.Count > 0)
                return GetFanNames(fans);
            else
                return null;
        }

        /// <summary>
        /// расхожд воздуха для выбранного вентилятора при перепаде давления
        /// </summary>
        /// <param name="fanName"></param>
        /// <returns></returns>
        public string GetFanAirFlowAtPresDrop(string fanName)
        {
            foreach (var fan in fans)
            {
                if (fan.fans.Model == fanName)
                {
                    return fan.AirFlow.ToString("f2");
                }
            }
            return "";
        }

        /// <summary>
        /// получить список вентиляторов
        /// </summary>
        /// <returns></returns>
        public IList<string> GetFans()
        {
            IList<string> list = new List<string>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanModelss.Load();
                foreach (var fan in db.FanModelss.ToList())
                {
                    list.Add(fan.Model);
                }
            }
            return list;
        }      

        /// <summary>
        /// Получить все материалы
        /// </summary>
        /// <returns></returns>
        public IList<string> GetMaterials()
        {
            IList<string> list = new List<string>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanMaterialss.Load();
                foreach (var materials in db.FanMaterialss.ToList())
                {
                    list.Add(materials.Name);
                }
            }
            return list;
        }

        /// <summary>
        /// Получить IP защиты
        /// </summary>
        /// <returns></returns>
        public IList<string> GetIP()
        {
            return new List<string>() {"IP00","IP10","IP11","IP12","IP20","IP21","IP22","IP23","IP30","IP31","IP32","IP33",
                "IP34","IP40","IP41","IP42","IP43","IP44","IP50","IP54","IP55","IP60","IP65","IP66","IP67","IP68"};
        }

        public string GetSelectedIP()
        {
            return "IP54";
        }

        public IList<string> GetDirections()
        {
            IList<string> list = new List<string>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanDirections.Load();
                foreach (var direction in db.FanDirections.ToList())
                {
                    list.Add(direction.Name);
                }
            }
            return list;
        }

        /// <summary>
        /// Получение всех точек переданного вентилятора
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<FanPointsDB> GetPoints(string name)
        {
            List<FanPointsDB> fanPoints = new List<FanPointsDB>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanPointss.Load();
                db.FanModelss.Load();
                foreach (var point in db.FanModelss.ToList())
                {
                    if (point.Model == name)
                    {
                        Add(point.Points01);
                        Add(point.Points02);
                        Add(point.Points03);
                        Add(point.Points04);
                        Add(point.Points05);
                        Add(point.Points06);
                        Add(point.Points07);
                        Add(point.Points08);
                        Add(point.Points09);
                        Add(point.Points10);
                        Add(point.Points11);
                        Add(point.Points12);
                        Add(point.Points13);
                        Add(point.Points14);
                        Add(point.Points15);
                        Add(point.Points16);
                        Add(point.Points17);
                        Add(point.Points18);
                        Add(point.Points19);
                        Add(point.Points20);
                        break;
                    }
                }
            }
            return fanPoints;

            void Add(FanPointsDB pFanPoints)
            {
                if (pFanPoints != null)
                {
                    fanPoints.Add(pFanPoints);
                }
            }
        }

        /// <summary>
        /// Сохранить отредактированную таблицу скоростей для выбранного вентилятора
        /// </summary>
        /// <param name="fanPoints"></param>
        public void SetPoints(List<FanPointsDB> fanPoints)
        {
            int count = fanPoints.Count;
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanPointss.Load();
                foreach(var p in db.FanPointss.ToList())
                {
                    foreach(var pIn in fanPoints)
                    {
                        if (p.Id == pIn.Id)
                        {
                            p.Voltage = pIn.Voltage; 
                            p.Frequency = pIn.Frequency; 
                            p.Speed = pIn.Speed;
                            p.Power = pIn.Power;
                            p.Current = pIn.Current;
                            p.Airflow = pIn.Airflow;
                            p.StatPressure = pIn.StatPressure;
                            p.DynamicPressure = pIn.DynamicPressure;
                            p.TotalPressure = pIn.TotalPressure;
                            p.AirflowS = pIn.AirflowS;
                            p.EffFactorProcent = pIn.EffFactorProcent;
                            p.PerfFactor = pIn.PerfFactor;
                            p.TotalPresFactor = pIn.TotalPresFactor;
                            p.StatPresFactor = pIn.StatPresFactor;
                            p.DynPresFactor = pIn.DynPresFactor;
                            p.PowerFactor = pIn.PowerFactor;
                            p.EffFactor = pIn.EffFactor;
                            p.SpeedFactor1 = pIn.SpeedFactor1;
                            p.SpeedFactor2 = pIn.SpeedFactor2;
                            p.SizeFactor1 = pIn.SizeFactor1;
                            p.SizeFactor2 = pIn.SizeFactor2;
                            count--;
                            break;
                        }
                    }
                    if (count < 1) break;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Получение таблицы FanStepEffPower - мощность по шагам
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<FanStepEffPowerDB> GetSteps(string name)
        {
            List<FanStepEffPowerDB> fanSteps = new List<FanStepEffPowerDB>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanStepEffPowers.Load();
                db.FanModelss.Load();
                foreach (var step in db.FanModelss.ToList())
                {
                    if (step.Model == name)
                    {
                        Add(step.FanStep01);
                        Add(step.FanStep02);
                        Add(step.FanStep03);
                        Add(step.FanStep04);
                        Add(step.FanStep05);
                        Add(step.FanStep06);
                        Add(step.FanStep07);
                        Add(step.FanStep08);
                        Add(step.FanStep09);
                        Add(step.FanStep10);
                        Add(step.FanStep11);
                        Add(step.FanStep12);
                        Add(step.FanStep13);
                        Add(step.FanStep14);
                        Add(step.FanStep15);
                        Add(step.FanStep16);
                        Add(step.FanStep17);
                        Add(step.FanStep18);
                        Add(step.FanStep19);
                        Add(step.FanStep20);
                        Add(step.FanStep21);
                        break;
                    }
                }
            }
            return fanSteps;

            void Add(FanStepEffPowerDB pFanSteps)
            {
                if (pFanSteps != null)
                {
                    fanSteps.Add(pFanSteps);
                }
            }
        }

        /// <summary>
        /// Сохранить таблицу "расход воздуха-мощность" для выбранного вентилятора
        /// </summary>
        /// <param name="fanSteps"></param>
        public void SetSteps(List<FanStepEffPowerDB> fanSteps)
        {
            int count = fanSteps.Count;
            using (ApplicationContext db = new ApplicationContext())
            {
                db.FanStepEffPowers.Load();
                foreach (var s in db.FanStepEffPowers.ToList())
                {
                    foreach(var sIn in fanSteps)
                    {
                        if (s.Id == sIn.Id)
                        {
                            s.Step = sIn.Step;
                            s.StatPressure = sIn.StatPressure;
                            s.TotalPressure = sIn.TotalPressure;
                            s.EffFacto1 = sIn.EffFacto1;
                            s.EffFacto2 = sIn.EffFacto2;
                            s.PowerInput = sIn.PowerInput;
                            count--;
                            break;
                        }
                    }
                    if (count < 1) break;
                }
            }
        }

        public void CalcPoint(float temp, float humidity, float density, float speed, float size, FanPointsDB fanPoints)
        {
            float f = (float)(Math.PI * Math.Pow(size / 1000.0, 2) / 4);
            float u = (float)(Math.PI * (size / 1000.0) * speed / 60);
            //float fu = (float)(f * u);
            //float pu2 = (float)(density * Math.Pow(u, 2) / 2);
            //float pFu3 = (float)(density * f * Math.Pow(u, 3) / 2);
            fanPoints.TotalPressure = (ushort)(fanPoints.StatPressure + fanPoints.DynamicPressure);
            fanPoints.AirflowS = (float)Math.Round(fanPoints.Airflow / 3600.0, 1);
            fanPoints.EffFactorProcent = (float)Math.Round((fanPoints.Airflow * fanPoints.StatPressure / 36.0) /
                fanPoints.Power, 1);
            fanPoints.PerfFactor = (float)Math.Round(fanPoints.AirflowS / (f * u), 3);
            fanPoints.TotalPresFactor = (float)Math.Round(fanPoints.TotalPressure / (0.5 * density * Math.Pow(u, 2)), 3);
            fanPoints.StatPresFactor = (float)Math.Round(fanPoints.StatPressure / (0.5 * density * Math.Pow(u, 2)), 3);
            fanPoints.DynPresFactor = (float)Math.Round(fanPoints.DynamicPressure / (0.5 * density * Math.Pow(u, 2)), 3);
            fanPoints.PowerFactor = (float)Math.Round(fanPoints.Power / (0.5 * density * f * Math.Pow(u, 3)), 3);
            fanPoints.EffFactor = (float)Math.Round(fanPoints.AirflowS * fanPoints.TotalPressure / fanPoints.Power, 3);
            fanPoints.SpeedFactor1 = (float)Math.Round(138 * Math.Sqrt(fanPoints.PerfFactor)
                    / Math.Pow(fanPoints.TotalPresFactor, 0.75), 3);
            fanPoints.SpeedFactor2 = (float)Math.Round(Math.Sqrt(fanPoints.AirflowS) * fanPoints.Speed
                    / Math.Pow(fanPoints.TotalPressure / 9.8, 0.75), 3);
            fanPoints.SizeFactor1 = (float)Math.Round(0.56 * Math.Pow(fanPoints.TotalPresFactor, 0.25)
                    / Math.Sqrt(fanPoints.PerfFactor), 3);
            fanPoints.SizeFactor2 = (float)Math.Round(Math.Pow(fanPoints.TotalPressure / 9.8, 0.25) * (size / 1000)
                    / Math.Sqrt(fanPoints.AirflowS), 3);
        }

        public void CalcStepEffPower(List<FanStepEffPowerDB> listFanStepEffPower, float max, float min, float AStatPres,
            float BStatPres, float CStatPres, float ATotalPres, float BTotalPres, float CTotalPres,
            float AEffFactor, float BEffFactor, float CEffFactor, float APowerInput, float BPowerInput, float CPowerInput)
        {
            int step = (int)((max - min) / 20.0);
            FanStepEffPowerDB fanStep = null;
            for (int i = 0; i < 21; i++)
            {
                fanStep = new FanStepEffPowerDB
                {
                    Step = (ushort)(min + i * step),
                };
                fanStep.StatPressure = (short)Math.Round(AStatPres * fanStep.Step * fanStep.Step
                    + BStatPres * fanStep.Step + CStatPres);
                fanStep.TotalPressure = (short)Math.Round(ATotalPres * fanStep.Step * fanStep.Step
                    + BTotalPres * fanStep.Step + CTotalPres);
                fanStep.EffFacto1 = (float)(Math.Round((AEffFactor * fanStep.Step * fanStep.Step
                    + BEffFactor * fanStep.Step + CEffFactor) / 1_000, 1));
                fanStep.PowerInput = (float)Math.Round(APowerInput * fanStep.Step * fanStep.Step
                    + BPowerInput * fanStep.Step + CPowerInput);
                fanStep.EffFacto2 = (float)Math.Round((fanStep.Step * fanStep.StatPressure / 36) / fanStep.PowerInput, 1);
                listFanStepEffPower.Add(fanStep);
            }
        }

        public void CalcStepEffPower(FanStepEffPowerDB selectedStep, float max, float min, float AStatPres,
            float BStatPres, float CStatPres, float ATotalPres, float BTotalPres, float CTotalPres,
            float AEffFactor, float BEffFactor, float CEffFactor, float APowerInput, float BPowerInput, float CPowerInput)
        {
            selectedStep.StatPressure = (short)Math.Round(AStatPres * selectedStep.Step * selectedStep.Step
                + BStatPres * selectedStep.Step + CStatPres);
            selectedStep.TotalPressure = (short)Math.Round(ATotalPres * selectedStep.Step * selectedStep.Step
                + BTotalPres * selectedStep.Step + CTotalPres);
            selectedStep.EffFacto1 = (float)(Math.Round((AEffFactor * selectedStep.Step * selectedStep.Step
                + BEffFactor * selectedStep.Step + CEffFactor) / 1_000, 1));
            selectedStep.PowerInput = (float)Math.Round(APowerInput * selectedStep.Step * selectedStep.Step
                + BPowerInput * selectedStep.Step + CPowerInput);
            selectedStep.EffFacto2 = (float)Math.Round((selectedStep.Step * selectedStep.StatPressure / 36) / selectedStep.PowerInput, 1);
        }


        /// <summary>
        /// Возвращяет объект вентилятора по имени
        /// </summary>
        /// <param name="db"></param>
        /// <param name="fanName"></param>
        /// <returns></returns>
        public FanModelsDB GetFan(ApplicationContext db, string fanName)
        {
            foreach (var v_fan in db.FanModelss.ToList())
            {
                if (v_fan.Model == fanName)
                {
                    return v_fan;
                }
            }
            return null;
        }

        /// <summary>
        /// Получает ток вентилятора по имени
        /// </summary>
        /// <param name="fanName">имя вентилятора</param>
        /// <returns></returns>
        public double GetCurrent(string fanName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var v_fan in db.FanModelss.ToList())
                {
                    if (v_fan.Model == fanName)
                    {
                        return v_fan.Current;
                    }
                }
            }
            return 0;
        }
        #endregion

        #region Приватные методы

        /// <summary>
        /// Загрузить все базы вентиялторов
        /// </summary>
        /// <param name="db"></param>
        private static void LoadFanAllDB(ApplicationContext db)
        {
            db.FanDirections.Load();
            db.FanMaterialss.Load();
            db.FanMounts.Load();
            db.FanBuilders.Load();
            db.FanSeriess.Load();
            db.FanPointss.Load();
            db.FanStepEffPowers.Load();
            db.FanTipologys.Load();
            db.FanModelss.Load();
        }

        private void SeedDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // пересоздадим базу данных
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // добавляем начальные данные
                FanTipologyDB fanTipology1 = new FanTipologyDB
                {
                    Name = Calculation.TO.Main.Properties.Resources.TipologyAxialMonophase,
                    NameRus = Calculation.TO.Main.Properties.Resources.TipologyAxialMonophaseRu
                };
                FanTipologyDB fanTipology2 = new FanTipologyDB
                {
                    Name = Calculation.TO.Main.Properties.Resources.TipologyAxialTriphase,
                    NameRus = Calculation.TO.Main.Properties.Resources.TipologyAxialTriphaseRu
                };
                FanTipologyDB fanTipology3 = new FanTipologyDB
                {
                    Name = Calculation.TO.Main.Properties.Resources.TipologyCentriphugal,
                    NameRus = Calculation.TO.Main.Properties.Resources.TipologyCentriphugalRu
                };
                FanTipologyDB fanTipology4 = new FanTipologyDB
                {
                    Name = Calculation.TO.Main.Properties.Resources.TipologyDirectlyCoupled,
                    NameRus = Calculation.TO.Main.Properties.Resources.TipologyDirectlyCoupledRu
                };
                db.FanTipologys.Add(fanTipology1);
                db.FanTipologys.Add(fanTipology2);
                db.FanTipologys.Add(fanTipology3);
                db.FanTipologys.Add(fanTipology4);

                FanMaterialsDB materials = new FanMaterialsDB
                {
                    Name = "Steel with PP"
                };
                db.FanMaterialss.Add(materials);
                FanMaterialsDB materialsAl = new FanMaterialsDB
                {
                    Name = "Aluminum"
                };
                db.FanMaterialss.Add(materialsAl);

                FanDirectionDB direction = new FanDirectionDB
                {
                    Name = "S"
                };
                db.FanDirections.Add(direction);

                FanMountDB mountCirc = new FanMountDB
                {
                    Name = "Круг"
                };
                db.FanMounts.Add(mountCirc);

                FanMountDB mountSquare = new FanMountDB
                {
                    Name = "Квадрат"
                };
                db.FanMounts.Add(mountSquare);

                FanBuilderDB fanBuilder1 = new FanBuilderDB { Name = "ebmpapst" };
                db.FanBuilders.Add(fanBuilder1);
                db.SaveChanges();

                FanModelsDB fanModel1 = SeadItem1(db, materialsAl, direction, fanTipology2, mountSquare);
                db.SaveChanges();
                FanModelsDB fanModel2 = SeadItem2(db, direction, fanTipology2, mountCirc);
                FanModelsDB fanModel3 = SeadItem3(db, materials, direction, fanTipology1, mountCirc, fanBuilder1);
                FanModelsDB fanModel4 = SeadItem4(db, materials, fanTipology1, mountCirc, fanBuilder1);
                FanModelsDB fanModel5 = SeadItem5(db, materialsAl, direction, fanTipology2, mountSquare);
                FanModelsDB fanModel6 = SeadItem6(db, direction, fanTipology1, mountCirc);

                List<FanStepEffPowerDB> listFanStepEffPower = new List<FanStepEffPowerDB>();
                CalcStepEffPower(listFanStepEffPower, fanModel1, db);
                listFanStepEffPower = new List<FanStepEffPowerDB>();
                CalcStepEffPower(listFanStepEffPower, fanModel2, db);
                listFanStepEffPower = new List<FanStepEffPowerDB>();
                CalcStepEffPower(listFanStepEffPower, fanModel3, db);
                listFanStepEffPower = new List<FanStepEffPowerDB>();
                CalcStepEffPower(listFanStepEffPower, fanModel4, db);
                listFanStepEffPower = new List<FanStepEffPowerDB>();
                CalcStepEffPower(listFanStepEffPower, fanModel5, db);
                listFanStepEffPower = new List<FanStepEffPowerDB>();
                CalcStepEffPower(listFanStepEffPower, fanModel6, db);

                db.SaveChanges();
            }

            void CalcPoints(FanModelsDB fanModel, List<FanPointsDB> listFanPoint, ApplicationContext db,
                float temp, float humidity, float density)
            {
                float speedSum = 0;
                foreach (var point in listFanPoint)
                {
                    speedSum += point.Speed;
                }
                float speed = (float)Math.Round(speedSum / listFanPoint.Count);

                float size = fanModel.Size1;
                float f = (float)(Math.PI * Math.Pow(fanModel.Size1 / 1000.0, 2) / 4);
                //float f = (float)Math.Round(Math.PI * Math.Pow(fanModel.Size1 / 1000, 2) / 4, 3);
                //float f = 0.159f; 
                float u = (float)(Math.PI * (size / 1000.0) * speed / 60);
                //float u = (float)Math.Round(Math.PI * (size / 1000) * speed / 60, 1);
                //float u = 31.4f; 
                float fu = (float)(f * u);
                //float fu = (float)Math.Round( f * u, 1);
                //float fu = 5.0f;
                float pu2 = (float)(density * Math.Pow(u, 2) / 2);
                //float pu2 = (float)Math.Round(density * Math.Pow( u, 2) / 2, 1);
                //float pu2 = 590.5f;
                float pFu3 = (float)(density * f * Math.Pow(u, 3) / 2);
                //float pFu3 = (float)Math.Round(density * f * Math.Pow(u, 3) / 2, 1);
                //float pFu3 = 2946.1f;
                float min = fanModel.AirFlow_Min;
                float max = fanModel.AirFlow_Max;
                foreach (var point in listFanPoint)
                {
                    point.TotalPressure = (ushort)(point.StatPressure + point.DynamicPressure);
                    point.AirflowS = (float)Math.Round(point.Airflow / 3600.0, 1);
                    point.EffFactorProcent = (float)Math.Round((point.Airflow * point.StatPressure / 36.0) / point.Power, 1);
                    point.PerfFactor = (float)Math.Round(point.AirflowS / (f * u), 3);
                    point.TotalPresFactor = (float)Math.Round(point.TotalPressure / (0.5 * density * Math.Pow(u, 2)), 3);
                    point.StatPresFactor = (float)Math.Round(point.StatPressure / (0.5 * density * Math.Pow(u, 2)), 3);
                    point.DynPresFactor = (float)Math.Round(point.DynamicPressure / (0.5 * density * Math.Pow(u, 2)), 3);
                    point.PowerFactor = (float)Math.Round(point.Power / (0.5 * density * f * Math.Pow(u, 3)), 3);
                    point.EffFactor = (float)Math.Round(point.AirflowS * point.TotalPressure / point.Power, 3);
                    point.SpeedFactor1 = (float)Math.Round(138 * Math.Sqrt(point.PerfFactor)
                        / Math.Pow(point.TotalPresFactor, 0.75), 3);
                    point.SpeedFactor2 = (float)Math.Round(Math.Sqrt(point.AirflowS) * point.Speed
                        / Math.Pow(point.TotalPressure / 9.8, 0.75), 3);
                    point.SizeFactor1 = (float)Math.Round(0.56 * Math.Pow(point.TotalPresFactor, 0.25)
                        / Math.Sqrt(point.PerfFactor), 3);
                    point.SizeFactor2 = (float)Math.Round(Math.Pow(point.TotalPressure / 9.8, 0.25) * (size / 1000)
                        / Math.Sqrt(point.AirflowS), 3);
                    db.FanPointss.Add(point);
                }
            }

            void CalcStepEffPower(List<FanStepEffPowerDB> listFanStepEffPower, FanModelsDB fanModel, ApplicationContext db)
            {
                float step = (fanModel.AirFlow_Max - fanModel.AirFlow_Min) / 20;
                FanStepEffPowerDB fanStep = null;
                for (int i = 0; i < 21; i++)
                {
                    fanStep = new FanStepEffPowerDB
                    {
                        Step = (ushort)(fanModel.AirFlow_Min + i * step),
                    };
                    fanStep.StatPressure = (short)Math.Round(fanModel.AStatPres * fanStep.Step * fanStep.Step
                        + fanModel.BStatPres * fanStep.Step + fanModel.CStatPres);
                    fanStep.TotalPressure = (short)Math.Round(fanModel.ATotalPres * fanStep.Step * fanStep.Step
                        + fanModel.BTotalPres * fanStep.Step + fanModel.CTotalPres);
                    fanStep.EffFacto1 = (float)(Math.Round((fanModel.AEffFactor * fanStep.Step * fanStep.Step
                        + fanModel.BEffFactor * fanStep.Step + fanModel.CEffFactor) / 1_000, 1));
                    fanStep.PowerInput = (float)Math.Round(fanModel.APowerInput * fanStep.Step * fanStep.Step
                        + fanModel.BPowerInput * fanStep.Step + fanModel.CPowerInput);
                    fanStep.EffFacto2 = (float)Math.Round((fanStep.Step * fanStep.StatPressure / 36) / fanStep.PowerInput, 1);
                    listFanStepEffPower.Add(fanStep);
                    db.FanStepEffPowers.Add(fanStep);
                }
                fanModel.FanStep01 = listFanStepEffPower[0];
                fanModel.FanStep02 = listFanStepEffPower[1];
                fanModel.FanStep03 = listFanStepEffPower[2];
                fanModel.FanStep04 = listFanStepEffPower[3];
                fanModel.FanStep05 = listFanStepEffPower[4];
                fanModel.FanStep06 = listFanStepEffPower[5];
                fanModel.FanStep07 = listFanStepEffPower[6];
                fanModel.FanStep08 = listFanStepEffPower[7];
                fanModel.FanStep09 = listFanStepEffPower[8];
                fanModel.FanStep10 = listFanStepEffPower[9];
                fanModel.FanStep11 = listFanStepEffPower[10];
                fanModel.FanStep12 = listFanStepEffPower[11];
                fanModel.FanStep13 = listFanStepEffPower[12];
                fanModel.FanStep14 = listFanStepEffPower[13];
                fanModel.FanStep15 = listFanStepEffPower[14];
                fanModel.FanStep16 = listFanStepEffPower[15];
                fanModel.FanStep17 = listFanStepEffPower[16];
                fanModel.FanStep18 = listFanStepEffPower[17];
                fanModel.FanStep19 = listFanStepEffPower[18];
                fanModel.FanStep20 = listFanStepEffPower[19];
                fanModel.FanStep21 = listFanStepEffPower[20];
            }

            FanModelsDB SeadItem1(ApplicationContext db, FanMaterialsDB materials, FanDirectionDB direction,
                FanTipologyDB fanTipology, FanMountDB mount)
            {
                FanBuilderDB fanBuilder1 = new FanBuilderDB { Name = "SANMU" };
                db.FanBuilders.Add(fanBuilder1);

                FanSeriesDB fanSeries = new FanSeriesDB { Name = "900 mm", Builder = fanBuilder1 };
                db.FanSeriess.Add(fanSeries);
                db.SaveChanges();
                List<FanPointsDB> listFanPoint = new List<FanPointsDB>();
                AddPoints1(listFanPoint);

                FanModelsDB fanModel = new FanModelsDB
                {
                    Model = "YNF6D900-ZF00C-S5I",
                    Voltage = 380,
                    ConnectionOfMotor = 2,
                    Size1 = 900,
                    MotorPoles = 6,
                    Direction = direction,
                    Speed = 960,
                    Power = 3.0f,
                    Current = 5.8f,
                    NoiseLp = 85,
                    AirFlow_Min = 18_000,
                    AirFlow_Max = 30_000,
                    Materials = materials,
                    NBlades = 5,
                    IP = "IP55",
                    Mount = mount,
                    MountSize = 1010,
                    Holes = "4x14,5",
                    Weight = 67,
                    AStatPres = -0.000_000_6,
                    BStatPres = 0.005_2,
                    CStatPres = 417.33,
                    ATotalPres = -0.000_000_5,
                    BTotalPres = 0.004_9,
                    CTotalPres = 419.52,
                    AEffFactor = -0.000_433,
                    BEffFactor = 17.7128,
                    CEffFactor = -138877.12,
                    APowerInput = -0.000_007,
                    BPowerInput = 0.2_232,
                    CPowerInput = 1817.1,
                    Tipology = fanTipology,
                    Builder = fanBuilder1,
                    Series = fanSeries,
                    Points01 = listFanPoint[0],
                    Points02 = listFanPoint[1],
                    Points03 = listFanPoint[2],
                    Points04 = listFanPoint[3],
                    Points05 = listFanPoint[4],
                    Points06 = listFanPoint[5],
                    Points07 = listFanPoint[6],
                    Points08 = listFanPoint[7],
                    Points09 = listFanPoint[8],
                    Points10 = listFanPoint[9],
                    Points11 = listFanPoint[10],
                    Points12 = listFanPoint[11],
                    Points13 = listFanPoint[12],
                    Points14 = listFanPoint[13],
                    Points15 = listFanPoint[14],
                    Points16 = listFanPoint[15],
                };
                CalcPoints(fanModel, listFanPoint, db, 33, 64, 1.2f);
                db.SaveChanges();
                db.FanModelss.Add(fanModel);
                return fanModel;
            }

            FanModelsDB SeadItem2(ApplicationContext db, FanDirectionDB direction,
                FanTipologyDB fanTipology, FanMountDB mount)
            {
                FanBuilderDB fanBuilder1 = new FanBuilderDB { Name = "Weiguang" };
                db.FanBuilders.Add(fanBuilder1);

                FanSeriesDB fanSeries = new FanSeriesDB { Name = "800 mm", Builder = fanBuilder1 };
                db.FanSeriess.Add(fanSeries);

                List<FanPointsDB> listFanPoint = new List<FanPointsDB>();
                AddPoints2(listFanPoint);

                FanMaterialsDB materials = new FanMaterialsDB
                {
                    Name = "Steel"
                };
                db.FanMaterialss.Add(materials);

                FanModelsDB fanModel = new FanModelsDB
                {
                    Model = "YWF6D-800S-180/105-G",
                    Voltage = 380,
                    ConnectionOfMotor = 1,
                    Size1 = 800,
                    MotorPoles = 6,
                    Direction = direction,
                    Speed = 890,
                    Power = 1.9f,
                    Current = 3.45f,
                    NoiseLp = 74,
                    AirFlow_Min = 12_000,
                    AirFlow_Max = 24_000,
                    Materials = materials,
                    NBlades = 5,
                    IP = "IP55",
                    Mount = mount,
                    MountSize = 835,
                    Holes = "-",
                    Weight = 35,
                    AStatPres = -0.000_000_6,
                    BStatPres = 0.005_9,
                    CStatPres = 169.42,
                    ATotalPres = -0.000_000_6,
                    BTotalPres = 0.005_9,
                    CTotalPres = 169.42,
                    AEffFactor = -0.000_388,
                    BEffFactor = 11.3495,
                    CEffFactor = -54_414.51,
                    APowerInput = 0.0,
                    BPowerInput = 0.0,
                    CPowerInput = 1900,
                    Tipology = fanTipology,
                    Builder = fanBuilder1,
                    Series = fanSeries,
                    Points01 = listFanPoint[0],
                    Points02 = listFanPoint[1],
                    Points03 = listFanPoint[2],
                    Points04 = listFanPoint[3],
                    Points05 = listFanPoint[4],
                    Points06 = listFanPoint[5],
                    Points07 = listFanPoint[6],
                    Points08 = listFanPoint[7],
                };
                CalcPoints(fanModel, listFanPoint, db, 20, 50, 1.2f);
                db.FanModelss.Add(fanModel);
                return fanModel;
            }

            FanModelsDB SeadItem3(ApplicationContext db, FanMaterialsDB materials, FanDirectionDB direction,
                FanTipologyDB fanTipology, FanMountDB mount, FanBuilderDB fanBuilder)
            {
                FanSeriesDB fanSeries = new FanSeriesDB { Name = "630 mm", Builder = fanBuilder };
                db.FanSeriess.Add(fanSeries);

                List<FanPointsDB> listFanPoint = new List<FanPointsDB>();
                AddPoints3(listFanPoint);

                FanModelsDB fanModel = new FanModelsDB
                {
                    Model = "S3G630-AC52-51",
                    Voltage = 230,
                    ConnectionOfMotor = 0,
                    Size1 = 630,
                    MotorPoles = 0,
                    Direction = direction,
                    Speed = 690,
                    Power = 0.18f,
                    Current = 1.2f,
                    NoiseLp = 55,
                    AirFlow_Min = 2_000,
                    AirFlow_Max = 9_000,
                    Materials = materials,
                    NBlades = 5,
                    IP = "IP54",
                    Mount = mount,
                    MountSize = 750,
                    Holes = "4x9,0",
                    Weight = 10.5f,
                    AStatPres = -0.000_001,
                    BStatPres = 0.000_3,
                    CStatPres = 74.65,
                    ATotalPres = -0.000_001,
                    BTotalPres = 0.000_3,
                    CTotalPres = 74.65,
                    AEffFactor = -0.003_53,
                    BEffFactor = 36.4927,
                    CEffFactor = -55074.89,
                    APowerInput = -0.000_001,
                    BPowerInput = 0.003_1,
                    CPowerInput = 191.98,
                    Tipology = fanTipology,
                    Builder = fanBuilder,
                    Series = fanSeries,
                    Points01 = listFanPoint[0],
                    Points02 = listFanPoint[1],
                    Points03 = listFanPoint[2],
                    Points04 = listFanPoint[3],
                    Points05 = listFanPoint[4],
                    Points06 = listFanPoint[5],
                    Points07 = listFanPoint[6],
                    Points08 = listFanPoint[7],
                };
                CalcPoints(fanModel, listFanPoint, db, 20, 50, 1.2f);
                db.FanModelss.Add(fanModel);
                return fanModel;
            }

            FanModelsDB SeadItem4(ApplicationContext db, FanMaterialsDB materials, FanTipologyDB fanTipology,
                FanMountDB mount, FanBuilderDB fanBuilder)
            {
                FanSeriesDB fanSeries = new FanSeriesDB { Name = "450 mm", Builder = fanBuilder };
                db.FanSeriess.Add(fanSeries);

                List<FanPointsDB> listFanPoint = new List<FanPointsDB>();
                AddPoints4(listFanPoint);

                FanDirectionDB direction = new FanDirectionDB
                {
                    Name = "B"
                };
                db.FanDirections.Add(direction);

                FanModelsDB fanModel = new FanModelsDB
                {
                    Model = "S4E450-BO09-02",
                    Voltage = 230,
                    ConnectionOfMotor = 0,
                    Size1 = 450,
                    MotorPoles = 4,
                    Direction = direction,
                    Speed = 490,
                    Power = 0.49f,
                    Current = 2.36f,
                    NoiseLp = 62,
                    AirFlow_Min = 3_000,
                    AirFlow_Max = 8_000,
                    Materials = materials,
                    NBlades = 5,
                    IP = "IP54",
                    Mount = mount,
                    MountSize = 560,
                    Holes = "4x9,0",
                    Weight = 9.8f,
                    AStatPres = -0.000_005,
                    BStatPres = 0.009_5,
                    CStatPres = 174.66,
                    ATotalPres = -0.000_005,
                    BTotalPres = 0.009_5,
                    CTotalPres = 174.66,
                    AEffFactor = -0.004_74,
                    BEffFactor = 43.1153,
                    CEffFactor = -67269.14,
                    APowerInput = -0.000_003,
                    BPowerInput = 0.008_3,
                    CPowerInput = 506.04,
                    Tipology = fanTipology,
                    Builder = fanBuilder,
                    Series = fanSeries,
                    Points01 = listFanPoint[0],
                    Points02 = listFanPoint[1],
                    Points03 = listFanPoint[2],
                    Points04 = listFanPoint[3],
                    Points05 = listFanPoint[4],
                    Points06 = listFanPoint[5],
                    Points07 = listFanPoint[6],
                };
                CalcPoints(fanModel, listFanPoint, db, 20, 50, 1.2f);
                db.FanModelss.Add(fanModel);
                return fanModel;
            }

            FanModelsDB SeadItem5(ApplicationContext db, FanMaterialsDB materials, FanDirectionDB direction,
               FanTipologyDB fanTipology, FanMountDB mount)
            {
                FanBuilderDB fanBuilder1 = new FanBuilderDB { Name = "Ziehl-Abegg" };
                db.FanBuilders.Add(fanBuilder1);

                FanSeriesDB fanSeries = new FanSeriesDB { Name = "910 mm", Builder = fanBuilder1 };
                db.FanSeriess.Add(fanSeries);

                List<FanPointsDB> listFanPoint = new List<FanPointsDB>();
                AddPoints5(listFanPoint);

                FanModelsDB fanModel = new FanModelsDB
                {
                    Model = "ZC091-SDQ.7Q.V5",
                    Voltage = 400,
                    ConnectionOfMotor = 2,
                    Size1 = 910,
                    MotorPoles = 6,
                    Direction = direction,
                    Speed = 840,
                    Power = 2.8f,
                    Current = 5.4f,
                    NoiseLp = 87,
                    AirFlow_Min = 20_000,
                    AirFlow_Max = 35_000,
                    Materials = materials,
                    NBlades = 5,
                    IP = "IP54",
                    Mount = mount,
                    MountSize = 1010,
                    Holes = "-",
                    Weight = 60,
                    AStatPres = -0.000_000_3,
                    BStatPres = 0.003_1,
                    CStatPres = 273.82,
                    ATotalPres = -0.000_000_3,
                    BTotalPres = 0.003_1,
                    CTotalPres = 273.82,
                    AEffFactor = -0.000_247,
                    BEffFactor = 10.8556,
                    CEffFactor = -76_768.72,
                    APowerInput = -0.000_002,
                    BPowerInput = 0.0_671,
                    CPowerInput = 2063.6,
                    Tipology = fanTipology,
                    Builder = fanBuilder1,
                    Series = fanSeries,
                    Points01 = listFanPoint[0],
                    Points02 = listFanPoint[1],
                    Points03 = listFanPoint[2],
                    Points04 = listFanPoint[3],
                    Points05 = listFanPoint[4],
                    Points06 = listFanPoint[5],
                    Points07 = listFanPoint[6],
                    Points08 = listFanPoint[7],
                    Points09 = listFanPoint[8],
                    Points10 = listFanPoint[9],
                };
                CalcPoints(fanModel, listFanPoint, db, 20, 50, 1.2f);
                db.FanModelss.Add(fanModel);
                return fanModel;
            }

            FanModelsDB SeadItem6(ApplicationContext db, FanDirectionDB direction,
                FanTipologyDB fanTipology, FanMountDB mount)
            {
                FanBuilderDB fanBuilder1 = new FanBuilderDB { Name = "Weiguang" };
                db.FanBuilders.Add(fanBuilder1);

                FanSeriesDB fanSeries = new FanSeriesDB { Name = "450 mm", Builder = fanBuilder1 };
                db.FanSeriess.Add(fanSeries);

                List<FanPointsDB> listFanPoint = new List<FanPointsDB>();
                AddPoints6(listFanPoint);

                FanMaterialsDB materials = new FanMaterialsDB
                {
                    Name = "Steel"
                };
                db.FanMaterialss.Add(materials);

                FanModelsDB fanModel = new FanModelsDB
                {
                    Model = "YWF4E-450S-137/35-G",
                    Voltage = 230,
                    ConnectionOfMotor = 0,
                    Size1 = 450,
                    MotorPoles = 4,
                    Direction = direction,
                    Speed = 1380,
                    Power = 0.37f,
                    Current = 1.70f,
                    NoiseLp = 70,
                    AirFlow_Min = 3_500,
                    AirFlow_Max = 6_000,
                    Materials = materials,
                    NBlades = 5,
                    IP = "IP54",
                    Mount = mount,
                    MountSize = 522,
                    Holes = "4x9,5",
                    Weight = 8.5f,
                    AStatPres = -7.00e-06,
                    BStatPres = 0.015_1,
                    CStatPres = 153.16,
                    ATotalPres = -7.00e-06,
                    BTotalPres = 0.015_1,
                    CTotalPres = 153.16,
                    AEffFactor = -6.42e-03,
                    BEffFactor = 47.6718,
                    CEffFactor = -57_025.96,
                    APowerInput = -4.00e-19,
                    BPowerInput = 0.0,
                    CPowerInput = 370,
                    Tipology = fanTipology,
                    Builder = fanBuilder1,
                    Series = fanSeries,
                    Points01 = listFanPoint[0],
                    Points02 = listFanPoint[1],
                    Points03 = listFanPoint[2],
                    Points04 = listFanPoint[3],
                    Points05 = listFanPoint[4],
                    Points06 = listFanPoint[5],
                    Points07 = listFanPoint[6],
                    Points08 = listFanPoint[7],
                };
                CalcPoints(fanModel, listFanPoint, db, 20, 50, 1.2f);
                db.FanModelss.Add(fanModel);
                return fanModel;
            }

            void AddPoints1(List<FanPointsDB> listFanPoint)
            {
                // 01
                FanPointsDB fanPoints = new FanPointsDB
                {
                    Voltage = 381.4f,
                    Frequency = 50,
                    Speed = 965,
                    Power = 3640,
                    Current = 6.98f,
                    Airflow = 18336,
                    StatPressure = 299,
                    DynamicPressure = 33,
                };
                listFanPoint.Add(fanPoints);
                // 02
                fanPoints = new FanPointsDB
                {
                    Voltage = 379.5f,
                    Frequency = 50,
                    Speed = 965,
                    Power = 3526f,
                    Current = 6.82f,
                    Airflow = 19525,
                    StatPressure = 281,
                    DynamicPressure = 37,
                };
                listFanPoint.Add(fanPoints);
                // 03
                fanPoints = new FanPointsDB
                {
                    Voltage = 380.1f,
                    Frequency = 50,
                    Speed = 966,
                    Power = 3559f,
                    Current = 6.87f,
                    Airflow = 20303,
                    StatPressure = 261,
                    DynamicPressure = 40,
                };
                listFanPoint.Add(fanPoints);
                // 04
                fanPoints = new FanPointsDB
                {
                    Voltage = 380f,
                    Frequency = 50,
                    Speed = 967f,
                    Power = 3447f,
                    Current = 6.7f,
                    Airflow = 21449,
                    StatPressure = 239,
                    DynamicPressure = 45,
                };
                listFanPoint.Add(fanPoints);
                // 05
                fanPoints = new FanPointsDB
                {
                    Voltage = 379.8f,
                    Frequency = 50,
                    Speed = 968f,
                    Power = 3430f,
                    Current = 6.68f,
                    Airflow = 21854,
                    StatPressure = 220,
                    DynamicPressure = 47,
                };
                listFanPoint.Add(fanPoints);
                // 06
                fanPoints = new FanPointsDB
                {
                    Voltage = 382.4f,
                    Frequency = 50,
                    Speed = 970,
                    Power = 3354,
                    Current = 6.58f,
                    Airflow = 22862,
                    StatPressure = 200,
                    DynamicPressure = 51,
                };
                listFanPoint.Add(fanPoints);
                // 07
                fanPoints = new FanPointsDB
                {
                    Voltage = 384.8f,
                    Frequency = 50,
                    Speed = 973,
                    Power = 3238,
                    Current = 6.43f,
                    Airflow = 23768,
                    StatPressure = 180,
                    DynamicPressure = 55,
                };
                listFanPoint.Add(fanPoints);
                // 08
                fanPoints = new FanPointsDB
                {
                    Voltage = 382.1f,
                    Frequency = 50,
                    Speed = 973,
                    Power = 3146,
                    Current = 6.30f,
                    Airflow = 24607,
                    StatPressure = 160,
                    DynamicPressure = 59,
                };
                listFanPoint.Add(fanPoints);
                // 09
                fanPoints = new FanPointsDB
                {
                    Voltage = 382.3f,
                    Frequency = 50,
                    Speed = 974,
                    Power = 3109f,
                    Current = 6.21f,
                    Airflow = 25630,
                    StatPressure = 140,
                    DynamicPressure = 64,
                };
                listFanPoint.Add(fanPoints);
                // 10
                fanPoints = new FanPointsDB
                {
                    Voltage = 381.6f,
                    Frequency = 50,
                    Speed = 974,
                    Power = 3016,
                    Current = 6.09f,
                    Airflow = 26252,
                    StatPressure = 119,
                    DynamicPressure = 68,
                };
                listFanPoint.Add(fanPoints);
                // 11
                fanPoints = new FanPointsDB
                {
                    Voltage = 381.1f,
                    Frequency = 50,
                    Speed = 976,
                    Power = 2883,
                    Current = 5.91f,
                    Airflow = 26772,
                    StatPressure = 101,
                    DynamicPressure = 70,
                };
                listFanPoint.Add(fanPoints);
                // 12
                fanPoints = new FanPointsDB
                {
                    Voltage = 380.2f,
                    Frequency = 50,
                    Speed = 976,
                    Power = 2767,
                    Current = 5.75f,
                    Airflow = 27653,
                    StatPressure = 82,
                    DynamicPressure = 75,
                };
                listFanPoint.Add(fanPoints);
                // 13
                fanPoints = new FanPointsDB
                {
                    Voltage = 382.4f,
                    Frequency = 50,
                    Speed = 978,
                    Power = 2638,
                    Current = 5.58f,
                    Airflow = 28046,
                    StatPressure = 60,
                    DynamicPressure = 77,
                };
                listFanPoint.Add(fanPoints);
                // 14
                fanPoints = new FanPointsDB
                {
                    Voltage = 381.5f,
                    Frequency = 50,
                    Speed = 980,
                    Power = 2599,
                    Current = 5.54f,
                    Airflow = 28988,
                    StatPressure = 40,
                    DynamicPressure = 82,
                };
                listFanPoint.Add(fanPoints);
                // 15
                fanPoints = new FanPointsDB
                {
                    Voltage = 381.9f,
                    Frequency = 50,
                    Speed = 982,
                    Power = 2370,
                    Current = 5.38f,
                    Airflow = 29236,
                    StatPressure = 22,
                    DynamicPressure = 84,
                };
                listFanPoint.Add(fanPoints);
                // 16
                fanPoints = new FanPointsDB
                {
                    Voltage = 381.1f,
                    Frequency = 50,
                    Speed = 982,
                    Power = 2370,
                    Current = 5.23f,
                    Airflow = 29950,
                    StatPressure = 1,
                    DynamicPressure = 88,
                };
                listFanPoint.Add(fanPoints);
            }

            void AddPoints2(List<FanPointsDB> listFanPoint)
            {
                // 01
                FanPointsDB fanPoints = new FanPointsDB
                {
                    Voltage = 380,
                    Frequency = 50,
                    Speed = 890,
                    Power = 1900,
                    Current = 3.45f,
                    Airflow = 14200,
                    StatPressure = 140,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 02
                fanPoints = new FanPointsDB
                {
                    Voltage = 380,
                    Frequency = 50,
                    Speed = 890,
                    Power = 1900,
                    Current = 3.45f,
                    Airflow = 15700,
                    StatPressure = 120,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 03
                fanPoints = new FanPointsDB
                {
                    Voltage = 380,
                    Frequency = 50,
                    Speed = 890,
                    Power = 1900,
                    Current = 3.45f,
                    Airflow = 17100,
                    StatPressure = 100,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 04
                fanPoints = new FanPointsDB
                {
                    Voltage = 380,
                    Frequency = 50,
                    Speed = 890,
                    Power = 1900,
                    Current = 3.45f,
                    Airflow = 18800,
                    StatPressure = 80,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 05
                fanPoints = new FanPointsDB
                {
                    Voltage = 380,
                    Frequency = 50,
                    Speed = 890,
                    Power = 1900,
                    Current = 3.45f,
                    Airflow = 20100,
                    StatPressure = 60,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 06
                fanPoints = new FanPointsDB
                {
                    Voltage = 380,
                    Frequency = 50,
                    Speed = 890,
                    Power = 1900,
                    Current = 3.45f,
                    Airflow = 21200,
                    StatPressure = 40,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 07
                fanPoints = new FanPointsDB
                {
                    Voltage = 380,
                    Frequency = 50,
                    Speed = 890,
                    Power = 1900,
                    Current = 3.45f,
                    Airflow = 22000,
                    StatPressure = 20,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 08
                fanPoints = new FanPointsDB
                {
                    Voltage = 380,
                    Frequency = 50,
                    Speed = 890,
                    Power = 1900,
                    Current = 3.45f,
                    Airflow = 23100,
                    StatPressure = 1,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
            }

            void AddPoints3(List<FanPointsDB> listFanPoint)
            {
                // 01
                FanPointsDB fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 750,
                    Power = 141,
                    Current = 0.9f,
                    Airflow = 8395,
                    StatPressure = 1,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 02
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 740,
                    Power = 149,
                    Current = 0.955f,
                    Airflow = 7800,
                    StatPressure = 10,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 03
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 730,
                    Power = 158,
                    Current = 1.01f,
                    Airflow = 7220,
                    StatPressure = 20,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 04
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 720,
                    Power = 166,
                    Current = 1.055f,
                    Airflow = 6570,
                    StatPressure = 30,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 05
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 710,
                    Power = 174,
                    Current = 1.1f,
                    Airflow = 5800,
                    StatPressure = 40,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 06
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 700,
                    Power = 179,
                    Current = 1.15f,
                    Airflow = 4900,
                    StatPressure = 50,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 07
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 690,
                    Power = 184,
                    Current = 1.2f,
                    Airflow = 4420,
                    StatPressure = 55,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 08
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 680,
                    Power = 189,
                    Current = 1.25f,
                    Airflow = 3800,
                    StatPressure = 60,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
            }

            void AddPoints4(List<FanPointsDB> listFanPoint)
            {
                // 01
                FanPointsDB fanPoints = new FanPointsDB
                {
                    Voltage = 230f,
                    Frequency = 50,
                    Speed = 1350,
                    Power = 430,
                    Current = 2.1f,
                    Airflow = 7070,
                    StatPressure = 1,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 02
                fanPoints = new FanPointsDB
                {
                    Voltage = 230f,
                    Frequency = 50,
                    Speed = 1345,
                    Power = 438.5f,
                    Current = 2.13f,
                    Airflow = 6700,
                    StatPressure = 20,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 03
                fanPoints = new FanPointsDB
                {
                    Voltage = 230f,
                    Frequency = 50,
                    Speed = 1340,
                    Power = 447f,
                    Current = 2.16f,
                    Airflow = 6330,
                    StatPressure = 40,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 04
                fanPoints = new FanPointsDB
                {
                    Voltage = 230f,
                    Frequency = 50,
                    Speed = 1332.5f,
                    Power = 458.5f,
                    Current = 2.21f,
                    Airflow = 5950,
                    StatPressure = 60,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 05
                fanPoints = new FanPointsDB
                {
                    Voltage = 230f,
                    Frequency = 50,
                    Speed = 1325f,
                    Power = 470f,
                    Current = 2.26f,
                    Airflow = 5520,
                    StatPressure = 80,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 06
                fanPoints = new FanPointsDB
                {
                    Voltage = 230f,
                    Frequency = 50,
                    Speed = 1317.5f,
                    Power = 480f,
                    Current = 2.31f,
                    Airflow = 5050,
                    StatPressure = 100,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 07
                fanPoints = new FanPointsDB
                {
                    Voltage = 230f,
                    Frequency = 50,
                    Speed = 1310f,
                    Power = 490f,
                    Current = 2.36f,
                    Airflow = 4315,
                    StatPressure = 125,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
            }

            void AddPoints5(List<FanPointsDB> listFanPoint)
            {
                // 01
                FanPointsDB fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2438,
                    Current = 4.99f,
                    Airflow = 34745,
                    StatPressure = 1,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 02
                fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2520,
                    Current = 5.09f,
                    Airflow = 32994,
                    StatPressure = 36,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 03
                fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2590,
                    Current = 5.18f,
                    Airflow = 31143,
                    StatPressure = 67,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 04
                fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2630,
                    Current = 5.24f,
                    Airflow = 29598,
                    StatPressure = 91,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 05
                fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2673,
                    Current = 5.29f,
                    Airflow = 27961,
                    StatPressure = 114,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 06
                fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2699,
                    Current = 5.33f,
                    Airflow = 26479,
                    StatPressure = 133,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 07
                fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2725,
                    Current = 5.36f,
                    Airflow = 25105,
                    StatPressure = 153,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 08
                fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2753,
                    Current = 5.40f,
                    Airflow = 23721,
                    StatPressure = 173,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 09
                fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2761,
                    Current = 5.41f,
                    Airflow = 22140,
                    StatPressure = 190,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 10
                fanPoints = new FanPointsDB
                {
                    Voltage = 400,
                    Frequency = 50,
                    Speed = 840,
                    Power = 2755,
                    Current = 5.40f,
                    Airflow = 20321,
                    StatPressure = 206,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
            }

            void AddPoints6(List<FanPointsDB> listFanPoint)
            {
                // 01
                FanPointsDB fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 13850,
                    Power = 370,
                    Current = 1.7f,
                    Airflow = 5900,
                    StatPressure = 1,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 02
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 13850,
                    Power = 370,
                    Current = 1.7f,
                    Airflow = 5600,
                    StatPressure = 20,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 03
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 13850,
                    Power = 370,
                    Current = 1.7f,
                    Airflow = 5300,
                    StatPressure = 40,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 04
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 13850,
                    Power = 370,
                    Current = 1.7f,
                    Airflow = 4900,
                    StatPressure = 60,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 05
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 13850,
                    Power = 370,
                    Current = 1.7f,
                    Airflow = 4500,
                    StatPressure = 80,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 06
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 13850,
                    Power = 370,
                    Current = 1.7f,
                    Airflow = 4300,
                    StatPressure = 90,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 07
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 13850,
                    Power = 370,
                    Current = 1.7f,
                    Airflow = 4100,
                    StatPressure = 100,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
                // 08
                fanPoints = new FanPointsDB
                {
                    Voltage = 230,
                    Frequency = 50,
                    Speed = 13850,
                    Power = 370,
                    Current = 1.7f,
                    Airflow = 3800,
                    StatPressure = 110,
                    DynamicPressure = 0,
                };
                listFanPoint.Add(fanPoints);
            }
        }

        /// <summary>
        /// Обновляем серии (Series)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="builderId"></param>
        /// <param name="Series"></param>
        /// <returns></returns>
        private ObservableCollection<string> Modify(ApplicationContext db, int builderId, ref ObservableCollection<string> Series)
        {
            Series.Clear();
            Series.Add(Calculation.TO.Main.Properties.Resources.FanAll);
            foreach (var series in db.FanSeriess.ToList())
            {
                if (builderId > 0)
                {
                    if (series.BuilderId == builderId)
                    {
                        Series.Add(series.Name);
                    }
                }
                else
                {
                    Series.Add(series.Name);
                }
            }
            return Series;
        }

        /// <summary>
        /// проверяем производителя
        /// </summary>
        /// <param name="selectedBuilder"></param>
        /// <param name="db"></param>
        /// <param name="fan"></param>
        /// <param name="fAdd"></param>
        /// <returns></returns>
        private static bool IsAddBuilder(string selectedBuilder, ApplicationContext db, FanModelsDB fan, bool fAdd, ref int id)
        {
            if (selectedBuilder != Calculation.TO.Main.Properties.Resources.FanAll)
            {
                foreach (var builder in db.FanBuilders.ToList())
                {
                    if (selectedBuilder == builder.Name)
                    {
                        id = builder.Id;
                        break;
                    }
                }
            }
            if (fan.Builder.Id != id && id > 0)
                fAdd = false;
            return fAdd;
        }

        /// <summary>
        /// Проверяет типологию
        /// </summary>
        /// <param name="FanTip1"></param>
        /// <param name="FanTip2"></param>
        /// <param name="FanTip3"></param>
        /// <param name="FanTip4"></param>
        /// <param name="fan"></param>
        /// <param name="fAdd"></param>
        /// <returns></returns>
        private static bool IsAdd(bool FanTip1, bool FanTip2, bool FanTip3, bool FanTip4, FanModelsDB fan, bool fAdd)
        {
            if (fan.Tipology.Id == 1)
            {
                if (FanTip1)
                {
                    fAdd = true;
                }
            }
            else if (fan.Tipology.Id == 2)
            {
                if (FanTip2)
                {
                    fAdd = true;
                }
            }
            else if (fan.Tipology.Id == 3)
            {
                if (FanTip3)
                {
                    fAdd = true;
                }
            }
            else if (fan.Tipology.Id == 4)
            {
                if (FanTip4)
                {
                    fAdd = true;
                }
            }
            return fAdd;
        }

        private FanPointsDB GetPoints(SelectedFans fan, ApplicationContext db)
        {
            FanPointsDB points = null;
            double maxAirFlow = fan.AirFlow;
            double minAirFlow = 0;
            GetMinAirFlow(fan.fans.Points01, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points02, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points03, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points04, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points05, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points06, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points07, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points08, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points09, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points10, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points11, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points12, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points13, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points14, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points15, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points16, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points17, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points18, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points19, ref minAirFlow, maxAirFlow, ref points);
            GetMinAirFlow(fan.fans.Points20, ref minAirFlow, maxAirFlow, ref points);
            return points;
        }

        private void GetMinAirFlow(FanPointsDB fanPoints, ref double minAirFlow, double maxAirFlow, ref FanPointsDB points)
        {
            if (fanPoints != null && fanPoints.Airflow < maxAirFlow && fanPoints.Airflow > minAirFlow)
            {
                minAirFlow = fanPoints.Airflow;
                points = fanPoints;
            }
        }

        /// <summary>
        /// Получение максимального давления
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private double GetMaxPressure(SelectedFans fan, double presDropDry, double airFlow, ApplicationContext db)
        {
            double maxPressure = Double.MaxValue;
            double maxAirFlow = 0;
            if (fan == null)
                return 0.0;
            if (fan.fans.Points01 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points01.Airflow, fan.fans.Points01.StatPressure);
            if (fan.fans.Points02 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points02.Airflow, fan.fans.Points02.StatPressure);
            if (fan.fans.Points03 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points03.Airflow, fan.fans.Points03.StatPressure);
            if (fan.fans.Points04 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points04.Airflow, fan.fans.Points04.StatPressure);
            if (fan.fans.Points05 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points05.Airflow, fan.fans.Points05.StatPressure);
            if (fan.fans.Points06 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points06.Airflow, fan.fans.Points06.StatPressure);
            if (fan.fans.Points07 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points07.Airflow, fan.fans.Points07.StatPressure);
            if (fan.fans.Points08 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points08.Airflow, fan.fans.Points08.StatPressure);
            if (fan.fans.Points09 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points09.Airflow, fan.fans.Points09.StatPressure);
            if (fan.fans.Points10 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points10.Airflow, fan.fans.Points10.StatPressure);
            if (fan.fans.Points11 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points11.Airflow, fan.fans.Points11.StatPressure);
            if (fan.fans.Points12 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points12.Airflow, fan.fans.Points12.StatPressure);
            if (fan.fans.Points13 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points13.Airflow, fan.fans.Points13.StatPressure);
            if (fan.fans.Points14 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points14.Airflow, fan.fans.Points14.StatPressure);
            if (fan.fans.Points15 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points15.Airflow, fan.fans.Points15.StatPressure);
            if (fan.fans.Points16 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points16.Airflow, fan.fans.Points16.StatPressure);
            if (fan.fans.Points17 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points17.Airflow, fan.fans.Points17.StatPressure);
            if (fan.fans.Points18 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points18.Airflow, fan.fans.Points18.StatPressure);
            if (fan.fans.Points19 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points19.Airflow, fan.fans.Points19.StatPressure);
            if (fan.fans.Points20 != null)
                CheckValue(fan, presDropDry, airFlow, ref maxAirFlow, ref maxPressure, fan.fans.Points20.Airflow, fan.fans.Points20.StatPressure);
            if (maxPressure != Double.MaxValue)
                return maxPressure;
            else return 0.0;
        }

        private void CheckValue(SelectedFans fan, double presDropDry, double airFlow, ref double maxAirFlow,
            ref double maxPressure, double fanAirflow, double fanStatPressure)
        {
            if (fanAirflow > airFlow)
            {
                if (fanStatPressure > presDropDry)
                {
                    if (fanAirflow > maxAirFlow)
                    {
                        maxAirFlow = fanAirflow;
                        maxPressure = fanStatPressure;
                        fan.AirFlow = fanAirflow;
                        fan.AirFlowDifference = fanAirflow - airFlow;
                    }
                }
            }
        }

        /// <summary>
        /// выбирает вентиляторы, у которых массовый расход больше
        /// </summary>
        /// <param name="airFlow"></param>
        /// <param name="db"></param>
        /// <param name="fans"></param>
        private void AirFlowAbove(double airFlow, ApplicationContext db)
        {
            foreach (var fan in db.FanModelss.ToList())
            {
                if (fan.AirFlow_Max > airFlow)
                {
                    fans.AddLast(new SelectedFans
                    {
                        fans = fan,
                        MaxAirFlowDifference = fan.AirFlow_Max - airFlow,
                    });
                }
            }
        }

        /// <summary>
        /// Проверка на максимальное давление
        /// </summary>
        /// <param name="presDropDry"></param>
        /// <param name="db"></param>
        private void CheckMaxPressure(double presDropDry, double airFlow, ApplicationContext db)
        {
            var node = fans.First;
            bool next = true;
            while (node != null)
            {
                var fan = node;
                if (fan != null)
                {
                    double pres = node.Value.Pressure = GetMaxPressure(fan.Value, presDropDry, airFlow, db);
                    if (presDropDry > pres)
                    {
                        node = fan.Next;
                        fans.Remove(fan);
                        next = false;
                    }
                }
                if (next)
                    node = fan.Next;
                else
                    next = true;
            }
        }

        /// <summary>
        /// Проверка на давление
        /// </summary>
        /// <param name="airFlow"></param>
        /// <param name="presDropDry"></param>
        /// <param name="db"></param>
        private void CheckPressure(double presDropDry, double airFlow, ApplicationContext db)
        {
            FanPointsDB points = null;
            double vol1 = 0;
            double pres1 = 0;
            double vol2 = 0;
            double pres2 = 0;
            double vol = 0;
            var node = fans.First;
            bool next = true;
            while (node != null)
            {
                var fan = node;
                if (fan != null)
                {
                    points = GetPoints(fan.Value, db);
                    pres2 = node.Value.Pressure;
                    vol2 = node.Value.AirFlow;
                    pres1 = points.StatPressure;
                    vol1 = points.Airflow;
                    vol = GetInterpolationAirFlow(presDropDry, vol1, pres1, vol2, pres2);
                    if (vol > airFlow)
                    {
                        fan.Value.AirFlow = vol;
                        fan.Value.AirFlowDifference = vol - airFlow;
                    }
                    else
                    {
                        node = fan.Next;
                        fans.Remove(fan);
                        next = false;
                    }
                    break;
                }
                if (next)
                    node = fan.Next;
                else
                    next = true;
            }
        }

        /// <summary>
        /// Возврат давления интерполяцией
        /// </summary>
        /// <param name="presDropDry"></param>
        /// <param name="vol1"></param>
        /// <param name="pres1"></param>
        /// <param name="vol2"></param>
        /// <param name="pres2"></param>
        /// <returns></returns>
        private double GetInterpolationAirFlow(double presDropDry, double vol1, double pres1, double vol2, double pres2)
        {
            // получаем коэфффициенты прямой
            double k = (pres2 - pres1) / (vol2 - vol1);
            double b = pres1 - k * vol1;
            return (presDropDry - b) / k;
        }

        /// <summary>
        /// Возврат списка имён вентиляторов
        /// </summary>
        /// <param name="fans"></param>
        /// <returns></returns>
        private IList<FanDTO> GetFanNames(LinkedList<SelectedFans> fans)
        {
            IList<FanDTO> fanDTOs = new List<FanDTO>();
            foreach (var item in fans)
            {
                fanDTOs.Add(MapperFanModelsToFanDTO.FanModelsToFanDTO(item.fans));
            }
            return fanDTOs;
        }

        /// <summary>
        /// выбор лучших вариантов вентиляторов
        /// </summary>
        /// <param name="airFlow"></param>
        /// <param name="presDropDry"></param>
        /// <param name="db"></param>
        private void SelectBest(double presDropDry, double airFlow, ApplicationContext db)
        {
            if (fans.Count <= 1) return;

            // создание списка "разностей давления"
            SortedSet<double> pres = new SortedSet<double>();
            foreach (var fan in fans)
            {
                pres.Add(fan.AirFlowDifference);
            }

            // уменьшение списка "разностей давления" до 5 элементов
            int col = fans.Count;
            if (col > 5) col = 5;
            List<double> presL = new List<double>();
            for (int i = 0; i < col; i++)
            {
                presL.Add(pres.ElementAt(i));
            }

            // уменьшение списка "подобранных вентиляторов" до 5 элементов
            var node = fans.First;
            bool next = true;
            while (node != null)
            {
                var fan = node;
                if (fan != null)
                {
                    if (!presL.Contains(fan.Value.AirFlowDifference))
                    {
                        node = fan.Next;
                        fans.Remove(fan);
                        next = false;
                    }
                }
                if (next)
                    node = fan.Next;
                else
                    next = true;
            }
            LinkedList<SelectedFans> lFans = new LinkedList<SelectedFans>();
            foreach (var item in presL)
            {
                foreach (var fan in fans)
                {
                    if (fan.AirFlowDifference == item)
                    {
                        lFans.AddLast(fan);
                        break;
                    }
                }
            }
            fans = lFans;
        }

        #endregion
    }
}
