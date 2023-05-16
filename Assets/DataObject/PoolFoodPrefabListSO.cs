using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.DataObject
{
    [Serializable]
    [CreateAssetMenu(fileName = "PrefabDataSO", menuName = "Custom Objects/Pool/PoolFoodPrefabListSO")]
    internal class PoolFoodPrefabListSO : PoolPrefabListSO<GameFoodObj>
    {
    }
}
