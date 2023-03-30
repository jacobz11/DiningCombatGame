using Assets.Scripts.Player.Offline.Player.States;
using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using DesignPatterns.Abstraction;
using DiningCombat.Player.Manger;
using System;
using UnityEngine;

namespace DiningCombat.Player.Offline.State
{
    internal class StateHoldsObjOffline : StateHoldsObj 
    {
        public StateHoldsObjOffline(PlayerHand i_PickUpItem, OfflinePlayerStateMachine i_Machine)
            : base(i_PickUpItem, i_Machine)
        {
        }

        protected override bool IsPassStage()
        {
            return this.IsPowerKeyPress;
        }
    }
}
