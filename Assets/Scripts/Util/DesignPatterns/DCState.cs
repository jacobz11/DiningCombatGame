using System;
using UnityEngine;
namespace DesignPatterns.Abstraction
{
    public abstract class DCState : MonoBehaviour
    {
        public event Action<byte> ChangeState;
        private byte m_StateId;

        //public DCState(byte stateId, string stateName)
        //{
        //    r_StateId = stateId;
        //    r_StateName = stateName;
        //}

        public byte StateId
        {
            get { return m_StateId; }
            set { m_StateId = value; }
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