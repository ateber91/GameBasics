using System.Collections.Generic;
using System.Text;
using Team8Project.Contracts;

namespace Team8Project.Data
{
    public interface IDataContainer
    {
        IList<IHero> ListHeros { get; set; }
        StringBuilder Log { get; set; }
        IAbility SelectedAbility { get; set; }
    }
}