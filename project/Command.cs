Console.WriteLine("Security and compliance score - CLI\n");

var settings = Settings.LoadSettings();

// Initialize Graph
TestHelper.InitializeGraph(settings);

int choice = -1;

while (choice != 0)
{
    Console.WriteLine("Please choose one of the following options:");
    Console.WriteLine("0. Exit");
    Console.WriteLine("Authentication");
    Console.WriteLine("1. Password Authentication");
    Console.WriteLine("Conditional Access");
    Console.WriteLine("2. Require MFA for Admins");
    Console.WriteLine("3. Block Legacy Authentication");

    try
    {
        choice = int.Parse(Console.ReadLine() ?? string.Empty);
    }
    catch (System.FormatException)
    {
        // Set to invalid value
        choice = -1;
    }

    ITestCase? testCase = null;

    switch (choice)
    {
        case 0:
            Console.WriteLine("Goodbye...");
            break;
        case 1:
            testCase = new TestCases.Users.PasswordAuthentication();
            break;
        case 2:
            testCase = new TestCases.ConditionalAccess.RequireMFAForAdmins();
            break;
        case 3:
            testCase = new TestCases.ConditionalAccess.BlockLegacyAuthentication();
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }

    if (testCase != null)
    {
        Console.WriteLine($"{testCase.name} : {(await TestHelper.Test(testCase) ? "PASS" : "FAIL (" + testCase.solution + ")")}");
    }
}
