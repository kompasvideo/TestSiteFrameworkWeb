using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants
{
    sealed internal class RefrigerantFactoryR134a : IRefrigerantFactory
    {
        public IRefrigerant GetRefrigerant()
    {
        return new RefrigerantR134a();
    }
}
}