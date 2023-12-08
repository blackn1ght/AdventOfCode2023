namespace AdventOfCode2023.Day07;

public class CamelCards : ChallengeBase<long>
{
    public CamelCards(string[] data) : base(data)
    { 
    }

    protected override long Part1()
    {
        var hands = GetHands();

        var swapped = false;

        do
        {
            swapped = false;

            for (var i = 0; i < hands.Count - 1; i++)
            {
                var thisHand = hands[i];
                var otherHand = hands[i+1];

                if (thisHand.GetHandType() == otherHand.GetHandType())
                {
                    var compareResult = thisHand.Compare(otherHand);
                    if (compareResult == 1)
                    {
                        hands[i] = otherHand;
                        hands[i+1] = thisHand;
                        swapped = true;
                    }
                }
            }
        }
        while (swapped);

        return hands.Select((hand, index) => hand.Score * (index + 1)).Sum();
    }
    protected override long Part2() => 0;

    private List<Hand> GetHands()
        => ChallengeDataRows
            .Select(row => row.Split(' '))
            .Select(parts => new Hand(parts[0].ToCharArray(), int.Parse(parts[1])))
            .OrderBy(hand => (int)hand.GetHandType())
            .ToList();
}

public enum HandType
{
    FiveOfAKind = 6,
    FourOfAKind = 5,
    FullHouse = 4,
    ThreeOfAKind = 3,
    TwoPair = 2,
    HighCard = 1,
    OnePair = 0
}

public record Hand(char[] Cards, int Score)
{
    public HandType GetHandType()
    {
        var grouping = Cards.GroupBy(card => card);
        var groupCount = grouping.Count();

        return groupCount switch
        {
            1 => HandType.FiveOfAKind,
            2 when grouping.Any(g => g.Count() == 4) => HandType.FourOfAKind,
            2 when grouping.Any(g => g.Count() == 3) && grouping.Count(g => g.Count() == 2) == 1 => HandType.FullHouse,
            3 when grouping.Any(g => g.Count() == 3) && grouping.Count(g => g.Count() == 1) == 2 => HandType.ThreeOfAKind,
            3 when grouping.Count(g => g.Count() == 2) == 2 => HandType.TwoPair,
            4 => HandType.OnePair,
            _ => HandType.HighCard
        };
    }

    /// <summary>
    ///  if other hand is bigger, return -1
    ///  if this hand is bigger, return 1
    ///  if hands are identical, return 0
    /// </summary>
    /// <param name="otherHand"></param>
    /// <returns></returns>
    public int Compare(Hand otherHand)
    {
        var cardScores = new List<char>{ '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'}; 

        var thisHandType = GetHandType();
        var otherHandType = otherHand.GetHandType();

        if (thisHandType > otherHandType) return 1;
        if (thisHandType < otherHandType) return -1;

        for (var i = 0; i < Cards.Length; i++)
        {
            var thisCardIndex = cardScores.IndexOf(Cards[i]);
            var otherCardIndex = cardScores.IndexOf(otherHand.Cards[i]);

            if (thisCardIndex > otherCardIndex) return 1;
            if (thisCardIndex < otherCardIndex) return -1;
        }

        return 0;
    }
}