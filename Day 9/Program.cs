using Day_9;

string[] input = File.ReadAllLines("input.txt");
long[][] serieses = input.Select(x => x.Split(' ').Select(y => Convert.ToInt64(y)).ToArray()).ToArray();

long result = serieses.Select(SeriesAnalyzer.GetNextValue).Sum();
Console.WriteLine(result);