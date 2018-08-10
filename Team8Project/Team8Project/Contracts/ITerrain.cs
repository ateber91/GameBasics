using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team8Project.Contracts
{
    public interface ITerrain
    {
        void HeroEffect(IHero hero);
        void ContinuousEffect(IHero hero);
        bool IsDay { get; set; }
        string ToString();
    }
}
