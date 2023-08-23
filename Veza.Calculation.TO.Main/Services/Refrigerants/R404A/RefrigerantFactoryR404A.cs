using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerant
{
    sealed internal class RefrigerantFactoryR404A : IRefrigerantFactory
    {
        public IRefrigerant GetRefrigerant()
        {
            return new RefrigerantR404A();
        }
    }
}
