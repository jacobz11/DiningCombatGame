using Assets.DataObject;
using System;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ManagerGameFoodObj : MonoBehaviour
{
    [SerializeField]
    private Cuntter m_Cuntter;
    [SerializeField]
    private ListFoodPrefab m_AllFoodPrefab;
    [SerializeField]
    private SpawnData m_SpawnData;
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
            o_Spawn = SpawnGameFoodObj();
            isSpawn = true;
        }

        return isSpawn;
    }

    private GameObject SpawnGameFoodObj()
    {
        GameObject spawn = Instantiate(m_AllFoodPrefab.GetRundomFoodPrefab(),
            m_SpawnData.GetRendonPos(), Quaternion.identity);
        GameFoodObj foodObj = spawn.AddComponent<GameFoodObj>();
        //foodObj.Destruction += OnDestruction_GameFoodObj;
        m_Cuntter.TryInc();
        //foodObj.UncollectState. += OnFoodCollect;
        return spawn;
    }

    private void OnDestruction_GameFoodObj()
    {
        m_Cuntter.TryDec();
    }

    internal void OnGameOver()
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        for(short i = 0; i< m_SpawnData.m_InitSpawn; i++)
        {
            SpawnGameFoodObj();
        }    
    }
    private void Update()
    {
        
    }
}
