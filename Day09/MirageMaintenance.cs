namespace AdventOfCode2023.Day09;

public class MirageMaintenance : ChallengeBase<long>
{
    private readonly List<List<int>> _histories;

    public MirageMaintenance(string[] data) : base(data)
    { 
        _histories = GetHistories();
    }

    protected override long Part1()
        => _histories.Sum(GetNextValue);
    
    protected override long Part2()
        => _histories.Sum(GetPreviousValue);

    private int GetNextValue(List<int> values)
    {
        var sequences = BuildSequences(values);

        sequences[sequences.Count - 1].Add(0);
        var secondToLastValue = sequences[sequences.Count-2][0];
        sequences[sequences.Count - 2].Add(secondToLastValue);

        for (var i = sequences.Count - 3; i >= 0; i--)
        {
            var valToAdd = sequences[i+1].Last();
            sequences[i].Add(sequences[i].Last() + valToAdd);
        }

        return sequences[0].Last();
    }

    private int GetPreviousValue(List<int> values)
    {
        var sequences = BuildSequences(values);

        sequences[sequences.Count - 1].Add(0);
        var secondToLastValue = sequences[sequences.Count-2][0];
        sequences[sequences.Count - 2].Insert(0, secondToLastValue);

        for (var i = sequences.Count - 3; i >= 0; i--)
        {
            var valToMinus = sequences[i+1].First();
            sequences[i].Insert(0, sequences[i].First() - valToMinus);
        }

        return sequences[0].First();
    }

    private List<List<int>> BuildSequences(List<int> values)
    {
        var set = new List<List<int>>
        {
            values
        };

        var currentValues = values;

        do
        {
            var nextValues = new List<int>();

            for (var i = 0; i < currentValues.Count - 1; i++)
            {
                nextValues.Add(currentValues[i+1] - currentValues[i]);
            }

            set.Add(nextValues);
            currentValues = nextValues;
        }
        while (currentValues.Any(v => v != 0));

        return set;
    }

    private List<List<int>> GetHistories()
        => ChallengeDataRows
            .Select(row => row.Split(' ').Select(int.Parse).ToList())
            .ToList();
}