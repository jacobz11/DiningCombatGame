using Assets.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Util.DesignPatterns
{
    internal class GenericObjectPoolNew<T> : NetworkBehaviour where T : Component
    {
        private const int k_AddingParTime = 1;
        [SerializeField]
        private PoolPrefabListSO<T> m_PrefabList;
        public PoolPrefabListSO<T> PrefabList { get { return m_PrefabList; } set { m_PrefabList = value; } }

        public static GenericObjectPoolNew<T> Instance { get; protected set; }

        protected Dictionary<string, Queue<T>> m_PrefabQueues = new Dictionary<string, Queue<T>>();

        protected virtual void Awake()
        {
            Instance = this;
        }
        
        public T Get()
        {
            return Get(m_PrefabList.GetRundomName());
        }

        public T Get(string i_PrefabKey)
        {
            if (!m_PrefabQueues.ContainsKey(i_PrefabKey))
            {
                Debug.Log("Key Not Valis");
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

        public void ReturnToPool(T enteringObject)
        {
            enteringObject.gameObject.SetActive(false);

            //var prefab = m_PrefabList.GetPrefabList().FirstOrDefault(p => enteringObject.GetType() == p.Prefab.GetType());
            if (enteringObject != null && m_PrefabQueues.ContainsKey((enteringObject as IDictionaryObject).NameKey))
            {
                var queue = m_PrefabQueues[(enteringObject as IDictionaryObject).NameKey];
                queue.Enqueue(enteringObject);
            }
            else
            {
                Debug.LogWarning("Prefab not found in the prefab list.");
            }
        }

        protected virtual void AddObject(string i_PrefabKey, int count)
        {
            var newObj = GameObject.Instantiate(m_PrefabList[i_PrefabKey]);
            newObj.gameObject.SetActive(false);
            (newObj as IDictionaryObject).NameKey = i_PrefabKey;

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
    }
}
