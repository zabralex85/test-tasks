namespace Google.T9Quiz.Lib.Classes.Common
{
    public static class Extensions
    {
        public static bool IsWithin(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }
}
