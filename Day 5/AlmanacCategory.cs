namespace Day_5
{
    internal class AlmanacCategory
    {
        public string Name { get; set; }
        public List<AlmanacEntry> Entries { get; set; }

        public long Map(long source)
        {
            AlmanacEntry? entry = Entries.Find(x => x.InRange(source));

            if (entry is null)
                return source;

            return entry.Map(source);
        }

        public List<(long, long)> Map(long inputStart, long inputRange)
        {
            List<(long, long)> outputMaps = new List<(long, long)>();
            Queue<(long, long)> unmappedRanges = new Queue<(long, long)>();
            unmappedRanges.Enqueue((inputStart, inputRange));

            while (unmappedRanges.Any())
            {
                (long unmappedStart, long unmappedRange) = unmappedRanges.Dequeue();
                bool mappedRange = false;

                foreach (AlmanacEntry entry in Entries)
                {
                    long overlapStart = Math.Max(unmappedStart, entry.SourceStart);
                    long overlapRange = Math.Min(unmappedStart + unmappedRange, entry.SourceStart + entry.Length) - overlapStart;

                    if (overlapRange < 1)
                        continue;

                    mappedRange = true;
                    outputMaps.Add((entry.DestinationStart, overlapRange));
                    // Split the unmapped range into the portions that remain unmapped and the portions that were mapped.
                    // Any range can be split into a range that was unmapped, then a range that was mapped, then a range that was unmapped again
                    long remainderBeforeStart = unmappedStart;
                    long remainderBeforeRange = overlapStart - unmappedStart;
                    if (remainderBeforeRange > 0)
                        unmappedRanges.Enqueue((remainderBeforeStart, remainderBeforeRange));

                    long remainderAfterStart = entry.SourceEnd;
                    long remainderAfterRange = unmappedStart + unmappedRange - remainderAfterStart;
                    if (remainderAfterRange > 0)
                        unmappedRanges.Enqueue((remainderAfterStart, remainderAfterRange));

                    break;
                }

                if (!mappedRange)
                    outputMaps.Add((unmappedStart, unmappedRange));
            }

            return outputMaps;
        }
    }
}
