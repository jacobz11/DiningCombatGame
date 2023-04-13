using Unity.Netcode;
using UnityEngine;

public class PleyrNewwork : NetworkBehaviour
{
    private const float k_Y = 0; 
    [SerializeField]
    private float m_MoveSpeed;

    void Update()
    {
        if (!IsOwner) return;

        float x = Input.GetKey(KeyCode.W) ? 1f :
            Input.GetKey(KeyCode.S) ? -1f : 0f;

        float z = Input.GetKey(KeyCode.A) ? 1f :
            Input.GetKey(KeyCode.D) ? -1f : 0f;

        transform.position += new Vector3(x, k_Y, z) * m_MoveSpeed * Time.deltaTime;
    }
}
