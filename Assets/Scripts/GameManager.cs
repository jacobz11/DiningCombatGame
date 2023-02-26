
using Assets.Scripts;
using Assets.Scripts.AbstractFactory;
using DiningCombat;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // ================================================
    // constant Variable 
    private const float k_Size = 19;
    // ================================================
    // Delegate

    // ================================================
    // Variable 
    private OnlineGameAbstractFactory m_GameAbstractFactory;
    private Vector3 m_MaxPosition;
    private Vector3 m_MinPosition;

    //private GameObject m_Ground;

    // ================================================
    // ----------------Serialize Field-----------------
    //[SerializeField]
    //private GameObject m_PrefabPlayer;
    //[SerializeField]
    //private GameObject m_PrefabApple;
    //[SerializeField]
    //private GameObject m_PrefabDask;
    //private GameObject m_PrefabCabbage;
    // ================================================
    // properties

    // ================================================
    // auxiliary methods programmings

    // ================================================
    // Unity Game Engine

    private void Awake()
    {
        m_GameAbstractFactory = new OfflineGameAbstractFactory();
    }
    void Start()
    {
        GameObject ground = GameObject.Find(GameGlobal.GameObjectName.k_Ground);

        // X 
        float minX = ground.transform.position.x - (ground.transform.localScale.x / 2);
        float maxX = ground.transform.position.x + (ground.transform.localScale.x / 2);

        // Y
        //float minY = ground.transform.position.y - (ground.transform.localScale.y / 2);
        //float maxY = ground.transform.position.y + (ground.transform.localScale.y / 2);

        // Z
        float minZ = ground.transform.position.z - (ground.transform.localScale.z / 2);
        float maxZ = ground.transform.position.z + (ground.transform.localScale.z / 2);

        m_MaxPosition = new Vector3(minX, 0.25f, minZ);
        m_MinPosition = new Vector3(maxX, 0.25f, maxZ);

        for (int i = 0; i < 10; i++)
        {
            SpawnGameFoodObj();
        }

        //m_GameAbstractFactory.InitiMap();

        //initGameFoodObj();
        //initPlayers();
    }

    private Vector3 getRandomPosition()
    {
        return new Vector3(
            Random.Range(m_MinPosition.x, m_MaxPosition.x),
            Random.Range(m_MinPosition.y, m_MaxPosition.y),
            Random.Range(m_MinPosition.z, m_MaxPosition.z)
        );
    }

    // ================================================
    //  methods

    // ================================================
    // auxiliary methods
    private void initPlayers()
    {
        for (int i = 0; i < 10; i++)
        {
            spawnPlayer();
        }
    }

    private void spawnPlayer()
    { 
    }

    private void initGameFoodObj()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnGameFoodObj();
        }
    }

    public GameObject SpawnGameFoodObj()
    {
        GameObject spawn = Instantiate(m_GameAbstractFactory.SpawnGameFoodObj(),
                                       getRandomPosition(),
                                   Quaternion.identity);

        GameFoodObj foodObj = spawn.GetComponent<GameFoodObj>();
        foodObj.Destruction += OnDestruction_GameFoodObj;

        return spawn;
    }

    // ================================================
    // Delegates Invoke 
    protected virtual void OnDestruction_GameFoodObj(object sender, EventArgs e)
    {
        SpawnGameFoodObj();
    }

    protected virtual void OnDestruction_Player(object sender, EventArgs e)
    {
    }
    // ================================================
    // ----------------Unity--------------------------- 
    // ----------------GameFoodObj---------------------
}
