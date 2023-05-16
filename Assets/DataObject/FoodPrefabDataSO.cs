using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.DataObject
{
    [CreateAssetMenu(fileName = "PrefabDataSO", menuName = "Custom Objects/Pool/Food Prefab data")]

    internal class FoodPrefabDataSO : PrefabDataSO<GameFoodObj>
    {
    }
}
