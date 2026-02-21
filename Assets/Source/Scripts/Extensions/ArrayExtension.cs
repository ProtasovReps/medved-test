namespace Extensions
{
    public static class ArrayExtension
    {
        private const int Step = 1;

        public static int GetCycledIndex<T>(this T[] array, int currentIndex)
        {
            return (currentIndex + Step) % array.Length;
        }
    }
}