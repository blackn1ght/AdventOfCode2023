namespace AdventOfCode2023.Day08;

using System.Text.RegularExpressions;

public class HauntedWasteland : ChallengeBase<long>
{
    private readonly char[] _instructions;
    private readonly Nodes _nodes;

    public HauntedWasteland(string[] data) : base(data)
    { 
        _instructions = ChallengeDataRows[0].ToCharArray();
        _nodes = GetNodes();
    }

    protected override long Part1() 
        => GetSteps("AAA", n => n == "ZZZ");

    protected override long Part2()
        => _nodes
            .Where(n => n.Key.EndsWith("A"))
            .Select(node => GetSteps(node.Key, n => n.EndsWith("Z")))
            .LeastCommonMultiple();

    private long GetSteps(string node, Func<string, bool> foundCondition)
    {
        long steps = 0;

        do
        {
            foreach (var instruction in _instructions)
            {
                node = instruction == 'L'
                    ? _nodes[node].Left
                    : _nodes[node].Right;

                steps++;

                if (foundCondition(node)) break;
            }
        }
        while (foundCondition(node) == false);

        return steps;
    }

    private Nodes GetNodes()
    {
        var nodes = new Nodes();
        const string RegexPattern = @"([A-Z]{3})\s=\s\(([A-Z]{3}),\s([A-Z]{3})\)";

        foreach (var row in ChallengeDataRows.Skip(2))
        {
            var match = Regex.Match(row, RegexPattern);

            var value = match.Groups[1].Value;
            var leftNode = match.Groups[2].Value;
            var rightNode = match.Groups[3].Value;

            nodes.Add(value, new (leftNode, rightNode));
        }

        return nodes;
    }
}

public class Nodes : Dictionary<string, (string Left, string Right)>
{

}