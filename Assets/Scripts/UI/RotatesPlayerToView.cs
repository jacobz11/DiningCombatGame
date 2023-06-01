using System.Collections.Generic;
using UnityEngine;

public class RotatesPlayerToView : MonoBehaviour
{
    public List<Material> m_Materials;
    private int m_CurrentMaterialIndex = 0;
    [SerializeField]
    private Renderer m_PlayerRenderer;
    [SerializeField]
    [Range(0f, 1f)]
    private float m_RotationSpeed = 100f;
    [SerializeField]
    private float m_ChangePoint;
    private float m_Rotation = -180.0f;

    private void Start()
    {
        if (m_Materials.Count > 0)
        {
            m_PlayerRenderer.material = m_Materials[m_CurrentMaterialIndex];
        }
    }

    private void Update()
    {
        // Rotate the player continuously
        float rotation = m_RotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotation);
        m_Rotation += rotation;
        if (m_Rotation >= 360.0f)
        {
            m_Rotation = 0f;
            // Switch to the next material in the list
            m_CurrentMaterialIndex = (m_CurrentMaterialIndex + 1) % m_Materials.Count;
            m_PlayerRenderer.material = m_Materials[m_CurrentMaterialIndex];
        }
    }
}
