using Day_7;

string[] input = File.ReadAllLines("input.txt");

IEnumerable<Hand> hands = input.Select(x =>
{
    string[] handInfo = x.Split(' ');
    IEnumerable<Card> cards = handInfo[0].Select(x => CardUtil.GetCard(x));
    int bet = Convert.ToInt32(handInfo[1]);
    return new Hand(cards, bet);
}).Order();

int rank = 1;
int result = hands.Select(x => rank++ * x.Bet).Sum();

Console.WriteLine(result);