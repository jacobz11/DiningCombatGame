using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    /*
    float rotationX = 0f;
    float rotationY = 0f;
    [SerializeField] float sensetivity = 15f;
    */
    PickUpItem pickUpItem;

    private void Start()
    {
        pickUpItem = FindObjectOfType<PickUpItem>();
    }
    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.E) && pickUpItem.forceMulti > 1600 && pickUpItem.forceMulti < 1910)
        {
            StartCoroutine(Shake(0.14f, 0.5f));
        }
        else
        {
            transform.position = target.position + offset;

        }
        /*
        rotationY += Input.GetAxis("Mouse X") * sensetivity;
        rotationX += Input.GetAxis("Mouse Y") * -1 * sensetivity;
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        */
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-0.2f, 0.2f) * magnitude;
            float y = Random.Range(-0.2f, 0.2f) * magnitude;
            transform.position = target.position  + offset + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = transform.position + offset;
    }
}
