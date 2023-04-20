using static ThrownState;
using System;
using UnityEngine;
using DesignPatterns.Abstraction;
using Assets.Util;
using Assets.Scripts.FoodObject.Pools;

namespace Assets.DataObject
{
    internal abstract class IThrownState : IFoodState, IAnimationDisturbing, IDamaging, IThrownObj
    {
        public event Action OnReturnToPool;
        public class HitPointEventArgs : EventArgs
        {
            public float m_Damage;
            public GameObject m_GetHitPlayer;
            public GameObject m_PlayerTrown;
        }

        public event Action<HitPointEventArgs> OnHit;
        protected Rigidbody m_Rigidbody;
        public AcitonStateMachine Activator { get; protected set; }
        public Vector2 RangeDamage { get; protected set; }
        public Vector3 ActionDirection { get; protected set; }
        public bool IsActionHappen { get; protected set; }

        public bool IsThrowingAction() => false;

        public IThrownState(ThrownActionTypesBuilder i_Data)
        {
            m_Rigidbody = i_Data.Rigidbody;
            RangeDamage = Vector2AsRang.PositiveConstruction(i_Data.m_MaxDamagePoint,
                i_Data.m_MinDamagePoint);
            ActionDirection = Vector3.zero;
            IsActionHappen = false;
        }

        public void SetCollcter(AcitonStateMachine i_Collcter)
        {
            Activator = i_Collcter;
        }
        public bool TryCollect(AcitonStateMachine i_Collcter)
        {
            return false;
        }
        public virtual void SetThrowDirection(Vector3 i_Direction, float i_PowerAmount)
        {
            Debug.Log("SetThrowDirection");
            ActionDirection = i_Direction * i_PowerAmount;
        }

        public virtual void Update()
        {
            Debug.Log("IThrownState");
        }

        public virtual void OnSteteExit()
        {
        }
        

        public virtual void OnSteteEnter()
        {
            Debug.Log("OnSteteEnter IThrownState");
            EnableRagdoll();
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

        public void DisableRagdoll()
        {
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.detectCollisions = false;
        }

        public void EnableRagdoll()
        {
            m_Rigidbody.isKinematic = false;
            m_Rigidbody.detectCollisions = true;
        }


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

        public virtual float CalculatorDamag() => Vector2AsRang.Clamp(m_Rigidbody.velocity.magnitude, RangeDamage);

        protected virtual void ReturnToPool()
        {
            OnReturnToPool?.Invoke();
        }

        internal void SendOnHit(HitPointEventArgs hitPointEventArgs)
        {
            OnHit?.Invoke(hitPointEventArgs);
        }
    }
}
