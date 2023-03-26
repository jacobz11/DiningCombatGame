using System;
using System.Collections;
using UnityEngine;

namespace DiningCombat
{
    internal abstract class IManager<IChannelGame> : MonoBehaviour
    {
        protected static GameManager s_GameManager;
        protected IManager<IChannelGame> m_Singlton;
        protected IChannelGame m_Channel;

        internal IChannelGame Channel { get { return m_Channel; } }
        internal static void SetGameManager(GameManager manager)
        {
            s_GameManager = manager;
        }

        internal abstract IEnumerable MainCoroutine();

        internal abstract void OnGameOver();
    }
}
