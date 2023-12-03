namespace AdventOfCode2023.Day03;

public class GearRatios : ChallengeBase<int>
{
    public GearRatios(string[] data) : base(data)
    {
    }

    protected override int Part1()
    {
        var coordinates = new HashSet<NumberPoint>();

        for (var y = 0; y < ChallengeDataRows.Count(); y++)
        {
            int? xStart = null;
            int? xEnd = null;
            for (var x = 0; x < ChallengeDataRows[y].Length; x++)
            {
                var item = ChallengeDataRows[y][x];
                if (IsNumber(item))
                {
                    xEnd = x;
                    if (xStart is null) xStart = x;
                }

                if (xEnd is not null && xStart is not null && IsNumber(item) == false || x == ChallengeDataRows[0].Length - 1 && IsNumber(item))
                {
                    var value = int.Parse(ChallengeDataRows[y].Substring(xStart.Value, (xEnd.Value - xStart.Value) + 1));
                    coordinates.Add(new NumberPoint(xStart.Value, xEnd.Value, y, value));
                    xEnd = null;
                    xStart = null;
                }
            }
        }

        var answer = coordinates
            .Where(IsAdjacentToSymbol)
            .Sum(c => c.Value);

           return answer;
    }

    private bool IsAdjacentToSymbol(NumberPoint point)
    {
        var minY = point.Y == 0 ? 0 : point.Y - 1;
        var maxY = point.Y == ChallengeDataRows.Length - 1 ? point.Y : point.Y + 1;
        var minX = point.StartX == 0 ? 0 : point.StartX - 1;
        var maxX = point.EndX == ChallengeDataRows[0].Length - 1 ? point.EndX : point.EndX + 1;

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                var c = ChallengeDataRows[y][x];
                if (IsNumber(c) == false && c != '.') return true;
            }
        }

        return false;
    }
    
    protected override int Part2() => 0;

    private static bool IsNumber(char c) => int.TryParse(c.ToString(), out var _);
}

public record NumberPoint(int StartX, int EndX, int Y, int Value);