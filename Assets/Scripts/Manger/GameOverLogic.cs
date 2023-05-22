using DiningCombat.Player;
using DiningCombat.UI;
using System;
using TMPro;
using UnityEngine;

// TODO : Integrate it into the code
namespace DiningCombat.Manger
{
    public class GameOverLogic : MonoBehaviour
    {
        private const string k_FormatLivingPlayer = "Living: {0}";

        public event Action GameOverOccured;

        private int m_NumOfAlivePlayers;
        private int m_LivingPlayerCounter;

        [SerializeField]
        private TMP_Text m_GameOverText;
        [SerializeField]
        private TMP_Text m_TextLivingPlayers;
        [SerializeField]
        private LifePointsVisual m_LifePointsVisual;
        public static GameOverLogic Instance { get; private set; }

        private int LivingPlayers
        {
            get => m_LivingPlayerCounter;
            set
            {
                m_LivingPlayerCounter = value;
                m_TextLivingPlayers.text = string.Format(k_FormatLivingPlayer, m_LivingPlayerCounter);
            }
        }
        public void AI_OnAiDead() => Player_OnPlayerDead(false);
        public void Player_OnPlayerDead() => Player_OnPlayerDead(true);
        public void ShowGameOverText() => m_GameOverText.enabled = true;
        public void CharacterEntersTheGame(PlayerLifePoint i_Player)
        {
            if (!i_Player.IsAi)
            {
                m_NumOfAlivePlayers++;
                i_Player.OnPlayerDead += Player_OnPlayerDead;
                i_Player.AddLifePointsVisual(m_LifePointsVisual);
            }
            else
            {
                i_Player.OnPlayerDead += AI_OnAiDead;
            }

            LivingPlayers++;
        }

        private void Awake()
        {
            if (Instance is not null)
            {
                return;
            }

            Instance = this;
            GameOverOccured += ShowGameOverText;
            m_GameOverText.enabled = false;
            m_TextLivingPlayers.enabled = true;
            LivingPlayers = 0;
            m_NumOfAlivePlayers = 0;
        }

        public void Player_OnPlayerDead(bool isAlive)
        {
            Debug.Log("Player_OnPlayerDead " + LivingPlayers);
            LivingPlayers--;

            if (isAlive)
            {
                m_NumOfAlivePlayers--;
            }

            bool isGameOver = LivingPlayers <= 1 || m_NumOfAlivePlayers == 0;

            if (isGameOver)
            {
                Debug.Log("isGameOver ");
                GameOverOccured?.Invoke();
            }
        }
    }
}