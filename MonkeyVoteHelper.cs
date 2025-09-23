using System;
using System.Collections.Generic;

namespace MyMonkeyApp;

/// <summary>
/// Manages voting for favorite monkeys and tracks votes.
/// </summary>
public static class MonkeyVoteHelper
{
    private static readonly Dictionary<string, int> votes = new();

    /// <summary>
    /// Adds a vote for the specified monkey.
    /// </summary>
    public static void Vote(string monkeyName)
    {
        if (string.IsNullOrWhiteSpace(monkeyName)) return;
        if (votes.ContainsKey(monkeyName))
            votes[monkeyName]++;
        else
            votes[monkeyName] = 1;
    }

    /// <summary>
    /// Gets the current vote count for a monkey.
    /// </summary>
    public static int GetVotes(string monkeyName)
    {
        return votes.TryGetValue(monkeyName, out var count) ? count : 0;
    }

    /// <summary>
    /// Gets all vote counts.
    /// </summary>
    public static IReadOnlyDictionary<string, int> GetAllVotes() => votes;
}
