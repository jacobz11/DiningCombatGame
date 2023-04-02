using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using DesignPatterns.Abstraction;
using DiningCombat.Player;
using DiningCombat.Player.Manger;
using UnityEngine;

namespace Assets.Scripts.Player.Offline.Player.States
{
    internal abstract class IStatePlayerHand : IDCState
    {
        protected BridgeAbstractionAction m_PlayrHand;
        protected BridgeImplementorAcitonStateMachine m_Machine;
        private float initTimeEnteState;

        protected bool IsBufferTime => Time.time - this.initTimeEnteState > 0.5f;
        protected IStatePlayerHand(BridgeAbstractionAction i_PickUpItem, BridgeImplementorAcitonStateMachine i_Machine)
        {
            this.m_PlayrHand = i_PickUpItem;
            this.m_Machine = i_Machine;
        }
        public bool IsPowerKeyPress => Input.GetKey(KeyCode.E);

        public virtual void OnStateEnter(params object[] list)
        {
            this.initTimeEnteState = Time.time;
        }
        public abstract void OnStateExit(params object[] list);
        public virtual void OnStateIK(params object[] list)
        {

        }
        public virtual void OnStateMove(params object[] list)
        {

        }
        public abstract void OnStateUpdate(params object[] list);

        public virtual void EnterCollisionFoodObj(Collider other)
        {
        }

        public virtual void ExitCollisionFoodObj(Collider other)
        {
        }
    }
}
