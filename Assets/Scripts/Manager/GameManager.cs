using DiningCombat.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Abstraction;
using static DiningCombat.GameGlobal;
using Random = UnityEngine.Random;
using DiningCombat.FoodObj.Managers;
namespace DiningCombat
{
    internal class GameManager : MonoBehaviour
    {
        [SerializeField]
        [Range(10, 100)]
        private byte m_MaxNumOfFoodObj;
        [SerializeField]
        [Range(0, 10)]
        public byte m_NumOfInitGameObj;
        [SerializeField]
        [Range(0, 10)]
        public float m_NumOfSecondsBetweenSpawn;
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
        //public static List<PlayerData> s_PlayerDatas = new List<PlayerData>();

        public bool IsRunning => true;
        public bool IsSpawnNewGameObj => m_NumOfExistingFoobObj < m_MaxNumOfFoodObj;

        private void Awake()
        {
            ManagerGameFoodObj.SetGameManager(this);
            m_FoodObjBuilder = ManagerGameFoodObj.InitManagerGameFood();
            PlayersManager.SetGameManager(this);
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
            yield return new WaitForSeconds(m_NumOfSecondsBetweenSpawn);
            while (IsRunning)
            {
                if (IsSpawnNewGameObj)
                {
                    this.SpawnGameFoodObj();
                }
                yield return new WaitForSeconds(m_NumOfSecondsBetweenSpawn);
            }
        }

        public Vector3 GetRandomPositionInMap()
        {
            return new Vector3(
                Random.Range(m_MinPosition.x, m_MaxPosition.x),
                Random.Range(m_MinPosition.y, m_MaxPosition.y),
                Random.Range(m_MinPosition.z, m_MaxPosition.z)
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
            if (managerGameFoodObj is ManagerGameFoodObj)
            {
                this.m_NumOfExistingFoobObj = m_NumOfInitGameObj;
                o_TimeToWait = (float)m_NumOfSecondsBetweenSpawn;
            }

            o_TimeToWait = 0;
        }

        internal List<Player.Manger.PlayerInternalManger.PlayerData> GetPlayersInitialization()
        {
            return new List<Player.Manger.PlayerInternalManger.PlayerData>()
            {
                new Player.Manger.PlayerInternalManger.PlayerData(m_PlayrPrefab, "player",
                ePlayerModeType.OfflinePlayer, GetRandomPositionInMap())
            };
        }
    }
}