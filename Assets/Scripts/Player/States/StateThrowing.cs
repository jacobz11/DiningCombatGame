using DesignPatterns.Abstraction;
using System;
using UnityEngine;

// TODO : to fix the namespace
namespace Assets.scrips.Player.States
{
    internal class StateThrowing : IStatePlayerHand
    {
        public const int k_Indx = 3;
        public event Action<bool> Throw;
        private float m_PowerMull;
        bool IStatePlayerHand.OnChargingAction { get => false; set { } }
        #region Not-Implemented
        public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
        {/* Not-Implemented */}
        public void EnterCollisionFoodObj(Collider other)
        {/* Not-Implemented */}
        public void ExitCollisionFoodObj(Collider other)
        {/* Not-Implemented */}
        public void OnChargingAction()
        {/* Not-Implemented */}
        public virtual void Update()
        {/* Not-Implemented */}
        #endregion
        #region when events occur
        public virtual void OnStateEnter()
        {
            Debug.Log("OnSteteEnter : StateThrowing");
        }

        public virtual void OnStateExit()
        {
            m_PowerMull = 0f;
        }

        public bool OnPickUpAction(out GameFoodObj o_Collcted)
        {
            o_Collcted = null;
            return false;
        }

        public bool OnThrowPoint(out float o_Force)
        {
            o_Force = m_PowerMull;
            return true;
        }

        internal void powering_OnStopPowering(float obj)
        {
            m_PowerMull = obj;
        }
        #endregion
    }
}
