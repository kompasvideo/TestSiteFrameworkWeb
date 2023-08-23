using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Cooler
{
    public interface IGetPropertiesCWService
    {
        /// <summary>
        /// Получение списка входных параметров
        /// </summary>
        Task<object> GetPropertiesCW();
    }
}
