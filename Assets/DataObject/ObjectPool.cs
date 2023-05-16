using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.DataObject
{
    [Serializable]
    public class ObjectPool<T> where T : Component
    {
        public bool m_UseList;
        public PoolPrefabListSO<T> m_PoolPrefabList;
        public PrefabDataSO<T> m_Prefab;

        public T GetPrefab(string i_KeyPrefab)
        {
            return m_UseList ? m_PoolPrefabList[i_KeyPrefab] : m_Prefab.m_Prefab;
        }
    }
}
