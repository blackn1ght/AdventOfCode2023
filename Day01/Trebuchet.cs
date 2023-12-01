namespace AdventOfCode2023.Day01;

using System.Text.RegularExpressions;

public class Trebuchet : ChallengeBase<int>
{
    private readonly Regex _regex = new ("(?=(one|two|three|four|five|six|seven|eight|nine|[0-9]))");

    public Trebuchet(string[] data) : base(data)
    {
    }

    protected override int Part1() 
        => ChallengeDataRows
            .Select(row => (row.First(IsDigit), row.Last(IsDigit)))
            .Sum((values) => int.Parse($"{values.Item1}{values.Item2}"));

    protected override int Part2()
        => ChallengeDataRows
            .Select(GetDigitOrWordValuesForRow)
            .Sum((values) => int.Parse($"{values.first}{values.last}"));

    private (string first, string last) GetDigitOrWordValuesForRow(string row)
    {
        var matches = _regex.Matches(row);

        var first = ReplaceNumberWordWithDigit(matches.First().Groups[1].Value);
        var last = ReplaceNumberWordWithDigit(matches.Last().Groups[1].Value);

        return (first, last);
    }

    private static bool IsDigit(char c) => int.TryParse(c.ToString(), out var _);

    private static string ReplaceNumberWordWithDigit(string row)
        => row
            .Replace("one", "1")
            .Replace("two", "2")
            .Replace("three", "3")
            .Replace("four", "4")
            .Replace("five", "5")
            .Replace("six", "6")
            .Replace("seven", "7")
            .Replace("eight", "8")
            .Replace("nine", "9");
    
}