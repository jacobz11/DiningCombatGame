using Assets.Scripts.FoodObject.Pools;
using DesignPatterns.Abstraction;
using System;
using UnityEngine;
using static Assets.DataObject.ThrownActionTypesBuilder;
using static Assets.Scripts.FoodObject.Pools.FoodEffactPool;

namespace Assets.DataObject
{
    internal class MineLike : IThrownState
    {
        private const float k_TImeToTrow = 0.7f;
        protected readonly float r_CountdownTime;
        protected readonly float r_EffctTime;
        private readonly float r_ForceHitExsplostin;
        private readonly float r_Radius;
        protected float m_Countdown;
        protected eElementSpecialByName m_EffectType;
        protected Transform m_Transform;
        protected ParticleSystem m_Effect;
        private GameObject m_ObjectVisal;
        private GameObject m_TransparentObjectVisal;
        private Collider m_Triger;
        private float m_TimeBefuerCollision;

        public MineLike(ThrownActionTypesBuilder i_BuilderData) 
            : base(i_BuilderData)
        {
            m_EffectType = i_BuilderData.m_ElementName;
            r_CountdownTime = i_BuilderData.m_MinData.m_CountdownTime;
            r_ForceHitExsplostin = i_BuilderData.m_MinData.m_ForceHitExsplostin;
            r_Radius = i_BuilderData.m_MinData.m_InpactRadius;
            m_ObjectVisal = i_BuilderData.m_MinData.m_GameObjectVisal;
            m_TransparentObjectVisal = i_BuilderData.m_MinData.m_AlmostTransparent;
            m_Triger = i_BuilderData.m_MinData.m_Triger;
        }

        public override void OnSteteEnter()
        {
            Debug.Log("OnSteteEnter MineLike");
            base.OnSteteEnter();
            m_TimeBefuerCollision = 0;
            m_Countdown = r_CountdownTime;
        }
        private void ToggleBetweenVisibility(bool i_Visibility)
        {
            m_ObjectVisal.SetActive(!i_Visibility);
            m_TransparentObjectVisal.SetActive(i_Visibility);
        }
        public override void Update()
        {

            m_TimeBefuerCollision += Time.deltaTime;
            m_Countdown -= Time.deltaTime;
            bool isCountdownOver = m_Countdown <= 0f;
            Debug.Log("m_Countdown :" + m_Countdown + " isCountdownOver :" + isCountdownOver + " IsActionHappen " + IsActionHappen);
            if (isCountdownOver || !IsActionHappen)
            {
                ReturnToPool();
            }
            else
            {
                Debug.LogFormat("m_Countdown {0} m_TimeBefuerCollision {1} IsActionHappen {2}", m_Countdown, m_TimeBefuerCollision, IsActionHappen);
            }
        }

        protected override void ReturnToPool()
        {
            Debug.Log("ReturnToPool");
            if (m_Effect!= null)
            {
                m_Effect.gameObject.SetActive(false);
                m_Effect.Stop();
                FoodEffactPool.Instance[m_EffectType].ReturnToPool(m_Effect);
                m_Effect = null;
            }
            ToggleBetweenVisibility(false);
            m_Triger.enabled = false;
            base.ReturnToPool();
        }
        public override void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
        {
            ToggleBetweenVisibility(true);
            m_Triger.enabled = true;
            m_Rigidbody.AddForce(0.5f, 0.5f, 0.5f);
        }

        public override void Activation(Collider i_Collider)
        {
            if (m_TimeBefuerCollision < k_TImeToTrow)
            {
                return;
            }
            m_Countdown = r_EffctTime;

            DisplayEffect();

            IsActionHappen = true;
            float damage = CalculatorDamag();
            float ponits = 0;
            int kills = 0;
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

            });
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
