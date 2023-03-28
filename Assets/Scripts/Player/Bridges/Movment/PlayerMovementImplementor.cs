using System;
using UnityEngine;

namespace DiningCombat.Player
{
    public abstract class PlayerMovementImplementor : MonoBehaviour
    {
        [SerializeField]
        [Range(0.0001f, 2f)]
        private static float s_MinMovmentAbs;
        protected PlayerMovement m_Movement;
        protected float m_Horizontal;
        protected float m_Vertical;
        protected PlayerAnimationChannel m_AnimationChannel;

        private void Awake()
        {
            m_AnimationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();
            if (m_AnimationChannel == null)
            {
                Debug.LogError("the PlayerAnimationChannel Not found");
            }
        }
        public bool IsIdeal()
        {
            return IsVertical(out bool _) && IsHorizontal(out bool _);
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
                m_AnimationChannel.SetPlayerAnimationToRun();
            }

            m_Horizontal = 0f;
        }

        public virtual void MoveVertonta()
        {
            if (IsMovment(m_Vertical, out bool o_IsForward))
            {
                if (o_IsForward)
                {
                    m_Movement.MoveForward();
                    m_AnimationChannel.SetPlayerAnimationToRun();
                }
                else
                {
                    m_Movement.MoveBackward();
                    m_AnimationChannel.SetPlayerAnimationToRunBack();

                }
            }

            m_Vertical = 0f;
        }
        public virtual void Jump()
        {
            if (m_Movement.Jump())
            {
                m_AnimationChannel.SetPlayerAnimationToJump();
            }
        }
        public virtual void SetPlayerMovement(PlayerMovement playerMovement)
        {
            m_Movement = playerMovement;
        }
    }
}
