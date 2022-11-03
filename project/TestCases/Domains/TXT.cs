using System.Runtime;
using Microsoft.Graph;

namespace TestCases.Domains
{
    class TXT : ITestCase
    {
        private string _textName;
        private string _solution = "Check TXT Spf record on domains";

        public TXT(string textName)
        {
            _textName = textName;
        }

        public string name
        {
            get
            {
                return "Domains - TXT " + _textName;
            }
        }

        public string solution
        {
            get
            {
                return _solution;
            }
        }

        public async Task<bool> Test(GraphServiceClient appClient)
        {
            var domains = await appClient.Domains
            .Request()
            .GetAsync();

            var result = false;
            foreach (var domain in domains)
            {
                var domainResult = false;
                var records = await appClient.Domains[domain.Id].ServiceConfigurationRecords
                .Request()
                .Filter("")
                .GetAsync();

                var pageIterator = PageIterator<DomainDnsRecord>.CreatePageIterator(appClient, records,
                    (m) => {
                        if(m.RecordType == "Txt")
                        {
                            var t = (DomainDnsTxtRecord)m;
                            if(t.Text.Contains(_textName))
                            {
                                domainResult = true;
                            }
                        }
                        return true;
                    }, (req) => {
                        req.Header("Prefer", "outlook.body-content-type=\"text\"");
                        return req;
                    }
                );
                await pageIterator.IterateAsync();
                if(!domainResult)
                {
                    result = false;
                    _solution = _solution + $" [{domain.Id}]";
                }
            }
            return result;
        }
    }
}