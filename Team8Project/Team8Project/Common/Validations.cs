using System;

namespace Team8Project.Common
{
    public class Validations
    {
        public static void ValidateLength(string value, int min, int max, string message)
        {
            if (value.Length < min || value.Length > max)
            {
                throw new ArgumentException(message);
            }
        }
        public static void ValidateRangeNumbers(int value, int min, int max, string message)
        {
            if (value < min || value > max)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
