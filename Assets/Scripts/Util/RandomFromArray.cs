using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Util
{
    public class RandomFromArray
    {
        public static T GetRandomElement<T>(T[] array)
        {
            if (array == null || array.Length == 0)
            {
                Debug.LogError("The input array is null or empty.");
                return default;
            }

            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }

        public static T GetRandomElement<T>(List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogError("The input list is null or empty.");
                return default;
            }

            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }

        public static TValue GetRandomValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                Debug.LogError("The input dictionary is null or empty.");
                return default;
            }

            int randomIndex = Random.Range(0, dictionary.Count);
            int currentIndex = 0;

            foreach (var pair in dictionary)
            {
                if (currentIndex == randomIndex)
                {
                    return pair.Value;
                }

                currentIndex++;
            }

            return default;
        }

        public static TKey GetRandomKey<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                Debug.LogError("The input dictionary is null or empty.");
                return default;
            }

            int randomIndex = Random.Range(0, dictionary.Count);

            return dictionary.Keys.ElementAt(randomIndex);
        }
    }
}
