using System;
using UnityEngine;

namespace Assets.DataObject
{
    [Serializable]
    public class PrefabDataSO<T> : ScriptableObject where T : Component
    {
        public T m_Prefab;
        [Tooltip("The position in the dictionary where the prefab will be stored. It must not be empty")]
        public string m_Key;
    }
}