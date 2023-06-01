using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
namespace DiningCombat.Player
{

    public class PlayerAnimationChannel : NetworkBehaviour
    {
        public event Action ThrowPoint;
        public event Action OnPlayerGotUp;
        private Animator m_Anim;

        private void Awake()
        {
            m_Anim = GetComponentInChildren<Animator>();
            ThrowPoint += () => StartCoroutine(StopAnimationToThrow());
        }

        public void AnimationBool(string i_AnimationClip, bool i_Active)
        {
            m_Anim.SetBool(i_AnimationClip, i_Active);
        }
        public void AnimationFloat(string i_AnimationClip, float i_Var)
        {
            m_Anim.SetFloat(i_AnimationClip, i_Var);
        }

        public void EnterThrowPoint()
        {
            ThrowPoint?.Invoke();
        }

        public void PlayerGotUp()
        {
            OnPlayerGotUp?.Invoke();
        }

        private IEnumerator StopAnimationToThrow()
        {
            yield return null;
            yield return null;
            m_Anim.SetBool("isThrow", false);
        }

        public class AnimationsNames
        {
            /// <summary>
            /// input type - bool AnimationBool 
            /// Animation for the player to run fast
            /// </summary>
            public const string k_RunFast = "isRunFast";

            /// <summary>
            /// input type - bool AnimationBool 
            /// Animation for the player to run
            /// </summary>
            public const string k_Running = "isRun";

            /// <summary>
            /// input type - bool AnimationBool 
            /// Animation for the player to Throw
            /// </summary>
            public const string k_Throw = "isThrow";

            /// <summary>
            /// input type - bool AnimationBool 
            /// Animation for the player to run Back
            /// </summary>
            public const string k_RunBack = "isRunBack";

            /// <summary>
            /// input type - bool AnimationBool 
            /// When the player wins
            /// </summary>
            public const string k_Winner = "isWin";

            /// <summary>
            /// input type - bool AnimationBool 
            /// Move to the Left
            /// </summary>
            public const string k_Left = "isWalkLeft";

            /// <summary>
            /// input type - bool AnimationBool 
            /// Move to the right 
            /// </summary>
            public const string k_Right = "isWalkRight";

            /// <summary>
            /// blend tree var - move Horizontal
            /// input type - float  AnimationFloat: 
            ///         inupt = 0 - not moving,
            ///         inupt < 0 - Forward ,
            ///         inupt > 0 - backwards,
            /// Move to Forward 
            /// </summary>
            public const string k_Forward = "Forward";

            /// <summary>
            /// blend tree var - move vertically
            /// input type - float  AnimationFloat: 
            ///         inupt = 0 - not moving,
            ///         inupt < 0 - Left,
            ///         inupt > 0 - right,
            /// Move to Sides 
            /// side 
            /// </summary>
            public const string k_Sides = "Sides";

            /// <summary>
            /// input type - bool AnimationBool 
            /// Animation for Jumping
            /// </summary>
            public const string k_Jumping = "isJumping";

            /// <summary>
            /// input type - bool AnimationBool 
            /// the character is on the floor
            /// changes it when a character jumps/or is in the air - false
            /// </summary>
            public const string k_Grounded = "isGrounded";

            /// <summary>
            /// input type - bool AnimationBool 
            /// Will trigger the animation when he is in the air regardless of whether he falls or completes the jump
            /// </summary>
            public const string k_Falling = "isFalling";

            /// <summary>
            /// input type - bool AnimationBool 
            /// Two handed shot for watermelon
            /// </summary>
            public const string k_ThrowIn = "isThrowIn";

            /// <summary>
            /// input type - bool AnimationBool 
            /// Sweep on banana
            /// </summary>
            public const string k_SweepFall = "isSweepFall";
        }
    }
}
