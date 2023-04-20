using Assets.Scripts.FoodObject.Pools;
using DesignPatterns.Abstraction;
using System;
using UnityEngine;
using static Assets.DataObject.ThrownActionTypesBuilder;

namespace Assets.DataObject
{
    internal class SmokeGrenade : GrenadeLike
    {
        public SmokeGrenade(ThrownActionTypesBuilder i_BuilderData) 
            : base(i_BuilderData) 
        {
        }

        public override float CalculatorDamag() => 0f;
        
        public override void Activate()
        {
            m_Effect = FoodEffactPool.Instance[m_EffectType].Get();
            m_Effect.transform.position = m_Transform.position;
            m_Effect.SetActive(true);
            m_Countdown = r_EffctTime;
        }
    }
}
