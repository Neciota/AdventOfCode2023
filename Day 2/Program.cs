using Day_2;

string[] inputLines = File.ReadAllLines("input.txt");

IEnumerable<Game> games = inputLines.Select(line => new Game(line));

const int BLUE_MAX = 14;
const int RED_MAX = 12;
const int GREEN_MAX = 13;

int result = games.Where(game =>
{
    foreach ((Color color, int count) in game.Showings)
    {
        if (color == Color.Blue && count > BLUE_MAX)
            return false;
        else if (color == Color.Red && count > RED_MAX)
            return false;
        else if (color == Color.Green && count > GREEN_MAX)
            return false;
    }
    return true;
}).Sum(game => game.Id);

Console.WriteLine(result);

int result2 = games.Select(game =>
{
    int greenMin = game.Showings.Where(showing => showing.Item1 == Color.Green).Max(showing => showing.Item2);
    int redMin = game.Showings.Where(showing => showing.Item1 == Color.Red).Max(showing => showing.Item2);
    int blueMin = game.Showings.Where(showing => showing.Item1 == Color.Blue).Max(showing => showing.Item2);

    return greenMin * redMin * blueMin;
}).Sum();

Console.WriteLine(result2);