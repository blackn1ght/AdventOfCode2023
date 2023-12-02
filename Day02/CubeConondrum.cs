namespace AdventOfCode2023.Day02;

using System.Text.RegularExpressions;

public class CubeConondrum : ChallengeBase<int>
{

    // 12 red cubes, 13 green cubes, and 14 blue cubes
    public CubeConondrum(string[] data) : base(data)
    {
    }

    protected override int Part1()
    {
        var answer = 0;

        var thresholds = new Dictionary<string, int>()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        var counter = 1;
        foreach (var row in ChallengeDataRows)
        {
            var thresholdsHit = false;
            foreach (var threshold in thresholds)
            {
                var matches = Regex.Matches(row, $"([0-9]*) {threshold.Key}");
                if (matches.Any(m => int.Parse(m.Groups[1].Value) > threshold.Value))
                {
                    thresholdsHit = true;
                }
            }
            if (thresholdsHit == false)
            {
                answer += counter;
            }
            counter++;
        }

        return answer;
    }

    protected override int Part2()
    {
        var answer = 0;

        var colours = new[] { "blue", "red", "green"};

        foreach (var row in ChallengeDataRows)
        {
            var colourResults = new List<int>();

            foreach (var colour in colours)
            {
                var matches = Regex.Matches(row, $"([0-9]*) {colour}");
                var max = matches.Max(m => int.Parse(m.Groups[1].Value));

                colourResults.Add(max);
            }

            answer += colourResults.Aggregate(1, (prev, curr) => prev * curr);
        }

        return answer;
    }   
}