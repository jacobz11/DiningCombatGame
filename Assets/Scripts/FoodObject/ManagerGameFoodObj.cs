using DiningCombat.DataObject;
using DiningCombat.Environment;
using DiningCombat.FoodObject;
using DiningCombat.Util.DesignPatterns;
using System;
using System.Collections.Generic;
using UnityEngine;
// TODO Replace the class that inherited the new method that works in the scriptable objects
namespace DiningCombat.Manger
{
    public class ManagerGameFoodObj : GenericObjectPool<GameFoodObj>
    {
        public event Action<List<Vector3>> UncollectedPos;
        public event Action OnCollected;

        private float m_LestSpanw;
        [SerializeField]
        private Cuntter m_CuntterOfFoodInTheGame;
        [SerializeField]
        private SpawnData m_SpawnData;
        [SerializeField]
        private Room m_RoomDimension;
        public bool IsSpawnNewGameObj { get; private set; }
        public new static ManagerGameFoodObj Instance { get; private set; }

        private new void Awake()
        {
            if (Instance is not null)
            {
                Destroy(this);
                return;
            }
            OnReturnToPool += FoodObj_OnReturnToPool;
            base.Awake();
            Instance = this;
        }

        private void FoodObj_OnReturnToPool()
        {
            m_CuntterOfFoodInTheGame.TryDec();
        }

        private GameFoodObj Get(Vector3 i_Pos)
        {
            GameFoodObj foodObj = Get();
            foodObj.transform.position = i_Pos;
            foodObj.gameObject.SetActive(true);

            return foodObj;
        }
        //private void InitializationPool(int numOfSetToEnterThePool)
        //{
        //    AddObject(numOfSetToEnterThePool);
        //    _ = m_Objects.OrderBy(obj => Guid.NewGuid());
        //}

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
            GameFoodObj foodObj = Get(m_RoomDimension.GetRendonPos());
            if (foodObj == null)
            {
                Debug.Log("foodObj == null");
            }

            _ = m_CuntterOfFoodInTheGame.TryInc();
            foodObj.OnCollect += FoodObj_OnCollect;
            UncollectedPos += foodObj.ViewElement;

            return foodObj.gameObject;
        }

        private void FoodObj_OnCollect()
        {
            Debug.Log("FoodObj_OnCollect");
            OnCollected?.Invoke();
        }
        public void OnGameOver()
        { /* Not-Implemented */}

        private void Start()
        {
            if (IsServer)
            {
                for (short i = 0; i < m_SpawnData.m_InitSpawn; i++)
                {
                    _ = SpawnGameFoodObj();
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
                _ = SpawnGameFoodObj();
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
}