using Microsoft.Graph;

interface ITestCase
{
    string name
    {
        get;
    }

    Task<bool> Test(GraphServiceClient appClient);
}
