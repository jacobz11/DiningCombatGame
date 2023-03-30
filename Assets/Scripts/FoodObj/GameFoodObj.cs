using DiningCombat.Channels.GameFoodObj;
using DiningCombat.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiningCombat.FoodObj
{
    internal class GameFoodObj : MonoBehaviour
    {
        public event EventHandler HitPlayer;

        public event EventHandler Destruction;

        private bool m_IsThrow;
        private Rigidbody m_Rigidbody;
        private PlayerHand m_PlayerHolding;
        [SerializeField]
        private ParticleSystem m_Effect;
        [SerializeField]
        [Range(0f, 2f)]
        private float m_HitPlayerMull;
        [SerializeField]
        [Range(0f, 100f)]
        private float m_MaxHitPlayerMull;

        public bool IsThrow
        {
            get => m_IsThrow;
            private set
            {
                tag = GameGlobal.TagNames.k_ThrowFoodObj;
                m_IsThrow = value;
            }
        }

        public Action<GameFoodObj> Collect { get; internal set; }

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            ChannelGameFoodObj.s_UickedFruit.ViewingElements += addPositionToTheListOfUnpic;
            
            if (m_Rigidbody == null)
            {
                Debug.LogError("cant find Rigidbody");
            }
        }

        private void addPositionToTheListOfUnpic(List<Vector3> i_ListPassedDuringTheInvoke)
        {
            i_ListPassedDuringTheInvoke.Add(transform.position);
        }

        public void ThrowFood(float i_ForceMulti, Vector3 i_ThrowDirection)
        {
            transform.parent = null;
            m_Rigidbody.constraints = RigidbodyConstraints.None;
            m_Rigidbody.useGravity = true;
            IsThrow = true;

            Debug.DrawRay(transform.position, i_ThrowDirection, Color.black, 3);
            m_Rigidbody.AddForce(i_ForceMulti * i_ThrowDirection);
        }


        internal void PickedUp(PlayerHand i_HoldingGameObj)
        {
            m_PlayerHolding = i_HoldingGameObj;

            if (m_PlayerHolding is not null)
            {
                this.transform.SetParent(this.m_PlayerHolding.PikUpPonit, true);
                this.transform.position = this.m_PlayerHolding.PikUpPonit.position;
                tag = GameGlobal.TagNames.k_Picked;
                ChannelGameFoodObj.s_UickedFruit.ViewingElements -= addPositionToTheListOfUnpic;
                this.m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        private void collisionAfterThrowingHandler(Collision i_Collision)
        {
            if (isPlayer(i_Collision))
            {
                //PlayerHp playerHit = i_Collision.gameObject.GetComponent<PlayerHp>();
                int kill = 0;
                float hitPoint = GetHitPonit();

                //if (playerHit != null)
                //{
                //    if (playerHit.HitYou(hitPoint))
                //    {
                //        kill = 1;
                //    }
                //}

                //if (m_HoldingGameObj.DidIHurtMyself(i_Collision))
                //{
                //    Debug.Log("you stupid son of a bitch? You hurt yourself");
                //}
                //else
                //{
                //    Debug.Log("OnHitPlayer");
                //    OnHitPlayer(new EventHitPlayer(kill, (int)hitPoint));
                //}
            }

            performTheEffect();
            destruction();
        }

        private float GetHitPonit()
        {
            float x = Math.Abs(this.m_Rigidbody.velocity.x);
            float y = Math.Abs(this.m_Rigidbody.velocity.y);
            float z = Math.Abs(this.m_Rigidbody.velocity.z);

            Vector3 v = new Vector3(x, y, z);
            float res = m_HitPlayerMull * (x + y + z);
            m_HitPlayerMull = 0;

            return Math.Min(res, m_MaxHitPlayerMull);
        }

        private bool performTheEffect()
        {
            if (IsThrow && m_Effect != null)
            {
                ParticleSystem effect = Instantiate(m_Effect, transform.position, transform.rotation);

                effect.Play();
                Destroy(effect, 1.5f);
            }

            return true;
        }


        private bool isPlayer(Collision i_Collision)
        {
            return i_Collision.gameObject.CompareTag(GameGlobal.TagNames.k_Player);
        }

        protected virtual void OnCollisionEnter(Collision i_Collision)
        {
            if (IsThrow && m_HitPlayerMull != 0)
            {
                //collisionAfterThrowingHandler(i_Collision);
            }
            //else if (isPlayer(i_Collision)) 
            //{
            //    this.m_Rigidbody.velocity = getInverseVelocity(i_Collision.gameObject);
            //}

        }

        private Vector3 getInverseVelocity(GameObject gameObject)
        {
            Debug.Log("in getInverseVelocity");
            float invX = 0;
            float invY = 0;
            float invZ = 0;
            if (gameObject != null)
            {
                Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    invX = rigidbody.velocity.x * (-0.5f);
                    //invY = rigidbody.velocity.y * (-0.5f);
                    invZ = rigidbody.velocity.z * (-0.5f);
                }
            }

            return new Vector3(invX, invY, invZ);
        }

        protected virtual void OnHitPlayer(EventArgs e)
        {
            HitPlayer?.Invoke(this, e);
        }

        private void destruction()
        {
            OnDestruction(EventArgs.Empty);
            Destroy(this.gameObject, 1);
        }

        protected virtual void OnDestruction(EventArgs e)
        {
            Destruction?.Invoke(this, e);
        }
    }
}
