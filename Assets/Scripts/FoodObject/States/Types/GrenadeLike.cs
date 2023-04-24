using Assets.Scripts.FoodObject.Pools;
using Assets.Util;
using DesignPatterns.Abstraction;
using System;
using UnityEngine;
using static Assets.DataObject.ThrownActionTypesBuilder;
using static Assets.Scripts.FoodObject.Pools.FoodEffactPool;

namespace Assets.DataObject
{
    internal class GrenadeLike : IThrownState
    {
        protected readonly float r_CountdownTime;
        protected readonly float r_EffectTime;
        private readonly float r_ForceHitExsplostin;
        private readonly float r_Radius;
        protected float m_Countdown;
        protected eElementSpecialByName m_EffectType;
        protected Transform m_Transform;
        protected ParticleSystem m_Effect;

        public GrenadeLike(ThrownActionTypesBuilder i_BuilderData) : base(i_BuilderData)
        {
            r_Radius = i_BuilderData.m_GrenadeData.InpactRadius;
            m_Transform = i_BuilderData.Transform;
            r_EffectTime = i_BuilderData.m_GrenadeData.EffctTime;
            m_EffectType = i_BuilderData.m_ElementName;
            r_CountdownTime = i_BuilderData.m_GrenadeData.LifeTimeUntilAction;
            r_ForceHitExsplostin = i_BuilderData.m_GrenadeData.ForceHitExsplostin;
        }

        public override void OnSteteEnter()
        {
            base.OnSteteEnter();
            m_Rigidbody.AddForce(ActionDirection);
            m_Countdown = r_CountdownTime;
        }


        public override void OnSteteExit()
        {
        }

        public override float CalculatorDamag() => Vector2AsRang.Random(RangeDamage);
        public override void Update()
        {
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
            if (m_Effect != null)
            {
                m_Effect.gameObject.SetActive(false);
                m_Effect.Stop();
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
        }

        public override void Activate()
        {
            DisplayEffect();
            m_Countdown = r_EffectTime;
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
    }
}

/*
 *    private const float k_TimeOFExplod = 1.5f;
    private float m_Timr = 2;
    private float m_Radius;
    private float m_CountDown;
    [SerializeField]
    private GameObject m_ExplodPartical;
    [SerializeField]
    private float r_ForceHitExsplostin = 200;
    [SerializeField]
    private float damage;

    public bool HasExploded { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        m_CountDown = m_Timr;
    }

    // Update is called once per frame
    void Update()
    {
        m_CountDown -= Time.deltaTime;
        if (m_CountDown <= 0 && !HasExploded)
        {
            Explod();
        }
    }

    private void Explod()
    {
        GameObject Explod = Instantiate(m_ExplodPartical, transform.position, transform.rotation);
        HasExploded = true;
        float ponits = 0;
        int kills = 0;
        foreach (Collider nearByObj in Physics.OverlapSphere(transform.position, m_Radius))
        {
            if (nearByObj.TryGetComponent<Rigidbody>(out Rigidbody o_Rb))
            {
                o_Rb.AddExplosionForce(r_ForceHitExsplostin, transform.position, m_Radius);
            }
            if (PlayerLifePoint.TryToDamagePlayer(nearByObj.gameObject, damage, out bool o_IsKill))
            {
                ponits += damage;
                kills += o_IsKill ? 1 : 0;
            }
        }

        Destroy(Explod, k_TimeOFExplod);
    }
 * 
 */