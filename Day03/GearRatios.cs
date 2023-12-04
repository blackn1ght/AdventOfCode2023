using System.Drawing;

namespace AdventOfCode2023.Day03;

public class GearRatios : ChallengeBase<int>
{
    public GearRatios(string[] data) : base(data)
    {
    }

    protected override int Part1()
        => GetNumberPoints()
            .Where(IsAdjacentToSymbol)
            .Sum(c => c.Value);

    protected override int Part2()
    {
        var numberCoordinates = GetNumberPoints();
        var gearCoordinates = GetGearCoordinates();

        var answer = 0;

        foreach (var gear in gearCoordinates)
        {
            var nearbyNumbers = numberCoordinates
                .Where(np => HasNumberOnSameLine(np, gear) || HasNumberOnLineAbove(np, gear) || HasNumberOnLineBelow(np, gear))
                .Select(np => np.Value)
                .ToList();

            if (nearbyNumbers.Count() == 2)
            {
                answer += nearbyNumbers[0] * nearbyNumbers[1];
            }
        }

        return answer;
    }

    private static bool HasNumberOnSameLine(NumberPoint np, Point p) 
        => np.Y == p.Y && (np.EndX == p.X - 1 || np.StartX == p.X + 1);

    private static bool HasNumberOnLineAbove(NumberPoint np, Point p) 
        => np.Y == p.Y - 1 && IsNumberAdjacentToGear(np, p);

    private static bool HasNumberOnLineBelow(NumberPoint np, Point p) 
        => np.Y == p.Y + 1 && IsNumberAdjacentToGear(np, p);

    private static bool IsNumberAdjacentToGear(NumberPoint np, Point p) => 
        np.EndX == p.X - 1 || np.EndX == p.X || np.EndX == p.X + 1 || np.StartX == p.X - 1 || np.StartX == p.X || np.StartX == p.X + 1;

    private HashSet<NumberPoint> GetNumberPoints()
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

        return coordinates;
    }

    private HashSet<Point> GetGearCoordinates()
    {
        var results = new HashSet<Point>();

        for (var y = 0; y < ChallengeDataRows.Count(); y++)
        {
            for (var x = 0; x < ChallengeDataRows[y].Length; x++)
            {
                if (ChallengeDataRows[y][x] == '*')
                {
                    results.Add(new Point(x, y));
                }
            }
        }

        return results;
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

    private static bool IsNumber(char c) => int.TryParse(c.ToString(), out var _);
}

public record NumberPoint(int StartX, int EndX, int Y, int Value);