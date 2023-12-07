using Common;

string[] input = File.ReadAllLines("input.txt");

int[] times = input[0].Split(':')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).Select(x => Convert.ToInt32(x)).ToArray();
int[] distances = input[1].Split(':')[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).Select(x => Convert.ToInt32(x)).ToArray();

IEnumerable<(int, int)> races = times.Zip(distances);

bool BeatsDistance(int timeHeld, int raceTime, long distance)
{
    return (long)timeHeld * (raceTime - timeHeld) > distance;
}

int result = races.Select(x =>
{
    return Enumerable.Range(0, x.Item1).Count(y => BeatsDistance(y, x.Item1, x.Item2));
}).Product();

Console.WriteLine(result);

int time = Convert.ToInt32(input[0].Split(':')[1].Replace(" ", ""));
long distance = Convert.ToInt64(input[1].Split(':')[1].Replace(" ", ""));

int result2 = Enumerable.Range(0, time).Count(y => BeatsDistance(y, time, distance));

Console.WriteLine(result2);