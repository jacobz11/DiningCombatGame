
using System;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerMovmentData", menuName = "Custom Objects/Player/Movment")]
public class PlayerMovmentDataSO : ScriptableObject
{
    private const float k_NoSlowDownJump = 1.0f;
    [Range(5, 100)]
    public float m_MouseSensetivity;
    [Range(5, 100)]
    public float m_PlayerSpeed;
    [Range(5, 100)]
    public float m_JumpHeight;
    [Range(0f, 1f)]
    public float m_JumpSlowDownSpeed;
    public LayerMask m_Ground;

    public float ConfigJumpSlowDownSpeed(bool i_IsGrounded)
    {
        return i_IsGrounded ? k_NoSlowDownJump : m_JumpSlowDownSpeed;
    }
}
