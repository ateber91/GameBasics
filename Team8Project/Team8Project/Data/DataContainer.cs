using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Contracts;

namespace Team8Project.Data
{
    public class DataContainer : IDataContainer
    {
        public DataContainer()
        {
            this.ListHeros = new List<IHero>();
            this.Log = new StringBuilder();
        }
        public StringBuilder Log { get; set; }
        public IList<IHero> ListHeros { get; set; }
        public IAbility SelectedAbility { get; set; }
        public bool EndGame { get; set; }


    }
}
