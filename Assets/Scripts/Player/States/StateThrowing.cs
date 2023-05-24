using DiningCombat.FoodObject;
using DiningCombat.Util.DesignPatterns;
using System;
using UnityEngine;
namespace DiningCombat.Player.States
{
    public class StateThrowing : IStatePlayerHand
    {
        public const int k_Indx = 3;
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

        public void Powering_OnStopPowering(float i_NewPowerMull)
        {
            m_PowerMull = i_NewPowerMull;
        }
        #endregion
    }
}
