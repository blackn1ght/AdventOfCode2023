namespace AdventOfCode2023.Day05;

public class SeedFertilizerTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 35)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 388071289)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 46)]
    //[InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(5, inputType);

        var answer = new SeedFertilizer(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }

    [Fact]
    public void FlattenRanges()
    {
        var ranges = new List<LongRange>
        {
            new (5, 10),
            new (3, 8),
            new (9,12),
            new (1, 2),
            new (13, 19)
        };
        var results = Flattener.FlattenRanges(ranges);

        Assert.Equal(3, results.Count());
        Assert.Equal(3, results.First().Start);
        Assert.Equal(12, results.First().End);
    }
}