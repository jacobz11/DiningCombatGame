using DiningCombat.FoodObj.Managers;
using DiningCombat.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
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

        [SerializeField]
        private GameObject m_PrefabUIHP;
        [SerializeField]
        private GameObject m_PrefabUIPower;
        [SerializeField]
        private GameObject m_PrefabUIScore;
        [SerializeField]
        protected GameObject m_PlayrPrefab;
        [SerializeField]
        protected GameObject m_MLPrefab;
        [SerializeField]
        private byte m_NumOfExistingFoobObj;
        [SerializeField]
        private ManagerGameFoodObj m_FoodObjBuilder;
        //[SerializeField]
        //private GameManagerData m_GameManagerData = new GameManagerData();
        [SerializeField]
        private PlayersManager m_PlayersManager;
        public event Action GameOver;
        [Range(10, 255)]
        public byte m_MaxNumOfFoodObj;
        [Range(0, 80)]
        public byte m_NumOfInitGameObj;
        [Range(0, 10)]
        public float m_NumOfSecondsBetweenSpawn;
        [Range(10, 1000)]
        public int m_KillMullPonit;
        [Range(5, 50)]
        public float m_MinAdditionForce;
        [Range(10, 200)]
        public float m_MaxAdditionForce;
        [Range(500, 5000)]
        public float m_MaxForce;
        [Range(20, 1000)]
        public float m_MinForce;
        public Vector3 m_MaxPosition;
        public Vector3 m_MinPosition;

        public bool IsRunning => true;

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
            internal set => m_KillMullPonit = value;
        }
        public float MinAdditionForce
        {
            get => m_MinAdditionForce;
            internal set => m_MinAdditionForce = value;
        }
        public float MaxAdditionForce
        {
            get => m_MaxAdditionForce;
            internal set => m_MaxAdditionForce = value;
        }
        public float MaxForce
        {
            get => m_MaxForce;
            internal set => m_MaxForce = value;
        }
        public float MinForce
        {
            get => m_MinForce; internal set => m_MinForce = value;
        }
        //public static GameManagerData Data => Singlton.ManagerData;

        //public GameManagerData ManagerData { get; private set; }
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
            m_FoodObjBuilder = ManagerGameFoodObj.Singlton;
            m_PlayersManager = PlayersManager.InitPlayersManager();
        }

        private void Start()
        {
            m_PlayersManager.InitPlayer();
            m_FoodObjBuilder.InitializeFoodObjOnTheMap(GetAllLoctingOfFoodPrefab(), NumOfInitGameObj);
            StartCoroutine(m_FoodObjBuilder.Spawnnder(NumOfSecondsBetweenSpawn));
        }

        public virtual Vector3 GetRandomPositionInMap()
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

        internal virtual List<Player.Manger.PlayerData> GetPlayersInitialization()
        {
            return new List<Player.Manger.PlayerData>()
            {
                new Player.Manger.PlayerData(m_PlayrPrefab, "player",
                ePlayerModeType.OfflinePlayer, GetRandomPositionInMap()),
                new Player.Manger.PlayerData(m_MLPrefab, "ai", ePlayerModeType.MLTrining,
                GetRandomPositionInMap()),
            };
        }
    }
}