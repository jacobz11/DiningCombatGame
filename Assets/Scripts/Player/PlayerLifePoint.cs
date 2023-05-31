using DiningCombat.Manger;
using System;
using Unity.Netcode;
using UnityEngine;
namespace DiningCombat.Player
{
    public class PlayerLifePoint : NetworkBehaviour
    {
        public event Action OnPlayerDied;
        public event Action<float> OnPlayerLifePointChanged;
        public const float k_StrtingLifePoint = 100f;

        [SerializeField]
        private float m_LifePoint;
        [SerializeField]
        private Player m_Player;
        private bool m_IsAlive;

        // TODO : is this set need to be public
        public bool IsAi { get => m_Player.IsAi;}

        private void Awake()
        {
            m_LifePoint = k_StrtingLifePoint;
            m_IsAlive = true;
        }

        private void Start()
        {
         
        }

        private void PlayerLifePoint_OnPlayerKilld()
        {
            if (!m_IsAlive)
            {
                return;
            }

            m_IsAlive = false;
            GameManger.Instance.GameOverLogic.Player_OnPlayerDead(IsAi, gameObject.name);
            OnPlayerDied?.Invoke();
        }

        public void OnHitPlayer(float i_HitPoint, out bool o_IsKiil)
        {
            if (i_HitPoint < 0)
            {
                o_IsKiil = false;
                return;
            }

            m_LifePoint -= i_HitPoint;
            o_IsKiil = m_LifePoint <= 0;
            if (o_IsKiil)
            {
                Debug.Log("o_IsKiil : " + o_IsKiil);
                PlayerLifePoint_OnPlayerKilld();
            }

            OnPlayerLifePointChanged?.Invoke(m_LifePoint);
        }

        public static bool TryToDamagePlayer(GameObject i_GameObject, float i_Damage, out bool o_IsKill)
        {
            Debug.Assert(i_Damage >= 0, "TryToDamagePlayer : i_Damage is nagtive");
            bool isPlayer = i_GameObject.gameObject.TryGetComponent<PlayerLifePoint>(out PlayerLifePoint o_PlayerLife);

            if (isPlayer)
            {
                o_PlayerLife.OnHitPlayer(i_Damage, out o_IsKill);
            }
            else
            {
                o_IsKill = false;
            }

            return isPlayer;
        }

        public void Healed(LifePackage i_LifePackage)
        {
            if (i_LifePackage.Amont < 0)
            {
                return;
            }

            m_LifePoint = Math.Max(m_LifePoint + i_LifePackage.Amont, k_StrtingLifePoint);
            OnPlayerLifePointChanged?.Invoke(m_LifePoint);
        }
    }
}