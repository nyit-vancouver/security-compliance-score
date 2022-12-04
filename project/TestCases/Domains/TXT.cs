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
            
            /* Samuel-2022- Here we check whether domains have spf and/or dkim and/or dmarc record in them */
            
            var result = false;
            foreach (var domain in domains) /*Here we are searching for domains that do not have spf record in them */
            {
                var domainResult = false;
                var records = await appClient.Domains[domain.Id].ServiceConfigurationRecords 
                .Request()
                .Filter("")
                .GetAsync();

                var pageIterator = PageIterator<DomainDnsRecord>.CreatePageIterator(appClient, records,
                    (m) => {
                        if(m.RecordType == "Txt")  /*Here we serch for TXT in service configutation records */
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
