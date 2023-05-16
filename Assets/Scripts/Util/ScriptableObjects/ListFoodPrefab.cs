using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.DataObject
{
    [Serializable]
    // TODO :  dell this after Completion of the merger scriptable objects
    internal struct ListFoodPrefab
    {
        public GameObject[] m_FoodPrefab;

        public GameObject GetRundomFoodPrefab()
        {
            return m_FoodPrefab[Random.Range(0, m_FoodPrefab.Length)];
        }
    }
}