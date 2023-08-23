using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants
{
    sealed internal class RefrigerantFactoryR407C : IRefrigerantFactory
    {
        public IRefrigerant GetRefrigerant()
        {
            return new RefrigerantR407C();
        }
    }
}
