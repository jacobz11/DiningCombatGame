
using System;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerMovmentData", menuName = "Custom Objects/Player/Movment")]
public class PlayerMovmentDataSO : ScriptableObject
{
    [Range(5, 100)]
    public float m_MouseSensetivity;
    [Range(5, 100)]
    public float m_PlayerSpeed;
    [Range(5, 100)]
    public float m_JumpHeight;
    [Range(0f, 1f)]
    public float m_JumpSlowDonwSpeep;
    public LayerMask m_Ground;
}
