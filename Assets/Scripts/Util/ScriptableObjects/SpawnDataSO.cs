using System;
using UnityEngine;

namespace DiningCombat.DataObject
{
    [Serializable]
    //[TestFixture]
    [CreateAssetMenu(fileName = "SpawnDataSO", menuName = "Custom Objects/SpawnData")]
    public class SpawnDataSO : ScriptableObject
    {
        [UnityEngine.Range(0.0f, 20.5f)]
        public float m_SpawnTimeBuffer;
        [UnityEngine.Range(0, 100)]
        public short m_InitSpawn;
        [UnityEngine.Range(1, 1000)]
        public short m_MaxItemSpawn;
        [UnityEngine.Range(1, 1000)]
        public short m_MinItemSpawn;
    }
}
