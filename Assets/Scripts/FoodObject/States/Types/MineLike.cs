
using DiningCombat.DataObject;
using DiningCombat.Player;
using UnityEngine;
using static DiningCombat.DataObject.ThrownActionTypesBuilder;

namespace DiningCombat.FoodObject
{
    public class MineLike : IThrownState
    {
        private const float k_TImeToTrow = 0.7f;

        protected readonly float r_CountdownTime;
        protected readonly float r_EffectTime;

        protected float m_Countdown;
        protected eElementSpecialByName m_EffectType;
        protected Transform m_Transform;
        protected ParticleSystem m_Effect;

        private readonly float r_ForceHitExsplostin;
        private readonly float r_Radius;

        private float m_TimeBefuerCollision;
        private GameObject m_ObjectVisal;
        private GameObject m_TransparentObjectVisal;
        private Collider m_Triger;

        public MineLike(ThrownActionTypesBuilder i_BuilderData)
            : base(i_BuilderData)
        {
            m_Triger = i_BuilderData.m_MinData.m_Triger;
            r_Radius = i_BuilderData.m_MinData.m_InpactRadius;
            m_Transform = i_BuilderData.Transform;
            m_EffectType = i_BuilderData.m_ElementName;
            r_EffectTime = i_BuilderData.m_MinData.m_EffctTime;
            m_ObjectVisal = i_BuilderData.m_MinData.m_GameObjectVisal;
            r_CountdownTime = i_BuilderData.m_MinData.m_CountdownTime;
            r_ForceHitExsplostin = i_BuilderData.m_MinData.m_ForceHitExsplostin;
            m_TransparentObjectVisal = i_BuilderData.m_MinData.m_AlmostTransparent;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
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
            #region Activation not possible
            bool isMinTime = m_TimeBefuerCollision < k_TImeToTrow;
            bool isHitEinv = i_Collider.CompareTag("environment");

            if (isMinTime || IsActionHappen || isHitEinv)
            {
                return;
            }
            #endregion
            DisplayEffect();
            float damage = CalculatorDamag();
            float ponits = 0;
            int kills = 0;
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

            base.SendOnHit(new HitPointEventArgs() { /* Not-Implemented */});
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
