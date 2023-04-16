//using DiningCombat.FoodObj.Managers;
//using DiningCombat.Managers;
//using System;
//using UnityEngine;

//[Serializable]
//internal class GameManagerData
//{
//    [Range(10, 255)]
//    public byte m_MaxNumOfFoodObj;
//    [Range(0, 80)]
//    public byte m_NumOfInitGameObj;
//    [Range(0, 10)]
//    public float m_NumOfSecondsBetweenSpawn;
//    [Range(10, 1000)]
//    public int m_KillMullPonit;
//    [Range(5, 50)]
//    public float m_MinAdditionForce;
//    [Range(10, 200)]
//    public float m_MaxAdditionForce;
//    [Range(500, 5000)]
//    public float m_MaxForce;
//    [Range(20, 1000)]
//    public float m_MinForce;
//    public Vector3 m_MaxPosition;
//    public Vector3 m_MinPosition;

//    public bool IsRunning => true;

//    public byte MaxNumOfFoodObj
//    {
//        get { return m_MaxNumOfFoodObj; }
//    }

//    public byte NumOfInitGameObj
//    {
//        get { return m_NumOfInitGameObj; }
//    }
//    public float NumOfSecondsBetweenSpawn
//    {
//        get { return m_NumOfSecondsBetweenSpawn; }
//    }
//    public Vector3 MinPosition
//    {
//        get { return m_MinPosition; }
//    }
//    public Vector3 MaxPosition
//    {
//        get { return m_MaxPosition; }
//    }

//    public int KillMullPonit
//    {
//        get => m_KillMullPonit;
//        internal set => m_KillMullPonit = value;
//    }
//    public float MinAdditionForce
//    {
//        get => m_MinAdditionForce;
//        internal set => m_MinAdditionForce = value;
//    }
//    public float MaxAdditionForce
//    {
//        get => m_MaxAdditionForce;
//        internal set => m_MaxAdditionForce = value; 
//    }
//    public float MaxForce
//    {
//        get => m_MaxForce;
//        internal set => m_MaxForce = value;
//    }
//    public float MinForce
//    {
//        get => m_MinForce; internal set => m_MinForce = value;
//    }
//}
