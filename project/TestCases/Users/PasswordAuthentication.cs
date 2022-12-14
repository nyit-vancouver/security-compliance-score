using System.Runtime;
using Microsoft.Graph;

namespace TestCases.Users
{
    class PasswordAuthentication : ITestCase
    {

        private string _solution = "Disable password authentication methods on users";
        
        /* Samuel 2022 -  In this code we check if any user uses password authentication. */
        
        public string name
        {
            get
            {
                return "Users - Password Authentication";
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
            var users= await appClient.Users  /* here we get a list of all users */
            .Request()
            .GetAsync();

            var result = true;
            foreach (var user in users) /* here we capture authentication of one user */
            {
                var methods = await appClient.Users[user.Id].Authentication.Methods
                .Request()
                .GetAsync();
            
                /* Here we check if any user matches has password authentication enabled */
                var pageIterator = PageIterator<AuthenticationMethod>.CreatePageIterator(appClient, methods,   
                    (m) => {
                        if(m.ToString() == "Microsoft.Graph.PasswordAuthenticationMethod")
                        {
                            _solution = _solution + $" [{user.DisplayName}]";
                            result = false;
                        }
                        return true;
                    }, (req) => {
                        req.Header("Prefer", "outlook.body-content-type=\"text\"");
                        return req;
                    }
                );
                await pageIterator.IterateAsync();
            }
            return result;
        }
    }
}
