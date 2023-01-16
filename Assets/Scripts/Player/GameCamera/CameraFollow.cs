
using System;
using UnityEngine;

namespace DiningCombat
{
    public class CameraFollow : MonoBehaviour
    {
        // ================================================
        // constant Variable 
        private const string k_AxisMouseX = "Mouse X";
        
        // ================================================
        // Delegate

        // ================================================
        // Fields 

        // ================================================
        // ----------------Serialize Field-----------------
        [SerializeField]
        private float m_MouseSensetivity;
        [SerializeField]
        private Transform m_Parent;

        // ================================================
        // properties

        // ================================================
        // auxiliary methods programmings
        private void initDefualtSerializeField()
        {
            if (m_Parent == null)
            {
                m_Parent = GameObject.Find(GameGlobal.GameObjectName.k_Player).transform;
            }
            if (m_MouseSensetivity <= 0f)
            {
                m_MouseSensetivity = GameGlobal.PlayerData
                    .k_DefaultCameraFollowMouseSensetivity;
            }
        }

        // ================================================
        // Unity Game Engine
        protected void Start()
        {
            m_Parent = transform.parent;

            Cursor.lockState = CursorLockMode.Locked;
            initDefualtSerializeField();
        }
        protected void Update()
        {
            rotate();
        }
        // ================================================
        //  methods
        private void rotate()
        {
            m_Parent.Rotate(Vector3.up, getMouseCameraMove());
        }

        // ================================================
        // auxiliary methods
        private float getMouseCameraMove()
        {
            return Input.GetAxis(k_AxisMouseX) * m_MouseSensetivity * Time.deltaTime;
        }
        // ================================================
        // Delegates Invoke 
        // ================================================
        // ----------------Unity--------------------------- 
    }
}