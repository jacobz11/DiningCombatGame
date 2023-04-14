using Assets.DataObject;
using System;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ManagerGameFoodObj : MonoBehaviour
{
    [SerializeField]
    private Cuntter cuntter;
    [SerializeField]
    private ListFoodPrefab m_AllFoodPrefab;
    private static Type m_Type = typeof(GameObject);
    public static ManagerGameFoodObj Instance { get; private set; }
    public bool IsSpawnNewGameObj { get; private set; }

    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public bool SpawnGameFoodObj(Vector3 i_Position, out GameObject o_Spawn)
    {
        bool isSpawn = false;
        o_Spawn = null;
        if (Instance.IsSpawnNewGameObj)
        {
            o_Spawn = SpawnGameFoodObj(i_Position);
            isSpawn = true;
        }

        return isSpawn;
    }

    private GameObject SpawnGameFoodObj(Vector3 i_Position)
    {
        GameObject spawn = Instantiate(m_AllFoodPrefab.GetRundomFoodPrefab(), i_Position, Quaternion.identity);
        GameFoodObj foodObj = spawn.GetComponent<GameFoodObj>();
        foodObj.Destruction += OnDestruction_GameFoodObj;
        //foodObj.UncollectState. += OnFoodCollect;
        return spawn;
    }

    private void OnDestruction_GameFoodObj()
    {
        throw new NotImplementedException();
    }

    internal void OnGameOver()
    {
        throw new NotImplementedException();
    }
}
