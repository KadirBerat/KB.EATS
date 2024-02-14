namespace KB.EATS.WebUI.Models
{
    public static class FilterRequestRepo
    {
        private static ulong _count = 0;
        public static ulong Count { get => _count; }
        public static void IncrementCount() => _count++;
    }
}
