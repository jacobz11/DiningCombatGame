using DiningCombat.Player;
using DiningCombat.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private readonly PlayerChannel r_PlayerChannel = new PlayerChannel();
        public PlayerChannel PlayerChannel { get { return r_PlayerChannel; } }

        private int LivingPlayers
        {
            get => m_LivingPlayerCounter;
            set
            {
                m_LivingPlayerCounter = value;
                m_TextLivingPlayers.text = string.Format(k_FormatLivingPlayer, m_LivingPlayerCounter);
            }
        }
        public void ShowGameOverText() => m_GameOverText.enabled = true;
        public void CharacterEntersTheGame(PlayerLifePoint i_Player)
        {
            if (!i_Player.IsAi)
            {
                m_NumOfAlivePlayers++;
            }

            r_PlayerChannel.AddPlayer(i_Player.name);
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
            GameOverOccured += EndGame;
            m_GameOverText.enabled = false;
            m_TextLivingPlayers.enabled = true;
            LivingPlayers = 0;
            m_NumOfAlivePlayers = 0;
        }

        public void Player_OnPlayerDead(bool i_IsAI, string i_PlayerName)
        {
            Debug.Log($"Player_OnPlayerDead name {i_PlayerName} {LivingPlayers}");
            LivingPlayers--;

            if (i_IsAI)
            {
                m_NumOfAlivePlayers--;
            }

            bool isGameOver = LivingPlayers <= 1 || m_NumOfAlivePlayers == 0;
            r_PlayerChannel.UpdatePlayer(i_PlayerName);
            if (isGameOver)
            {
                GameOverOccured?.Invoke();
            }
        }

        public override string ToString()
        {
            return r_PlayerChannel.GetPrintabulTable();
        }
        private void EndGame()
        {
            SceneManager.LoadScene(GameGlobal.ScenesName.k_GameOver);
        }
    }
}