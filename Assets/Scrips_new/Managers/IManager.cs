using System.Collections;
using UnityEngine;

namespace Assets.Scrips_new.Managers
{
    public abstract class IManager<IChannelGame> : MonoBehaviour
    {
        protected static GameManager s_GameManager;
        protected IManager<IChannelGame> m_Singlton;
        protected IChannelGame m_Channel;

        public IChannelGame Channel { get { return m_Channel; } }
        public static void SetGameManager(GameManager manager)
        {
            s_GameManager = manager;
        }

        protected abstract IManager<IChannelGame> Instance();

        public abstract IEnumerable MainCoroutine();

        public abstract void OnGameOver();
    }
}
