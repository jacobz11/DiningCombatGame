using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float m_MouseSensitivity = 800f;
    private const float k_AngleClamp = 15.0f;
    private float m_MouseX;
    private float m_XRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        m_MouseX = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        m_XRotation -= m_MouseX;
        m_XRotation = Mathf.Clamp(m_XRotation, -k_AngleClamp, k_AngleClamp);
        transform.localRotation = Quaternion.Euler(m_XRotation, 0f, -0f);
    }
}
