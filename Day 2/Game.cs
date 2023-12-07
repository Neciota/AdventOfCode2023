namespace Day_2
{
    internal class Game
    {
        public Game(string line)
        {
            int colonIndex = line.IndexOf(':');
            Id = Convert.ToInt32(line.Substring(5, colonIndex - 5));

            string games = line.Substring(colonIndex + 1);
            string[] showings = games.Split(',', ';');
            Showings = showings.Select(showing =>
            {
                showing = showing.Trim();
                if (showing.EndsWith("blue"))
                    return (Color.Blue, Convert.ToInt32(showing.Substring(0, showing.IndexOf("blue"))));
                if (showing.EndsWith("red"))
                    return (Color.Red, Convert.ToInt32(showing.Substring(0, showing.IndexOf("red"))));
                if (showing.EndsWith("green"))
                    return (Color.Green, Convert.ToInt32(showing.Substring(0, showing.IndexOf("green"))));
                throw new ArgumentException("Invalid showing string.");
            });
        }


        public int Id { get; }
        public IEnumerable<(Color, int)> Showings { get; }
    }
}
