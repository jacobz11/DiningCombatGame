using DiningCombat.DataObject;
using DiningCombat.FoodObject;
using System;
using UnityEngine;

namespace DiningCombat.Player.States
{
    public class StatePowering : IStatePlayerHand
    {
        public class OnStopPoweringEventArg
        {
            public float m_Power;
        }
        public const int k_Indx = 2;

        public event Action<float> OnPoweringNormalized;
        public event Action<float> OnStopPowering;

        public bool m_IsPowering;
        private float m_PowerCharging;
        private ActionStateMachine m_AcitonStateMachine;
        protected PoweringDataSo m_Powering;

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

        public StatePowering(ActionStateMachine acitonStateMachine, PoweringDataSo i_Powering)
        {
            m_AcitonStateMachine = acitonStateMachine;
            m_Powering = i_Powering;
        }
        #region Not-Implemented
        public void EnterCollisionFoodObj(Collider other)
        {/* Not-Implemented */}
        public void ExitCollisionFoodObj(Collider other)
        {/* Not-Implemented */}
        public virtual void OnStateExit()
        {/* Not-Implemented */}

        #endregion

        public virtual void OnStateEnter()
        {
            m_IsPowering = m_AcitonStateMachine.IsPower;
            PowerCharging = 0.0f;
        }

        public virtual void Update()
        {
            if (m_IsPowering)
            {
                PowerCharging += m_Powering.m_DataTimeAddingPower * Time.deltaTime;
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
