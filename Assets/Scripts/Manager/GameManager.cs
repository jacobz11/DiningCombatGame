using DiningCombat.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Abstraction;
using static DiningCombat.GameGlobal;
using Random = UnityEngine.Random;
using DiningCombat.FoodObj.Managers;
using Assets.Scripts.Manager;

namespace DiningCombat
{
    internal class GameManager : MonoBehaviour
    {
        private static GameManager s_Instance;
        public static GameManager Singlton 
        { 
            get =>s_Instance;
            private set
            {
                if (s_Instance is null)
                {
                    s_Instance = value;
                }
                else
                {
                    Debug.LogError("GameManager Singlton can only set once");
                }
            }
        }

        [SerializeField]
        [Range(10, 100)]
        private byte m_MaxNumOfFoodObj;
        [SerializeField]
        [Range(0, 10)]
        private byte m_NumOfInitGameObj;
        [Range(0, 10)]
        [SerializeField]
        public float m_NumOfSecondsBetweenSpawn;
        [SerializeField]
        private Vector3 m_MaxPosition;
        [SerializeField]
        private Vector3 m_MinPosition;
        [SerializeField]
        [Range(10, 1000)]
        private int m_KillMullPonit;
        [SerializeField]
        [Range(5, 50)]
        private float m_MinAdditionForce;
        [SerializeField]
        [Range(10, 200)]
        private float m_MaxAdditionForce;
        [SerializeField]
        [Range(500, 5000)]
        private float m_MaxForce;
        [SerializeField]
        [Range(20, 1000)]
        private float m_MinForce;
        [SerializeField]
        private GameObject m_PrefabUIHP;
        [SerializeField]
        private GameObject m_PrefabUIPower;        
        [SerializeField]
        private GameObject m_PrefabUIScore;

        public byte MaxNumOfFoodObj
        {
            get { return m_MaxNumOfFoodObj; }
        }

        public byte NumOfInitGameObj
        {
            get { return m_NumOfInitGameObj; }
        }
        public float NumOfSecondsBetweenSpawn
        {
            get { return m_NumOfSecondsBetweenSpawn; }
        }
        public Vector3 MinPosition
        {
            get { return m_MinPosition; }
        }
        public Vector3 MaxPosition
        {
            get { return m_MaxPosition; }
        }

        public int KillMullPonit
        {
            get => m_KillMullPonit;
            //  internal set; 
        }
        public float MinAdditionForce
        {
            get => m_MinAdditionForce;
            //  internal set; 
        }
        public float MaxAdditionForce
        {
            get => m_MaxAdditionForce;
            //  internal set; 
        }
        public float MaxForce
        {
            get => m_MaxForce;
            //  internal set; 
        }
        public float MinForce
        {
            get => m_MinForce;
            //  internal set; 
        }

        [SerializeField]
        private GameObject m_PlayrPrefab;
        private byte m_NumOfExistingFoobObj;
        private ManagerGameFoodObj m_FoodObjBuilder;
        private PlayersManager m_PlayersManager;
        public event Action GameOver;
        //public static List<PlayerDataG> s_PlayerDatas = new List<PlayerDataG>();

        public bool IsRunning => true;
        public bool IsSpawnNewGameObj => m_NumOfExistingFoobObj < MaxNumOfFoodObj;

        public bool GetPrefabUiHP(out GameObject gameObject)
        {
            if (m_PrefabUIHP is null)
            {
                gameObject = null;
                return false;
            }
            else
            {
                gameObject = m_PrefabUIHP;
                return true;
            }
        }

        public bool GetPrefabUIPower(out GameObject gameObject)
        {
            if (m_PrefabUIPower is null)
            {
                gameObject = null;
                return false;
            }
            else
            {
                gameObject = m_PrefabUIPower;
                return true;
            }
        }

        public bool GetPrefabUIScore(out GameObject gameObject)
        {
            if (m_PrefabUIScore is null)
            {
                gameObject = null;
                return false;
            }
            else
            {
                gameObject = m_PrefabUIScore;
                return true;
            }
        }

        private void Awake()
        {
            Debug.Log("Awake");
            Singlton = this;
            m_FoodObjBuilder = ManagerGameFoodObj.InitManagerGameFood();
            m_PlayersManager = PlayersManager.InitPlayersManager();
            m_PlayersManager.InitPlayer();
            StartCoroutine(SpawnCoroutine());
        }

        void Start()
        {

            //GameObject ground = GameObject.Find(GameGlobal.GameObjectName.k_Ground);

            //// X 
            //float minX = ground.transform.position.x - (ground.transform.localScale.x / 2);
            //float maxX = ground.transform.position.x + (ground.transform.localScale.x / 2);

            //// Y
            ////float minY = ground.transform.position.y - (ground.transform.localScale.y / 2);
            ////float maxY = ground.transform.position.y + (ground.transform.localScale.y / 2);

            //// Z
            //float minZ = ground.transform.position.z - (ground.transform.localScale.z / 2);
            //float maxZ = ground.transform.position.z + (ground.transform.localScale.z / 2);

            //m_MaxPosition = new Vector3(minX, 0.25f, minZ);
            //m_MinPosition = new Vector3(maxX, 0.25f, maxZ);
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(NumOfSecondsBetweenSpawn);
            while (IsRunning)
            {
                if (IsSpawnNewGameObj)
                {
                    this.SpawnGameFoodObj();
                }
                yield return new WaitForSeconds(NumOfSecondsBetweenSpawn);
            }
        }

        public Vector3 GetRandomPositionInMap()
        {
            return new Vector3(
                Random.Range(MinPosition.x, MaxPosition.x),
                Random.Range(MinPosition.y, MaxPosition.y),
                Random.Range(MinPosition.z, MaxPosition.z)
            );
        }

        private void initPlayers()
        {
            for (int i = 0; i < 1; i++)
            {
                spawnPlayer(ePlayerModeType.OfflinePlayer);
            }
        }

        private void spawnPlayer(ePlayerModeType modeType)
        {
            Debug.Log("spawnPlayer  modeType :" + modeType);
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
            //Debug.Log("in SpawnGameFoodObj");
            //Vector3 v = GetRandomPositionInMap();
            //Debug.Log("Vector3 : " + v);
            bool isSpawn = m_FoodObjBuilder.SpawnGameFoodObj(GetRandomPositionInMap(), out GameObject o_Spawn);

            if (isSpawn)
            {
                ++m_NumOfExistingFoobObj;
                return o_Spawn;
            }
            return null;
        }

        public virtual void OnDestruction_GameFoodObj(object sender, EventArgs e)
        {
            --m_NumOfExistingFoobObj;
        }

        protected virtual void OnDestruction_Player(object sender, EventArgs e)
        {
        }

        internal List<string> GetAllLoctingOfFoodPrefab()
        {
            return new List<string>()
            {
                GameGlobal.FoodObjData.k_AppleLocation,
                GameGlobal.FoodObjData.k_FlourLocation,
                GameGlobal.FoodObjData.k_CabbageLocation,
                GameGlobal.FoodObjData.k_TomatoLocation
            };
        }

        internal void EndInitMainCoroutine(IManager<IChannelGame> managerGameFoodObj, out float o_TimeToWait)
        {
            o_TimeToWait = 0;
            if (managerGameFoodObj is ManagerGameFoodObj)
            {
                this.m_NumOfExistingFoobObj = NumOfInitGameObj;
                o_TimeToWait = (float)NumOfSecondsBetweenSpawn;
            }

        }

        internal List<Player.Manger.PlayerData> GetPlayersInitialization()
        {
            return new List<Player.Manger.PlayerData>()
            {
                new Player.Manger.PlayerData(m_PlayrPrefab, "player",
                ePlayerModeType.OfflinePlayer, GetRandomPositionInMap()),
                new Player.Manger.PlayerData(m_PlayrPrefab, "ai", ePlayerModeType.OfflineAiPlayer,
                GetRandomPositionInMap()),
            };
        }
    }
}