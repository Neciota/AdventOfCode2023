namespace Common
{
    public static class IntConstructor
    {
        public static int ToInt(this IEnumerable<int> digits)
        {
            int result = 0;
            int power = digits.Count() - 1;
            foreach (int digit in digits)
            {
                result += Convert.ToInt32(Math.Pow(10, power--)) * digit;
            }

            return result;
        }

        public static int Product(this IEnumerable<int> numbers)
        {
            if (!numbers.Any())
                throw new ArgumentException("No numbers present.");

            int result = numbers.ElementAt(0);
            foreach (int number in numbers.Skip(1))
            {
                result *= number;
            }
            return result;
        }
    }
}
