
using System;
using MyMonkeyApp;

namespace MyMonkeyApp;

public class Program
{
	private static readonly string[] asciiArt = new[]
	{
		@"  (\__/)",
		@"  (o.o )", 
		@"  (> < )", 
		@"   /\_/\   ",
		@"  ( . . )  ",
		@"  ( > < )  "
	};

	public static void Main(string[] args)
	{
		var random = new Random();
		bool running = true;
		while (running)
		{
			Console.Clear();
			// Display random ASCII art
			if (random.NextDouble() < 0.5)
			{
				Console.WriteLine(asciiArt[random.Next(asciiArt.Length)] + "\n");
			}
			Console.WriteLine("Monkey Management Console\n");
			Console.WriteLine("1. List all monkeys");
			Console.WriteLine("2. Get details for a specific monkey by name");
			Console.WriteLine("3. Get a random monkey");
			Console.WriteLine("4. Exit app\n");
			Console.Write("Select an option (1-4): ");

			var input = Console.ReadLine();
			Console.WriteLine();
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
		}
	}

	private static void DisplayMonkeyGrid()
	{
		var monkeys = MonkeyHelper.GetMonkeys();
		Console.WriteLine($"{"Name",-22} | {"Location",-25} | {"Population",-10} | {"Latitude",-9} | {"Longitude",-10}");
		Console.WriteLine(new string('-', 85));
		foreach (var monkey in monkeys)
		{
			Console.WriteLine($"{monkey.Name,-22} | {monkey.Location,-25} | {monkey.Population,-10} | {monkey.Latitude,-9:F4} | {monkey.Longitude,-10:F4}");
		}
	}

	private static void DisplayMonkeyDetails(string? name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			Console.WriteLine("No name entered.");
			return;
		}
		var monkey = MonkeyHelper.GetMonkeyByName(name);
		if (monkey == null)
		{
			Console.WriteLine($"Monkey '{name}' not found.");
			return;
		}
		Console.WriteLine($"Name: {monkey.Name}\nLocation: {monkey.Location}\nPopulation: {monkey.Population}\nLatitude: {monkey.Latitude}\nLongitude: {monkey.Longitude}\nDetails: {monkey.Details}\nImage: {monkey.Image}");
	}

	private static void DisplayRandomMonkey()
	{
		var monkey = MonkeyHelper.GetRandomMonkey();
		Console.WriteLine($"Random Monkey: {monkey.Name}\nLocation: {monkey.Location}\nPopulation: {monkey.Population}\nLatitude: {monkey.Latitude}\nLongitude: {monkey.Longitude}\nDetails: {monkey.Details}\nImage: {monkey.Image}");
		Console.WriteLine($"Random monkey accessed {MonkeyHelper.GetRandomMonkeyAccessCount()} times.");
	}
}
