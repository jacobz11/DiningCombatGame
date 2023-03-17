using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class OnlineGameAbstractFactory
    {
        protected AbstractFactoryProductInitiMap m_InitiMapFactory;

        public OnlineGameAbstractFactory()
        {
            //SceneManager.LoadScene(i_SceneName, LoadSceneMode.Additive);
            m_InitiMapFactory = new AbstractFactoryProductInitiMap();
            //m_FoodFactory = new AbstractFactoryProductFood();
        }

        public virtual void InitiMap()
        {
            m_InitiMapFactory.EnvironmentObjectMaker();
        }

        internal virtual GameObject SpawnPlayer()
        {
            return null;
        }
    }
}
