using Assets.scrips.Player.Data;
using DesignPatterns.Abstraction;
using System;
using UnityEngine;
namespace Assets.scrips.Player.States
{
    internal class StatePowering : IStatePlayerHand
    {
        public class OnStopPoweringEventArg
        {
            public float power;
        }
        public const int k_Indx = 2;

        public event Action<float> OnPoweringNormalized;
        public event Action<float> OnStopPowering;

        public bool m_IsPowering;
        private float m_PowerCharging;
        private bool m_StopPowering;

        private AcitonStateMachine m_AcitonStateMachine;
        public PoweringData m_Powering;

        public float PowerCharging
        {
            get => m_PowerCharging;
            private set
            {
                m_PowerCharging = value;
                OnPoweringNormalized?.Invoke(m_Powering.NormalizingPower(value));
            }
        }

        public bool OnChargingAction
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
        #region Not-Implemented
        public void EnterCollisionFoodObj(Collider other)
        {/* Not-Implemented */}
        public void ExitCollisionFoodObj(Collider other)
        {/* Not-Implemented */}
        public virtual void OnSteteExit()
        {/* Not-Implemented */}
        public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
        {/* Not-Implemented */}
        #endregion

        public virtual void OnSteteEnter()
        {
            m_IsPowering = m_AcitonStateMachine.IsPower;
            Debug.Log("OnSteteEnter : StatePowering");
        }

        public virtual void Update()
        {
            if (m_IsPowering)
            {
                PowerCharging += m_Powering.DataTimeAddingPower * Time.deltaTime;
            }
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
