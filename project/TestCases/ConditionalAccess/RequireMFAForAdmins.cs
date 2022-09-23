using System.Runtime;
using Microsoft.Graph;

namespace TestCases.ConditionalAccess
{
    class RequireMFAForAdmins : ITestCase
    {
        string _name = "Conditional Access - Require MFA for Admins";

        public string name()
        {
            return _name;
        }

        public async Task<bool> Test(GraphServiceClient appClient)
        {
            var policies = await appClient.Policies.ConditionalAccessPolicies
            .Request()
            .GetAsync();

            var result = false;
            foreach (var policy in policies)
            {
                if(policy.DisplayName == "Require MFA for admins")
                {
                    result = policy.State == ConditionalAccessPolicyState.Enabled;
                }
            }
            return result;
        }
    }
}