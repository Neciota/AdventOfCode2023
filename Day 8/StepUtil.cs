namespace Day_8
{
    internal static class StepUtil
    {
        public static Step CharToStep(char c) => c switch
        {
            'L' => Step.Left,
            'R' => Step.Right,
            _ => throw new ArgumentException($"{c} is not a correct step direction.")
        };
    }
}
