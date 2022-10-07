using System.Runtime;
using Microsoft.Graph;

namespace TestCases.SecureScores
{
    class SecureScore : ITestCase
    {
        private Settings _settings;
        private string _controlName;

        public SecureScore(Settings settings, string controlName)
        {
            _settings = settings;
            _controlName = controlName;
        }

        private string _name = "Secure scores - ";

        public string name
        {
            get
            {
                return _name + _controlName;
            }
        }

        private string _solution = "";

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
                foreach(var controlScore in secureScore.ControlScores)
                {
                    if(controlScore.ControlName == _controlName)
                    {
                        _solution = controlScore.Description;

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
                                    _solution = "Score percentage: " + p + " created at " + secureScore.CreatedDateTime.ToString() + ". " + controlScore.AdditionalData["implementationStatus"].ToString() + " " + _solution;
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