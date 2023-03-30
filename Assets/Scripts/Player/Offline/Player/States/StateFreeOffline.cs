using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using DiningCombat.Player.Manger;
using UnityEngine;

namespace DiningCombat.Player.Offline.State
{
    internal class StateFreeOffline : StateFree
    {
        public StateFreeOffline(PlayerHand i_PickUpItem, OfflinePlayerStateMachine i_Machine) 
            : base(i_PickUpItem, i_Machine)
        {
        }

        protected override bool IsPassStage()
        {
            return this.HaveGameObject && this.IsPowerKeyPress;
        }
    }
}