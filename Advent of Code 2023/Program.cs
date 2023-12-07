using Common;

string[] inputLines = File.ReadAllLines("input.txt");

bool SubstringToDigit(string x, out int digit)
{
    if (x.Contains("one"))
    {
        digit = 1;
        return true;
    } else if (x.Contains("two"))
    {
        digit = 2;
        return true;
    } else if (x.Contains("three"))
    {
        digit = 3;
        return true;
    } else if (x.Contains("four"))
    {
        digit = 4;
        return true;
    } else if (x.Contains("five"))
    {
        digit = 5;
        return true;
    } else if (x.Contains("six"))
    {
        digit = 6;
        return true;
    } else if (x.Contains("seven"))
    {
        digit = 7;
        return true;
    } else if (x.Contains("eight"))
    {
        digit = 8;
        return true;
    } else if (x.Contains("nine"))
    {
        digit = 9;
        return true;
    }

    digit = 0;
    return false;
}

int FirstDigit(string x)
{
    for (int i = 0; i < x.Length; i++)
    {
        char c = x[i];
        if (SubstringToDigit(x.Substring(0, i), out int digit))
            return digit;
        else if (CharConverter.CharToDigit(c, out digit))
            return digit;
    }

    throw new ArgumentException("No digits in string");
}

int LastDigit(string x)
{
    for (int i = x.Length - 1; i >= 0; i--)
    {
        char c = x[i]; 
        if (SubstringToDigit(x.Substring(i), out int digit))
            return digit;
        else if (CharConverter.CharToDigit(c, out digit))
            return digit;
    }

    throw new ArgumentException("No digits in string");
};

int answer = inputLines.Select(line =>
{
    return FirstDigit(line) * 10 + LastDigit(line);
}).Sum();

Console.WriteLine(answer);