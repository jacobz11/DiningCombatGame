using System;
using UnityEngine;

namespace Assets.Scrips_new.DesignPatterns
{
    internal abstract class State : ScriptableObject
    {
        public event Action<byte> ChangeState;
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