namespace AdventOfCode2023.Day02;

using System.Text.RegularExpressions;

public class CubeConondrum : ChallengeBase<int>
{
    private readonly Dictionary<string, int> _colours = new ()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };

    public CubeConondrum(string[] data) : base(data)
    {
    }

    protected override int Part1()
        => ChallengeDataRows
            .Select((row, index) => _colours
                .Select(colour => ValuesForColour(row, colour.Key).Any(m => m > colour.Value))
                .All(x => !x) ? index + 1 : 0)
            .Sum();
    
    protected override int Part2()
        => ChallengeDataRows
            .Sum(row => _colours
                .Select(colour => ValuesForColour(row, colour.Key).Max())
                .Aggregate(1, (prev, curr) => prev * curr)
            );
     

    private static IEnumerable<int> ValuesForColour(string input, string colour) 
        => Regex
            .Matches(input, $"([0-9]*) {colour}")
            .Select(m => int.Parse(m.Groups[1].Value));
}