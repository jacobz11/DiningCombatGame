using Assets.Scrips_new.DesignPatterns;

namespace Assets.Scrips_new.Player
{
    internal class StateThrowing : State
    {
        public override string ToString()
        {
            return "StateThrowing : " + this.name;
        }
    }
}
