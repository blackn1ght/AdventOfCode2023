namespace AdventOfCode2023.Day09;

public class MirageMaintenanceTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 114)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 1980437560)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 2)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 977)] 
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(9, inputType);

        var answer = new MirageMaintenance(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }

    [Theory]
    [InlineData(ChallengePart.Part1, "0 3 6 9 12 15", 18)]
    [InlineData(ChallengePart.Part2, "10 13 16 21 30 45", 5)]
    public void ExampleSample(ChallengePart challengePart, string data, long expectedAnswer)
    {
        var answer = new MirageMaintenance(new[] { data }).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}