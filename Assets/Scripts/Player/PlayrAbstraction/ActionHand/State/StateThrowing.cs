using Assets.Scripts.Player.Offline.Player.States;
using DiningCombat.Player;
using UnityEngine;

namespace Assets.Scripts.Player.PlayrAbstraction.ActionHand
{
    internal abstract class StateThrowing : IStatePlayerHand
    {
        public StateThrowing(PlayerHand i_PickUpItem, AcitonHandStateMachine i_Machine)
            : base(i_PickUpItem, i_Machine)
        {
        }

        internal void ThrowingPointObj()
        {
            Debug.Log("ThrowingPointObj");
        }

        public override void OnStateEnter(params object[] list)
        {
            base.OnStateEnter(list);
            Debug.Log(" StateThrowing init");
            this.m_PlayrHand.ThrowingAnimator = true;
        }

        public override void OnStateExit(params object[] list)
        {
        }

        public override void OnStateIK(params object[] list)
        {
        }

        public override void OnStateMove(params object[] list)
        {
        }

        public override void OnStateUpdate(params object[] list)
        {
        }


        public override string ToString()
        {
            return "StateThrowing : ";
        }
    }
}
