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
                /*Jenny-2022-policy of blocking legacy authentication
                  for future work, change fix string to variable input that can be configured by user
                */
                if(policy.DisplayName == "Block legacy authentication")//the name should be configured instead of hard code
                {
                    result = policy.State == ConditionalAccessPolicyState.Enabled;
                }
            }
            return result;
        }
    }
}
