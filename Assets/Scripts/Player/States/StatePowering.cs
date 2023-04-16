namespace Assets.scrips.Player.States
{
    using Assets.scrips.Player.Data;
    using DesignPatterns.Abstraction;
    using System;
    using UnityEngine;

    internal class StatePowering : IStatePlayerHand
    {
        public const int k_Indx = 2;

        public event Action<float> OnPoweringNormalized;
        public event Action<float> OnStopPowering;
        private AcitonStateMachine m_AcitonStateMachine;
        public PoweringData m_Powering;
        public bool m_IsPowering;
        public class OnStopPoweringEventArg
        {
            public float power;
        }

        private float m_PowerCharging;
        private bool m_StopPowering;
        public float PowerCharging
        {
            get
            {
                return m_PowerCharging;
            }
            private set
            {
                m_PowerCharging = value;
                OnPoweringNormalized?.Invoke(m_Powering.NormalizingPower(value));
            }
        }

        bool IStatePlayerHand.OnChargingAction 
        { 
            get => m_IsPowering;
            set 
            {
                if (!value)
                {
                    OnStopPowering?.Invoke(PowerCharging);
                }
            } 
        }

        public StatePowering(AcitonStateMachine acitonStateMachine, PoweringData i_Powering) 
        {
            m_AcitonStateMachine = acitonStateMachine;
            m_Powering = i_Powering;
        }
        public void EnterCollisionFoodObj(Collider other)
        {
        }

        public void ExitCollisionFoodObj(Collider other)
        {
        }

        public void OnSteteEnter()
        {
            m_IsPowering = m_AcitonStateMachine.IsPower;
            Debug.Log("OnSteteEnter : StatePowering");
        }

        public void OnSteteExit()
        {


        }

        public void Update()
        {
            if (m_IsPowering)
            {
                PowerCharging += m_Powering.DataTimeAddingPower * Time.deltaTime;
            }
            else
            {
                Debug.Log("Else");
            }
        }

        public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
        {
        }

        public bool OnPickUpAction(out GameFoodObj o_Collcted)
        {
            o_Collcted = null;
            return false;
        }

        public bool OnThrowPoint(out float o_Force)
        {
            o_Force = 0f;
            PowerCharging = 0f;
            return false;
        }
    }
}
