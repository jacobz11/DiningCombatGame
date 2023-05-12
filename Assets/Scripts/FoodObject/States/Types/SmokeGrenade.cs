using UnityEngine;

namespace Assets.DataObject
{
    internal class SmokeGrenade : GrenadeLike
    {
        public override float CalculatorDamag() => 0f;
        public SmokeGrenade(ThrownActionTypesBuilder i_BuilderData)
            : base(i_BuilderData)
        { /* Not-Implemented */}

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("GrenadeLike OnSteteEnter");
        }

        public override void Activate()
        {
            DisplayEffect();
            m_Countdown = r_EffectTime;
        }
    }
}
