using Assets.Scripts;
using DiningCombat;
using Random = UnityEngine.Random;
using UnityEngine;
using System;
using UnityEngine.Internal;

public class ManagerGameFoodObj : MonoBehaviour
{
    private GameObject[] m_FoodPrefab;
    private Type m_Type = typeof(GameObject);
    private GameManager m_GameManager;
    public GameManager Manager
    {
        get => m_GameManager; 
        set => m_GameManager = value;
    }

    private void Awake()
    {
        //GameFoodObjBuilder
        m_FoodPrefab = new GameObject[]
        {
            getPrefabFrom(GameGlobal.FoodObjData.k_AppleLocation),
            getPrefabFrom(GameGlobal.FoodObjData.k_FlourLocation),
            getPrefabFrom(GameGlobal.FoodObjData.k_CabbageLocation),
            getPrefabFrom(GameGlobal.FoodObjData.k_TomatoLocation)
        };
    }

    private GameObject getPrefabFrom(string i_Location)
    {
        return (GameObject)Resources.Load(i_Location, m_Type);
    }
    public GameObject GetRnaomFoodObj()
    {
        return m_FoodPrefab[Random.RandomRange(0, m_FoodPrefab.Length - 1)];
    }
    public bool SpawnGameFoodObj(Vector3 i_Position, out GameObject o_Spawn)
    {
        bool isSpawn = false;
        if (Manager.IsSpawnNewGameObj)
        {
            o_Spawn = Instantiate(GetRnaomFoodObj(), i_Position, Quaternion.identity);
            GameFoodObj foodObj = o_Spawn.GetComponent<GameFoodObj>();
            foodObj.Destruction += Manager.OnDestruction_GameFoodObj;
            isSpawn = true;
        }
        else
        {
            o_Spawn = null;
        }

        return isSpawn;
    }
}
