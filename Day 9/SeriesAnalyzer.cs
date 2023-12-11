namespace Day_9
{
    internal static class SeriesAnalyzer
    {
        public static long GetNextValue(IEnumerable<long> series)
        {
            IEnumerable<long> differences = series.SkipLast(1).Zip(series.Skip(1)).Select(x => x.Second - x.First);

            long nextValue = differences.Any(x => x != 0) ? GetNextValue(differences) : 0;

            return series.Last() + nextValue;
        }
    }
}
