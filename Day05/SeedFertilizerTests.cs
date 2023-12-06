namespace AdventOfCode2023.Day05;

public class SeedFertilizerTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 35)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 388071289)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 46)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 84206669)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(5, inputType);

        var answer = new SeedFertilizer(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}