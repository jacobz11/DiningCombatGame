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

        public override void OnSteteEnter()
        {
            base.OnSteteEnter();
            Debug.Log("GrenadeLike OnSteteEnter");
        }

        public override float CalculatorDamag() => 0f;
        
        public override void Activate()
        {
            DisplayEffect();
            m_Countdown = r_EffectTime;
        }
    }
}
