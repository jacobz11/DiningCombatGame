using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float m_MouseX;
    public float m_MouseSensitivity = 800f;
    float m_XRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // Update is called once per frame
    void Update()
    {
        m_MouseX = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        m_XRotation -= m_MouseX;
        m_XRotation = Mathf.Clamp(m_XRotation, -15f, 15f);
        transform.localRotation = Quaternion.Euler(m_XRotation, 0f, -0f);
    }
}
