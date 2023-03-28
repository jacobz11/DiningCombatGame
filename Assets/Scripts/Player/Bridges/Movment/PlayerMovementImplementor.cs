using System;
using System.Collections;
using UnityEngine;

namespace DiningCombat.Player
{
    public abstract class PlayerMovementImplementor : MonoBehaviour
    {
        [SerializeField]
        [Range(0.0001f, 2f)]
        private static float s_MinMovmentAbs;
        [SerializeField]
        [Range(31f, 200f)]
        private float m_BoostRunnigSpeed = 31;
        [SerializeField]
        [Range(1f, 20f)]
        private float m_BoostTime = 5;
        [SerializeField]
        [Range(1f, 200f)]
        private float m_StandbyTime = 50;
        private float m_LestBost;

        private Action RunAnimation;
        protected PlayerMovement m_Movement;
        protected float m_Horizontal;
        protected float m_Vertical;
        protected PlayerAnimationChannel m_AnimationChannel;
        protected bool m_IsAnyMovement = false;

        public bool IsWaitingBoostTimeOver => Time.time >= m_LestBost + m_StandbyTime;
        private float ALotOfTimeToPreventDoubleEntry => (m_StandbyTime + m_BoostTime) * 10 ;

        private void Awake()
        {
            m_AnimationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();

            if (m_AnimationChannel == null)
            {
                Debug.LogError("the PlayerAnimationChannel Not found");
            }
            else
            {
                RunAnimation += m_AnimationChannel.SetPlayerAnimationToRun;
            }

            m_LestBost = Time.time - m_StandbyTime* 2;
        }

        public void Ideal()
        {
            //m_AnimationChannel.
        }

        public static bool IsMovment(float i_Vale, out bool o_IsPositive)
        {
            o_IsPositive = i_Vale > 0f;
            return Math.Abs(i_Vale) > s_MinMovmentAbs;
        }
        public bool IsVertical(out bool o_IsLeft)
        {
            o_IsLeft = m_Vertical > 0;
            return Math.Abs(m_Vertical) > s_MinMovmentAbs;
        }
        public bool IsHorizontal(out bool o_IsForward)
        {
            o_IsForward = m_Horizontal > 0;
            return Math.Abs(m_Horizontal) > s_MinMovmentAbs;
        }

        public virtual void MoveHorizontal()
        {
            if (IsMovment(m_Horizontal, out bool o_IsRight))
            {
                if (o_IsRight)
                {
                    m_Movement.MoveRight();
                }
                else
                {
                    m_Movement.MoveLeft();
                }
                m_IsAnyMovement = true;
                RunAnimation?.Invoke();
            }

            m_Horizontal = 0f;
        }

        protected IEnumerator BoostRunning()
        {
            if (IsWaitingBoostTimeOver)
            {
                m_LestBost = Time.time + ALotOfTimeToPreventDoubleEntry;
                Debug.Log("BoostRunning IsWaitingBoostTimeOver");
                RunAnimation += m_AnimationChannel.SetPlayerAnimationToRunFast;
                RunAnimation -= m_AnimationChannel.SetPlayerAnimationToRun;
                m_Movement.ChangeRunningSpeed(m_BoostRunnigSpeed, out float o_RunningSpeed);
                
                yield return new WaitForSeconds(m_BoostTime);

                m_Movement.ChangeRunningSpeed(o_RunningSpeed, out float _);
                RunAnimation += m_AnimationChannel.SetPlayerAnimationToRun;
                RunAnimation -= m_AnimationChannel.SetPlayerAnimationToRunFast;
                m_LestBost = Time.time;
                Debug.Log("BoostRunning IsWaitingBoostTimeOver IS OVER");

            }
            else
            {
                Debug.Log("BoostRunning NOT  IsWaitingBoostTimeOver");
            }
        }

        public virtual void MoveVertonta()
        {
            if (IsMovment(m_Vertical, out bool o_IsForward))
            {
                if (o_IsForward)
                {
                    m_Movement.MoveForward();
                    RunAnimation?.Invoke();
                }
                else
                {
                    m_Movement.MoveBackward();
                    m_AnimationChannel.SetPlayerAnimationToRunBack();

                }
                m_IsAnyMovement = true;
            }

            m_Vertical = 0f;
        }
        public virtual bool Jump()
        {
            bool res = false;
            if (m_Movement.Jump())
            {
                m_AnimationChannel.SetPlayerAnimationToJump();
                res = true;
            }

            return res;
        }
        public virtual void SetPlayerMovement(PlayerMovement playerMovement)
        {
            m_Movement = playerMovement;
        }
    }
}
