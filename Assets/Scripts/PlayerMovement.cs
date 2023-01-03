using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiningCombat
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string k_AxisHorizontal = "Horizontal";
        private const string k_AxisVertical = "Vertical";
        private const bool k_NotOnTheGround = false;


        [SerializeField]
        private float m_Speed = 7f;

        [SerializeField]
        private float m_JumpForce = 5f;

        [SerializeField]
        private bool m_IsOnGround = true;
        private Rigidbody m_PlayerRb;

        protected void Start()
        {
            m_PlayerRb = GetComponent<Rigidbody>();
        }

        protected void Update()
        {
            getPlayerAxis(out float horizontal, out float forward);
            float speedTime = m_Speed * Time.deltaTime;

            transform.Translate(forward * speedTime * Vector3.forward);
            transform.Translate(horizontal * speedTime * Vector3.right);

            jump();
        }

        private void jump()
        {
            if (IsJump)
            {
                m_PlayerRb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
                m_IsOnGround = k_NotOnTheGround;
            }
        }

        private bool IsJump
        {
            get
            {
                return Input.GetKeyDown(GameGlobal.k_JumpKey) && m_IsOnGround;
            }
        }

        private void getPlayerAxis(out float o_Horizontal, out float o_Vertical)
        {
            o_Horizontal = Input.GetAxis(k_AxisHorizontal);
            o_Vertical = Input.GetAxis(k_AxisVertical);
        }

        protected void OnCollisionEnter(Collision collision)
        {
            m_IsOnGround = isCollisionGround(collision);
        }

        private bool isCollisionGround(Collision collision)
        {
            return collision.gameObject.CompareTag(GameGlobal.k_TagCapsule);
        }
    }
}