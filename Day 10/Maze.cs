namespace Day_10
{
    internal class Maze
    {
        private readonly Tile[][] _maze;
        private readonly HashSet<Tile> _northAdjacents = new[] { Tile.NorthSouth, Tile.SouthEast, Tile.SouthWest }.ToHashSet();
        private readonly HashSet<Tile> _eastAdjacents = new[] { Tile.EastWest, Tile.NorthWest, Tile.SouthWest }.ToHashSet();
        private readonly HashSet<Tile> _southAdjacents = new[] { Tile.NorthSouth, Tile.NorthWest, Tile.NorthEast }.ToHashSet();
        private readonly HashSet<Tile> _westAdjacents = new[] { Tile.EastWest, Tile.NorthEast, Tile.SouthEast }.ToHashSet();

        public Maze(string[] lines)
        {
            _maze = new Tile[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                _maze[i] = new Tile[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    _maze[i][j] = TileUtil.GetTile(lines[i][j]);
                }
            }

            Console.WriteLine("Maze constructed.");
        }

        public (int, int) FindStart()
        {
            for (int i = 0; i < _maze.Length; i++)
            {
                int col = Array.IndexOf(_maze[i], Tile.Start);
                if (col > -1)
                    return (i, col);
            }

            throw new NotSupportedException("No start in maze.");
        }

        public int FindFurthestDistance()
        {
            // Strategy: make the full loop, the total steps taken divided by 2 is the answer.
            (int startX, int startY) = FindStart();

            int previousX = startX;
            int previousY = startY;

            // First step
            List<(int, int)> routeStarts = GetNextTiles(startX, startY);
            int x = routeStarts[0].Item1;
            int y = routeStarts[0].Item2;

            int steps = 1;
            while (x != startX || y != startY)
            {
                (int newAX, int newAY) = GetNextTile(x, y, previousX, previousY);

                previousX = x;
                previousY = y;

                x = newAX; 
                y = newAY;

                steps++;
            }

            return steps / 2;
        }

        public (int, int) GetNextTile(int currentX, int currentY, int previousX, int previousY)
        {
            Tile currentTile = _maze[currentX][currentY];

            switch (currentTile)
            {
                case Tile.NorthSouth:
                    return currentX - 1 == previousX ? (currentX + 1, currentY) : (currentX - 1, currentY);
                case Tile.EastWest:
                    return currentY - 1 == previousY ? (currentX, currentY + 1) : (currentX, currentY - 1);
                case Tile.NorthEast:
                    return currentX - 1 == previousX ? (currentX, currentY + 1) : (currentX - 1, currentY);
                case Tile.NorthWest:
                    return currentX - 1 == previousX ? (currentX, currentY - 1) : (currentX - 1, currentY);
                case Tile.SouthEast:
                    return currentX + 1 == previousX ? (currentX, currentY + 1) : (currentX + 1, currentY);
                case Tile.SouthWest:
                    return currentX + 1 == previousX ? (currentX, currentY - 1) : (currentX + 1, currentY);
            }

            throw new ArgumentException($"{currentX}, {currentY} has no adjacent tiles to move to.");
        }

        public List<(int, int)> GetNextTiles(int currentX, int currentY)
        {
            List<(int, int)> adjacentTiles = new List<(int, int)>();

            if (currentX > 0 && _northAdjacents.Contains(_maze[currentX - 1][currentY]))
                adjacentTiles.Add((currentX - 1, currentY));
            if (currentY < _maze[currentX].Length && _eastAdjacents.Contains(_maze[currentX][currentY + 1]))
                adjacentTiles.Add((currentX, currentY + 1));
            if (currentX < _maze.Length && _southAdjacents.Contains(_maze[currentX + 1][currentY]))
                adjacentTiles.Add((currentX + 1, currentY));
            if (currentY > 0 && _westAdjacents.Contains(_maze[currentX][currentY - 1]))
                adjacentTiles.Add((currentX, currentY - 1));

            if (adjacentTiles.Count > 0)
                return adjacentTiles;

            throw new ArgumentException($"{currentX}, {currentY} has no adjacent tiles to move to.");
        }
    }
}
