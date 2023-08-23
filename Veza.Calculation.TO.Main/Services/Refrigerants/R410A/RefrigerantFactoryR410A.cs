using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants.R410A
{
    sealed internal class RefrigerantFactoryR410A : IRefrigerantFactory
    {
        public IRefrigerant GetRefrigerant()
        {
            return new RefrigerantR410A();
        }
    }
}
