using DiningCombat.FoodObject;
using UnityEngine;

namespace DiningCombat.Util.DesignPatterns
{
    public class FoodPool : GenericObjectPoolNew<GameFoodObj>
    {
        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                GameFoodObj o = Instance.Get();
                Debug.Log((o as IDictionaryObject).NameKey);

            }
        }
    }
}
