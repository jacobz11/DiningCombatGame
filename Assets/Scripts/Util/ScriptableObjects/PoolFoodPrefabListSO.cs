using System;
using UnityEngine;

namespace Assets.DataObject
{
    [Serializable]
    [CreateAssetMenu(fileName = "PrefabDataSO", menuName = "Custom Objects/Pool/PoolFoodPrefabListSO")]
    internal class PoolFoodPrefabListSO : PoolPrefabListSO<GameFoodObj>
    {
    }
}
