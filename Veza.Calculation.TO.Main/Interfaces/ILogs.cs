using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veza.HeatExchanger.Interfaces
{
    /// <summary>
    /// Интерфейс к классу Logs
    /// </summary>
    public interface ILogs
    {
        NLog.Logger Logger { get; set; }
    }
}
