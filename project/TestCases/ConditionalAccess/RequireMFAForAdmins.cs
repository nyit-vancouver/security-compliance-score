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
                if (policy.DisplayName == "Require MFA for admins")
                {
                    result = policy.State == ConditionalAccessPolicyState.Enabled;
                }
            }
            return result;
        }
    }
}