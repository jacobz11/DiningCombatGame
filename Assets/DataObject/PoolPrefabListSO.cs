using Assets.DataObject;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolPrefabListSO<T> : ScriptableObject where T : Component
{
    public List<PrefabDataSO<T>> m_List;
    private Dictionary<string, T> m_PrefabDictionary;

    private void OnEnable()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        m_PrefabDictionary = new Dictionary<string, T>();

        foreach (PrefabDataSO<T> item in m_List)
        {
            m_PrefabDictionary[item.m_Key] = item.m_Prefab;
        }
    }

    public string GetRundomName()
    {
        return m_List[UnityEngine.Random.Range(0, m_List.Count)].m_Key;
    }

    public T GetRundomPrefab()
    {
        return m_List[UnityEngine.Random.Range(0, m_List.Count)].m_Prefab;
    }

    public T this[string key] 
    {
        get
        {
            if (m_PrefabDictionary.TryGetValue(key, out T prefab))
            {
                return prefab;
            }
            else
            {
                Debug.LogError($"Prefab with key '{key}' not found in the dictionary.");
                return null;
            }
        }
    }
}