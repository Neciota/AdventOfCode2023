namespace Day_7
{
    internal enum Card
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Queen = 12,
        King = 13,
        Ace = 14,
#if PART_2
        Jack = 1
#else
        Jack = 11
#endif
    }
}
