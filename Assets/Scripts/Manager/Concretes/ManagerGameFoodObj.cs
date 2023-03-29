using Random = UnityEngine.Random;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using DiningCombat.Channels.GameFoodObj;

namespace DiningCombat.FoodObj.Managers
{
    internal class ManagerGameFoodObj : IManager<ChannelGameFoodObj>
    {
        private static Type m_Type = typeof(GameObject);
        private List<GameObject> m_FoodPrefab;
        private static ManagerGameFoodObj s_Singlton;
        public static ManagerGameFoodObj Singlton
        {
            get
            {
                return s_Singlton;
            }
            private set
            {
                s_Singlton = value;
            }
        }

        public ChannelGameFoodObj FoodChannel
        {
            get { return Channel as ChannelGameFoodObj; }
        }

        private void Awake()
        {
            m_Channel = new ChannelGameFoodObj();
        }
        private GameObject getPrefabFrom(string i_Location)
        {
            return (GameObject)Resources.Load(i_Location, m_Type);
        }
        public GameObject GetRnaomFoodObj()
        {
            return m_FoodPrefab[Random.RandomRange(0, m_FoodPrefab.Count - 1)];
        }
        public bool SpawnGameFoodObj(Vector3 i_Position, out GameObject o_Spawn)
        {
            bool isSpawn = false;
            if (s_GameManager.IsSpawnNewGameObj)
            {
                o_Spawn = SpawnGameFoodObj(i_Position);
                isSpawn = true;
            }
            else
            {
                o_Spawn = null;
            }

            return isSpawn;
        }

        private GameObject SpawnGameFoodObj(Vector3 i_Position)
        {
            GameObject spawn = Instantiate(GetRnaomFoodObj(), i_Position, Quaternion.identity);
            GameFoodObj foodObj = spawn.GetComponent<GameFoodObj>();
            foodObj.Destruction += s_GameManager.OnDestruction_GameFoodObj;
            //foodObj.Collect += OnFoodCollect;
            return spawn;
        }

        private void SetFoodPrefab()
        {
            if (s_GameManager == null)
            {
                Debug.LogError("GameManager is null ");
            }
            else
            {
                List<string> loctingOfPrefab = s_GameManager.GetAllLoctingOfFoodPrefab();
                m_FoodPrefab = new List<GameObject>();

                foreach (string go in loctingOfPrefab)
                {
                    m_FoodPrefab.Add(getPrefabFrom(go));
                }
            }
        }

        internal override void OnGameOver()
        {
            throw new NotImplementedException();
        }

        internal override IEnumerable MainCoroutine()
        {
            for (byte i = 0; i < s_GameManager.m_NumOfInitGameObj; i++)
            {
                SpawnGameFoodObj(s_GameManager.GetRandomPositionInMap());
            }

            //s_GameManager.EndInitMainCoroutine(this, out float o_TimeToWait);
            //yield return new WaitForSeconds(o_TimeToWait);

            while (s_GameManager.IsRunning)
            {
                if (SpawnGameFoodObj(s_GameManager.GetRandomPositionInMap(), out GameObject _))
                {
                    yield return new WaitForSeconds(s_GameManager.m_NumOfSecondsBetweenSpawn);
                }
                else
                {
                    yield return null;
                }
            }
        }

        public static ManagerGameFoodObj InitManagerGameFood()
        {
            if (Singlton == null)
            {
                if (s_GameManager == null)
                {
                    Debug.Log("this it null");
                }

                ManagerGameFoodObj instance = s_GameManager.AddComponent<ManagerGameFoodObj>();
                instance.SetFoodPrefab();
                s_GameManager.GameOver += instance.OnGameOver;
                Singlton = instance;
            }
            else
            {
                Debug.LogWarning("Singlton is not null");
            }

            return Singlton;
        }
        //protected override IManager<ChannelGameFoodObj> Instance()
        //{
        //    if (Singlton == null)
        //    {
        //        SetFoodPrefab();
        //        s_GameManager.GameOver += OnGameOver;
        //        m_Singlton = this;
        //    }

        //    return m_Singlton;
        //}
    }
}
