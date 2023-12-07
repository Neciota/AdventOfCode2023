namespace Common
{
    public static class CharConverter
    {
        public static bool CharToDigit(char c, out int digit)
        {
            digit = 0;
            if (c < 48 || c > 57)
                return false;

            digit = (int)c - 48;
            return true;
        }
    }
}
