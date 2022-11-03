using System.Runtime;
using Microsoft.Graph;
using System.Linq;

namespace TestCases.Policies
{
    class SecurityDefaults : ITestCase
    {
        public string name
        {
            get
            {
                return "Policies - Security Defaults";
            }
        }

        public string solution
        {
            get
            {
                return "Security Defaults must be enabled";
            }
        }

        public async Task<bool> Test(GraphServiceClient appClient)
        {
            var result = await appClient.Policies.IdentitySecurityDefaultsEnforcementPolicy
            .Request()
            .GetAsync();

            return result.IsEnabled ?? false;
        }
    }
}