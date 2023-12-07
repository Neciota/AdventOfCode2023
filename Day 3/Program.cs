using Common;

string[] schematic = File.ReadAllLines("input.txt");
HashSet<char> symbols = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '[', '{', ']', '}', ':', ';', '\'', '"', '\\', '|', ',', '<', '>', '/', '?'}.ToHashSet();

List<(int, int)> GetAdjacents(int line, int index)
{
    List<(int, int)> positions = new List<(int, int)>();
    if (line > 0 && index > 0)
        positions.Add((line - 1, index - 1));
    if (line > 0 && index < schematic[line].Length - 1)
        positions.Add((line - 1, index + 1));
    if (line > 0)
        positions.Add((line - 1, index));

    if (line < schematic.Length - 1 && index > 0)
        positions.Add((line + 1, index - 1));
    if (line < schematic.Length - 1 && index < schematic[line].Length - 1)
        positions.Add((line + 1, index + 1));
    if (line < schematic.Length - 1)
        positions.Add((line + 1, index));

    if (index > 0)
        positions.Add((line, index - 1));
    if (index < schematic[line].Length - 1)
        positions.Add((line, index + 1));

    return positions;
}

bool HasAdjacentSymbol(int line, int index)
{
    foreach ((int i, int j) in GetAdjacents(line, index))
    {
        if (symbols.Contains(schematic[i][j]))
            return true;
    }

    return false;
}

int result = 0;

for (int i = 0; i < schematic.Length; i++)
{
    bool symbolNeighbor = false;
    List<int> digits = new List<int>();
    for (int j = 0; j < schematic[i].Length; j++)
    {
        bool isNumber = CharConverter.CharToDigit(schematic[i][j], out int digit);

        if (isNumber && HasAdjacentSymbol(i, j))
            symbolNeighbor = true;

        if (symbolNeighbor && !isNumber)
        {
            symbolNeighbor = false;
            result += digits.ToInt();
            digits.Clear();
        } else if (isNumber)
        {
            digits.Add(digit);
        }
        else
        {
            symbolNeighbor = false;
            digits.Clear();
        }
    }

    if (symbolNeighbor)
    {
        result += digits.ToInt();
        digits.Clear();
    }
}

Console.WriteLine(result);

List<(int, int)> gearPositions = new List<(int, int)>();
for (int i = 0; i < schematic.Length; i++)
{
    for (int j = 0; j < schematic[i].Length; j++)
    {
        if (schematic[i][j] == '*')
            gearPositions.Add((i, j));
    }
}

char[] symbolArray = symbols.Append('.').ToArray();
int result2 = 0;

IEnumerable<int> ParseSegmentForInts(string segment)
{
    if (segment.Length > 7)
        throw new ArgumentException("Segment exceeds expected length.");

    List<int> result = new List<int>();
    List<int> digits = new List<int>();
    for (int i = 0; i < segment.Length; i++)
    {
        if (CharConverter.CharToDigit(segment[i], out int digit))
        {
            digits.Add(digit);
        }
        else
        {
            if (i > 2 && i - digits.Count < 5 && digits.Count > 0)
            {
                int adjacentNumber = digits.ToInt();
                result.Add(adjacentNumber);
            }
            digits.Clear();
        }
    }

    if (digits.Count > 2)
    {
        int adjacentNumber = digits.ToInt();
        result.Add(adjacentNumber);
    }

    return result;
}

foreach ((int line, int number) in gearPositions)
{
    List<int> numbers = new List<int>();
    if (line > 0)
    {
        numbers.AddRange(ParseSegmentForInts(schematic[line - 1].Substring(number - 3, 7)));
    }
    if (line < schematic.Length - 1)
    {
        numbers.AddRange(ParseSegmentForInts(schematic[line + 1].Substring(number - 3, 7)));
    }
    if (number > 0)
    {
        string? adjacentNumber = schematic[line].Substring(Math.Max(number - 3, 0), Math.Min(number, 3)).Split(symbolArray).Last();
        if (adjacentNumber != "")
        {
            numbers.Add(Convert.ToInt32(adjacentNumber));
        }
    }
    if (number < schematic[line].Length)
    {
        string? adjacentNumber = schematic[line].Substring(number + 1, Math.Min(schematic[line].Length - 1 - number, 3)).Split(symbolArray).First();
        if (adjacentNumber != "")
        {
            numbers.Add(Convert.ToInt32(adjacentNumber));
        }
    }

    if (numbers.Count != 2)
        continue;

    result2 += numbers.Product();
}

Console.WriteLine(result2);