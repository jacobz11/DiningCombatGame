using DiningCombat.FoodObject;
using System;
using UnityEngine;

namespace DiningCombat.DataObject
{
    [Serializable]
    [CreateAssetMenu(fileName = "PoolFoodPrefabListSO", menuName = "Custom Objects/Pool/PoolFoodPrefabListSO")]
    public class PoolFoodPrefabListSO : PoolPrefabListSO<GameFoodObj>
    {
    }
}
