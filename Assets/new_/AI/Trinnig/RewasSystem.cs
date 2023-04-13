using Assets.Scripts.Player.Trinnig;
using Assets.Scripts.Util.Channels;
using DiningCombat;
using DiningCombat.FoodObj.Managers;
using DiningCombat.Managers;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.Offline.AI.ML
{
    internal class RewasSystem
    {
        #region Bonuses
        private enum eBonus { Tiny, Small, Medium, Large, Huge }
        #endregion
        #region Prevention of dead neurons
        private const int k_MaxHorizontal = 15;
        private const int k_MaxJumping = 15;
        private TimeBuffer m_JumpingBuffer = new TimeBuffer(20);
        private TimeBuffer m_HorizontalBuffer = new TimeBuffer(20);
        private TimeBuffer m_MonvingBuffer = new TimeBuffer(5);
        private TimeBuffer m_TimeHoldind = new TimeBuffer(5);
        private bool m_IsAnyRight;
        private bool m_IsAnyLeft;
        private bool m_Power;
        private bool m_LongCharging;
        private bool m_Is1stTimePickUpZooEnter;
        private bool m_Is1stTimePickUp;
        private bool m_Is1StPowering;
        private int m_NumOfEntringPickUpZoon = 0;
        private int m_NumOfEntringPickUpBonus = 5;
        private int m_Episode;
        private int m_NumOfJump;
        #endregion
        #region Variables and properties depend on a maximum step size
        private int m_MaxStap;
        private float previousDistanceToObject;
        private float distanceToPlayer;

        public float TinyBonus { get; private set; }
        public float TinyPenalty { get; private set; }
        public float SmallBonus { get; private set; }
        public float SmallPenalty { get; private set; }
        public float MediumBonus { get; private set; }
        public float MediumPenalty { get; private set; }
        public float LargeBonus { get; private set; }
        public float LargePenalty { get; private set; }
        public float HugeBonus { get; private set; }
        public float HugePenalty { get; private set; }

        #endregion
        public event Action<DCActionBuffers> ConfiigFromML;
        public event Action m;
        private GameObject m_MLAgant;
        MLAcitonStateMachine m_AcitonState;
        public RewasSystem(int maxStap, GameObject ml, MLAcitonStateMachine i_AcitonState)
        {
            UpdateValues(maxStap);
            m_MLAgant = ml;
            m_AcitonState = i_AcitonState;
        }

        public Vector3 TargetPosition { get; private set; }
        private void UpdateValues(int maxStap)
        {
            m_MaxStap= maxStap;
            TinyBonus = 1/maxStap;
            SmallBonus = 10/maxStap;
            MediumBonus = 100/maxStap;
            LargeBonus = 1000/maxStap;
            HugeBonus = 10000/maxStap;

            TinyPenalty = -1/maxStap;
            SmallPenalty = -10/maxStap;
            MediumPenalty = -100/maxStap;
            LargePenalty = -1000/maxStap;
            HugePenalty = -1000/maxStap;
        }


        public float CalculateReward(DCActionBuffers va)
        {
            float distanceToNearestObject = CalculateDistanceToNearestObject();
            //float distanceToObject = CalculateDistanceToObject();
            bool hasObject = CheckIfHasObject();
            bool isTargetingPlayer = CheckIfTargetingPlayer();
            bool isAvoidingObjects = CheckIfAvoidingObjects();

            float reward = 0f;

            // Reward for finding the nearest object
            if (distanceToNearestObject <= 1f)
            {
                reward += SmallBonus;
            }

            // Reward for navigating to the object
            if (distanceToNearestObject < previousDistanceToObject)
            {
                reward += SmallBonus;
            }
            else
            {
                previousDistanceToObject = distanceToNearestObject;
                return HugePenalty + reward;
            }

            // Reward for taking the object
            if (hasObject)
            {
                reward += HugeBonus;
            }

            // Reward for targeting another player
            if (isTargetingPlayer)
            {
                reward += SmallBonus;
            }

            // Reward for throwing/approaching the player
            if (distanceToPlayer <= 2f && hasObject)
            {
                reward += SmallBonus;
            }

            // Reward for avoiding thrown objects
            if (isAvoidingObjects)
            {
                reward += SmallBonus;
            }

            // Penalty for time spent
            reward += TinyPenalty;

            previousDistanceToObject = distanceToNearestObject;
            return reward;
        }

        private bool CheckIfAvoidingObjects()
        {
            return false;
        }

        private bool CheckIfTargetingPlayer()
        {
            return false;
        }

        private bool CheckIfHasObject()
        {
            return (m_AcitonState.StatesIndex != 0);
        }

        private float CalculateDistanceToObject()
        {
            return 0f;
        }

        private float CalculateDistanceToNearestObject()
        {
            Vector3 res;
            if (m_AcitonState.StatesIndex == 0)
            {
                ManagerGameFoodObj.Singlton.FindTheNearestOne(m_MLAgant.transform.position, out res);
            }
            else
            {
                PlayersManager.Singlton.FindTheNearestOne(m_MLAgant.transform.position, out res);

            }
            TargetPosition = res;
            return Vector3.Distance(m_MLAgant.transform.position , TargetPosition);
        }

        internal void OnEpisodeBegin()
        {
        }

        internal float OnStruckByShot(int obj)
        {
            return HugeBonus;
        }

        internal float OnPlayerDead()
        {
            return HugePenalty;
        }

        internal float OnScoreChange(int obj)
        {
            return HugeBonus;
        }

        internal float OnThrowingPoint()
        {
            return HugeBonus;
        }

        internal float OnEnterPickUpZoon(Collider obj)
        {
            return SmallBonus;
        }

        internal float OnCollectedFood(GameObject obj)
        {
            return HugeBonus;
        }

        internal float OnPower(float obj)
        {
            float res = 0;
            if (obj > 0)
            {
                if (obj < GameManager.Singlton.MaxForce)
                {
                    res = SmallBonus + obj/ 1000;
                }
                else
                {
                    res = HugePenalty;
                }
            }

            return res;
        }

        internal float OnStuckTheWall()
        {
            return HugePenalty;
        }

        //private float m_LongChargingBuns;

        //m_JumpingBuffer.Clear();
        //    m_HorizontalBuffer.Clear();
        //    m_MonvingBuffer.Clear();
        //    m_IsAnyRight = false;
        //    m_IsAnyLeft = false;
        //    m_Power = false;
        //    m_LongCharging = false;
        //    m_Is1StPowering = false;
        //    m_NumOfJump = 0;




        //[Tooltip("Force to apply when moving")]
        //public float moveForce = 2f;

        //[Tooltip("Speed to pitch up or down")]
        //public float pitchSpeed = 100f;

        //[Tooltip("Speed to rotate around the up axis")]
        //public float yawSpeed = 100f;

        //[Tooltip("Transform at the tip of the beak")]
        //public Transform beakTip;

        //[Tooltip("The agent's camera")]
        //public Camera agentCamera;

        #region punishment
        public float FailureToProgressToTheGoal => TinyPenalty;

    
        #endregion
    }
}



//class MyAgent : Agent
//{
//    private StateMachine stateMachine;

//    void Start()
//    {
//        // Initialize the state machine
//        stateMachine = new StateMachine();
//        stateMachine.SetState(State.HandIsFree);
//    }

//    public override void CollectObservations(VectorSensor sensor)
//    {
//        // Pass the state of the state machine to the observation vector
//        sensor.AddObservation((int)stateMachine.GetCurrentState());
//    }

//    public override void OnActionReceived(float[] vectorAction)
//    {
//        // Get the action from the ML-Agent and use it to control the state machine
//        int action = Mathf.FloorToInt(vectorAction[0]);

//        switch (stateMachine.GetCurrentState())
//        {
//            case State.HandIsFree:
//                if (action == 0)
//                {
//                    stateMachine.SetState(State.MovingTowardsObject);
//                }
//                break;
//            case State.HandHoldsObject:
//                if (action == 0)
//                {
//                    stateMachine.SetState(State.AimingAtPlayer);
//                }
//                else if (action == 1)
//                {
//                    stateMachine.SetState(State.HandAsserts);
//                }
//                break;
//            case State.HandAsserts:
//                if (action == 0)
//                {
//                    stateMachine.SetState(State.Throwing);
//                }
//                break;
//            case State.Throwing:
//                stateMachine.SetState(State.HandIsFree);
//                break;
//        }
//    }

//    // ... Other agent methods ...
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class MyAgent : MonoBehaviour
//{
//    // public variables for the ML input
//    public Vector3 targetPosition;
//    public GameObject targetPlayer;
//    public bool holdingObject;
//    public bool chargingShot;
//    public bool throwingObject;

//    // private variables for the agent
//    private NavMeshAgent agent;
//    private Animator animator;
//    private bool isMoving;
//    private bool isAiming;
//    private bool isThrowing;

//    // Start is called before the first frame update
//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        animator = GetComponent<Animator>();
//        isMoving = false;
//        isAiming = false;
//        isThrowing = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // check if the agent is currently holding an object
//        if (holdingObject)
//        {
//            // set the animator parameter to indicate that the agent is holding an object
//            animator.SetBool("isHoldingObject", true);

//            // check if the agent is currently aiming at a player
//            if (targetPlayer != null && isAiming)
//            {
//                // face the target player
//                Vector3 direction = targetPlayer.transform.position - transform.position;
//                direction.y = 0f;
//                transform.rotation = Quaternion.LookRotation(direction);

//                // check if the agent is far enough away to throw the object
//                if (Vector3.Distance(transform.position, targetPlayer.transform.position) > throwDistance)
//                {
//                    // move towards the target player
//                    agent.SetDestination(targetPlayer.transform.position);
//                    isMoving = true;
//                }
//                else
//                {
//                    // stop moving and start the throwing animation
//                    agent.SetDestination(transform.position);
//                    isMoving = false;
//                    animator.SetTrigger("throwObject");
//                    isAiming = false;
//                    isThrowing = true;
//                }
//            }
//            else
//            {
//                // check if the agent is close enough to a pick-up object
//                if (Vector3.Distance(transform.position, targetPosition) < pickUpDistance)
//                {
//                    // stop moving and pick up the object
//                    agent.SetDestination(transform.position);
//                    isMoving = false;
//                    animator.SetTrigger("pickUpObject");
//                    holdingObject = true;
//                }
//                else
//                {
//                    // move towards the pick-up object
//                    agent.SetDestination(targetPosition);
//                    isMoving = true;
//                }
//            }
//        }
//        else
//        {
//            // set the animator parameter to indicate that the agent is not holding an object
//            animator.SetBool("isHoldingObject", false);

//            // check if the agent is currently aiming at a player
//            if (targetPlayer != null && isAiming)
//            {
//                // face the target player
//                Vector3 direction = targetPlayer.transform.position - transform.position;
//                direction.y = 0f;
//                transform.rotation = Quaternion.LookRotation(direction);

//                // check if the agent is far enough away to throw the object
//                if (Vector3.Distance(transform.position, targetPlayer.transform.position) > throwDistance)
//                {
//                    // move towards the target player
//                    agent.SetDestination(targetPlayer.transform.position);
//                    isMoving = true;
//                }
//                else
//                {
//                    // stop moving and start the throwing animation
//                    agent.SetDestination(transform.position);
//                    isMoving = false;
//                    animator.SetTrigger("throwObject");
//                    isAiming = false;
//                    isThrowing = true;
//                }
//            }
//            else
//            {
//                // move towards the nearest pick-up object
//                GameObject nearestObject = Find


//public class MyAgent : Agent
//{
//    public Transform handTransform;
//    public float moveSpeed = 5f;
//    public float turnSpeed = 180f;
//    public float maxPickupDistance = 2f;
//    public float maxThrowDistance = 10f;
//    public GameObject throwableObjectPrefab;
//    public int lives = 3;
//    public int score = 0;

//    private Rigidbody rb;
//    private GameObject throwableObject;
//    private GameObject aimTarget;
//    private bool canShoot = false;
//    private float shootPower = 0f;
//    private float lastShotTime = -100f;

//    // Observations
//    public override void CollectObservations(VectorSensor sensor)
//    {
//        // Distance to the nearest pickupable object
//        GameObject nearestObject = GetNearestPickupableObject();
//        float distanceToObject = 0f;
//        if (nearestObject != null)
//        {
//            distanceToObject = Vector3.Distance(transform.position, nearestObject.transform.position);
//        }
//        sensor.AddObservation(distanceToObject);

//        // Distance and direction to the aim target
//        if (aimTarget != null)
//        {
//            Vector3 targetDirection = aimTarget.transform.position - transform.position;
//            sensor.AddObservation(targetDirection.normalized);
//            sensor.AddObservation(targetDirection.magnitude / maxThrowDistance);
//        }
//        else
//        {
//            sensor.AddObservation(Vector3.zero);
//            sensor.AddObservation(0f);
//        }

//        // Whether the agent can shoot
//        sensor.AddObservation(canShoot);

//        // Current shoot power
//        sensor.AddObservation(shootPower);
//    }

//    // Initialization
//    public override void Initialize()
//    {
//        rb = GetComponent<Rigidbody>();
//        throwableObject = Instantiate(throwableObjectPrefab, transform.position + Vector3.up, Quaternion.identity);
//        aimTarget = null;
//        canShoot = false;
//        shootPower = 0f;
//        lastShotTime = -100f;
//    }

//    // Actions
//    public override void OnActionReceived(float[] vectorAction)
//    {
//        // Move the agent
//        float forwardAmount = Mathf.Clamp(vectorAction[0], -1f, 1f);
//        float turnAmount = Mathf.Clamp(vectorAction[1], -1f, 1f);
//        transform.Translate(transform.forward * moveSpeed * forwardAmount * Time.deltaTime, Space.World);
//        transform.Rotate(Vector3.up * turnSpeed * turnAmount * Time.deltaTime);

//        // Pick up an object
//        if (vectorAction[2] > 0f)
//        {
//            GameObject nearestObject = GetNearestPickupableObject();
//            if (nearestObject != null && Vector3.Distance(transform.position, nearestObject.transform.position) <= maxPickupDistance)
//            {
//                PickUpObject(nearestObject);
//            }
//        }

//        // Aim at a player/agent
//        if (vectorAction[3] > 0f && throwableObject != null)
//        {
//            aimTarget = GetAimTarget();
//            if (aimTarget != null && Vector3.Distance(transform.position, aimTarget.transform.position) <= maxThrowDistance)
//            {
//                canShoot = true;
//            }
//        }

//        // Increase shoot power
//        if (vectorAction[4] > 0f && canShoot)
//        {
//            shootPower += 0.1f;
//            shootPower = Mathf.Clamp01(shootPower);
//        }

//        // Throw the object
//        if (vectorAction[5] > 0f && canShoot && Time.time - lastShotTime > 1f)
//        {
//            lastShotTime = Time.time;
//            ThrowObject(shootPower);
//            canShoot = false;
//            shootPower = 0


//using System.Collections.Generic;
//using UnityEngine;
//using Unity.MLAgents;
//using Unity.MLAgents.Sensors;

//public class AgentScript : Agent
//{
//    public float moveSpeed = 5.0f;
//    public float rotationSpeed = 180.0f;
//    public float throwForce = 500.0f;
//    public GameObject fruitPrefab;

//    private Transform targetFruit;
//    private GameObject carriedFruit;
//    private Transform targetAgent;

//    public override void OnEpisodeBegin()
//    {
//        // Reset agent and environment at the start of each episode
//        base.OnEpisodeBegin();

//        // Spawn a new fruit and set it as the target
//        targetFruit = SpawnFruit();
//    }

//    public override void CollectObservations(VectorSensor sensor)
//    {
//        // Observe agent's position and velocity
//        sensor.AddObservation(transform.position);
//        sensor.AddObservation(GetComponent<Rigidbody>().velocity);

//        // Observe distance to the target fruit and agent
//        sensor.AddObservation(Vector3.Distance(transform.position, targetFruit.position));
//        sensor.AddObservation(Vector3.Distance(transform.position, targetAgent.position));

//        // Observe whether the agent is carrying a fruit
//        sensor.AddObservation(carriedFruit != null);
//    }

//    public override void OnActionReceived(float[] vectorAction)
//    {
//        // Convert the vector action into movement and interaction decisions
//        float moveX = vectorAction[0];
//        float moveZ = vectorAction[1];
//        float rotateY = vectorAction[2];
//        bool pickup = vectorAction[3] > 0.5f;
//        bool throwAtTarget = vectorAction[4] > 0.5f;

//        // Move and rotate the agent based on the input
//        Vector3 move = new Vector3(moveX, 0f, moveZ);
//        transform.Translate(move * moveSpeed * Time.deltaTime, Space.Self);
//        transform.Rotate(Vector3.up, rotateY * rotationSpeed * Time.deltaTime);

//        // Pick up a fruit if the agent is close enough and not already carrying a fruit
//        if (pickup && !carriedFruit && Vector3.Distance(transform.position, targetFruit.position) < 1.0f)
//        {
//            carriedFruit = targetFruit.gameObject;
//            carriedFruit.transform.parent = transform;
//            carriedFruit.transform.localPosition = new Vector3(0f, 1f, 0f);
//            targetFruit = null;
//        }

//        // Aim at the target agent if specified
//        if (throwAtTarget && targetAgent != null && carriedFruit != null)
//        {
//            Vector3 targetDir = targetAgent.position - transform.position;
//            carriedFruit.transform.parent = null;
//            carriedFruit.GetComponent<Rigidbody>().AddForce(targetDir.normalized * throwForce);
//            carriedFruit = null;
//            targetAgent = null;
//            AddReward(1.0f);
//            EndEpisode();
//        }
//    }

//    private Transform SpawnFruit()
//    {
//        // Spawn a new fruit at a random location and return its transform
//        GameObject newFruit = Instantiate(fruitPrefab, new Vector3(Random.Range(-5.0f, 5.0f), 0.5f, Random.Range(-5.0f, 5.0f)), Quaternion.identity);
//        return newFruit.transform;
//    }

//    public void SetTargetAgent(Transform target)
//    {
//        // Set a new target agent for the agent to throw at
//        targetAgent = target;
//    }

//    public void SetCarriedFruit(GameObject fruit)
//    {
//        // Set the
