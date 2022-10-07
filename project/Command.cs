Console.WriteLine("Security and compliance score - CLI\n");

var settings = Settings.LoadSettings();

// Initialize Graph
TestHelper.InitializeGraph(settings);

int choice = -1;

while (choice != 0)
{
    Console.WriteLine("Please choose one of the following options:");
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. Authentication");
    Console.WriteLine("10. Password Authentication");
    Console.WriteLine("2. Conditional Access");
    Console.WriteLine("20. Require MFA for Admins");
    Console.WriteLine("21. Block Legacy Authentication");
    Console.WriteLine("3. Secure Scores");
    Console.WriteLine("30. Role Overlap");
    Console.WriteLine("31. One Admin");
    Console.WriteLine("32. Legacy Authentication");
    Console.WriteLine("33. Admin MFA");

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
        case 10:
            testCase = new TestCases.Users.PasswordAuthentication();
            break;
        case 20:
            testCase = new TestCases.ConditionalAccess.RequireMFAForAdmins();
            break;
        case 21:
            testCase = new TestCases.ConditionalAccess.BlockLegacyAuthentication();
            break;
        case 30:
            testCase = new TestCases.SecureScores.SecureScore(settings, "RoleOverlap");
            break;
        case 31:
            testCase = new TestCases.SecureScores.SecureScore(settings, "OneAdmin");
            break;
        case 32:
            testCase = new TestCases.SecureScores.SecureScore(settings, "BlockLegacyAuthentication");
            break;
        case 33:
            testCase = new TestCases.SecureScores.SecureScore(settings, "AdminMFAV2");
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
