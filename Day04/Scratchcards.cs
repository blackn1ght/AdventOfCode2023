namespace AdventOfCode2023.Day04;

public class Scratchcards : ChallengeBase<int>
{
    public Scratchcards(string[] data) : base(data)
    {
    }

    protected override int Part1()
        => ChallengeDataRows.Sum(card => GetCardScore(GetMatchingNumbers(card)));

    protected override int Part2()
    {
        var winningCopies = new Dictionary<int, int>();

        for (var i = 0; i < ChallengeDataRows.Length; i++)
        {
            winningCopies.AddAndIncrement(i);

            var card = ChallengeDataRows[i];

            var noOfMatchingNumbers = GetMatchingNumbers(card).Count();

            for (var k = 0; k < winningCopies[i]; k++)
            {
                for (var n = i + 1; n < (noOfMatchingNumbers + i + 1); n++)
                {
                    winningCopies.AddAndIncrement(n);
                }
            }
        }

        return winningCopies.Sum(w => w.Value);
    }

    private static int GetCardScore(IEnumerable<int> matchingNumbers)
    {
        var multiplier = 0;
        foreach (var matchingNumber in matchingNumbers)
        {
            multiplier = multiplier == 0 ? 1 : multiplier * 2;
        }

        return multiplier;
    }

    private static IEnumerable<int> GetMatchingNumbers(string card)
    {
        var indexOfColon = card.IndexOf(':');
        card = card.Substring(indexOfColon + 2);
        var indexOfPipe = card.IndexOf('|');
        var winningNumbers = ParseNumbers(card.Substring(0, indexOfPipe - 1));
        var playerNumers = ParseNumbers(card.Substring(indexOfPipe + 1));

        return winningNumbers.Intersect(playerNumers);
    }

    private static IEnumerable<int> ParseNumbers(string numberSet)
        => numberSet.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n));
}

public static class DictionaryExtensions
{
    public static void AddAndIncrement(this Dictionary<int, int> dictionary, int key)
    {
        if (dictionary.ContainsKey(key) == false)
        {
            dictionary.Add(key, 0);
        }
        dictionary[key]++;
    }
}