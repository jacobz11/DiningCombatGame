using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class AbstractFactoryProductFood 
{
    private OnlineGameAbstractFactory m_OnlineGameAbstractFactory;
    public AbstractFactoryProductFood(OnlineGameAbstractFactory i_AbstractFactory)
    {

    }

    public GameObject CreatFoodObj()
    {
        return null;
    }

}
