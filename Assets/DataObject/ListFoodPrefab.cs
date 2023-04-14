using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.DataObject
{
    [Serializable]
    internal struct ListFoodPrefab
    {
        public GameObject[] FoodPrefab;

        public GameObject GetRundomFoodPrefab()
        {
            return FoodPrefab[Random.Range(0, FoodPrefab.Length)];
        }
    }
}
