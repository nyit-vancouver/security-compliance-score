Console.WriteLine(".NET Graph Tutorial\n");

var settings = Settings.LoadSettings();

// Initialize Graph
InitializeGraph(settings);

int choice = -1;

while (choice != 0)
{
    Console.WriteLine("Please choose one of the following options:");
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. [Me] Greet User");
    Console.WriteLine("2. [Me] Display access token");
    Console.WriteLine("3. [Me] List my inbox");
    Console.WriteLine("4. [Me] Send mail");
    Console.WriteLine("5. [Client] List users");

    try
    {
        choice = int.Parse(Console.ReadLine() ?? string.Empty);
    }
    catch (System.FormatException)
    {
        // Set to invalid value
        choice = -1;
    }

    switch (choice)
    {
        case 0:
            // Exit the program
            Console.WriteLine("Goodbye...");
            break;
        case 1:
            // Greet the user by name
            await GreetUserAsync();
            break;
        case 2:
            // [Me] Display access token
            await DisplayAccessTokenAsync();
            break;
        case 3:
            // [Me] List emails from user's inbox
            await ListInboxAsync();
            break;
        case 4:
            // [Me] Send an email message
            await SendMailAsync();
            break;
        case 5:
            // [Client] List users
            await ListUsersAsync();
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }
}

void InitializeGraph(Settings settings)
{
    GraphHelper.InitializeGraphForUserAuth(settings,
        (info, cancel) =>
        {
            // Display the device code message to
            // the user. This tells them
            // where to go to sign in and provides the
            // code to use.
            Console.WriteLine(info.Message);
            return Task.FromResult(0);
        });
}

async Task GreetUserAsync()
{
    try
    {
        var user = await GraphHelper.GetUserAsync();
        Console.WriteLine($"Hello, {user?.DisplayName}!");
        // For Work/school accounts, email is in Mail property
        // Personal accounts, email is in UserPrincipalName
        Console.WriteLine($"Email: {user?.Mail ?? user?.UserPrincipalName ?? ""}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting user: {ex.Message}");
    }
}

async Task DisplayAccessTokenAsync()
{
    try
    {
        var userToken = await GraphHelper.GetUserTokenAsync();
        Console.WriteLine($"User token: {userToken}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting user access token: {ex.Message}");
    }
}

async Task ListInboxAsync()
{
    try
    {
        var messagePage = await GraphHelper.GetInboxAsync();

        // Output each message's details
        foreach (var message in messagePage.CurrentPage)
        {
            Console.WriteLine($"Message: {message.Subject ?? "NO SUBJECT"}");
            Console.WriteLine($"  From: {message.From?.EmailAddress?.Name}");
            Console.WriteLine($"  Status: {(message.IsRead!.Value ? "Read" : "Unread")}");
            Console.WriteLine($"  Received: {message.ReceivedDateTime?.ToLocalTime().ToString()}");
        }

        // If NextPageRequest is not null, there are more messages
        // available on the server
        // Access the next page like:
        // messagePage.NextPageRequest.GetAsync();
        var moreAvailable = messagePage.NextPageRequest != null;

        Console.WriteLine($"\nMore messages available? {moreAvailable}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting user's inbox: {ex.Message}");
    }
}

async Task SendMailAsync()
{
    try
    {
        // Send mail to the signed-in user
        // Get the user for their email address
        var user = await GraphHelper.GetUserAsync();

        var userEmail = user?.Mail ?? user?.UserPrincipalName;

        if (string.IsNullOrEmpty(userEmail))
        {
            Console.WriteLine("Couldn't get your email address, canceling...");
            return;
        }

        await GraphHelper.SendMailAsync("Testing Microsoft Graph",
            "Hello world!", userEmail);

        Console.WriteLine("Mail sent.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error sending mail: {ex.Message}");
    }
}

async Task ListUsersAsync()
{
    try
    {
        var userPage = await GraphHelper.GetUsersAsync();

        // Output each users's details
        foreach (var user in userPage.CurrentPage)
        {
            Console.WriteLine($"User: {user.DisplayName ?? "NO NAME"}");
            Console.WriteLine($"  ID: {user.Id}");
            Console.WriteLine($"  Email: {user.Mail ?? "NO EMAIL"}");
        }

        // If NextPageRequest is not null, there are more users
        // available on the server
        // Access the next page like:
        // userPage.NextPageRequest.GetAsync();
        var moreAvailable = userPage.NextPageRequest != null;

        Console.WriteLine($"\nMore users available? {moreAvailable}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.ToString()}");
        Console.WriteLine($"Error getting users: {ex.Message}");
    }
}
