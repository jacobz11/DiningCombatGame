using DiningCombat.DataObject;
using DiningCombat.Player;
using DiningCombat.Util;
using UnityEngine;
using UnityEngine.Rendering;

namespace DiningCombat.FoodObject
{
    public class ThrownState : IThrownState
    {
        public const int k_Indx = 2;
        private const float k_TimeToTrow = 0.7f;
        private const float k_TimeToReturn = 7.7f;

        private float m_TimeBefuerCollision;
        public ThrownState(ThrownActionTypesBuilder i_Data) : base(i_Data)
        {
            m_TimeBefuerCollision = 0f;
        }

        public override float CalculatorDamag()
        {
            return Vector2AsRang.Random(RangeDamage);
        }


        public override void OnStateEnter()
        {
            base.OnStateEnter();
            m_TimeBefuerCollision = 0f;
        }

        public override void Update()
        {
            if (!IsActionHappen)
            {
                m_Rigidbody.AddForce(ActionDirection);
                IsActionHappen = true;
                m_TimeBefuerCollision = 0f;
            }

            m_TimeBefuerCollision += Time.deltaTime;
            if (m_TimeBefuerCollision > k_TimeToReturn)
            {
                ReturnToPool();
            }
        }

        public override void Activation(Collision i_Collision)
        {
            #region Collision befor the time
            if (i_Collision.gameObject.CompareTag(GameGlobal.TagNames.k_Environment))
            {
                ReturnToPool();
                return;
            }
            if (m_TimeBefuerCollision < k_TimeToTrow)
            {
                return;
            }
            #endregion

            float damage = CalculatorDamag();
            bool isHitPlayer = PlayerLifePoint.TryToDamagePlayer(i_Collision.gameObject, damage, out bool o_IsKiil);

            if (isHitPlayer && !IsHitMyself(i_Collision))
            {
                int kill = o_IsKiil ? 1 : 0;
                Activator.GetScore()?.HitPlayer(i_Collision, damage, kill);
                base.SendOnHit(new IThrownState.HitPointEventArgs
                {
                    m_Damage = damage,
                    m_Kills = kill
                });
            }

            m_TimeBefuerCollision = k_TimeToReturn;

            bool IsHitMyself(Collision collision)
            {
                return collision.gameObject.Equals(Activator.gameObject);
            }
        }
    }

}