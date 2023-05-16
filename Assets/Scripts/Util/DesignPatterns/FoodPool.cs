using UnityEngine;
// TODO : to fix the namespace

namespace Assets.Util.DesignPatterns
{
    internal class FoodPool : GenericObjectPoolNew<GameFoodObj>
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
