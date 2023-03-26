using Abstraction.Player;
using Assets.Scripts.Test.Stubs;
using DiningCombat.Player.Offline.Movement;
using System;
using UnityEngine;
using static DiningCombat.GameGlobal;



namespace DiningCombat.Player
{
            public class PlayerMovement : MonoBehaviour, IMovement, IRotation
            {
                private bool m_IsGrounded;
                private Rigidbody m_Rb;
                [SerializeField]
                [Range(5, 1000)]
                private float m_MouseSensetivity = 20;
                [SerializeField]
                [Range(5, 100)]
                private float m_PlayerSpeed = 30.0f;
                [SerializeField]
                [Range(2, 500)]
                private float m_JumpHeight = 430.0f;
                [SerializeField]
                private float m_GravityValue = -9.81f;

                private void Awake()
                {
                    m_Rb = gameObject.AddComponent<Rigidbody>();
                    m_Rb.constraints = RigidbodyConstraints.FreezeRotation;
                }
                public virtual bool IsGrounded
                {
                    get => this.m_IsGrounded;
                    set => this.m_IsGrounded = value;
                }

                public virtual void Jump()
                {
                    //Debug.Log("Jump PlayerMovement IsGrounded = " + IsGrounded);

                    if (IsGrounded)
                    {
                        m_Rb.AddForce(Vector3.up * m_JumpHeight);
                    }
                }
                public virtual void MoveBackward()
                {
                    Debug.DrawRay(this.transform.position, Vector3.back, Color.red, 10);
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
                    if (collision.gameObject.CompareTag("Ground"))
                    {
                        IsGrounded = false;
                    }
                }

                private void OnCollisionEnter(Collision collision)
                {
                    if (collision.gameObject.CompareTag("Ground"))
                    {
                        IsGrounded = true;
                    }
                }

                public static void Builder(GameObject i_PlayerCharacter, ePlayerModeType i_Type,
                    out PlayerMovement o_Movement, out PlayerMovementImplementor o_Implementor)
                {
                    o_Movement = i_PlayerCharacter.AddComponent<PlayerMovement>();

                    switch (i_Type)
                    {
                        case ePlayerModeType.OfflinePlayer:
                            Debug.Log("Builder  PlayerMovement : OfflinePlayer");
                            o_Implementor = i_PlayerCharacter.AddComponent<OfflinePlayerMovement>();
                            o_Implementor.SetPlayerMovement(o_Movement);
                            break;
                        case ePlayerModeType.OnlinePlayer:
                            Debug.Log("Builder  PlayerMovement : OnlinePlayer");
                            o_Implementor = null;
                            return;
                        case ePlayerModeType.OfflineAiPlayer:
                            Debug.Log("Builder  PlayerMovement : OfflineAiPlayer");
                            o_Implementor = null;
                            return;
                        case ePlayerModeType.OnlineAiPlayer:
                            Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
                            o_Implementor = null;
                            return;
                        case ePlayerModeType.OfflineTestPlayer:
                            o_Implementor = i_PlayerCharacter.AddComponent<PlayerMovementStub>();
                            o_Implementor.SetPlayerMovement(o_Movement);
                            break;
                        case ePlayerModeType.OnlineTestPlayer:
                            Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
                            o_Implementor = null;
                            return;
                        default:
                            o_Implementor = null;
                            return;
                    }
                    // TODO : this 
                }
            }
        }
