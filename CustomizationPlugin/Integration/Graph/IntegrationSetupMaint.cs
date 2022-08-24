using PX.Data;

namespace Integration
{
    public class IntegrationSetupMaint : PXGraph<IntegrationSetupMaint, IntegrationSetup>
    {
        public PXSelect<IntegrationSetup> IntegrationSetup;
    }
}