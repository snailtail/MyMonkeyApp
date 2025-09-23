using MyMonkeyApp;


var asciiArt = new[]
{
	@"  (\__/)",
	@"  (o.o )",
	@"  (> < )",
	@"   /\_/\   ",
	@"  ( . . )  ",
	@"  ( > < )  "
};


var bananaArt = new[]
{
	@"   _",
	@"  //\\",
	@" | \_/ |",
	@"  \___/",
	@"   | |",
	@"  (banana mode!)"
};

var secretArt = new[]
{
	@"      .-""""-.",
	@"     / .===. \",
	@"     \/ 6 6 \/",
	@"     ( \___/ )",
	@" ___ooo__V__ooo___",
	@"  SECRET MONKEY!"
};

var random = new Random();
bool bananaMode = false;
ShowMonkeyOfTheDay();
Console.WriteLine("\nPress Enter to continue to menu...");
Console.ReadLine();

bool running = true;
while (running)
{
	Console.Clear();
	if (bananaMode)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.BackgroundColor = ConsoleColor.DarkYellow;
		Console.WriteLine(bananaArt[random.Next(bananaArt.Length)] + "\n");
		Console.WriteLine("🍌 Welcome to BANANA MODE! 🍌\n");
	}
	else
	{
		Console.ResetColor();
		// Display random ASCII art
		if (random.NextDouble() < 0.5)
		{
			Console.WriteLine(asciiArt[random.Next(asciiArt.Length)] + "\n");
		}
	}
	Console.WriteLine("Monkey Management Console\n");
	Console.WriteLine("1. List all monkeys");
	Console.WriteLine("2. Get details for a specific monkey by name");
	Console.WriteLine("3. Get a random monkey");
	Console.WriteLine("4. Show a random monkey fact or joke");
	Console.WriteLine("5. Vote for your favorite monkey");
	Console.WriteLine("6. Show monkey leaderboard");
	Console.WriteLine("7. Exit app\n");
	Console.WriteLine("Type 'banana' to activate Banana Mode!");
	Console.WriteLine("Type 'secret' to unlock secret ASCII art!");
	Console.Write("Select an option (1-7): ");

	var input = Console.ReadLine();
	Console.WriteLine();
	var trimmedInput = input?.Trim().ToLower();
	if (trimmedInput == "banana")
	{
		bananaMode = !bananaMode;
		Console.WriteLine(bananaMode ? "Banana mode activated! 🍌" : "Banana mode deactivated.");
		Console.WriteLine("Press Enter to continue...");
		Console.ReadLine();
		continue;
	}
	if (trimmedInput == "secret")
	{
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.BackgroundColor = ConsoleColor.Black;
		Console.WriteLine(secretArt[random.Next(secretArt.Length)] + "\n");
		Console.WriteLine("You found the secret monkey! 🐵");
		Console.ResetColor();
		Console.WriteLine("Press Enter to continue...");
		Console.ReadLine();
		continue;
	}
	switch (input)
	{
		case "1":
			DisplayMonkeyGrid();
			break;
		case "2":
			Console.Write("Enter monkey name: ");
			var name = Console.ReadLine();
			DisplayMonkeyDetails(name);
			break;
		case "3":
			DisplayRandomMonkey();
			break;
		case "4":
			ShowRandomFactOrJoke();
			break;
		case "5":
			VoteForMonkey();
			break;
		case "6":
			DisplayLeaderboard();
			break;
		case "7":
			running = false;
			Console.WriteLine("Goodbye!");
			break;
		default:
			Console.WriteLine("Invalid option. Please try again.");
			break;
	}
	if (running)
	{
		Console.WriteLine("\nPress Enter to continue...");
		Console.ReadLine();
	}
	Console.ResetColor();
}

void ShowMonkeyOfTheDay()
{
	var monkeys = MonkeyHelper.GetMonkeys();
	// Deterministic selection based on date
	var today = DateTime.UtcNow.Date;
	int index = Math.Abs(today.GetHashCode()) % monkeys.Count;
	var monkey = monkeys[index];
	var random = new Random(today.GetHashCode());
	var art = asciiArt[random.Next(asciiArt.Length)];
	Console.Clear();
	Console.WriteLine("🐒 Monkey of the Day 🐒\n");
	Console.WriteLine(art + "\n");
	Console.WriteLine($"Name: {monkey.Name}\nDetails: {monkey.Details}");
	Console.WriteLine("\nSpecial Fact or Joke:");
	Console.WriteLine(MonkeyFactHelper.GetRandomFactOrJoke());
}

void ShowRandomFactOrJoke()
{
	bool showAnother = true;
	while (showAnother)
	{
		Console.WriteLine("\nMonkey Fact or Joke:");
		Console.WriteLine(MonkeyFactHelper.GetRandomFactOrJoke());
		Console.Write("\nShow another? (y/n): ");
		var again = Console.ReadLine();
		showAnother = again?.Trim().ToLower() == "y";
	}
}

void VoteForMonkey()
{
	Console.Write("Enter the name of the monkey you want to vote for: ");
	var name = Console.ReadLine();
	var monkey = MonkeyHelper.GetMonkeyByName(name ?? string.Empty);
	if (monkey == null)
	{
		Console.WriteLine($"Monkey '{name}' not found.");
		return;
	}
	MonkeyHelper.Vote(monkey.Name);
	var votes = MonkeyHelper.GetVotes(monkey.Name);
	Console.WriteLine($"You voted for {monkey.Name}! Total votes: {votes}");
}

void DisplayMonkeyGrid()
{
	var monkeys = MonkeyHelper.GetMonkeys();
	Console.WriteLine($"{"Name",-22} | {"Location",-25} | {"Population",-10} | {"Latitude",-9} | {"Longitude",-10} | {"Votes",-5}");
	Console.WriteLine(new string('-', 93));
	foreach (var monkey in monkeys)
	{
		Console.WriteLine($"{monkey.Name,-22} | {monkey.Location,-25} | {monkey.Population,-10} | {monkey.Latitude,-9:F4} | {monkey.Longitude,-10:F4} | {monkey.Votes,-5}");
	}
}

void DisplayMonkeyDetails(string? name)
{
	if (string.IsNullOrWhiteSpace(name))
	{
		Console.WriteLine("No name entered.");
		return;
	}
	var monkey = MonkeyHelper.GetMonkeyByName(name);
	if (monkey != null)
	{
		MonkeyHelper.TrackViewed(monkey.Name);
	}
	if (monkey == null)
	{
		Console.WriteLine($"Monkey '{name}' not found.");
		return;
	}
	Console.WriteLine($"Name: {monkey.Name}\nLocation: {monkey.Location}\nPopulation: {monkey.Population}\nLatitude: {monkey.Latitude}\nLongitude: {monkey.Longitude}\nDetails: {monkey.Details}\nVotes: {monkey.Votes}\nImage: {monkey.Image}");
}

void DisplayRandomMonkey()
{
	var monkey = MonkeyHelper.GetRandomMonkey();
	MonkeyHelper.TrackViewed(monkey.Name);
	Console.WriteLine($"Random Monkey: {monkey.Name}\nLocation: {monkey.Location}\nPopulation: {monkey.Population}\nLatitude: {monkey.Latitude}\nLongitude: {monkey.Longitude}\nDetails: {monkey.Details}\nImage: {monkey.Image}");
	Console.WriteLine($"Random monkey accessed {MonkeyHelper.GetRandomMonkeyAccessCount()} times.");
}

void DisplayLeaderboard()
{
	var monkeys = MonkeyHelper.GetMonkeys()
		.OrderByDescending(m => m.Votes)
		.ThenBy(m => m.Name)
		.ToList();
	Console.WriteLine("\nMonkey Leaderboard (by Votes):\n");
	Console.WriteLine($"{"Rank",-5} | {"Name",-22} | {"Votes",-5}");
	Console.WriteLine(new string('-', 38));
	int rank = 1;
	foreach (var monkey in monkeys)
	{
		Console.WriteLine($"{rank,-5} | {monkey.Name,-22} | {monkey.Votes,-5}");
		rank++;
	}
}
