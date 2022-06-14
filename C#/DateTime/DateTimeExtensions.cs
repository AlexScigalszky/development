namespace Example.Utils
{
    public static class DateTimeExtensions
    {
        public static DateTime? ParseOrNull(string text)
        {
            return DateTime.TryParse(text, out DateTime date)
                ? date
                : null;
        }

        public static DateTime ParseOrMinValue(string text)
        {
            return DateTime.TryParse(text, out DateTime date)
                ? date
                : DateTime.MinValue;
        }
    }
}
