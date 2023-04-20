using Assets.Util.DesignPatterns;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static Assets.DataObject.ThrownActionTypesBuilder;

namespace Assets.Scripts.FoodObject.Pools
{
    internal class FoodEffactPool : NetworkBehaviour
    {
        [Serializable]
        public class ParticleSystemPool
        {
            private const int k_AddingParTime = 1;
            [SerializeField]
            private ParticleSystem m_Prefap;

            protected Queue<ParticleSystem> m_Objects = new Queue<ParticleSystem>();

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
                newObj.gameObject.SetActive(false);
                m_Objects.Enqueue(newObj);
            }
        }
        private ParticleSystem m_PrefapFlour;
        private ParticleSystem m_PrefapPomegranate;
        private ParticleSystem m_PrefapBanana;

        [SerializeField]
        private ParticleSystemPool m_FlourPool;
        [SerializeField] 
        private ParticleSystemPool m_PomegranatePool;
        [SerializeField]
        private ParticleSystemPool m_BananaPool;

        public static FoodEffactPool Instance { get; protected set; }

        private void Awake()
        {
            if(Instance is not null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            if (m_PrefapFlour is null)
            {
                Debug.Log("m_PrefapFlour is null");
            }
            //m_FlourPool.Prefap = m_PrefapFlour;
            //m_PomegranatePool.Prefap = m_PrefapPomegranate;
            //m_BananaPool.Prefap = m_PrefapBanana;
        }
        
        public ParticleSystemPool this[eElementSpecialByName i_Type]
        {
            get 
            {
                ParticleSystemPool res = null;
                switch (i_Type)
                {
                    case eElementSpecialByName.FlourSmokeGrenade:
                        res =  m_FlourPool;
                        break;
                    case eElementSpecialByName.PomegranateGrenade:
                        res = m_PomegranatePool;
                        break;
                    case eElementSpecialByName.BananaMine:
                        res = m_BananaPool;
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