using DesignPatterns.Abstraction;

namespace DiningCombat.Player.Offline.State
{
    internal class StateThrowing : DCState
    {
        public StateThrowing(byte stateId, string stateName)
            : base(stateId, stateName)
        {
        }
        public virtual void OnStateEnter(params object[] list)
        {
            //this.playrHand.ThrowingAnimator = true;
        }



        public override string ToString()
        {
            return "StateThrowing : " + this.name;
        }
    }
}
