using System;
using UnityEngine;

public class TestFromUL : MonoBehaviour
{
    public float m_Speed = 10.0f;
    public float m_AutoSpeed = 2f;
    public float m_AutoRotationSpeed = 0.5f;
    public float m_RotationSpeed = 10.0f;
    public GameObject m_Target;
    private bool m_AutoPailot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void autoPailot() 
    {
        calculatAngles();
        this.transform.position += this.transform.forward * m_AutoSpeed * Time.deltaTime;
    }

    private void calculatAngles()
    {
        Vector3 aiForward = transform.forward;
        Vector3 targetDirection = m_Target.transform.position - this.transform.position;

        Debug.DrawRay(this.transform.position, aiForward * 10, Color.green, 5);
        Debug.DrawRay(this.transform.position, targetDirection, Color.red, 5);

        float dot = aiForward.x * targetDirection.x + aiForward.y * targetDirection.y +
            aiForward.z * targetDirection.z;

        float angle = Mathf.Acos(dot/  (aiForward.magnitude * targetDirection.magnitude));
        
        Debug.Log("angle : " + angle* Mathf.Rad2Deg);
        Debug.Log("Unity angle : " + Vector3.Angle(aiForward, targetDirection));

        int clockwise = (cross(aiForward, targetDirection )).z > 0 ? 1 : -1;

        if (angle * Mathf.Rad2Deg > 10) 
        {
            this.transform.Rotate(0, 0, angle * m_AutoRotationSpeed * clockwise * Mathf.Rad2Deg);
        }
    }

    private Vector3 cross(Vector3 i_Left, Vector3 i_Right)
    {
        float xMull = i_Left.y * i_Right.z - i_Left.z * i_Right.y;
        float yMull = i_Left.x * i_Right.z - i_Left.z * i_Right.x;
        float zMull = i_Left.x * i_Right.y - i_Left.y * i_Right.x;

        return new Vector3(zMull, xMull, yMull); 
    }

    private float calculatDirection()
    {
        float distance = (float) Math.Sqrt(Math.Pow(m_Target.transform.position.x - transform.position.x, 2)
           + Math.Pow(m_Target.transform.position.x - transform.position.x, 2));

        float uDistance = Vector3.Distance(m_Target.transform.position, transform.position);

        Debug.Log("distance : " + distance);
        Debug.Log("uDistance : " + uDistance);

        return distance;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        float translation = Input.GetAxis("Vertical") * m_Speed;
        float rotation = Input.GetAxis("Horizontal") * m_RotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, translation, 0);

        transform.Rotate(0, 0, rotation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            calculatDirection();
            calculatAngles();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            m_AutoPailot = !m_AutoPailot;
        }
        if (calculatDirection() < 3 )
        {
            m_AutoPailot = false;
        }
        if (m_AutoPailot)
        {
            autoPailot();
        }
    }

}
