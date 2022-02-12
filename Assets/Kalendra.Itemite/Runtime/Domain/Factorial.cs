namespace Kalendra.Itemite.Runtime.Domain
{
    public static class Factorial
    {
        public static int Fact(this int n)
        {
            return Of(n);
        }

        public static int Of(int n)
        {
            if(n <= 1)
                return 1;

            return n * Of(n - 1);
        }
    }
}