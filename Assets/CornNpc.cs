using Assets.Util.DesignPatterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornNpc : MonoBehaviour
{
    [SerializeField]
    private CornNpcData m_CornData;
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(5);

        for (int i = 0; i < m_CornData.m_NumOfExpltin; i++)
        {
            GameObject popcorn = Instantiate(m_CornData.m_Popcorn, transform.position, Quaternion.identity);
            popcorn.GetComponent<Rigidbody>().velocity = GetVelocity(i) * m_CornData.m_PopcornPower;
        }
    }

    private Vector3 GetVelocity(int i)
    {
        const int k_Directions = 4;
        switch (i % k_Directions)
        {
            case 0:
                return Vector3.forward;
            case 1:
                return Vector3.left;
            case 2:
                return Vector3.back;
            case 3:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
