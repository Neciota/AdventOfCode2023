using Day_5;

string[] input = File.ReadAllLines("input.txt");

List<long> seeds = input[0].Split(':')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt64(x)).ToList();

List<AlmanacCategory> categories = new List<AlmanacCategory>();
AlmanacCategory? categoryToMap = null;
foreach (string line in input)
{
    if (string.IsNullOrEmpty(line))
        continue;

    if (line.Contains("map"))
    {
        categoryToMap = new AlmanacCategory()
        {
            Name = line,
            Entries = new List<AlmanacEntry>()
        };
        categories.Add(categoryToMap);
    } else if (categoryToMap is null)
    {
        continue;
    }
    else
    {
        long[] values = line.Split(' ').Select(x => Convert.ToInt64(x)).ToArray();
        categoryToMap.Entries.Add(new AlmanacEntry()
        {
            DestinationStart = values[0],
            SourceStart = values[1],
            Length = values[2]
        });
    }
}

foreach (AlmanacCategory category in categories)
{
    category.Entries = category.Entries.OrderBy(x => x.SourceStart).ToList();
}

long lowestLocation = long.MaxValue;

foreach (long seed in seeds)
{
    long nextValue = seed;
    foreach (AlmanacCategory category in categories)
    {
        nextValue = category.Map(nextValue);
    }

    if (nextValue < lowestLocation)
        lowestLocation = nextValue;
}

Console.WriteLine(lowestLocation);

List<(long, long)> seedRangePairs = new List<(long, long)>();
for (int i = 0; i < seeds.Count; i += 2)
{
    seedRangePairs.Add((seeds[i], seeds[i + 1]));
}

List<(long, long)> nextInputToProcess = seedRangePairs;
foreach (AlmanacCategory category in categories)
{
    List<(long, long)> resultingInputs = new List<(long, long)>();
    foreach ((long inputStart, long inputRange) in nextInputToProcess)
    {
        resultingInputs.AddRange(category.Map(inputStart, inputRange));
    }

    nextInputToProcess = resultingInputs;
}

Console.WriteLine(nextInputToProcess.Min(x => x.Item1));

//long GetLocation(long seed)
//{
//    long nextValue = seed;
//    foreach (AlmanacCategory category in categories)
//    {
//        nextValue = category.Map(nextValue);
//    }
//    return nextValue;
//}

//Console.WriteLine(seeds.Select(GetLocation).Min());