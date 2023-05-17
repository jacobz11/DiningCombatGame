using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DiningCombat.DataObject
{
    [Serializable]
    // TODO :  dell this after Completion of the merger scriptable objects
    public struct ListFoodPrefab
    {
        public GameObject[] m_FoodPrefab;

        public GameObject GetRundomFoodPrefab()
        {
            return m_FoodPrefab[Random.Range(0, m_FoodPrefab.Length)];
        }
    }
}