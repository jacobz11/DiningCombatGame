using Assets.Scrips_new.Bridges.Player.Movment.Abstraction;
using Assets.Scrips_new.Player;
using System;
using UnityEngine;
using static DiningCombat.GameGlobal;

public class PlayerMovement :MonoBehaviour, IMovement , IRotation
{
    private bool m_IsGrounded;
    private Rigidbody m_Rb;
    [SerializeField]
    private float m_MouseSensetivity = 1000;
    [SerializeField]
    [Range(5, 100)]
    private float m_PlayerSpeed = 2.0f;
    [SerializeField]
    [Range(2, 70)]
    private float m_JumpHeight = 1.0f;
    [SerializeField]
    private float m_GravityValue = -9.81f;

    private void Awake()
    {
        m_Rb = gameObject.AddComponent<Rigidbody>();
    }
    public virtual bool IsGrounded
    {
        get => this.m_IsGrounded;
        set => this.m_IsGrounded = value;
    }

    public virtual void Jump()
    {
        if (!IsGrounded)
        {
            m_Rb.AddForce(Vector3.up * m_JumpHeight);
        }
    }
    public virtual void MoveBackward()
    {
        transform.Translate(Vector3.back * Time.deltaTime * m_PlayerSpeed);
    }
    public virtual void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * m_PlayerSpeed);
    }
    public virtual void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * m_PlayerSpeed);
    }
    public virtual void MoveRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime * m_PlayerSpeed);
    }
    public virtual void Rotate(float amount)
    {
        transform.Rotate(Vector3.up, amount * m_MouseSensetivity);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            IsGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            IsGrounded = true;
        }
    }

    public static void Builder(GameObject i_PlayerCharacter, ePlayerModeType i_Type)
    {
        PlayerMovement movement =  i_PlayerCharacter.AddComponent<PlayerMovement>();
        PlayerMovementImplementor implementor;

        switch (i_Type)
        {
            case ePlayerModeType.OfflinePlayer:
                Debug.Log("Builder  PlayerMovement : OfflinePlayer");
                implementor = i_PlayerCharacter.AddComponent<OfflinePlayerMovement>();
                implementor.SetPlayerMovement(movement);
                break; 
            case ePlayerModeType.OnlinePlayer:
                Debug.Log("Builder  PlayerMovement : OnlinePlayer");
                return;
            case ePlayerModeType.OfflineAiPlayer:
                Debug.Log("Builder  PlayerMovement : OfflineAiPlayer");
                return;
            case ePlayerModeType.OnlineAiPlayer:
                Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
                return;
        }
       // TODO : this 
    }
}
