namespace Day_8
{
    internal static class LeastCommonMultiple
    {
        private static int[] _basicPrimes = new[]
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131,137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541
        };

        public static long GetLCM(IEnumerable<long> numbers)
        {
            int[] primeOccurence = new int[_basicPrimes.Length];

            IEnumerable<List<int>> primeFactorsPerNumber = numbers.Select(PrimeFactorize);
            
            for (int i = 0; i < primeOccurence.Length; i++)
            {
                foreach (List<int> primeFactors in primeFactorsPerNumber)
                {
                    int count = primeFactors.Count(prime => prime == _basicPrimes[i]);
                    if (count > primeOccurence[i])
                        primeOccurence[i] = count;
                }
            }

            long factorOfOccurences = 1;
            for (int i = 0; i < _basicPrimes.Length; i++)
            {
                if (primeOccurence[i] > 0)
                    factorOfOccurences *= Convert.ToInt32(Math.Pow(_basicPrimes[i], primeOccurence[i]));
            }
            return factorOfOccurences;
        }

        public static List<int> PrimeFactorize(long number)
        {
            List<int> primeFactors = new List<int>();

            while (number != 1)
            {
                foreach (int prime in _basicPrimes)
                {
                    if (number % prime == 0)
                    {
                        primeFactors.Add(prime);
                        number /= prime;
                        break;
                    }
                }
            }

            return primeFactors;
        }
    }
}
