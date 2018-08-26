using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team8Project.Common
{
    internal static class Constants
    {
        //hero
        internal const int NAME_MIN_LEN = 2;
        internal const int NAME_MAX_LEN = 20;
        internal const int HP_MIN = 1;
        internal const int HP_MAX = 500;
        internal const int MIN_CD = -2;
        internal const int MAX_CD = 6;
        internal const int MIN_ABILITYPOWER = 0;
        internal const int MAX_ABILITYPOWER = 100;

        internal const int MIN_TURN = 0;
        internal const int MAX_TURN = 1000;

        internal const string INITIAL_MESSAGE = "Choose a character:\n 1.{0}\n 2.{1}\n 3.{2}\n 4.{3}";
        internal const int LOG_ROW_POS = 12;
        internal const int LOG_COL_POS = 0;

    }
}
