using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseScale : MonoBehaviour
{
    public void PointerEnter()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    public void PointerExit()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
