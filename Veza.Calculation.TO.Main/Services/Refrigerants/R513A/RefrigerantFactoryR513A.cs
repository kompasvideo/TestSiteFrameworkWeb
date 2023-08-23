using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants.R513A
{
    sealed internal class RefrigerantFactoryR513A : IRefrigerantFactory
    {
        public IRefrigerant GetRefrigerant()
        {
            return new RefrigerantR513A();
        }
    }
}
