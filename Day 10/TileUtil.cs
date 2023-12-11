namespace Day_10
{
    internal static class TileUtil
    {
        public static Tile GetTile(char c) => c switch
        {
            '|' => Tile.NorthSouth,
            '-' => Tile.EastWest,
            'L' => Tile.NorthEast,
            'J' => Tile.NorthWest,
            '7' => Tile.SouthWest,
            'F' => Tile.SouthEast,
            '.' => Tile.Ground,
            'S' => Tile.Start,
            _ => throw new ArgumentException($"{c} is not a valid tile character.")
        };
    }
}
