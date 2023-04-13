using Random = UnityEngine.Random;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace DiningCombat.FoodObj.Managers
{
    internal class ManagerGameFoodObj : IElementsViewer
    {
        private static Type m_Type = typeof(GameObject);
        private static ManagerGameFoodObj s_Singlton;
        private List<GameObject> m_FoodPrefab;
        public event Action<List<Vector3>> UickedFruit;
        public int NumOfExistingFoobObj { get; private set; }
        public bool IsSpawnNewGameObj => NumOfExistingFoobObj < GameManager.Singlton.MaxNumOfFoodObj;

        static ManagerGameFoodObj()
        {
            s_Singlton = new ManagerGameFoodObj();
        }

        private ManagerGameFoodObj()
        {
        }

        public static ManagerGameFoodObj Singlton
        {
            get => s_Singlton;
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
            o_Spawn = null;
            if (IsSpawnNewGameObj)
            {
                o_Spawn = SpawnGameFoodObj(i_Position);
                isSpawn = true;
            }

            return isSpawn;
        }

        private GameObject SpawnGameFoodObj(Vector3 i_Position)
        {
            GameObject spawn = GameObject.Instantiate(GetRnaomFoodObj(), i_Position, Quaternion.identity);
            FoodObject foodObj = spawn.GetComponent<FoodObject>();
            foodObj.Destruction += OnDestruction_GameFoodObj;
            UickedFruit += foodObj.ViewElement;
            NumOfExistingFoobObj++;

            return spawn;
        }

        private void OnDestruction_GameFoodObj()
        {
            NumOfExistingFoobObj--;
        }

        public void InitializeFoodObjOnTheMap(List<string> i_LoctingOfPrefab, int i_NumOfFoodObjToSpawn)
        {
            if (i_LoctingOfPrefab is null || i_LoctingOfPrefab.Count == 0)
            {
                Debug.LogError("the list must with at least one object");
                return;
            }
            m_FoodPrefab = new List<GameObject>();

            foreach (string go in i_LoctingOfPrefab)
            {
                m_FoodPrefab.Add(getPrefabFrom(go));
            }

            for(int i = 0; i< i_NumOfFoodObjToSpawn; ++i)
            {
                SpawnGameFoodObj(GameManager.Singlton.GetRandomPositionInMap());
            }
        }

        public IEnumerator Spawnnder(float i_InitSpawnTime)
        {
            yield return new WaitForSeconds(i_InitSpawnTime);

            while (GameManager.Singlton.IsRunning)
            {
                if (IsSpawnNewGameObj)
                {
                    SpawnGameFoodObj(GameManager.Singlton.GetRandomPositionInMap());
                }
                yield return new WaitForSeconds(GameManager.Singlton.NumOfSecondsBetweenSpawn);
            }
        }
        internal void OnGameOver()
        {
            throw new NotImplementedException();
        }

        public List<Vector3> GetElements()
        {
            List<Vector3> list = new List<Vector3>();
            UickedFruit?.Invoke(list);
            return list;
        }

        public bool FindTheNearestOne(Vector3 i_From, out Vector3 o_NearestElement)
        {
            bool res = false;
            List<Vector3> allPos = GetElements();

            if (allPos.Count < 0) 
            {
                allPos.OrderBy(other => Vector3.Distance(i_From, other));
                o_NearestElement = allPos.ElementAt(0);
                res = true;
            }
            else
            {
                o_NearestElement = Vector3.zero;
            }

            return res;
        }
    }
}