namespace AdventOfCode2023.Day08;

public class HauntedWastelandTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 2)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 11309)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 2)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 13740108158591)] 
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(8, inputType);

        var answer = new HauntedWasteland(data).GetAnswerForPart(challengePart);

        Assert.Equal(expectedAnswer, answer);
    }

    [Fact]
    public void Part1Example2()
    {
        var data = new string[]
        {
            "LLR",
            "",
            "AAA = (BBB, BBB)",
            "BBB = (AAA, ZZZ)",
            "ZZZ = (ZZZ, ZZZ)"
        };

        var answer = new HauntedWasteland(data).GetAnswerForPart(ChallengePart.Part1);

        Assert.Equal(6, answer);
    }

    [Fact]
    public void Part2Example2()
    {
        var data = new string[]
        {
            "LR",
            "",
            "KKA = (KKB, XXX)",
            "KKB = (XXX, KKZ)",
            "KKZ = (KKB, XXX)",
            "JJA = (JJB, XXX)",
            "JJB = (JJC, JJC)",
            "JJC = (JJZ, JJZ)",
            "JJZ = (JJB, JJB)",
            "XXX = (XXX, XXX)"
        };

        var answer = new HauntedWasteland(data).GetAnswerForPart(ChallengePart.Part2);

        Assert.Equal(6, answer);
    }
}