namespace Assets.scrips.Player.States
{
    using DesignPatterns.Abstraction;
    using System;
    using UnityEngine;

    internal class StateThrowing : IStatePlayerHand
    {
        public const int k_Indx = 3;

        private float m_PowerMull;

        bool IStatePlayerHand.OnChargingAction { get => false; set { } }

        public event Action<bool> Throw;
        public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
        {
        }

        public void EnterCollisionFoodObj(Collider other)
        {
        }

        public void ExitCollisionFoodObj(Collider other)
        {
        }

        public void OnChargingAction()
        {
        }

        public bool OnPickUpAction(out GameFoodObj o_Collcted)
        {
            o_Collcted = null;
            return false;
        }

        public void OnSteteEnter()
        {
            Debug.Log("OnSteteEnter : StateThrowing");
        }

        public void OnSteteExit()
        {
            m_PowerMull = 0f;
        }

        public bool OnThrowPoint(out float o_Force)
        {
            o_Force = m_PowerMull;
            return true;
        }

        public void Update()
        {
        }

        internal void powering_OnStopPowering(float obj)
        {
            m_PowerMull = obj;
        }
    }
}
