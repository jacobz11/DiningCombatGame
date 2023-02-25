using Assets.Scripts;
using DiningCombat;
using System;
using UnityEngine;
using Random = System.Random;

public class AbstractFactoryProductFood 
{

    // ================================================
    // constant Variable 
    private const byte k_NumOfPrefab = GameGlobal.FoodObjData.k_NumOfPrefab;
    private const byte k_ApplIndex = GameGlobal.FoodObjData.k_AppleVar;
    private const byte k_FlourIndex = GameGlobal.FoodObjData.k_FlourVar;
    private const byte k_CabbageIndex = GameGlobal.FoodObjData.k_CabbageVar;
    private const byte k_TomatoIndex = GameGlobal.FoodObjData.k_TomatoVar;

    private static readonly Type sr_TypeGameObject = typeof(GameObject);
    // ================================================
    // Delegate

    // ================================================
    // Fields 
    private OnlineGameAbstractFactory m_OnlineGameAbstractFactory;
    private GameObject[] m_FoodPrefab;
    private readonly Random r_Rnd;
    // ================================================
    // ----------------Serialize Field-----------------

    // ================================================
    // properties

    // ================================================
    // auxiliary methods programmings

    // ================================================
    // Unity Game Engine

    // ================================================
    //  methods
    public AbstractFactoryProductFood(OnlineGameAbstractFactory i_AbstractFactory)
    {
        m_OnlineGameAbstractFactory = i_AbstractFactory;
        r_Rnd = new Random();

        // init 
        m_FoodPrefab = new GameObject[k_NumOfPrefab];
        m_FoodPrefab[k_ApplIndex] = getPrefabFrom(GameGlobal.FoodObjData.k_AppleLocation);
        m_FoodPrefab[k_FlourIndex] = getPrefabFrom(GameGlobal.FoodObjData.k_FlourLocation);
        m_FoodPrefab[k_TomatoIndex] = getPrefabFrom(GameGlobal.FoodObjData.k_TomatoLocation);
        m_FoodPrefab[k_CabbageIndex] = getPrefabFrom(GameGlobal.FoodObjData.k_CabbageLocation);
    }

    private GameObject getPrefabFrom(string i_Location)
    {
        return (GameObject)Resources.Load( i_Location, sr_TypeGameObject);
    }
    public GameObject CreatFoodObj()
    {
        return m_FoodPrefab[r_Rnd.Next(m_FoodPrefab.Length)];
    }

    // ================================================
    // auxiliary methods

    // ================================================
    // Delegates Invoke 

    // ================================================
    // ----------------Unity--------------------------- 
    // ----------------GameFoodObj---------------------



}
