using Assets.DataObject;
using Assets.Util;
using DesignPatterns.Abstraction;
using System;
using UnityEngine;

internal class ThrownState : IThrownState
{
    public const int k_Indx = 2;
    private const float k_TimeToTrow = 0.7f;
    private const float k_TimeToReturn = 7.7f;
    
    private float m_TimeBefuerCollision;
    public ThrownState(ThrownActionTypesBuilder i_Data) : base(i_Data)
    {
        m_TimeBefuerCollision = 0f;
    }

    public override void OnSteteEnter()
    {
        base.OnSteteEnter();
        m_TimeBefuerCollision = Time.time;
    }

    public override void Update()
    {
        if (!IsActionHappen)
        {
            m_Rigidbody.AddForce(ActionDirection);
            IsActionHappen = true;
        }
        m_TimeBefuerCollision += Time.deltaTime;
        if (m_TimeBefuerCollision > k_TimeToReturn)
        {
            ReturnToPool();
        }
    }

    public override void Activation(Collision collision)
    {
        if (m_TimeBefuerCollision < k_TimeToReturn)
        {
            return;
        }
        float damage = CalculatorDamag();
        bool isHitPlayer = PlayerLifePoint.TryToDamagePlayer(collision.gameObject, damage, out bool o_IsKiil);

        if (isHitPlayer && !IsHitMyself(collision))
        {
            int kill = o_IsKiil ? 1 : 0;
            Activator.GetScore()?.HitPlayer(collision, damage, kill);
            base.SendOnHit(new IThrownState.HitPointEventArgs
            {
                m_Damage = damage,
                m_GetHitPlayer = collision.gameObject,
                m_PlayerTrown = Activator.gameObject
            });
        }

        bool IsHitMyself(Collision collision)
        {
            return collision.gameObject.Equals(Activator.gameObject);
        }
    }
}
