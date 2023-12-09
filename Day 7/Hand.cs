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
            Rank = GetHandRank();
        }

        public Card[] Cards { get; private set; }
        public int Bet { get; private set; }
        public HandRank Rank { get; private set; }

        public int CompareTo(Hand? other)
        {
            if (other is null)
                return 1;

            if (Rank > other.Rank)
                return 1;
            else if (Rank < other.Rank)
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
            int[] cardCounts = new int[15];

            foreach (Card card in Cards)
            {
                cardCounts[(int)card]++;
            }

#if PART_2
            int jackCounts = cardCounts[(int)Card.Jack];
            cardCounts = cardCounts.Skip(2).ToArray();
#endif

            HandRank handRank = HandRank.HighCard;
            if (Array.Exists(cardCounts, x => x == 5))
                handRank = HandRank.FiveOfAKind;
            else if (Array.Exists(cardCounts, x => x == 4))
                handRank = HandRank.FourOfAKind;
            else if (Array.Exists(cardCounts, x => x == 3) && Array.Exists(cardCounts, x => x == 2))
                handRank = HandRank.FullHouse;
            else if (Array.Exists(cardCounts, x => x == 3))
                handRank = HandRank.ThreeOfAKind;
            else if (cardCounts.Count(x => x == 2) == 2)
                handRank = HandRank.TwoPair;
            else if (cardCounts.Count(x => x == 2) == 1)
                handRank = HandRank.OnePair;

#if PART_2
            switch (handRank)
            {
                case HandRank.FourOfAKind:
                    if (jackCounts == 1)
                        handRank = HandRank.FiveOfAKind;
                    break;
                case HandRank.ThreeOfAKind:
                    if (jackCounts == 2)
                        handRank = HandRank.FiveOfAKind;
                    else if (jackCounts == 1)
                        handRank = HandRank.FourOfAKind;
                    break;
                case HandRank.TwoPair:
                    if (jackCounts == 1)
                        handRank = HandRank.FullHouse;
                    break;
                case HandRank.OnePair:
                    if (jackCounts == 3)
                        handRank = HandRank.FiveOfAKind;
                    else if (jackCounts == 2)
                        handRank = HandRank.FourOfAKind;
                    else if (jackCounts == 1)
                        handRank = HandRank.ThreeOfAKind;
                    break;
                case HandRank.HighCard:
                    if (jackCounts == 5)
                        handRank = HandRank.FiveOfAKind;
                    else if (jackCounts == 4)
                        handRank = HandRank.FiveOfAKind;
                    else if (jackCounts == 3)
                        handRank = HandRank.FourOfAKind;
                    else if (jackCounts == 2)
                        handRank = HandRank.ThreeOfAKind;
                    else if (jackCounts == 1)
                        handRank = HandRank.OnePair;
                    break;
            }
#endif
            return handRank;
        }

        public override string ToString()
        {
            return string.Concat(Cards.Select(x => x.GetCard()));
        }
    }
}
