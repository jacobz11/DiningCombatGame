using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
namespace DiningCombat.Util.DesignPatterns
{
    [Serializable]
    public abstract class GenericObjectPool<T> : NetworkBehaviour where T : Component
    {
        public event Action OnReturnToPool;
        private const int k_AddingParTime = 1;
        [SerializeField]
        private PoolPrefabListSO<T> m_PrefabList;
        public PoolPrefabListSO<T> PrefabList { get { return m_PrefabList; } set { m_PrefabList = value; } }

        protected Dictionary<string, Queue<T>> m_PrefabQueues = new Dictionary<string, Queue<T>>();

        public static GenericObjectPool<T> Instance { get; protected set; }

        protected virtual void Awake()
        {
            Instance = this;
        }

        public T Get()
        {
            string key = RandomFromArray.GetRandomKey<string, T>(m_PrefabList.m_PrefabDictionary);
            return Get(key);
        }

        public T Get(string i_PrefabKey)
        {
            if (!m_PrefabQueues.ContainsKey(i_PrefabKey))
            {
                m_PrefabQueues[i_PrefabKey] = new Queue<T>();
                AddObject(i_PrefabKey, k_AddingParTime);
            }

            var queue = m_PrefabQueues[i_PrefabKey];
            if (queue.Count == 0)
            {
                AddObject(i_PrefabKey, k_AddingParTime);
            }

            return queue.Dequeue();
        }

        public virtual void ReturnToPool(T i_EnteringObject, string i_Key)
        {
            i_EnteringObject.gameObject.SetActive(false);

            if (i_EnteringObject != null && m_PrefabQueues.ContainsKey((i_EnteringObject as IDictionaryObject).NameKey))
            {
                var queue = m_PrefabQueues[(i_EnteringObject as IDictionaryObject).NameKey];
                queue.Enqueue(i_EnteringObject);
                OnReturnToPool?.Invoke();
            }
            else
            {
                Debug.LogWarning("Prefab not found in the prefab list.");
            }
        }

        public virtual void ReturnToPool(T i_EnteringObject)
        {
            if (i_EnteringObject is not IDictionaryObject obj)
            {
                Debug.Log("Error");
                return;
            }

            ReturnToPool(i_EnteringObject, obj.NameKey);
        }

        protected virtual void AddObject(string i_PrefabKey, int i_Count)
        {
            var newObj = GameObject.Instantiate(m_PrefabList[i_PrefabKey]);
            newObj.gameObject.SetActive(false);

            if (newObj is IDictionaryObject dictionaryObject)
            {
                dictionaryObject.NameKey = i_PrefabKey;
            }

            if (m_PrefabQueues.ContainsKey(i_PrefabKey))
            {
                var queue = m_PrefabQueues[i_PrefabKey];
                queue.Enqueue(newObj);
            }
            else
            {
                Debug.LogWarning("Prefab not found in the prefab list.");
            }
        }

        protected virtual void AddObject(int i_Count)
        {
            AddObject(RandomFromArray.GetRandomKey(m_PrefabQueues), i_Count);
        }
    }
}