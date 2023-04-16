//using System;
//using UnityEngine;
//using DiningCombat.FoodObj;
//using System.Collections;

//namespace DiningCombat.Player
//{
//    public class BridgeAbstractionAction : MonoBehaviour, ICollecter
//    {
//        [SerializeField]
//        [Range(500f, 3000f)]
//        public int m_MaxCargingPower = 1800;
//        private float m_ChargingPower;
//        private FoodObject m_FoodItem;
//        private IntiraelPlayerManger m_Manger;
//        private Rigidbody m_Rigidbody;

//        public Transform PikUpPonit {get; private set;}
        
//        public float ForceMulti
//        {
//            get => this.m_ChargingPower;
//        }

//        public void SetForceMulti(float value)
//        {
//            this.m_ChargingPower = value;
//        }

//        private void Awake()
//        {
//            m_Manger = gameObject.GetComponentInChildren<IntiraelPlayerManger>();
//            m_Rigidbody = gameObject.GetComponent<Rigidbody>();
//        }

//        internal void SetGameFoodObj(GameObject i_GameObject)
//        {
//            bool isSucceed = false;

//            if (i_GameObject is null)
//            {
//                this.m_FoodItem = null;
//            }
//            else
//            {
//                FoodObject obj = i_GameObject.GetComponent<FoodObject>();
//                if (obj is not null)
//                {
//                    isSucceed = true;
//                    this.m_FoodItem = obj;
//                    obj.Collect(this);
//                }
//            }
//        }

//        internal void ThrowObj()
//        {
//            if (this.m_FoodItem is null)
//            {
//                Debug.LogError("foodItem is null");
//            }
//            else
//            {
//                FoodObject gameFoodObj = m_FoodItem.GetComponent<FoodObject>();
//                Debug.Assert(gameFoodObj is not null, "gameFoodObj is null");
//                Debug.Assert(PikUpPonit is not null, "m_PikUpPonit is null");
//                Debug.DrawRay(PikUpPonit.position, PikUpPonit.forward,
//                    Color.green, 10f);
//                StartCoroutine(ReturnToOriginVelocity(m_Rigidbody.velocity));
//                m_FoodItem.Throw(PikUpPonit.forward, m_Manger.ForceMull);
//                m_FoodItem = null;
//            }
//        }

//        /// <summary>
//        /// When there is a shot the player's body moves, because of the added force,
//        /// the purpose of this method is to prevent this
//        /// </summary>
//        /// <param name="i_CurntVelocity"></param>
//        /// <returns></returns>
//        private IEnumerator ReturnToOriginVelocity(Vector3 i_CurntVelocity)
//        {
//            for(int i = 0; i<5; i++)
//            {
//                m_Rigidbody.velocity = i_CurntVelocity;
//                yield return null;
//            }
//        }

//        public void SetPickUpPoint(Transform i_PickUpPoint)
//        {
//            if (i_PickUpPoint is null)
//            {
//                Debug.LogError("i_PickUpPoint is null");
//                return;
//            }
//            if (PikUpPonit is not null)
//            {
//                Debug.LogError("Try to set PickUpPoint more then once");
//                return;
//            }

//            PikUpPonit = i_PickUpPoint;
//        }

//        bool ICollecter.DidIHurtMyself(Collision i_Collision)
//        {
//            return this.gameObject.Equals(i_Collision.gameObject);
//        }
//    }
//}