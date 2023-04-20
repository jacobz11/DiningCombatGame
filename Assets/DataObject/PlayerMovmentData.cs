
using System;
using UnityEngine;
[Serializable]
internal struct PlayerMovmentData
{
    [Range(5, 100)]
    public float m_MouseSensetivity;
    [Range(5, 100)]
    public float m_PlayerSpeed;
    [Range(2, 500)]
    public float m_JumpHeight;
    [Range(0f, 1f)]
    public float m_JumpSlowDonwSpeep;
    public LayerMask m_Ground;
}
