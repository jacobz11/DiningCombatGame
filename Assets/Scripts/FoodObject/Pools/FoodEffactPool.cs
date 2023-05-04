using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static Assets.DataObject.ThrownActionTypesBuilder;

namespace Assets.Scripts.FoodObject.Pools
{
    internal class FoodEffactPool : NetworkBehaviour
    {
        [SerializeField]
        private GameObject m_ObjectLayer;
        [Serializable]
        public class ParticleSystemPool
        {
            private const int k_AddingParTime = 1;
            [SerializeField]
            private ParticleSystem m_Prefap;

            protected Queue<ParticleSystem> m_Objects = new();
            public Transform ObjectLayer { get; set; }
            public ParticleSystem Get()
            {
                if (m_Objects.Count == 0)
                {
                    AddObject(k_AddingParTime);
                }

                return m_Objects.Dequeue();
            }

            public void ReturnToPool(ParticleSystem i_EnteringObject)
            {
                i_EnteringObject.gameObject.SetActive(false);
                m_Objects.Enqueue(i_EnteringObject);
            }

            protected virtual void AddObject(int i_Count)
            {
                ParticleSystem newObj = GameObject.Instantiate(m_Prefap);
                newObj.transform.parent = ObjectLayer.transform;
                newObj.gameObject.SetActive(false);
                m_Objects.Enqueue(newObj);
            }
        }
        public static FoodEffactPool Instance { get; protected set; }

        private  ParticleSystem m_PrefapFlour;
        private ParticleSystem m_PrefapPomegranate;
        private ParticleSystem m_PrefapBanana;

        [SerializeField]
        private ParticleSystemPool m_FlourPool;
        [SerializeField]
        private ParticleSystemPool m_PomegranatePool;
        [SerializeField]
        private ParticleSystemPool m_BananaPool;
        [SerializeField]
        private ParticleSystemPool m_PopcornPool;

        private void Awake()
        {
            if (Instance is not null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            if (m_PrefapFlour is null)
            {
                Debug.Log("m_PrefapFlour is null");
            }
            m_FlourPool.ObjectLayer = m_ObjectLayer.transform;
            m_PomegranatePool.ObjectLayer = m_ObjectLayer.transform;
            m_BananaPool.ObjectLayer = m_ObjectLayer.transform;
        }

        public ParticleSystemPool this[eElementSpecialByName i_Type]
        {
            get
            {
                ParticleSystemPool res = null;
                switch (i_Type)
                {
                    case eElementSpecialByName.FlourSmokeGrenade:
                        res = m_FlourPool;
                        break;
                    case eElementSpecialByName.PomegranateGrenade:
                        res = m_PomegranatePool;
                        break;
                    case eElementSpecialByName.BananaMine:
                        res = m_BananaPool;
                        break;
                    case eElementSpecialByName.NpcCorn:
                        res = m_PopcornPool;
                        break;
                    default:
                        Debug.LogError("try to get not exiting" + i_Type);
                        break;
                }
                return res;
            }
        }
    }
}