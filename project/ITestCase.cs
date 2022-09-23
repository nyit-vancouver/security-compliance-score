using Microsoft.Graph;

interface ITestCase
{
    string name
    {
        get;
    }
    string solution
    {
        get;
    }

    Task<bool> Test(GraphServiceClient appClient);
}
