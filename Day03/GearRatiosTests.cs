namespace AdventOfCode2023.Day03;

public class GearRatiosTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 4361)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 528819)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 467835)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 80403602)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(3, inputType);

        var answer = new GearRatios(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}