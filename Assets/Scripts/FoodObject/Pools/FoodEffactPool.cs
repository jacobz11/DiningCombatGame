using Assets.Util.DesignPatterns;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static Assets.DataObject.ThrownActionTypesBuilder;

namespace Assets.Scripts.FoodObject.Pools
{
    internal class FoodEffactPool : NetworkBehaviour
    {
        [SerializeField]
        private GenericGameObjectPool m_FlourPool;
        [SerializeField]
        private GenericGameObjectPool m_PomegranatePool;
        [SerializeField]
        private GenericGameObjectPool m_PopcornPool;
        public static FoodEffactPool Instance { get; protected set; }

        private void Awake()
        {
            if(Instance is not null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }
        
        public GenericGameObjectPool this[eElementSpecialByName i_Type]
        {
            get 
            {
                GenericGameObjectPool res = null;
                switch (i_Type)
                {
                    case eElementSpecialByName.FlourSmokeGrenade:
                        res =  m_FlourPool;
                        break;
                    case eElementSpecialByName.PomegranateGrenade:
                        res = m_PomegranatePool;
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