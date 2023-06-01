using DiningCombat.DataObject;
using DiningCombat.Environment;
using DiningCombat.Util.DesignPatterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiningCombat.Manger
{
    public class ManagerGamePackage : GenericObjectPool<IPackage>
    {
        public event Action<List<Vector3>> UncollectedPos;
        private short m_Cuntter;
        public short Cuntter
        {
            get => m_Cuntter;
            private set
            {
                m_Cuntter = value;
                if (m_Cuntter < m_SpawnData.m_MinItemSpawn)
                {
                    SpawnPackage();
                }
            }
        }
        private float m_LestSpanw;
        [SerializeField]
        private SpawnDataSO m_SpawnData;
        [SerializeField]
        private Room m_RoomDimension;

        public bool IsSpawnNewGameObj { get; private set; }
        public new static ManagerGamePackage Instance { get; private set; }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        private void Awake()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            if (Instance is not null)
            {
                Destroy(this);
                return;
            }

            base.Awake();
            Instance = this;
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
            IPackage package = Get(m_RoomDimension.GetRendonPos());
            Cuntter++;
            m_LestSpanw = 0;

            return package.gameObject;
        }

        public void OnGameOver()
        { /* Not-Implemented */}

        private void OnDestruction_GameFoodObj()
        {
            Cuntter--;
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
            bool isNotMax = Cuntter <= m_SpawnData.m_MaxItemSpawn;

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