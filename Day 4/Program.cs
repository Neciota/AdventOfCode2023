using System.Runtime.CompilerServices;

string[] input = File.ReadAllLines("input.txt");

(HashSet<int>, int[]) GetCard(string line)
{
    string card = line.Split(':')[1];
    string[] numbers = card.Split('|');
    HashSet<int> winningNumbers = numbers[0].Split(' ')
        .Select(x => x.Trim())
        .Where(x => !string.IsNullOrEmpty(x))
        .Select(x => Convert.ToInt32(x))
        .ToHashSet();
    int[] ownedNumbers = numbers[1].Split(' ')
        .Select(x => x.Trim())
        .Where(x => !string.IsNullOrEmpty(x))
        .Select(x => Convert.ToInt32(x))
        .ToArray();
    return (winningNumbers, ownedNumbers);
}

int result = 0;
foreach (string line in input)
{
    (HashSet<int> winningNumbers, int[] ownedNumbers) = GetCard(line);

    int wins = ownedNumbers.Count(winningNumbers.Contains);

    if (wins > 1)
        result += Convert.ToInt32(Math.Pow(2, wins - 1));
    else if (wins == 1)
        result += 1;
}

Console.WriteLine(result);

int result2 = 205;

void ProcessCard(int cardIndex)
{
    (HashSet<int> winningNumbers, int[] ownedNumbers) = GetCard(input[cardIndex]);

    int wins = ownedNumbers.Count(winningNumbers.Contains);
    result2 += wins;

    for (int i = cardIndex + 1; i < cardIndex + 1 + wins; i++)
    {
        ProcessCard(i);
    }
}

for (int i = 0; i < input.Length; i++)
{
    ProcessCard(i);
}

Console.WriteLine(result2);