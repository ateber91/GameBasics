using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Contracts;

namespace Team8Project.Common
{
    public class Effects
    {
        public string Name { get; set; }
        public IHero Target { get; set; }
    }
}
