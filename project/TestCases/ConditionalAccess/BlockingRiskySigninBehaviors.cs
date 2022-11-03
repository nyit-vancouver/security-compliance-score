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
                if (policy.DisplayName == "Blocking risky sign-in behaviors")
                {
                    result = policy.State == ConditionalAccessPolicyState.Enabled;
                }
            }
            return result;
        }
    }
}