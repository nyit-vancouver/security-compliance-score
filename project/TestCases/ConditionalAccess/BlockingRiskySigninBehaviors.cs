using System.Runtime;
using Microsoft.Graph;

namespace TestCases.ConditionalAccess
{
    class BlockingRiskySigninBehaviors : ITestCase
    {
        public string name
        {
            get
            {
                return "Conditional Access - Blocking risky sign-in behaviors";
            }
        }

        public string solution
        {
            get
            {
                return "A policy that blocks risky sign-ins";
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
                /*Jenny-2022-policy of blocking risky behavious
                  for future work, change fix string to variable input that can be configured by user
                */
                if (policy.DisplayName == "Blocking risky sign-in behaviors")//the name should be configured instead of hard code
                {
                    result = policy.State == ConditionalAccessPolicyState.Enabled;
                }
            }
            return result;
        }
    }
}
