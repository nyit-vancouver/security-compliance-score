using System.Runtime;
using Microsoft.Graph;

namespace TestCases.SecureScores
{
    class RoleOverlap : ITestCase
    {
        private Settings _settings;

        public RoleOverlap(Settings settings)
        {
            _settings = settings;
        }

        public string name
        {
            get
            {
                return "Secure scores - Role Overlap";
            }
        }

        private string _solution = "Ensure that your administrators can accomplish their work with the least amount of privilege assigned to their account. Assigning users roles like Password Administrator or Exchange Online Administrator, instead of Global Administrator, reduces the likelihood of a global administrative privileged account being breached.";

        public string solution
        {
            get
            {
                return _solution;
            }
        }

        public async Task<bool> Test(GraphServiceClient appClient)
        {
            var secureScores = await appClient.Security.SecureScores
            .Request()
            .Top(1)
            .Select(e => new
            {
                e.CreatedDateTime,
                e.ControlScores
            })
            .Filter("azureTenantId eq '" + _settings.TenantId + "'")
            .GetAsync();

            foreach (var secureScore in secureScores)
            {
                Console.WriteLine("Secure score created: " + secureScore.CreatedDateTime.ToString());
                foreach(var controlScore in secureScore.ControlScores)
                {
                    if(controlScore.ControlName == "RoleOverlap")
                    {
                        object? percentage;
                        if (controlScore.AdditionalData.TryGetValue("scoreInPercentage", out percentage))
                        {
                            float p;
                            if(float.TryParse(percentage.ToString(), out p))
                            {
                                if(p == 100.0)
                                {
                                    return true;
                                } else
                                {
                                    _solution = "Score percentage: " + p + " created at " + secureScore.CreatedDateTime.ToString() + ". " + _solution;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}