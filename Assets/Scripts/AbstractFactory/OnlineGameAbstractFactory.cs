using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class OnlineGameAbstractFactory
    {
        // ================================================
        // constant Variable 

        // ================================================
        // Delegate

        // ================================================
        // Fields 
        protected AbstractFactoryProductInitiMap m_InitiMapFactory;
        protected AbstractFactoryProductFood m_FoodFactory;

        // ================================================
        // ----------------Serialize Field-----------------

        // ================================================
        // properties

        // ================================================
        // auxiliary methods programmings

        // ================================================
        // Unity Game Engine

        // ================================================
        //  methods
        public OnlineGameAbstractFactory()
        {
            //SceneManager.LoadScene(i_SceneName, LoadSceneMode.Additive);
            m_InitiMapFactory = new AbstractFactoryProductInitiMap();
            m_FoodFactory = new AbstractFactoryProductFood(this);
            //m_FoodFactory = new AbstractFactoryProductFood();
        }

        public virtual void InitiMap()
        {
            m_InitiMapFactory.EnvironmentObjectMaker();
        }

        public virtual GameObject SpawnGameFoodObj()
        {
            return m_FoodFactory.CreatFoodObj();
        }

        internal virtual GameObject SpawnPlayer()
        {
            return null;
        }
        // ================================================
        // auxiliary methods

        // ================================================
        // Delegates Invoke 

        // ================================================
        // ----------------Unity--------------------------- 
        // ----------------GameFoodObj---------------------
    }
}
