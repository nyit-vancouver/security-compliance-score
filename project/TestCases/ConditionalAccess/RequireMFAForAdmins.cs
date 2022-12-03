using System.Runtime;
using Microsoft.Graph;

namespace TestCases.ConditionalAccess
{
    class RequireMFAForAdmins : ITestCase
    {
        public string name
        {
            get
            {
                return "Conditional Access - Require MFA for Admins";
            }
        }

        public string solution
        {
            get
            {
                return "A policy that enforces MFA for Admins must be enabled";
            }
        }

        public async Task<bool> Test(GraphServiceClient appClient)
        {
            var policies = await appClient.Policies.ConditionalAccessPolicies
            .Request()
            .GetAsync();

            var result = false;
            foreach (var policy in policies)
            {
                /*Jenny-2022-policy of MFA for Admins
                  for future work, change fix string to variable input that can be configured by user
                */
                if (policy.DisplayName == "Require MFA for admins")//the name should be configured instead of hard code
                {
                    result = policy.State == ConditionalAccessPolicyState.Enabled;
                }
            }
            return result;
        }
    }
}
