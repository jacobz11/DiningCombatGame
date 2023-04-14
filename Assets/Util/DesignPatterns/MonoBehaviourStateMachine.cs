using System;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Abstraction
{
    public abstract class MonoBehaviourStateMachine : MonoBehaviour
    {
        protected IDCState[] m_States;
        protected int m_CurrentState = 0;
        protected byte m_InitialState = 0;
        protected bool m_IsDefaultToInitialState = true;
        protected bool m_IsModulo = true;

        public virtual int StatesIndex
        {
            get => m_CurrentState;
            set
            {
                m_States[m_CurrentState].OnStateExit();
                m_CurrentState = value % m_States.Length;
                m_States[m_CurrentState].OnStateEnter();
            }
        }

        protected virtual IDCState CurrentStates => m_States[m_CurrentState];

        public virtual void SetStates(params IDCState[] i_States)
        {
            bool isValid = i_States != null && i_States.Length > 0;
            Debug.Assert(isValid, "States must be initialized and contain at least one element");
            m_States = i_States;
            m_CurrentState = m_InitialState;
        }

        public virtual void SetStates(List<IDCState> i_States)
        {
            SetStates(i_States.ToArray());
        }
    }
}

