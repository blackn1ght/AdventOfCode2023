namespace AdventOfCode2023.Day02;

public class CubeConondrumTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 8)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 2377)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 2286)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 71220)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(2, inputType);

        var answer = new CubeConondrum(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}