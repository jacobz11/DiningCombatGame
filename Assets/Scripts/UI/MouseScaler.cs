using UnityEngine;

public class MouseScaler : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_MagnifiedScale = new Vector3()
    {
        x = 1.2f,
        y = 1.2f,
        z = 1.2f
    };
    public void OnPointerEnter(Transform i_BtmTra)
    {
        i_BtmTra.localScale = m_MagnifiedScale;
    }
    public void OnPointerExit(Transform i_BtmTra)
    {
        i_BtmTra.localScale = Vector3.one;
    }
}
