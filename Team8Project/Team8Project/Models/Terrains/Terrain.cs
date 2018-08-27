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
        public abstract string ContinuousEffect(IHero hero);
        public abstract void ApplyInitialWarriorEffect(IHero hero);
        public abstract void ApplyInitialAssasinEffect(IHero hero);
        public abstract void ApplyInitialClericEffect(IHero hero);
        public abstract void ApplyInitialMageEffect(IHero hero);
    }
}