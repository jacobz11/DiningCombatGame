
using DiningCombat.DataObject;
using DiningCombat.Player;
using System;
using UnityEngine;
using static DiningCombat.DataObject.ThrownActionTypesBuilder;
namespace DiningCombat.FoodObject
{
    public class MineLike : IThrownState
    {
        private const float k_TImeToTrow = 0.7f;
        private Action DisplayEffectAction;
        protected readonly float r_CountdownTime;
        protected readonly float r_EffectTime;

        protected float m_Countdown;
        protected eElementSpecialByName m_EffectType;
        protected Transform m_Transform;
        protected ParticleSystem m_Effect;

        private readonly float r_ForceHitExsplostin;
        private readonly float r_Radius;

        private float m_TimeBefuerCollision;
        private readonly GameObject r_ObjectVisal;
        private readonly GameObject r_TransparentObjectVisal;
        private readonly Collider r_Triger;

        public MineLike(ThrownActionTypesBuilder i_BuilderData)
            : base(i_BuilderData)
        {
            r_Triger = i_BuilderData.m_MinData.m_Triger;
            r_Radius = i_BuilderData.m_MinData.m_InpactRadius;
            m_Transform = i_BuilderData.Transform;
            m_EffectType = i_BuilderData.m_ElementName;
            r_EffectTime = i_BuilderData.m_MinData.m_EffctTime;
            r_ObjectVisal = i_BuilderData.m_MinData.m_GameObjectVisal;
            r_CountdownTime = i_BuilderData.m_MinData.m_CountdownTime;
            r_ForceHitExsplostin = i_BuilderData.m_MinData.m_ForceHitExsplostin;
            r_TransparentObjectVisal = i_BuilderData.m_MinData.m_AlmostTransparent;
            DisplayEffectAction += DisplayEffect;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            m_TimeBefuerCollision = 0;
            m_Countdown = r_CountdownTime;
        }
        private void ToggleBetweenVisibility(bool i_Visibility)
        {
            r_ObjectVisal.SetActive(!i_Visibility);
            r_TransparentObjectVisal.SetActive(i_Visibility);
        }
        public override void Update()
        {
            m_TimeBefuerCollision += Time.deltaTime;
            m_Countdown -= Time.deltaTime;
            bool isCountdownOver = m_Countdown <= 0f;

            if (isCountdownOver)
            {
                ReturnToPool();
            }
        }

        protected override void ReturnToPool()
        {
            #region Return To Pool Effect 
            if (m_Effect != null)
            {
                m_Effect.gameObject.SetActive(false);
                m_Effect.Stop();
                FoodEffactPool.Instance[m_EffectType].ReturnToPool(m_Effect);
                m_Effect = null;
            }
            #endregion
            ToggleBetweenVisibility(false);
            r_Triger.enabled = false;
            base.ReturnToPool();
        }
        public override void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
        {
            ToggleBetweenVisibility(true);
            r_Triger.enabled = true;
            m_Rigidbody.AddForce(i_Direction.normalized);
        }

        public override void Activation(Collider i_Collider)
        {
            #region Activation not possible
            bool isMinTime = m_TimeBefuerCollision < k_TImeToTrow;

            if (isMinTime || IsActionHappen)
            {
                return;
            }

            float damage = CalculatorDamag();
            if (PlayerLifePoint.TryToDamagePlayer(i_Collider.gameObject, damage, out bool o_Iskill))
            {
                IsActionHappen = true;
                #endregion
                if (!o_Iskill && i_Collider.gameObject.TryGetComponent<Player.Player>(out Player.Player player))
                {
                    player.StartCoroutine(player.ToggleSweepFallEnds(DisplayEffectAction));
                }

                base.SendOnHit(new HitPointEventArgs()
                {
                    m_Damage = damage,
                    m_Kills = o_Iskill ? 1 : 0
                });
            }
        }

        protected void DisplayEffect()
        {
            m_Effect = FoodEffactPool.Instance[m_EffectType].Get();
            m_Effect.transform.position = m_Transform.position;
            m_Effect.gameObject.SetActive(true);
            m_Effect.Play();
        }
    }
}
