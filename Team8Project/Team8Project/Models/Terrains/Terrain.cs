using Team8Project.Contracts;


namespace Team8Project.Models.Terrains
{
    public abstract class Terrain : ITerrain
    {
        private bool isDay = true;
        public bool IsDay
        {
            get { return this.isDay; }
            set
            {
                this.isDay = value;
            }
        }
        public abstract void ApplyInitialEffect(IHero hero);
        public abstract string ContinuousEffect(IHero hero);
    }
}