using UnityEngine;
// TODO : to fix the namespace
namespace Assets.DataObject
{
    [CreateAssetMenu(fileName = "PrefabDataSO", menuName = "Custom Objects/Pool/Food Prefab data")]

    internal class FoodPrefabDataSO : PrefabDataSO<GameFoodObj>
    {
    }
}
