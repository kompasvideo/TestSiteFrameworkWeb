using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Evaporater
{
    public interface IGetPropertiesDXService
    {
        /// <summary>
        /// Получение списка входных параметров
        /// </summary>
        Task<object> GetPropertiesDX();
    }
}
