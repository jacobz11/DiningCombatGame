using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Util
{
    public class RandomFromArray
    {
        public static T GetRandomElement<T>(T[] i_Array)
        {
            if (i_Array is null || i_Array.Length == 0)
            {
                Debug.LogError("The input array is null or empty.");
                return default;
            }

            int randomIndex = Random.Range(0, i_Array.Length);
            return i_Array[randomIndex];
        }

        public static T GetRandomElement<T>(List<T> i_List)
        {
            if (i_List is null || i_List.Count == 0)
            {
                Debug.LogError("The input list is null or empty.");
                return default;
            }

            int randomIndex = Random.Range(0, i_List.Count);
            return i_List[randomIndex];
        }

        public static TValue GetRandomValue<TKey, TValue>(Dictionary<TKey, TValue> i_Dictionary)
        {
            if (i_Dictionary is null || i_Dictionary.Count == 0)
            {
                Debug.LogError("The input dictionary is null or empty.");
                return default;
            }

            int randomIndex = Random.Range(0, i_Dictionary.Count);
            int currentIndex = 0;

            foreach (var pair in i_Dictionary)
            {
                if (currentIndex == randomIndex)
                {
                    return pair.Value;
                }

                currentIndex++;
            }

            return default;
        }

        public static TKey GetRandomKey<TKey, TValue>(Dictionary<TKey, TValue> i_Dictionary)
        {
            if (i_Dictionary is null || i_Dictionary.Count == 0)
            {
                Debug.LogError("The input dictionary is null or empty.");
                return default;
            }

            int randomIndex = Random.Range(0, i_Dictionary.Count);

            return i_Dictionary.Keys.ElementAt(randomIndex);
        }
    }
}
