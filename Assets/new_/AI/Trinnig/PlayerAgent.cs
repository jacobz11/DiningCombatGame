//using Assets.Scripts.Player.Offline.AI.ML;
//using DiningCombat;
//using System;
//using Unity.MLAgents;
//using Unity.MLAgents.Actuators;
//using Unity.MLAgents.Sensors;
//using UnityEngine;

//namespace Assets.Scripts.Player.Trinnig
//{
//    internal class PlayerAgent : Agent
//    {
//        private RewasSystem m_RewasSystem;

//        private Vector3 m_InitPos = new Vector3(0f,0f,0f);

//        [Tooltip("Whether this is training mode or gameplay mode")]
//        public bool trainingMode;
//        private int m_NumOfTimeGetOutOfRinng;
//        private bool frozen = false;
//        private ML3DMovement m_Movement;
//        private MLAcitonStateMachine m_AcitonState;
//        private IntiraelPlayerManger r_Manger;

//        public override void Initialize()
//        {
//            m_RewasSystem = new RewasSystem(MaxStep, gameObject, m_AcitonState);
//            base.Initialize();
//        }

//        public override void OnEpisodeBegin()
//        {
//            m_RewasSystem.OnEpisodeBegin();
//            gameObject.transform.position = m_InitPos;
//        }

//        public void SetBridges(ML3DMovement movement, MLAcitonStateMachine acitonState, Vector3 i_InitPos)
//        {
//            this.m_Movement = movement;
//            this.m_AcitonState = acitonState;
//            this.m_AcitonState.Powering.OnPower += OnPower;
//            this.m_AcitonState.Free.PlayerCollectedFood += OnCollectedFood;
//            m_InitPos = i_InitPos;
//            m_RewasSystem = new RewasSystem(MaxStep, gameObject, m_AcitonState);

//            IntiraelPlayerManger manger = gameObject.GetComponentInChildren<IntiraelPlayerManger>();
//            r_Manger = manger;
//            if (manger is not null)
//            {
//                manger.PlayerDead += OnPlayerDead;
//                manger.LifePointChange += OnStruckByShot;
//                manger.PlayerScoreChange += OnScoreChange;
//                manger.CollisionEnter += CollisionEnter;
//                manger.PickUpZonEnter += OnEnterPickUpZoon;
//            }
//            else
//            {
//                Debug.LogError("Is null");
//            }

//            gameObject.transform.position = m_InitPos;
//            PlayerAnimationChannel animationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();
//            if (animationChannel is not null )
//            {
//                animationChannel.ThrowPoint += OnThrowingPoint;
//            }
//            else
//            {
//                Debug.Log("Is null");
//            }
//        }

//        private void OnThrowingPoint()
//        {
//            AddReward(m_RewasSystem.OnThrowingPoint());
//        }

//        private void OnPlayerDead()
//        {
//            AddReward(m_RewasSystem.OnPlayerDead());
//        }

//        private void OnStruckByShot(int obj)
//        {
//            AddReward(m_RewasSystem.OnStruckByShot(obj));
//        }

//        private void OnScoreChange(int obj)
//        {
//            AddReward(m_RewasSystem.OnScoreChange(obj));
//        }

//        private void OnEnterPickUpZoon(Collider obj)
//        {
//            AddReward(m_RewasSystem.OnEnterPickUpZoon(obj));
//        }

//        private void OnCollectedFood(GameObject obj)
//        {
//            AddReward(m_RewasSystem.OnCollectedFood(obj));
//        }

//        private void OnPower(float obj)
//        {
//            AddReward(m_RewasSystem.OnPower(obj));
//        }

//        private void CollisionEnter(Collision obj)
//        {
//            if (obj != null && obj.collider.CompareTag(GameGlobal.TagNames.k_Wall)) 
//            {
//                OnStuckTheWall();
//            }
//        }

//        private void OnStuckTheWall()
//        {
//            AddReward(m_RewasSystem.OnStuckTheWall());
//        }

//        public override void OnActionReceived(ActionBuffers vectorAction)
//        {
//            DCActionBuffers va = new DCActionBuffers(vectorAction);
//            m_Movement.SetSensorData(va); 
//            m_AcitonState.SetMlAgnetData(va);

//            if (transform.position.y < -5)
//            {
//                AddReward(m_RewasSystem.HugePenalty * m_NumOfTimeGetOutOfRinng);
//                if (transform.position.y < -10)
//                {
//                    m_NumOfTimeGetOutOfRinng++;
//                    transform.position = m_InitPos;
//                }
//            }
            
//            AddReward(m_RewasSystem.CalculateReward(va));
//        }

      
//        #region Fine Action
//        //private void OnStruckByShot(int i_LifePonitLose)
//        //{
//        //    AddReward(FineConfig(eBonus.Huge) * i_LifePonitLose);
//        //}
//        //private  float FineConfig(eBonus bonus)
//        //{
//        //    return -1 * BonusConfig(bonus);
//        //}
//        //private void OnStuckTheWall()
//        //{
//        //    AddReward(FineConfig(eBonus.Large));
//        //}
//        //private void OnPlayerDead()
//        //{
//        //    AddReward(FineConfig(eBonus.Huge) *100);
//        //}
//        #endregion

//        public override void CollectObservations(VectorSensor sensor)
//        {
//            sensor.AddObservation(m_AcitonState.StatesIndex);
//            sensor.AddObservation(m_AcitonState.Player.ForceMulti);
//            sensor.AddObservation(m_NumOfTimeGetOutOfRinng);
//            sensor.AddObservation(r_Manger.LifePoint);
//            sensor.AddObservation(r_Manger.Score);
//            sensor.AddObservation(transform.position);
//            sensor.AddObservation(m_RewasSystem.TargetPosition);
//            base.CollectObservations(sensor);
//        }
//    }
//}

////public override void OnEpisodeBegin()
////{
////    m_JumpingBuffer.Clear();
////    m_HorizontalBuffer.Clear();
////    m_IsAnyRight = false;
////    m_IsAnyLeft = false;
////    m_Power = false;
////    m_LongCharging = false;
////    gameObject.transform.position = m_InitPos;
////    m_Is1stTimePickUpZooEnter = false;
////    m_Is1stTimePickUp = false;
////    numOfOutOfRinng = 0;
////    m_NumOfEnterThrowingPoint = 0;


////    if (m_NumOfEntringPickUpBonus > 0)
////    {
////        m_Is1StPowering = false;
////        m_NumOfEntringPickUpZoon = m_NumOfEntringPickUpBonus;
////        m_NumOfEntringPickUpBonus--;
////        m_Is1StBoost = false;
////        m_LongCharging = false;
////    }
////    else if (m_Episode == 0)
////    {
////        m_NumOfEntringPickUpBonus++;
////        m_Episode = 7;
////    }
////    else
////    {
////        m_Episode++;
////    }
////}


//////Horizontal Vertical  IsBoost IsPress
////if (m_Is1StPowering && va.IsPress)
////{
////    m_Is1StPowering = true;
////    AddReward(BonusConfig(eBonus.Small));
////}

////if (m_Is1StBoost && va.IsBoost)
////{
////    m_Is1StBoost = true;
////    AddReward(BonusConfig(eBonus.Small));
////}

////if (va.IsJumping)
////{
////    if (m_NumOfJump > 10)
////    {
////        AddReward(FineConfig(eBonus.Tiny));
////    }
////    else if (m_JumpingBuffer.IsBufferOver())
////    {
////        if(m_NumOfJump < k_MaxJumping)
////        {
////            m_JumpingBuffer.Clear();
////            AddReward(BonusConfig(eBonus.Tiny));
////        }
////        else
////        {
////            AddReward(FineConfig(eBonus.Tiny));
////        }
////    }
////    m_NumOfJump++;
////}
////bool isAnyHorizontal = BridgeImplementor3DMovement.IsMovment(va.Horizontal, out bool o_IsRight);
////bool isAnyVertical = BridgeImplementor3DMovement.IsMovment(va.Vertical, out bool _);

////if (isAnyHorizontal && m_HorizontalBuffer.IsBufferOver() && GetRand(k_MaxHorizontal))
////{
////    if (o_IsRight && !m_IsAnyRight)
////    {
////        m_IsAnyRight = true;
////        AddReward(BonusConfig(eBonus.Small));
////    }
////    else if (!o_IsRight && !m_IsAnyLeft)
////    {
////        m_IsAnyLeft = true;
////        AddReward(BonusConfig(eBonus.Small));
////    }
////    else
////    {
////        AddReward(BonusConfig(eBonus.Tiny));
////    }
////    m_HorizontalBuffer.Clear();
////}

////if (isAnyHorizontal || isAnyVertical)
////{
////    m_MonvingBuffer.Clear();
////}
////else if (m_MonvingBuffer.IsBufferOver())
////{
////    AddReward(FineConfig(eBonus.Small));
////}

////static bool GetRand(int i_Max)
////{
////    return UnityEngine.Random.Range(0, i_Max) == (i_Max % 3);
////}
///// <summary>
///// In order to make the neural network not neglect certain things,
///// a random bonus is given based on some things
///// </summary>
////private void configBonus(DCActionBuffers va)
////{
////    if (m_NumOfEntringPickUpZoon > 0)
////    {
////        m_NumOfEntringPickUpZoon --;
////        AddReward(BonusConfig(eBonus.Tiny));
////    }

////    if (m_AcitonState.StatesIndex == 1)
////    {
////        if (!va.IsPress)
////        {
////            AddReward(FineConfig(eBonus.Large));
////        }
////        else
////        {
////            AddReward(BonusConfig(eBonus.Large));
////        }
////    }

////    if (m_AcitonState.StatesIndex == 2 && va.IsPress && m_AcitonState.Player.ForceMulti > 1800)
////    {
////        AddReward(FineConfig(eBonus.Large));
////    }

////}
////#region Bonus Action
////private float BonusConfig(eBonus bonus)
////{
////    switch (bonus)
////    {
////        case eBonus.Tiny: return 1 / MaxStep;
////        case eBonus.Small: return 10 / MaxStep;
////        case eBonus.Medium: return 100 / MaxStep;
////        case eBonus.Large: return 500 / MaxStep;
////        case eBonus.Huge: return 5000 / MaxStep;
////        default:
////            return 0f;
////    }
////}
////public void OnCollectedFood(GameObject food)
////{
////    if (!m_Is1stTimePickUp)
////    {
////        AddReward(BonusConfig(eBonus.Huge));
////        m_Is1stTimePickUp = true;
////    }
////    m_TimeHoldind.Clear();
////    AddReward(BonusConfig(eBonus.Medium));
////    //StartCoroutine(FineAntyTrow());
////}

////private IEnumerator FineAntyTrow()
////{
////    yield return new WaitForSeconds(10);
////    while (!m_IsTrorw)
////    {
////        AddReward(FineConfig(eBonus.Small));
////        yield return new WaitForSeconds(1);
////    }
////}

////public void OnThrowingPoint() 
////{
////    m_NumOfEnterThrowingPoint++;
////    AddReward(BonusConfig(eBonus.Large)* m_NumOfEnterThrowingPoint);
////    m_IsTrorw = true;
////}

////public void OnEnterPickUpZoon(Collider collider)
////{
////    if (!m_Is1stTimePickUpZooEnter)
////    {
////        m_Is1stTimePickUpZooEnter = true;
////        AddReward(BonusConfig(eBonus.Medium));
////    }
////}
////public void OnPower(float power)
////{
////    if (power == float.NegativeInfinity)
////    {
////        m_Power = false;
////        return;
////    }
////    if (!m_Power)
////    {
////        AddReward(BonusConfig(eBonus.Tiny));
////        m_Power = true;
////        return;
////    }

////    if (power > m_LongChargingBuns) 
////    {
////        m_LongChargingBuns+=17;
////        AddReward(BonusConfig(eBonus.Small));
////        if (!m_LongCharging)
////        {
////            AddReward(BonusConfig(eBonus.Huge));
////            m_LongCharging = true;
////        }
////        return;
////    }
////}

////private void OnScoreChange(int newSoce)
////{
////    AddReward(BonusConfig(eBonus.Huge) * newSoce);
////}

//// #endregion