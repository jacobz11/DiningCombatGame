using Assets.DataObject;
using Assets.Util.DesignPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ManagerGameFoodObj : GenericObjectPool<GameFoodObj>
{
    public event Action<List<Vector3>> UncollectedPos;
    public event Action OnCollected;

    private float m_LestSpanw;
    [SerializeField]
    private Cuntter m_CuntterOfFoodInTheGame;
    [SerializeField]
    private SpawnData m_SpawnData;
    [SerializeField]
    private Assets.Scripts.Room m_RoomDimension;
    [SerializeField]
    private ListFoodPrefab m_AllFoodPrefab;
    public bool IsSpawnNewGameObj { get; private set; }
    public static ManagerGameFoodObj Instance { get; private set; }


    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(this);
            return;
        }
        base.Awake();
        Instance = this;
        int numOfSetToEnterThePool = 5;
    }

    protected override void AddObject(int i_Count)
    {
        for (int i = 0; i < i_Count; i++)
        {
            GameFoodObj newObj = (GameObject.Instantiate(m_AllFoodPrefab.GetRundomFoodPrefab())).GetComponent<GameFoodObj>();
            newObj.gameObject.SetActive(false);
            m_Objects.Enqueue(newObj);
        }
    }

    private GameFoodObj Get(Vector3 i_Pos)
    {
        GameFoodObj foodObj = Get();
        foodObj.transform.position = i_Pos;
        foodObj.gameObject.SetActive(true);

        return foodObj;
    }

    private void InitializationPool(int numOfSetToEnterThePool)
    {
        AddObject(numOfSetToEnterThePool);
        m_Objects.OrderBy(obj => Guid.NewGuid());
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
        //GameObject spawn = Instantiate(m_AllFoodPrefab.GetRundomFoodPrefab(),
        //    m_RoomDimension.GetRendonPos(), Quaternion.identity);
        //GameFoodObj foodObj = spawn.GetComponent<GameFoodObj>();
        GameFoodObj foodObj = Get(m_RoomDimension.GetRendonPos());
        //foodObj.Destruction += OnDestruction_GameFoodObj;
        m_CuntterOfFoodInTheGame.TryInc();
        foodObj.OnCollect += foodObj_OnCollect;
        UncollectedPos += foodObj.ViewElement;

        return foodObj.gameObject;
    }

    private void foodObj_OnCollect()
    { /* Not-Implemented */}
    internal void OnGameOver()
    { /* Not-Implemented */}

    private void OnDestruction_GameFoodObj()
    {
        m_CuntterOfFoodInTheGame.TryDec();
    }

    private void Start()
    {
        if (IsServer)
        {
            for (short i = 0; i < m_SpawnData.m_InitSpawn; i++)
            {
                SpawnGameFoodObj();
            }
        }
        m_LestSpanw = Time.time;
    }
    private void Update()
    {
        if (!IsServer)
        {
            return;
        }
        if (IsTimeToSpanw())
        {
            SpawnGameFoodObj();
        }
    }

    private bool IsTimeToSpanw()
    {
        m_LestSpanw += Time.deltaTime;
        bool isTimeOver = m_LestSpanw >= m_SpawnData.m_SpawnTimeBuffer;
        bool isNotMax = m_CuntterOfFoodInTheGame.CanInc();
        return isTimeOver && isNotMax;
    }

    public List<Vector3> GetAllUncollcted()
    {
        List<Vector3> list = new List<Vector3>();
        UncollectedPos?.Invoke(list);
        return list;
    }
}
