using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using DiningCombat.Player;
using DiningCombat.Player.Manger;


namespace Assets.Scripts.Player.Offline.AI.Stats
{
    internal class StateThrowingOfflineAI : StateThrowing
    {
        public StateThrowingOfflineAI(PlayerHand i_PickUpItem, OfflineAIStateMachine i_Machine) 
            : base(i_PickUpItem, i_Machine)
        {
        }
    }
}
