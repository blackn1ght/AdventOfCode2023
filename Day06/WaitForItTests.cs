namespace AdventOfCode2023.Day06;

public class WaitForItTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 288)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 449550)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 71503)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 28360140)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(6, inputType);

        var answer = new WaitForIt(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}