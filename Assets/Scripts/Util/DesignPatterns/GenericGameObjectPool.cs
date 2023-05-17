using System;
using System.Collections.Generic;
using UnityEngine;
namespace DiningCombat.Util.DesignPatterns
{
    [Serializable]
    public class GenericGameObjectPool
    {
        private const int k_AddingParTime = 1;
        [SerializeField]
        private GameObject m_Prefap;

        public static GenericGameObjectPool Instance { get; protected set; }

        protected Queue<GameObject> m_Objects = new Queue<GameObject>();
        protected virtual void Awake()
        {
            Instance = this;
        }

        public GameObject Get()
        {
            if (m_Objects.Count == 0)
            {
                AddObject(k_AddingParTime);
            }

            return m_Objects.Dequeue();
        }

        public void ReturnToPool(GameObject i_EnteringObject)
        {
            i_EnteringObject.gameObject.SetActive(false);
            m_Objects.Enqueue(i_EnteringObject);
        }

        protected virtual void AddObject(int i_Count)
        {
            GameObject newObj = GameObject.Instantiate(m_Prefap);
            newObj.gameObject.SetActive(false);
            m_Objects.Enqueue(newObj);
        }
    }
}
