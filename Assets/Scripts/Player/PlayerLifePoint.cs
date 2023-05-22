using DiningCombat.Manger;
using DiningCombat.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace DiningCombat.Player
{
    public class PlayerLifePoint : MonoBehaviour
    {
        public event Action OnPlayerDead;
        private const float k_StrtingLifePoint = 100f;

        [SerializeField]
        private float m_LifePoint;
        [SerializeField]
        private List<LifePointsVisual> m_LifePointsVisual;
        [SerializeField]
        private bool m_IsAi;
        private bool m_IsAlive;

        // TODO : is this set need to be public
        public bool IsAi { get => m_IsAi; set => m_IsAi = value; }

        private void Awake()
        {
            m_LifePoint = k_StrtingLifePoint;
            m_IsAlive = true;
        }

        private void Start()
        {
            GameOverLogic.Instance.CharacterEntersTheGame(this);
            PlayerAnimationChannel animationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();
            if (animationChannel is null)
            {
                Debug.Log("animationChannel is null");
            }
            else
            {
                // TODO: 
                // OnPlayerDead +=  animationChannel.OnPlayerDead;
                //OnPlayerDead += () => animationChannel.AnimationBool(PlayerAnimationChannel.AnimationsNames., true);
            }
        }

        private void PlayerLifePoint_OnPlayerDead()
        {
            if (!m_IsAlive)
            {
                return;
            }

            m_IsAlive = false;
            GameManger.Instance.GameOverLogic.Player_OnPlayerDead(IsAi);
            OnPlayerDead?.Invoke();
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
                PlayerLifePoint_OnPlayerDead();
            }

            float normalizHp = m_LifePoint / k_StrtingLifePoint;
            m_LifePointsVisual.ForEach(visual => { visual.UpdateBarNormalized(normalizHp); });
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

        public void AddLifePointsVisual(LifePointsVisual i_LifePointsVisual)
        {
            m_LifePointsVisual.Add(i_LifePointsVisual);
        }

        public void Healed(LifePackage i_LifePackage)
        {
            if (i_LifePackage.Amont < 0)
            {
                return;
            }

            m_LifePoint = Math.Max(m_LifePoint + i_LifePackage.Amont, k_StrtingLifePoint);

            float normalizHp = m_LifePoint / k_StrtingLifePoint;
            m_LifePointsVisual.ForEach(visual => { visual.UpdateBarNormalized(normalizHp); });
        }
    }
}