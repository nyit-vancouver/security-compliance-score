using Microsoft.Graph;

interface ITestCase
{
    string name();

    Task<bool> Test(GraphServiceClient appClient);
}
