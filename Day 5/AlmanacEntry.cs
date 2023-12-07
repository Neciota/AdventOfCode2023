namespace Day_5
{
    internal class AlmanacEntry
    {
        public long DestinationStart { get; set; }
        public long SourceStart { get; set; }
        public long Length { get; set; }

        public long SourceEnd => SourceStart + Length;

        public bool InRange(long input)
        {
            return input >= SourceStart && input < SourceEnd;
        }

        public long Map(long input)
        {
            return (input - SourceStart) + DestinationStart;
        }
    }
}
