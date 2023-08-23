using Veza.HeatExchanger.Models;

namespace Veza.HeatExchanger.Interfaces
{
    public interface IFinThk
    {
        /// <summary>
        /// Сохраняем толщину трубки для проверки с ней выходных результатов
        /// если указать её в входных параметрах получаем ошиибку
        /// 8: Fin thickness wrong
        /// </summary>
        /// <param name="fins"></param>
        void SetFinThickness(string fins);

        /// <summary>
        /// Проверяем толщину трубки из подобранных результатов с заданной
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        bool IsFinThickness(OutViewLines line);
    }
}
