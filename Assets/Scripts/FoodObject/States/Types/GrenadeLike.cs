﻿using DiningCombat.DataObject;
using DiningCombat.Player;
using DiningCombat.Util;
using UnityEngine;

using static DiningCombat.DataObject.ThrownActionTypesBuilder;

namespace DiningCombat.FoodObject
{
    public class GrenadeLike : IThrownState
    {
        private readonly float r_ForceHitExsplostin;
        private readonly float r_Radius;
        private readonly GameObject r_ObjectVisal;

        protected readonly float r_CountdownTime;
        protected readonly float r_EffectTime;

        private bool m_IsThrown;
        protected float m_Countdown;

        protected eElementSpecialByName m_EffectType;

        protected Transform m_Transform;
        protected ParticleSystem m_Effect;
        public override float CalculatorDamag() => Vector2AsRang.Random(RangeDamage);

        public GrenadeLike(ThrownActionTypesBuilder i_BuilderData) : base(i_BuilderData)
        {
            r_Radius = i_BuilderData.m_GrenadeData.InpactRadius;
            m_Transform = i_BuilderData.Transform;
            r_EffectTime = i_BuilderData.m_GrenadeData.EffctTime;
            m_EffectType = i_BuilderData.m_ElementName;
            r_CountdownTime = i_BuilderData.m_GrenadeData.LifeTimeUntilAction;
            r_ForceHitExsplostin = i_BuilderData.m_GrenadeData.ForceHitExsplostin;
            r_ObjectVisal = i_BuilderData.m_MinData.m_GameObjectVisal;
            m_Effect = i_BuilderData.m_Effect;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            m_Rigidbody.AddForce(ActionDirection);
            m_Countdown = r_CountdownTime;
            m_IsThrown = false;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            r_ObjectVisal.SetActive(true);
        }

        public override void Update()
        {
            if (!m_IsThrown)
            {
                m_Rigidbody.AddForce(ActionDirection);
                m_IsThrown = true;
            }

            m_Countdown -= Time.deltaTime;
            bool isCountdownOver = m_Countdown <= 0f;

            if (isCountdownOver)
            {
                if (!IsActionHappen)
                {
                    Activate();
                }
                else
                {
                    ReturnToPool();
                }
            }
        }

        protected override void ReturnToPool()
        {
            if (m_Effect is not null)
            {
                m_Effect.Stop();
                m_Effect.gameObject.SetActive(false);
                FoodEffactPool.Instance[m_EffectType].ReturnToPool(m_Effect);
                m_Effect = null;
            }

            base.ReturnToPool();
        }

        protected void DisplayEffect()
        {
            m_Effect = FoodEffactPool.Instance[m_EffectType].Get();
            m_Effect.gameObject.transform.position = m_Transform.position;
            m_Effect.gameObject.SetActive(true);
            m_Effect.Play();
            r_ObjectVisal.SetActive(false);
        }

        public override void Activate()
        {
            float damage = CalculatorDamag();
            float ponits = 0;
            int kills = 0;

            DisplayEffect();

            m_Countdown = r_EffectTime;
            IsActionHappen = true;
            foreach (Collider nearByObj in Physics.OverlapSphere(m_Transform.position, r_Radius))
            {
                if (nearByObj.TryGetComponent<Rigidbody>(out Rigidbody o_Rb))
                {
                    o_Rb.AddExplosionForce(r_ForceHitExsplostin, m_Transform.position, r_Radius);
                }

                if (PlayerLifePoint.TryToDamagePlayer(nearByObj.gameObject, damage, out bool o_IsKill))
                {
                    ponits += damage;
                    kills += o_IsKill ? 1 : 0;
                }
            }

            base.SendOnHit(new HitPointEventArgs()
            {
                m_Damage = ponits,
                m_Kills = kills,
            });
        }
    }
}