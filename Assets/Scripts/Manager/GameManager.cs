using DiningCombat.FoodObj.Managers;
using DiningCombat.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Abstraction;
using static DiningCombat.GameGlobal;
using Random = UnityEngine.Random;

namespace DiningCombat
{
    internal class GameManager : MonoBehaviour
    {
        private static GameManager s_Instance;
        public static GameManager Singlton
        {
            get => s_Instance;
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

        [SerializeField] [Range(10, 100)]
        private byte m_MaxNumOfFoodObj;
        [SerializeField] [Range(0, 10)]
        private byte m_NumOfInitGameObj;
        [SerializeField] [Range(0, 10)]
        public float m_NumOfSecondsBetweenSpawn;
        [SerializeField] [Range(10, 1000)]
        private int m_KillMullPonit;
        [SerializeField] [Range(5, 50)]
        private float m_MinAdditionForce;
        [SerializeField] [Range(10, 200)]
        private float m_MaxAdditionForce;
        [SerializeField] [Range(500, 5000)]
        private float m_MaxForce;
        [SerializeField] [Range(20, 1000)]
        private float m_MinForce;
        [SerializeField]
        private GameObject m_PrefabUIHP;
        [SerializeField]
        private GameObject m_PrefabUIPower;
        [SerializeField]
        private GameObject m_PrefabUIScore;
        [SerializeField]
        private Vector3 m_MaxPosition;
        [SerializeField]
        private Vector3 m_MinPosition;
        [SerializeField]
        private GameObject m_PlayrPrefab;
        private byte m_NumOfExistingFoobObj;
        private ManagerGameFoodObj m_FoodObjBuilder;
        private PlayersManager m_PlayersManager;
        public event Action GameOver;
        public bool IsRunning => true;
        public bool IsSpawnNewGameObj => m_NumOfExistingFoobObj < MaxNumOfFoodObj;

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
        //public static List<PlayerDataG> s_PlayerDatas = new List<PlayerDataG>();

        public bool GetPrefabUiHP(out GameObject gameObject)
        {
            gameObject = m_PrefabUIHP;
            return m_PrefabUIHP is not null;
        }

        public bool GetPrefabUIPower(out GameObject gameObject)
        {
            gameObject = m_PrefabUIPower;
            return m_PrefabUIPower is not null;
        }

        public bool GetPrefabUIScore(out GameObject gameObject)
        {
            gameObject = m_PrefabUIScore;
            return m_PrefabUIScore is not null;
        }

        private void Awake()
        {
            Singlton = this;
            m_FoodObjBuilder = ManagerGameFoodObj.InitManagerGameFood();
            m_PlayersManager = PlayersManager.InitPlayersManager();
        }

        private void Start()
        {
            m_PlayersManager.InitPlayer();
            StartCoroutine(m_FoodObjBuilder.MainCoroutine());
        }

        public Vector3 GetRandomPositionInMap()
        {
            return new Vector3(
                Random.Range(MinPosition.x, MaxPosition.x),
                Random.Range(MinPosition.y, MaxPosition.y),
                Random.Range(MinPosition.z, MaxPosition.z)
            );
        }

        public GameObject SpawnGameFoodObj()
        { 
            if (m_FoodObjBuilder.SpawnGameFoodObj(GetRandomPositionInMap(), out GameObject o_Spawn))
            {
                ++m_NumOfExistingFoobObj;
            }
            return o_Spawn;
        }

        public virtual void OnDestruction_GameFoodObj()
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
                FoodObjData.k_AppleLocation,
                FoodObjData.k_FlourLocation,
                FoodObjData.k_CabbageLocation,
                FoodObjData.k_TomatoLocation
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