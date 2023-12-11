using Day_10;

string[] input = File.ReadAllLines("input.txt");

Maze maze = new Maze(input);

int steps = maze.FindFurthestDistance();
Console.WriteLine(steps);