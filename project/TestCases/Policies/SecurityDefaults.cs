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
                //Jenny-2022-change return output alert
                return "Security Defaults is recommended to be enabled";
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
