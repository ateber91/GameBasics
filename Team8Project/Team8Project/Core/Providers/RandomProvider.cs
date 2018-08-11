using System;

namespace Team8Project.Core.Providers
{
    public static class RandomProvider
    {
        private static Random random = new Random();
        public static int Generate(int start, int end)
        {
            int randomDmg = random.Next(start, end + 1);
            return randomDmg;
        }
    }
}
