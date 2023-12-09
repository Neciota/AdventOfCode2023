namespace Day_7
{
    internal static class CardUtil
    {
        public static Card GetCard(char c) => c switch
        {
            'A' => Card.Ace,
            'K' => Card.King,
            'Q' => Card.Queen,
            'J' => Card.Jack,
            'T' => Card.Ten,
            '9' => Card.Nine,
            '8' => Card.Eight,
            '7' => Card.Seven,
            '6' => Card.Six,
            '5' => Card.Five,
            '4' => Card.Four,
            '3' => Card.Three,
            '2' => Card.Two,
            _ => throw new ArgumentException("Character does not represent a card.")
        };

        public static char GetCard(this Card card) => card switch
        {
            Card.Ace => 'A',
            Card.King => 'K',
            Card.Queen => 'Q',
            Card.Jack => 'J',
            Card.Ten => 'T',
            Card.Nine => '9',
            Card.Eight => '8',
            Card.Seven => '7',
            Card.Six => '6',
            Card.Five => '5',
            Card.Four => '4',
            Card.Three => '3',
            Card.Two => '2',
            _ => throw new ArgumentException("Card does have a character representation.")
        };
    }
}
