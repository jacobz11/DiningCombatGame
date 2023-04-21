using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    private const string k_FormatLivingPlayer = "Living: {0}";
    public event Action GameOverOccured;
    [SerializeField] TMP_Text m_Text;
    [SerializeField] TMP_Text m_GameOverText;
    private int m_NumOfAlivePlayers;
    private int m_LivingPlayerCounter;
    public int LivingPlayers
    {
        get => m_LivingPlayerCounter;
        set
        {
            m_LivingPlayerCounter = value;
            m_Text.text = string.Format(k_FormatLivingPlayer, m_LivingPlayerCounter);
        }
    }

    private void Awake()
    {
        GameOverOccured += ShowGameOverText;
        m_GameOverText.enabled = false;
        m_LivingPlayerCounter = 0;
        m_NumOfAlivePlayers = 0;
    }

    public void AI_OnAiDead()
    {
        Player_OnPlayerDead(false);
    }

    public void Player_OnPlayerDead()
    {
        Player_OnPlayerDead(true);
    }

    private void Player_OnPlayerDead(bool isAlive)
    {
        Debug.Log("cubeScript_CubeDestroyed " + isAlive + " Alives: " + m_NumOfAlivePlayers);
        LivingPlayers--;
        if (isAlive)
        {
            m_NumOfAlivePlayers--;
        }
        bool isGameOver = LivingPlayers <= 1 || m_NumOfAlivePlayers == 0;
        if (isGameOver)
        {
            GameOverOccured?.Invoke();
        }
    }

    public void ShowGameOverText()
    {
        m_GameOverText.enabled = true;
    }
}
