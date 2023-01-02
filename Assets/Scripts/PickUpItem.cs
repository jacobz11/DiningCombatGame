using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private Transform pickUpPoint;
    private Transform player;
    private Rigidbody rb;
  // public CameraShake cameraShake;

    [SerializeField] float pickUpDistance;
    [SerializeField] public float forceMulti;
    [SerializeField] bool readyToThrow;
    [SerializeField] bool itemIsPicked;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        pickUpPoint = GameObject.Find("PickUpPoint").transform;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.E) && itemIsPicked && readyToThrow)
        {
            
            if (forceMulti < 1900) 
            {
              //  StartCoroutine(cameraShake.Shake(0.14, 0.5));
                forceMulti += 500 * Time.deltaTime;
                PowerCounter.powerValue = forceMulti;
                
            }       
        }

        pickUpDistance = Vector3.Distance(player.position, transform.position);

        if (pickUpDistance <= 2)
        {
            if (Input.GetKeyDown(KeyCode.E) && itemIsPicked == false && pickUpPoint.childCount < 1)
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<BoxCollider>().enabled = false;
                this.transform.position = pickUpPoint.position;
                this.transform.parent = GameObject.Find("PickUpPoint").transform;

                itemIsPicked = true;
                forceMulti = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && itemIsPicked == true)
        {
            readyToThrow = true;

            if (forceMulti > 10)
            {
                rb.AddForce(player.transform.forward * forceMulti);
                this.transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().enabled = true;
                itemIsPicked = false;

                forceMulti = 0;
                readyToThrow = false;
            }
            forceMulti = 0;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Capsule"))
        {
            ScoreCounter.scoreValue += 1;
            Destroy(col.gameObject, 1);
            Destroy(gameObject, 1);
        }
        
    }
}
