using DiningCombat.FoodObj.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine;
using Assets.Scripts.Util.Channels;

namespace Assets.Scripts.AI.Algo
{
    internal class SimpleAiAlgo //  : IAiAlgoAgent<Vector3, Vector3>
    {
        private const float k_MinLestMoveVerticalAxis = 0.5f;
        private const float k_UpdateRate = 10f;
        private const float k_ClosesrTargetUpdateRate = 2.5f;
        protected List<Vector3> m_Targets;
        private Vector3 m_ClosesrTarget;
        private Transform m_Transform;
        private TimeBuffer m_LestUpdate = new TimeBuffer(k_UpdateRate);
        private TimeBuffer m_BufferLestMoveVertical = new TimeBuffer(k_MinLestMoveVerticalAxis);
        private TimeBuffer m_ClosesrTargetLestUpdate = new TimeBuffer(k_ClosesrTargetUpdateRate);
        private float m_Angle =0;
        float minRotationAngle = 5f;
        int i =0;
        
        public SimpleAiAlgo(Transform transform)
        {
            m_Transform = transform;
            m_Angle = 5 * minRotationAngle;

        }

        private bool IsTargetforward => Math.Abs(m_Angle) < minRotationAngle;
        private Vector3 Position => m_Transform.position;
        public List<Vector3> GetData()
        {
            return m_Targets;
        }

        public void RunAlog()
        {
            if (m_LestUpdate.IsBufferOver())
            {
                m_Targets = ManagerGameFoodObj.Singlton.GetAllUnpicFood();
                if (m_Targets.Count > 0)
                {
                    m_LestUpdate.SetDataToInit();
                }
            }
            if (m_ClosesrTargetLestUpdate.IsBufferOver())
            {
                m_ClosesrTargetLestUpdate.SetDataToInit();
                m_ClosesrTarget = RunAlgo(Position, m_Targets);
            }
        }

        public static Vector3 RunAlgo(Vector3 pos, List<Vector3> i_Targets)
        {
            if (i_Targets.Count == 0)
            {
                return Vector3.zero;
            }

            IEnumerable<Vector3> query = i_Targets.OrderBy(other => DistanceAI(other, pos));

            foreach (Vector3 target in query)
            {
                return target;
            }

            return Vector3.zero;
        }

        protected static float DistanceAI(Vector3 v, Vector3 w)
        {
            return Vector3.Distance(v, w);
        }  


        public void SetData(List<Vector3> data)
        {
            m_Targets = data;
        }

        public float GetAxis(string v)
        {
            return 0f;
        }

        public bool Jump()
        {
            return false;
        }

        internal float GetRotateAxis()
        {
            if (m_Targets.Count == 0)
            {
                Debug.Log("GetRotateAxis   :" + m_Angle);

                m_Angle = 5 * minRotationAngle;
                return 0;
            }
            calculatAngles();
            return m_Angle;
        }

        private void calculatAngles()
        {
            if (IsTargetforward)
            {
                if (i > 15)
                {
                    i++; 
                    Debug.Log("IsTargetforward :" + m_Angle);
                    m_Angle = 0;
                    return;
                }
                else
                {
                    i = 0;
                }
            }
            // Get the current position of the AI
            Vector3 currentPosition = m_Transform.position;

            // Get the direction from the current position to the target position
            Vector3 directionToTarget = m_ClosesrTarget - currentPosition;

            // Calculate the rotation angle from the current forward direction to the target direction
            float rotationAngle = Vector3.SignedAngle(m_Transform.forward, directionToTarget, Vector3.up);

            // Clamp the rotation angle to a maximum value
            float maxRotationAngle = 40.0f;
            rotationAngle = Mathf.Clamp(rotationAngle, -maxRotationAngle, maxRotationAngle);
            float d = DistanceAI(currentPosition, m_ClosesrTarget);
            if (d > 1)
            {
                //Debug.Log("DistanceAI  :" + d);
                m_Angle = Math.Abs(rotationAngle) > minRotationAngle ? rotationAngle : 0;
            }
            else
            {
                //Debug.Log("m_Angle : " + m_Angle + " , rotationAngle " + rotationAngle);
                m_Angle = 0;
            }
            // Return the rotation angle as the rotate axis
            //return rotationAngle;
            //Vector3 aiForward = m_Transform.forward;
            //Vector3 targetDirection = m_ClosesrTarget - this.m_Transform.position;

            Debug.DrawRay(this.m_Transform.position, m_Transform.forward * 10, Color.green, 5);
            Debug.DrawRay(this.m_Transform.position, directionToTarget, Color.red, 5);

            //float dot = aiForward.x * targetDirection.x + aiForward.y * targetDirection.y +
            //    aiForward.z * targetDirection.z;

            //float angle = Mathf.Acos(dot / (aiForward.magnitude * targetDirection.magnitude));
            ////float angle = Vector3.Angle(aiForward, targetDirection);
            ////if (Math.Abs(angle) < 5.0f)
            ////{
            ////    m_Angle = 0f;
            ////}
            ////else
            ////{
            ////    m_Angle = angle;
            ////}
            //Debug.Log("angle : " + angle * Mathf.Rad2Deg);
            //Debug.Log("Unity angle : " + Vector3.Angle(aiForward, targetDirection));

            //int clockwise = (cross(aiForward, targetDirection)).z > 0 ? 1 : -1;
            //m_Angle = isLowerBound(angle) ? angle * clockwise * Mathf.Rad2Deg : 0;
        }

        private bool isLowerBound(float angle)
        {
            return angle * Mathf.Rad2Deg > 10;
        }

        private Vector3 cross(Vector3 i_Left, Vector3 i_Right)
        {
            float xMull = i_Left.y * i_Right.z - i_Left.z * i_Right.y;
            float yMull = i_Left.x * i_Right.z - i_Left.z * i_Right.x;
            float zMull = i_Left.x * i_Right.y - i_Left.y * i_Right.x;

            return new Vector3(zMull, xMull, yMull);
        }

        internal float GetVerticalAxis()
        {
            float res = 0;
            if (IsTargetforward || m_BufferLestMoveVertical.IsBufferOver())
            {
                m_BufferLestMoveVertical.SetDataToInit();
                res = 1;
            }

            return res;
        }

        internal float GetHorizontalAxis()
        {
            int max = 12; int min = -max;
            int rend = Random.Range(min, max);
            if (rend == 0)
            {
                return -1f;
            }else if (rend == 7)
            {
                return 1f;
            }
            return 0f;
        }

        /*=========================================================
         *  public float m_Speed = 10.0f;
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
         =======================================================================*/
    }
}
