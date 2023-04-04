using System;
using System.Collections;
using UnityEngine;

namespace DiningCombat.Player
{
    public abstract class BridgeImplementor3DMovement : MonoBehaviour
    {
        private bool ToFastRunnig => true;
        private bool ToRunnig => false;
        [SerializeField] [Range(0.0001f, 2f)]
        private static float s_MinMovmentAbs;
        [SerializeField] [Range(31f, 200f)]
        private float m_BoostRunnigSpeed = 31;
        [SerializeField] [Range(1f, 20f)]
        private float m_BoostTime = 5;
        [SerializeField] [Range(1f, 200f)]
        private float m_StandbyTime = 50;
        private float m_LestBost;

        private Action<bool> RunAnimation;
        protected BridgeAbstraction3DMovement m_Movement;
        protected float m_Horizontal;
        protected float m_Vertical;
        protected PlayerAnimationChannel m_AnimationChannel;
        protected bool m_IsAnyMovement = false;

        public bool IsWaitingBoostTimeOver => Time.time >= m_LestBost + m_StandbyTime;

        protected void Awake()
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

        protected void AnimationRunnig(bool i_IsJump)
        {
            if (!i_IsJump)
            {
                RunAnimation?.Invoke(m_IsAnyMovement);
            }
            m_IsAnyMovement = false;
        }

        public static bool IsMovment(float i_Vale, out bool o_IsPositive)
        {
            o_IsPositive = i_Vale > 0f;
            return Math.Abs(i_Vale) > s_MinMovmentAbs;
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
            }

            m_Horizontal = 0f;
        }

        protected IEnumerator BoostRunning()
        {
            if (IsWaitingBoostTimeOver)
            {
                ALotOfTimeToPreventDoubleEntry();
                Debug.Log("BoostRunning IsWaitingBoostTimeOver");
                changRuningAniamtion(ToFastRunnig);
                m_Movement.ChangeRunningSpeed(m_BoostRunnigSpeed, out float o_RunningSpeed);

                yield return new WaitForSeconds(m_BoostTime);
                m_Movement.ChangeRunningSpeed(o_RunningSpeed, out float _);
                changRuningAniamtion(ToRunnig);
                m_LestBost = Time.time;
                Debug.Log("BoostRunning IsWaitingBoostTimeOver IS OVER");

            }
            else
            {
                Debug.Log("BoostRunning NOT  IsWaitingBoostTimeOver");
            }
        }

        private void ALotOfTimeToPreventDoubleEntry()
        {
            m_LestBost = Time.time + (m_StandbyTime + m_BoostTime) * 10;
        }

        private void changRuningAniamtion(bool i_IsToFastRunnin)
        {
            if (i_IsToFastRunnin)
            {
                RunAnimation += m_AnimationChannel.SetPlayerAnimationToRunFast;
                RunAnimation -= m_AnimationChannel.SetPlayerAnimationToRun;
            }
            else
            {
                RunAnimation += m_AnimationChannel.SetPlayerAnimationToRun;
                RunAnimation -= m_AnimationChannel.SetPlayerAnimationToRunFast;
            }
        }

        public virtual void MoveVertonta()
        {
            bool isRunBack = false;
            if (IsMovment(m_Vertical, out bool o_IsForward))
            {
                if (o_IsForward)
                {
                    m_Movement.MoveForward();
                    m_IsAnyMovement= true;
                }
                else
                {
                    m_Movement.MoveBackward();
                    isRunBack = true;
                    m_IsAnyMovement = false;
                }
            }
            if(m_AnimationChannel is null)
            {
                Debug.Log("m_AnimationChannel is null");
            }
            else
            {
                m_AnimationChannel.SetPlayerAnimationToRunBack(isRunBack);
            }
            RunAnimation?.Invoke(o_IsForward);

            m_Vertical = 0f;
        }

        public virtual bool Jump()
        {
            bool res = false;
            if (m_Movement.Jump())
            {
                m_AnimationChannel.SetPlayerAnimationToJump(true);
                res = true;
            }

            return res;
        }

        public virtual void SetPlayerMovement(BridgeAbstraction3DMovement playerMovement)
        {
            m_Movement = playerMovement;
        }
    }
}
