using System;
using System.Collections;
using UnityEngine;

namespace DiningCombat
{
    internal abstract class IManager<IChannelGame> : MonoBehaviour
    {
        protected IManager<IChannelGame> m_Singlton;
        protected IChannelGame m_Channel;
        
        internal virtual IChannelGame Channel { get { return m_Channel; } }

        internal abstract IEnumerable MainCoroutine();

        internal abstract void OnGameOver();
    }
}
