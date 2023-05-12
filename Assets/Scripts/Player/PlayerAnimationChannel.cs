using Assets.scrips;
using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimationChannel : NetworkBehaviour
{
    public event Action ThrowPoint;
    //public static Action<string, bool> SetAnimationBool;
    //public static Action<string, float> SetAnimationFloat;
    private Animator m_Anim;

    private void Awake()
    {
        m_Anim = GetComponentInChildren<Animator>();
        ThrowPoint += () => StartCoroutine(StopAnimationToThrow());
    }

    public void AnimationBool(string arg1, bool arg2)
    {
        // TODO: change the ones of the inputs
        m_Anim.SetBool(arg1, arg2);
    }
    public void AnimationFloat(string arg1, float arg2)
    {
        // TODO: change the ones of the inputs
        m_Anim.SetFloat(arg1, arg2);
    }

    private void OnStart()
    {
        PlayerMovment player = GetComponentInParent<PlayerMovment>();
        if (player != null)
        {
            //player.OnIsRunnigBackChang += player_OnIsRunnigBackChang;
            //player.OnIsRunnigChang += player_OnIsRunnigChang;
        }
        else
        {
            Debug.Log("cant find PlayerMovment");
        }
    }
    public void EnterThrowPoint()
    {
        ThrowPoint?.Invoke();
    }
    //private void player_OnIsRunnigChang(bool i_IsActive)
    //{
    //    m_Anim.SetBool("isRun", i_IsActive);
    //}

    //private void player_OnIsRunnigBackChang(bool i_IsActive)
    //{
    //    m_Anim.SetBool("isRunBack", i_IsActive);
    //}

    private IEnumerator StopAnimationToThrow()
    {
        yield return null;
        yield return null;
        m_Anim.SetBool("isThrow", false);
        m_Anim.SetBool("isThrow2", false);
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
    }


    //public void SetPlayerAnimationToRunFast(bool i_IsActive)
    //{
    //    m_Anim.SetBool("isRunFast", i_IsActive);
    //}

    //public void SetPlayerAnimationToRun(bool i_IsActive)
    //{
    //    m_Anim.SetBool("isRun", i_IsActive);
    //}

    //public void SetPlayerAnimationToThrow(float i)
    //{
    //    m_Anim.SetTrigger("isThrow");
    //}

    //public void OnJumpingUpEnd()
    //{
    //    JumpingEnd?.Invoke();
    //    m_Anim.SetTrigger("isJumpingDonw");
    //}

    //public void SetPlayerAnimationToRunBack(bool i_IsActive)
    //{
    //    m_Anim.SetBool("isRunBack", i_IsActive);
    //}


    //public void SetPlayerAnimationToIdleFall(bool i_IsActive)
    //{
    //    m_Anim.SetBool("isIdleFall", i_IsActive);
    //}

    //public void SetPlayerAnimationToJump()
    //{
    //    m_Anim.SetTrigger("isJump");
    //}

    //public void SetPlayerAnimationToThrow2(bool i_IsActive)
    //{
    //    m_Anim.SetBool("isThrow", i_IsActive);
    //}

    //public void SetPlayerAnimationToWin(bool i_IsActive)
    //{
    //    m_Anim.SetBool("isWin", i_IsActive);
    //}

    //internal void OnPlayerDead()
    //{
    //    SetPlayerAnimationToIdleFall(true);
    //}

    //internal void SetPlayerAnimationDroping()
    //{
    //    Debug.Log("SetPlayerAnimationDroping");
    //}
}
