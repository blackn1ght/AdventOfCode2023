namespace AdventOfCode2023.Day04;

public class ScratchcardsTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 13)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 32001)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 30)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 5037841)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(4, inputType);

        var answer = new Scratchcards(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }
}