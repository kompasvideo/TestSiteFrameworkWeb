using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Condensator
{
    public interface IGetPropertiesCXService
    {
        /// <summary>
        /// Получение списка входных параметров
        /// </summary>
        Task<object> GetPropertiesCX();
    }
}
