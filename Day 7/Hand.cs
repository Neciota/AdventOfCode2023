namespace Day_7
{
    internal class Hand : IComparable<Hand>
    {
        private const int HAND_SIZE = 5;

        public Hand(IEnumerable<Card> cards, int bet)
        {
            if (cards.Count() != HAND_SIZE)
                throw new ArgumentException("Hands should be 5 cards.");

            Cards = cards.ToArray();
            Bet = bet;
        }

        public Card[] Cards { get; private set; }
        public int Bet { get; private set; }
        public int Rank { get; set; }

        public int CompareTo(Hand? other)
        {
            if (other is null)
                return 1;

            HandRank ownRank = GetHandRank();
            HandRank otherRank = other.GetHandRank();
            if (ownRank > otherRank)
                return 1;
            else if (ownRank < otherRank)
                return -1;

            // High card rules
            for (int i = 0; i < HAND_SIZE; i++)
            {
                if (Cards[i] > other.Cards[i])
                    return 1;
                else if (Cards[i] < other.Cards[i])
                    return -1;
            }

            return 0;
        }

        public HandRank GetHandRank()
        {
            int cardSize = Enum.GetValues(typeof(Card)).Length;
            int[] cardCounts = new int[cardSize];
            
            foreach (Card card in Cards)
            {
                cardCounts[(int)card]++;
            }

            if (Array.Exists(cardCounts, x => x == 5))
                return HandRank.FiveOfAKind;
            if (Array.Exists(cardCounts, x => x == 4))
                return HandRank.FourOfAKind;
            if (Array.Exists(cardCounts, x => x == 3) && Array.Exists(cardCounts, x => x == 2))
                return HandRank.FullHouse;
            if (Array.Exists(cardCounts, x => x == 3))
                return HandRank.ThreeOfAKind;
            if (cardCounts.Count(x => x == 2) == 2)
                return HandRank.TwoPair;
            if (cardCounts.Count(x => x == 2) == 1)
                return HandRank.OnePair;

            return HandRank.HighCard;
        }

        public override string ToString()
        {
            return string.Concat(Cards.Select(x => x.GetCard()));
        }
    }
}
