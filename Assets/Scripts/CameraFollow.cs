using System.Collections;
using UnityEngine;

namespace DiningCombat
{
    public class CameraFollow : MonoBehaviour
    {
        private const int k_MinForceForShack = 1600;
        private const int k_MaxForceForShack = 1910;
        private PickUpItem m_PickUpItem;

        [SerializeField]
        private PlayerMovement m_Player;

        [SerializeField]
        private Transform m_Target;

        [SerializeField]
        private Vector3 m_Offset;

        [SerializeField]
        private Camera m_Camera;


        protected void Start()
        {
            m_PickUpItem = FindObjectOfType<PickUpItem>();
            m_Player = GetComponent<PlayerMovement>();
            m_Camera = gameObject.GetComponent<Camera>();
        }

        protected void Update()
        {
            Vector3 mousePos = Input.mousePosition;


            //Debug.Log(mousePos.ToString());
            //m_Camera.transform.position = mousePos + m_Player.transform.position;
        }

        protected void LateUpdate()
        {
            if (isShake())
            {
                StartCoroutine(Shake(0.14f, 0.5f));
            }
            else
            {
                transform.position = m_Target.position + m_Offset;
            }
        }

        private bool isShake()
        {
            bool isForceInLimit = m_PickUpItem.ForceMulti > k_MinForceForShack 
                && m_PickUpItem.ForceMulti < k_MaxForceForShack ;

            return Input.GetKey(GameGlobal.k_PowerKey) && isForceInLimit;
        }


        public IEnumerator Shake(float duration, float magnitude)
        {
            float dlataTime = 0.0f;

            while (dlataTime < duration)
            {
                transform.position = getRendV3ToShake(magnitude);
                dlataTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = transform.position + m_Offset;
        }

        private Vector3 getRendV3ToShake(float i_Magnitude)
        {
            return m_Target.position + m_Offset +
                new Vector3(getRandRange(i_Magnitude) , getRandRange(i_Magnitude) , 0);
        }

        private float getRandRange(float i_Mull)
        {
            return Random.Range(-0.2f, 0.2f) * i_Mull;
        }
    }

}
