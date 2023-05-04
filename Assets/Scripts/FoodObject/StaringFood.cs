using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.FoodObject
{
    internal class StaringFood : NetworkBehaviour, IDamaging
    {
        private Rigidbody m_Rigidbody;
        [SerializeField]
        private float m_Higt;
        [SerializeField]
        private float m_Damag;
        [SerializeField]
        protected Vector2 m_RangeDamage;
        [SerializeField]
        private ParticleSystem m_PrEffect;
        [SerializeField]
        private float r_ForceHitExsplostin;
        [SerializeField]
        private float r_Radius;

        public float CalculatorDamag() => m_Damag;
        public Vector2 RangeDamage => m_RangeDamage;

        public Vector3 ActionDirection => Vector3.up;

        public bool IsActionHappen { get; private set; }

        private void Awake()
        {
            m_Rigidbody.AddForce(ActionDirection * m_Higt);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Activation(collision);
        }
        protected void DisplayEffect()
        {
            GameObject.Instantiate(m_PrEffect, this.transform);
        }

        public void Activation(Collision collision)
        {
            float damage = CalculatorDamag();

            DisplayEffect();

            IsActionHappen = true;

            foreach (Collider nearByObj in Physics.OverlapSphere(transform.position, r_Radius))
            {
                if (nearByObj.TryGetComponent<Rigidbody>(out Rigidbody o_Rb))
                {
                    o_Rb.AddExplosionForce(r_ForceHitExsplostin, transform.position, r_Radius);
                }
                PlayerLifePoint.TryToDamagePlayer(nearByObj.gameObject, damage, out bool _);
            }

            Destroy(this, 0.3f);
        }

        public void Activation(Collider i_Collider)
        {
        }

        public void Activate()
        {
        }


    }
}
