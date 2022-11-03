using System.Runtime;
using Microsoft.Graph;
using System.Linq;

namespace TestCases.Policies
{
    class SelfServicePasswordReset : ITestCase
    {
        public string name
        {
            get
            {
                return "Policies - Self Service Password Reset";
            }
        }

        public string solution
        {
            get
            {
                return "Self Service Password Reset must be enabled";
            }
        }

        public async Task<bool> Test(GraphServiceClient appClient)
        {
            var result = await appClient.Policies.AuthorizationPolicy
            .Request()
            .GetAsync();

            return result.AllowedToUseSSPR ?? false;
        }
    }
}