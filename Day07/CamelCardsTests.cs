namespace AdventOfCode2023.Day07;

public class CamelCardsTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 6440)]
    //[InlineData(ChallengePart.Part1, InputTypes.Input, 0)] // not 253152430, 253544686 -- too low, 254384197 -- too high
    // [InlineData(ChallengePart.Part2, InputTypes.Example, 0)]
    // [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(7, inputType);

        var answer = new CamelCards(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }

    [Fact]
    public void Part1InputShouldGiveCorrectAnswer()
    {
        var data = ChallengeDataReader.GetDataForDay(7, InputTypes.Input);

        var answer = new CamelCards(data).GetAnswerForPart(ChallengePart.Part1);

        Assert.True(answer > 253152430, "answer is less or equal than 253152430");
        Assert.True(answer > 253544686, "answer is less or equal than 253544686");
        Assert.True(answer < 254384197, "answer is greater or equal than 254384197");
        Assert.Equal(0, answer);
    }
}