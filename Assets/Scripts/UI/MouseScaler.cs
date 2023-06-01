using UnityEngine;

public class MouseScaler : MonoBehaviour
{
    private readonly Vector3 r_MagnifiedScale = new Vector3()
    {
        x = 1.2f,
        y = 1.2f,
        z = 1.2f
    };
    public void PointerEnter()
    {
        transform.localScale = r_MagnifiedScale;
    }
    public void PointerExit()
    {
        transform.localScale = Vector3.one;
    }
}
