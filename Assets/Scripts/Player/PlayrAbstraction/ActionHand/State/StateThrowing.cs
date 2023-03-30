using Assets.Scripts.Player.Offline.Player.States;
using DiningCombat.Player.Manger;
using DiningCombat.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player.PlayrAbstraction.ActionHand
{
    internal abstract class StateThrowing : IStatePlayerHand
    {
        public StateThrowing(PlayerHand i_PickUpItem, AcitonHandStateMachine i_Machine)
            : base(i_PickUpItem, i_Machine)
        {
        }

        public override void OnStateEnter(params object[] list)
        {
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
