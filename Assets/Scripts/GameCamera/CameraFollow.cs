
using System;
using UnityEngine;

namespace DiningCombat
{
    public class CameraFollow : MonoBehaviour
    {
        private const string k_AxisMouseX = "Mouse X";

        // variables
        [SerializeField]
        private float m_MouseSensetivity;

        // references
        [SerializeField]
        private Transform m_Parent;

        protected void Start()
        {
            m_Parent = transform.parent;
         
            Cursor.lockState = CursorLockMode.Locked;
            initDefualtSerializeField();
        }

        private void initDefualtSerializeField()
        {
            if (m_Parent == null)
            {
                m_Parent = GameObject.Find(GameGlobal.GameObjectName.k_Player).transform;
            }
            if (m_MouseSensetivity <= 0f)
            {
                m_MouseSensetivity = GameGlobal.k_DefaultCameraFollowMouseSensetivity;
            }
        }

        protected void Update()
        {
            rotate();
        }

        private void rotate()
        {
            m_Parent.Rotate(Vector3.up, getMouseCameraMove());
        }

        private float getMouseCameraMove()
        {
            return Input.GetAxis(k_AxisMouseX) * m_MouseSensetivity * Time.deltaTime;
        }
    }
}