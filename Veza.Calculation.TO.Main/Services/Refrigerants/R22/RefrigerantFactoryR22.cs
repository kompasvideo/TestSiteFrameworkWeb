using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants
{
    sealed internal class RefrigerantFactoryR22 : IRefrigerantFactory
    {
        public IRefrigerant GetRefrigerant()
        {
            return new RefrigerantR22();
        }
    }
}
