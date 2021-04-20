namespace FindHiddenLines.Lib
{
    public readonly struct IntPoint
    {
        public readonly int X;
        public readonly int Y;

        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(IntPoint c1, IntPoint c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(IntPoint c1, IntPoint c2)
        {
            return !c1.Equals(c2);
        }
    }
}