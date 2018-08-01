using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Contracts;

namespace Team8Project.Providers
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
