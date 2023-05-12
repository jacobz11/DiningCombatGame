using Assets.DataObject;
using Assets.Scripts;
using Assets.Util.DesignPatterns;
using System;
using System.Collections.Generic;
using UnityEngine;

internal class ManagerGamePackage : GenericObjectPool<IPackage>
{
    public event Action<List<Vector3>> UncollectedPos;

    private float m_LestSpanw;
    [SerializeField]
    private Cuntter m_Cuntter;
    [SerializeField]
    private SpawnData m_SpawnData;
    [SerializeField]
    private Room m_RoomDimension;
    [SerializeField]
    private IPackage[] m_PackagesPreFfa;
    private int m_IndexInPackagesArr;
    public bool IsSpawnNewGameObj { get; private set; }
    public new static ManagerGamePackage Instance { get; private set; }

    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(this);
            return;
        }

        base.Awake();
        Instance = this;
    }

    protected override void AddObject(int i_Count)
    {
        IPackage newObj = (GameObject.Instantiate(m_PackagesPreFfa[m_IndexInPackagesArr])).GetComponent<IPackage>();
        m_IndexInPackagesArr = (1 + m_IndexInPackagesArr) % m_PackagesPreFfa.Length;
        newObj.gameObject.SetActive(false);
        m_Objects.Enqueue(newObj);
    }

    private IPackage Get(Vector3 i_Pos)
    {
        IPackage foodObj = Get();
        foodObj.transform.position = i_Pos;
        foodObj.gameObject.SetActive(true);

        return foodObj;
    }

    private GameObject SpawnPackage()
    {
        //GameObject spawn = Instantiate(m_AllFoodPrefab.GetRundomFoodPrefab(),
        //    m_RoomDimension.GetRendonPos(), Quaternion.identity);
        //GameFoodObj package = spawn.GetComponent<GameFoodObj>();
        IPackage package = Get(m_RoomDimension.GetRendonPos());
        //package.Destruction += OnDestruction_GameFoodObj;
        _ = m_Cuntter.TryInc();
        m_LestSpanw = 0;

        //package.OnCollect += foodObj_OnCollect;
        //UncollectedPos += package.ViewElement;

        return package.gameObject;
    }

    internal void OnGameOver()
    { /* Not-Implemented */}

    private void OnDestruction_GameFoodObj()
    {
        _ = m_Cuntter.TryDec();
    }

    private void Start()
    {
        if (IsServer)
        {
            //for (short i = 0; i < m_SpawnData.m_InitSpawn; i++)
            //{
            //    SpawnPackage();
            //}
        }
        m_LestSpanw = 0;
    }
    private void Update()
    {
        if (!IsServer)
        {
            return;
        }

        if (IsTimeToSpanw())
        {
            _ = SpawnPackage();
        }
    }

    private bool IsTimeToSpanw()
    {
        m_LestSpanw += Time.deltaTime;
        bool isTimeOver = m_LestSpanw >= m_SpawnData.m_SpawnTimeBuffer;
        //Debug.Log("isTimeOver " + isTimeOver);
        bool isNotMax = m_Cuntter.CanInc();
        //Debug.Log("isNotMax  " + isNotMax);

        return isTimeOver && isNotMax;
    }

    public List<Vector3> GetAllUncollcted()
    {
        List<Vector3> list = new List<Vector3>();
        UncollectedPos?.Invoke(list);
        return list;
    }
}