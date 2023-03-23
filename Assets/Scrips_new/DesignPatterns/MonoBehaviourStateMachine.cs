using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrips_new.DesignPatterns
{
    internal abstract class MonoBehaviourStateMachine : MonoBehaviour
    {
        protected List<State> m_States;
        private byte m_CurrentState;
        protected byte m_InitialState = 0;
        protected bool m_IsDefaultToInitialState = true;
        protected bool m_IsModulo = true;

        protected virtual byte StatesIndex
        {
            get => m_CurrentState;
            private set => m_CurrentState = value;
        }
        protected virtual State CurrentStates => m_States[m_CurrentState];

        public virtual void SetStates(List<State> i_States)
        {
            bool isValid = i_States != null && i_States.Count > 0;
            if (!isValid) 
            {
                throw new InvalidOperationException("States must be initialized and contain at least one element");
            }
            m_States = i_States;

            foreach(State state in m_States)
            {
                state.ChangeState += OnChangeState;
            }
            
        }

        protected virtual void OnChangeState(byte i_ChangeStatTo)
        {
            byte newState = i_ChangeStatTo;
            if (m_CurrentState == i_ChangeStatTo)
            { 
                Debug.LogError("A state change to the current state has happened : " + CurrentStates.ToString())  ;
                return;
            }

            bool isOverflow = i_ChangeStatTo > m_States.Count;

            if (isOverflow)
            {
                if (m_IsModulo)
                {
                    newState = (byte)(i_ChangeStatTo % m_States.Count);
                }
                else if (m_IsDefaultToInitialState)
                {
                    newState = m_InitialState;
                }
                else
                {
                    Debug.LogError("m_IsDefaultToInitialState" + CurrentStates.ToString());
                }
            }

            if (m_CurrentState == newState)
            {
                Debug.LogError("A state change to the current state has happened : " + CurrentStates.ToString());
            }
            else
            {
                CurrentStates.OnStateExit();
                m_CurrentState = newState;
                CurrentStates.OnStateEnter();
            }
        }
    }
}
