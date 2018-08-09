using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Contracts;

namespace Team8Project.Core
{
  public  class EffectChecker
    {
        private static EffectChecker instance = new EffectChecker();
        
        private EffectChecker()
        {

        }

       //public bool CheckForHpBoost


        public static EffectChecker Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
