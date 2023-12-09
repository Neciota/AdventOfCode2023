using Day_8;

string[] input = File.ReadAllLines("input.txt");

Step[] steps = input[0].Select(c => StepUtil.CharToStep(c)).ToArray();

Network network = new Network(input.Skip(2));
Console.WriteLine(network.StepsToEnd(steps));