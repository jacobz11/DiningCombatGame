using System;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    internal class GameingData
    {
        [Range(10, 100)]
        private byte m_MaxNumOfFoodObj;
        [Range(0, 10)]
        private byte m_NumOfInitGameObj;
        [Range(0, 10)]
        public float m_NumOfSecondsBetweenSpawn;
        private Vector3 m_MaxPosition;
        private Vector3 m_MinPosition;
        private int m_KillMullPonit;
        private float m_MinAdditionForce;
        private float m_MaxAdditionForce;
        private float m_MaxForce;
        private float m_MinForce;

        public byte MaxNumOfFoodObj
        {
            get { return m_MaxNumOfFoodObj;}
        }

        public byte NumOfInitGameObj
        {
            get { return m_NumOfInitGameObj;}
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
    }
}
