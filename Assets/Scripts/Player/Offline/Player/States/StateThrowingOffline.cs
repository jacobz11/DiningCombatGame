using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using DiningCombat.Player.Manger;

namespace DiningCombat.Player.Offline.State
{
    internal class StateThrowingOffline : StateThrowing
    {
        public StateThrowingOffline(PlayerHand i_PickUpItem, OfflinePlayerStateMachine i_Machine) 
            : base(i_PickUpItem, i_Machine)
        {
        }
    }
}
