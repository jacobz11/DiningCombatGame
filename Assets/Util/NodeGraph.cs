using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Util
{
    internal class NodeGraph
    {
        public bool IsFree { get; private set; }
        private Action m_ChangeAction;
        public NodeGraph(Action i_ChangeAction) 
        {
            IsFree = true;
            m_ChangeAction += i_ChangeAction;
        }

        public void SetNotFree()
        {
            if (IsFree)
            {
                m_ChangeAction?.Invoke();
                IsFree = false;
            }
        }
    }
}