namespace AdventOfCode2023.Day06;

public class WaitForIt : ChallengeBase<long>
{
    public WaitForIt(string[] data) : base(data)
    { 

    }

    protected override long Part1()
        => GetRaces()
            .Aggregate((long)1, (answer, race) => answer * GetNumberOfTimesRecordBeaten(race));

    protected override long Part2()
        => GetNumberOfTimesRecordBeaten(GetRace());
    
    private static long GetNumberOfTimesRecordBeaten(Race race)
    {
        var recordBeaten = 0;

        for (var pressDuration = 1; pressDuration < race.Time; pressDuration++)
        {
            if (pressDuration * (race.Time - pressDuration) > race.Distance)
                recordBeaten++;
        }

        return recordBeaten;
    }

    private List<Race> GetRaces()
    {
        var times = ParseRow(ChallengeDataRows[0]);
        var distances = ParseRow(ChallengeDataRows[1]);

        return times.Select((t, i) => new Race(t, distances[i])).ToList();
    }

    private Race GetRace()
    {
        var time = ParseRowAsSingleUnit(ChallengeDataRows[0]);
        var distance = ParseRowAsSingleUnit(ChallengeDataRows[1]);

        return new(time, distance);
    }

    private static long ParseRowAsSingleUnit(string row)
        => long.Parse(string.Join(' ', row
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)).Replace(" ", ""));

    private static List<long> ParseRow(string row)
        => row
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(long.Parse)
            .ToList();
}

public record Race(long Time, long Distance);