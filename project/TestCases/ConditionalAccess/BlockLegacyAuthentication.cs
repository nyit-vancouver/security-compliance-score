using System.Runtime;
using Microsoft.Graph;

namespace TestCases.ConditionalAccess
{
    class BlockLegacyAuthentication : ITestCase
    {
        public string name
        {
            get {
                return "Conditional Access - Block legacy authentication";
            }
        }

        public string solution
        {
            get
            {
                return "A policy that restricts Legacy Authentication must be enabled";
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

                if(policy.DisplayName == "Block legacy authentication")
                {
                    result = policy.State == ConditionalAccessPolicyState.Enabled;
                }
            }
            return result;
        }
    }
}