using DiningCombat.Channels.GameFoodObj;
using DiningCombat.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace DiningCombat.FoodObj
{
    internal class GameFoodObj : MonoBehaviour
    {
        public event Action<float, int> HitPlayer;

        public event Action Destruction;

        private bool m_IsThrow;
        private Rigidbody m_Rigidbody;
        private BridgeAbstractionAction m_PlayerHolding;
        [SerializeField]
        private ParticleSystem m_Effect;
        [SerializeField]
        [Range(0f, 2f)]
        private float m_HitPlayerMull;
        [SerializeField]
        [Range(0f, 100f)]
        private float m_MaxHitPlayerMull;
        public int FramesToExitPlayer = 5;

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
            EnableRagdoll();
            transform.parent = null;
            m_Rigidbody.useGravity = true;
            m_Rigidbody.AddForce(i_ForceMulti * i_ThrowDirection);
            StartCoroutine(WaitForFrames());
            Debug.DrawRay(transform.position, i_ThrowDirection, Color.black, 3);
        }

        private IEnumerator WaitForFrames()
        {
            for(int i =0; i< FramesToExitPlayer; ++i)
            {
                yield return null;
            }
            IsThrow = true;
        }

        internal void PickedUp(BridgeAbstractionAction i_HoldingGameObj)
        {
            m_PlayerHolding = i_HoldingGameObj;

            if (m_PlayerHolding is not null)
            {
                this.transform.SetParent(this.m_PlayerHolding.PikUpPonit, true);
                this.transform.position = this.m_PlayerHolding.PikUpPonit.position;
                tag = GameGlobal.TagNames.k_Picked;
                ChannelGameFoodObj.s_UickedFruit.ViewingElements -= addPositionToTheListOfUnpic;
                DisableRagdoll();
            }
        }

        // Let the rigidbody take control and detect collisions.
        void EnableRagdoll()
        {
            m_Rigidbody.isKinematic = false;
            m_Rigidbody.detectCollisions = true;
        }

        // Let animation control the rigidbody and ignore collisions.
        void DisableRagdoll()
        {
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.detectCollisions = false;
        }

        private void collisionAfterThrowingHandler(Collision i_Collision)
        {
            Debug.Log("collisionAfterThrowingHandler : "  + isPlayer(i_Collision)); 
            if (isPlayer(i_Collision))
            {
                Debug.Log("in (isPlayer(i_Collision)");
                IntiraelPlayerManger playerManger = i_Collision.gameObject.GetComponentInChildren<IntiraelPlayerManger>();
                if (playerManger != null)
                {
                    Debug.Log("playerManger != null ");
                    float hitPoint = GetHitPonit();
                    playerManger.HitPlayer(hitPoint, out bool o_IsKiil);
                    int kill = o_IsKiil ? 1 : 0;

                    if (m_PlayerHolding.DidIHurtMyself(i_Collision))
                    {
                        Debug.Log("you stupid son of a bitch? You hurt yourself");
                    }
                    else
                    {
                        HitPlayer?.Invoke(hitPoint, kill);
                    }
                }
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
                collisionAfterThrowingHandler(i_Collision);
            }
        }

        private void destruction()
        {
            Destruction?.Invoke();
            Destroy(this.gameObject, 1);
        }
    }
}
