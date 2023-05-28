using DiningCombat.DataObject;
using DiningCombat.Player;
using DiningCombat.Util;
using DiningCombat.Util.DesignPatterns;
using System;
using UnityEngine;

namespace DiningCombat.FoodObject
{
    public abstract class IThrownState : IFoodState, IDamaging
    {
        public class HitPointEventArgs : EventArgs
        {
            public float m_Damage;
            public int m_Kills;
        }
        public event Action OnReturnToPool;
        public event Action<HitPointEventArgs> OnHit;

        protected Rigidbody m_Rigidbody;
        public ActionStateMachine Activator { get; protected set; }
        public Vector2 RangeDamage { get; protected set; }
        public Vector3 ActionDirection { get; protected set; }
        public bool IsActionHappen { get; protected set; }

        public string TagState => GameGlobal.TagNames.k_ThrowFoodObj;
        public bool IsThrowingAction() => false;
        public bool TryCollect(ActionStateMachine i_Collcter) => false;
        protected virtual void ReturnToPool() => OnReturnToPool?.Invoke();
        public virtual float CalculatorDamag() => Vector2AsRang.Clamp(m_Rigidbody.velocity.magnitude, RangeDamage);
        public void SendOnHit(HitPointEventArgs hitPointEventArgs) => OnHit?.Invoke(hitPointEventArgs);

        public IThrownState(ThrownActionTypesBuilder i_Data)
        {
            m_Rigidbody = i_Data.Rigidbody;
            RangeDamage = Vector2AsRang.PositiveConstruction(i_Data.m_MaxDamagePoint, i_Data.m_MinDamagePoint);
            ActionDirection = Vector3.zero;
            IsActionHappen = false;
        }

        public void SetCollector(ActionStateMachine i_Collcter)
        {
            Activator = i_Collcter;
        }

        public virtual void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
        {
            ActionDirection = i_Direction * i_PowerAmount;
        }

        public virtual void Update()
        { /* Not-Implemented */}

        public virtual void OnStateExit()
        { /* Not-Implemented */}

        public virtual void OnStateEnter()
        {
            Ragdoll.EnableRagdoll(m_Rigidbody);
            ActionDirection = Vector3.zero;
            Activator = null;
        }

        public void AddListener(Action<EventArgs> i_Action, IDCState.eState i_State)
        {
            switch (i_State)
            {
                case IDCState.eState.ExitingState:
                    OnHit += i_Action;
                    break;
            }
        }
        #region Activation 
        public virtual void Activation(Collision collision)
        {
            Debug.LogWarning("ThrownState Activation by Collision");
        }

        public virtual void Activation(Collider i_Collider)
        {
            Debug.LogWarning("ThrownState Activation by triger");
        }

        public virtual void Activate()
        {
            Debug.LogWarning("ThrownState Activation");
        }
        #endregion
    }
}