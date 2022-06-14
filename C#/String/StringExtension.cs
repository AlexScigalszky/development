namespace Example.Utils
{
    public static class StringExtension
    {
        public static bool IsValidDate(this string value)
        {
            return value != "  /  /" && value != "";
        }

        public static bool IsEmptyDate(this string value)
        {
            return value == "  /  /" ||
                value == "  /  /  ";
        }

        public static bool Is1900(this string value)
        {
            return value == "00/01/1900";
        }

        public static bool Is0001(this string value)
        {
            return value == "01/01/0001";
        }
    }
}
