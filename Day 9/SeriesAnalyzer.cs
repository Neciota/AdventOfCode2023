namespace Day_9
{
    internal static class SeriesAnalyzer
    {
        public static long GetNextValue(IEnumerable<long> series)
        {
            IEnumerable<long> differences = GetDifferences(series);

            long nextValue = differences.Any(x => x != 0) ? GetNextValue(differences) : 0;

            return series.Last() + nextValue;
        }

        public static long GetPreviousValue(IEnumerable<long> series)
        {
            IEnumerable<long> differences = GetDifferences(series);

            long previousValue = differences.Any(x => x != 0) ? GetPreviousValue(differences) : 0;

            return series.First() - previousValue;
        }

        public static IEnumerable<long> GetDifferences(IEnumerable<long> series) => series.SkipLast(1).Zip(series.Skip(1)).Select(x => x.Second - x.First);
    }
}
