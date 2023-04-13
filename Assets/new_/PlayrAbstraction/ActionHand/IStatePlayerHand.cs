using Assets.Scripts.Player.PlayrAbstraction.ActionHand;
using Assets.Scripts.Util.Channels;
using DesignPatterns.Abstraction;
using DiningCombat;
using DiningCombat.Player;
using UnityEngine;

namespace Assets.Scripts.Player.Offline.Player.States
{
    internal abstract class IStatePlayerHand : IDCState
    {
        protected TimeBuffer m_Buffer;
        protected BridgeAbstractionAction m_PlayrHand;
        protected BridgeImplementorAcitonStateMachine m_Machine;
        protected IStatePlayerHand(BridgeAbstractionAction i_PlayrHand, BridgeImplementorAcitonStateMachine i_Machine)
        {
            this.m_PlayrHand = i_PlayrHand;
            this.m_Machine = i_Machine;
            m_Buffer = new TimeBuffer(1f);
        }
        public virtual void OnStateEnter(params object[] list)
        {
            m_Buffer.Clear();
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
