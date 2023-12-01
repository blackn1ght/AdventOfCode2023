namespace AdventOfCode2023.Day01;

public class TrebuchetTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 142)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 54927)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 142)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 54581)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(1, inputType);

        var answer = new Trebuchet(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }

    
    [Fact]
    public void Part2Example()
    {
        var data = new string[] 
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        };

        var answer = new Trebuchet(data).GetAnswerForPart(ChallengePart.Part2);

        Assert.Equal(281, answer);
    }
}