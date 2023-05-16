using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Util.DesignPatterns
{
    [Serializable]
    public abstract class GenericObjectPool<T> : NetworkBehaviour where T : Component
    {
        private const int k_AddingParTime = 1;
        [SerializeField]
        private T m_Prefap;
        public T Prefap { get { return m_Prefap; } set { m_Prefap = value; } }

        public static GenericObjectPool<T> Instance { get; protected set; }

        protected Queue<T> m_Objects = new Queue<T>();
        protected virtual void Awake()
        {
            Instance = this;
        }

        public T Get()
        {
            if (m_Objects.Count == 0)
            {
                AddObject(k_AddingParTime);
            }

            return m_Objects.Dequeue();
        }

        public void ReturnToPool(T i_EnteringObject)
        {
            i_EnteringObject.gameObject.SetActive(false);
            m_Objects.Enqueue(i_EnteringObject);
        }

        protected virtual void AddObject(int i_Count)
        {
            T newObj = GameObject.Instantiate(m_Prefap);
            newObj.gameObject.SetActive(false);
            m_Objects.Enqueue(newObj);
        }
    }
}