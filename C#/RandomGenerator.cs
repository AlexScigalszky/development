using System;

namespace Example.Utils
{
    public class RandomGenerator
    {
        public static string NewRandomPassword(int length = 8)
        {
            var value = Guid.NewGuid().ToString().PadLeft(length);
            return value.Substring(0, length);
        }
    }
}
