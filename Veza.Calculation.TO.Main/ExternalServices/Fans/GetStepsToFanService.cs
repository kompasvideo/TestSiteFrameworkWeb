using System.Threading.Tasks;

namespace Veza.Calculation.TO.Main.ExternalServices.Fans
{
    public class GetStepsToFanService : IGetStepsToFanService
    {

        public GetStepsToFanService()
        {
        }

        /// <summary>
        /// Получить вентилятор для редактирования
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetSteps(string name)
        {
            //return await Task.Run(() =>
            //{
            //    return calcTO.GetFanService().GetSteps(name);
            //});
            object ret = null;
            return await Task.Run(() => ret);
        }
    }
}
