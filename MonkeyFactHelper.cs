using System;
using System.Collections.Generic;

namespace MyMonkeyApp;

/// <summary>
/// Provides random monkey facts and jokes.
/// </summary>
public static class MonkeyFactHelper
{
    private static readonly List<string> factsAndJokes = new()
    {
        "Monkeys use grooming to strengthen social bonds.",
        "A group of monkeys is called a troop.",
        "Some monkeys can understand basic arithmetic!",
        "Why did the monkey like the banana? Because it had appeal!",
        "Capuchin monkeys use tools to crack nuts.",
        "How do monkeys get down the stairs? They slide down the banana-ster!",
        "Spider monkeys have the longest tail of any primate.",
        "What do you call a monkey at the North Pole? Lost!",
        "Monkeys can recognize themselves in mirrors.",
        "Why don't monkeys play cards in the jungle? Too many cheetahs!"
    };

    /// <summary>
    /// Gets a random fact or joke.
    /// </summary>
    public static string GetRandomFactOrJoke()
    {
        var random = new Random();
        return factsAndJokes[random.Next(factsAndJokes.Count)];
    }
}
