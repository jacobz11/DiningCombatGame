
using Assets.Scripts;
using Assets.Scripts.AbstractFactory;
using DiningCombat;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private OnlineGameAbstractFactory m_GameAbstractFactory;

    [SerializeField]
    private GameObject m_PrefabPlayer;

    [SerializeField]
    private GameObject m_PrefabApple;
    [SerializeField]
    private GameObject m_PrefabDask;

    void Start()
    {
        m_GameAbstractFactory = new OfflineGameAbstractFactory();
        m_GameAbstractFactory.InitiMap();

        initGameFoodObj();
        initPlayers();
    }

    private void initPlayers()
    {
        for(int i = 0; i < 10; i++)
        {
            spawnPlayer();
        }
    }

    private void spawnPlayer()
    {
        GameObject spawn = m_GameAbstractFactory.SpawnPlayer();
        spawn.GetComponent<PlayerMovement>().Destruction += OnDestruction_Player;
    }

    private void initGameFoodObj()
    {
        for (int i = 0; i < 10; i++)
        {
            spawnGameFoodObj();
        }
    }

    private void spawnGameFoodObj()
    {
        GameObject spawn = m_GameAbstractFactory.SpawnGameFoodObj();
        spawn.GetComponent<GameFoodObj>().Destruction += OnDestruction_GameFoodObj;
    }

    protected virtual void OnDestruction_GameFoodObj(object sender, EventArgs e)
    {
    }

    protected virtual void OnDestruction_Player(object sender, EventArgs e)
    {
    }
}
