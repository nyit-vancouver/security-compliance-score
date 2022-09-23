using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.SecurityNamespace;
using System.Runtime;

class TestHelper
{
    // Settings object
    private static Settings? _settings;

    // App-ony auth token credential
    private static ClientSecretCredential? _clientSecretCredential;
    // Client configured with app-only authentication
    private static GraphServiceClient? _appClient;

    public static void InitializeGraph(Settings settings)
    {
        _settings = settings;

        // Ensure settings isn't null
        _ = _settings ??
            throw new System.NullReferenceException("Settings cannot be null");

        if (_clientSecretCredential == null)
        {
            _clientSecretCredential = new ClientSecretCredential(
                _settings.TenantId, _settings.ClientId, _settings.ClientSecret);
        }

        if (_appClient == null)
        {
            _appClient = new GraphServiceClient(_clientSecretCredential,
                // Use the default scope, which will request the scopes
                // configured on the app registration
                new[] { "https://graph.microsoft.com/.default" });
        }
    }

    public static Task<bool> Test(ITestCase testCase)
    {
        // Ensure client isn't null
        _ = _appClient ??
            throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

        return testCase.Test(_appClient);
    }
}
