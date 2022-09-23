Console.WriteLine("Security and compliance score - CLI\n");

var settings = Settings.LoadSettings();

// Initialize Graph
TestHelper.InitializeGraph(settings);

int choice = -1;

while (choice != 0)
{
    Console.WriteLine("Please choose one of the following options:");
    Console.WriteLine("0. Exit");
    Console.WriteLine("Conditional Access");
    Console.WriteLine("1. Require MFA for Admins");

    try
    {
        choice = int.Parse(Console.ReadLine() ?? string.Empty);
    }
    catch (System.FormatException)
    {
        // Set to invalid value
        choice = -1;
    }

    ITestCase testCase = null;

    switch (choice)
    {
        case 0:
            // Exit the program
            Console.WriteLine("Goodbye...");
            break;
        case 1:
            // Conditional Access: Require MFA for Admins
            testCase = new TestCases.ConditionalAccess.RequireMFAForAdmins();
            
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }

    if (testCase != null)
    {
        Console.WriteLine($"{testCase.name()} : {(await TestHelper.Test(testCase) ? "PASS" : "FAIL")}");
    }
}
