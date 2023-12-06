using System.Collections.Concurrent;

namespace AdventOfCode2023.Day05;

public class SeedFertilizer : ChallengeBase<long>
{
    private readonly IEnumerable<long> _seed;
    private readonly List<IEnumerable<Mapping>> _allMappings;

    public SeedFertilizer(string[] data) : base(data)
    { 
        _seed = GetSeedNumbers();
        _allMappings = GetMappings();
    }

    protected override long Part1()
    {
        var endLocations = new List<long>();

        foreach (var seed in _seed)
        {
            long nextValue = seed;

            foreach (var mappings in _allMappings)
            {
                var mapping = mappings.FirstOrDefault(m => m.Source.Start < nextValue && m.Source.End >= nextValue);
                nextValue = mapping is not null ? nextValue + (mapping.Destination.Start - mapping.Source.Start) : nextValue;
            }

            endLocations.Add(nextValue);
        }

        return endLocations.Min();
    }

    protected override long Part2()
    {
        var seeds = _seed
            .Chunk(2)
            .Select(chnk => new LongRange(chnk[0], chnk[0] + chnk[1]-1));



        var cache = new ConcurrentDictionary<long, long>();

        // check if any number ranges overlap to reduce multiple runs
        foreach (var seedRange in seeds)
        {
            for (var seed = seedRange.Start; seed <= seedRange.End; seed++)
            {
                if (cache.ContainsKey(seed)) continue;

                long nextValue = seed;

                foreach (var mappings in _allMappings)
                {
                    var mapping = mappings.FirstOrDefault(m => m.Source.Start <= nextValue && m.Source.End >= nextValue);
                    nextValue = mapping is not null ? nextValue + (mapping.Destination.Start - mapping.Source.Start) : nextValue;
                }

                cache.TryAdd(seed, nextValue);
            }
        }

        return cache.Values.Min();
    }

    private IEnumerable<long> GetSeedNumbers()
        => ChallengeDataRows[0].Substring(7).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);

    private List<IEnumerable<Mapping>> GetMappings()
    {
        var results = new List<IEnumerable<Mapping>>();
        var currentSet = new List<Mapping>();

        for (var i = 2; i < ChallengeDataRows.Length; i++)
        {
            var row = ChallengeDataRows[i];

            if (row.EndsWith("map:"))
            {
                currentSet = new List<Mapping>();
            }
            else if (string.IsNullOrEmpty(row))
            {
                results.Add(currentSet);
            }
            else 
            {
                var numbers = row.Split(' ').Select(long.Parse).ToList();
                var sourceRange = new LongRange(numbers[1], numbers[1] + numbers[2]-1);
                var destinationRange = new LongRange(numbers[0], numbers[0] + numbers[2]-1);

                currentSet.Add(new Mapping(sourceRange, destinationRange, numbers[2]));
            }
        }

        results.Add(currentSet);

        return results;
    }
}

public record Mapping(LongRange Source, LongRange Destination, long RangeLength);

public record LongRange(long Start, long End)
{
    public bool Overlaps(LongRange other)
    {
        return Start >= other.Start && End >= other.End
            || End >= other.Start && End < other.End
            || Start <= other.Start && End >= other.End;
    }
}

public class Flattener
{
    public static IEnumerable<LongRange> FlattenRanges(IEnumerable<LongRange> ranges)
    {
        var results = new HashSet<LongRange>();

        foreach (var seedRange in ranges)
        {
            var overlapLow = ranges
                .Where(s => 
                    s.Start != seedRange.Start && s.End != seedRange.End 
                    && s.Start < seedRange.Start && s.End <= seedRange.End && s.End > seedRange.Start
                );

            var overlapHigh = ranges
                .Where(s => 
                    s.Start != seedRange.Start && s.End != seedRange.End 
                    && s.Start < seedRange.End && s.End > seedRange.End
                );

            var low = overlapLow.Any() ? overlapLow.Min(x => x.Start) : seedRange.Start;
            var high = overlapHigh.Any() ? overlapHigh.Max(x => x.End) : seedRange.End;

            //if (results.Any(r => r.Start > low && r.Start > high))

            results.Add(new LongRange(low, high));
        }

        return results;
    }
}