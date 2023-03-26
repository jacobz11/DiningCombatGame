using System;
using UnityEngine;

namespace Abstraction
{
    namespace DesignPatterns
    {
        public abstract class DCState : ScriptableObject
        {
            public event Action<byte> ChangeState;
            public readonly byte r_StateId;
            public readonly string r_StateName;

            public DCState(byte stateId, string stateName)
            {
                r_StateId = stateId;
                r_StateName = stateName;
            }

            protected void ChangeStateHappened(byte i_ChangTo)
            {
                ChangeState?.Invoke(i_ChangTo);
            }
            public virtual void OnStateEnter(params object[] list)
            {
            }

            public virtual void OnStateUpdate(params object[] list)
            {
            }

            public virtual void OnStateExit(params object[] list)
            {
            }

            public virtual void OnStateMove(params object[] list)
            {
            }

            public virtual void OnStateIK(params object[] list)
            {
            }

            public abstract override string ToString();
        }
    }
}